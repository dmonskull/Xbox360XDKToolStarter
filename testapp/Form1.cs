using Microsoft.Test.Xbox.XDRPC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XDevkit;
using XDRPC;
using System.Windows.Shell;
using Newtonsoft.Json;


namespace testapp
{
    public partial class Form1 : Form
    {
        public static IXboxManager xbManager = null;
        public static IXboxConsole xbCon = null;
        public static bool activeConnection = false;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        private string currentPath = "Hdd:\\";
        private readonly System.Windows.Application _wpfApp;
        private readonly string[] _startupArgs;
        private System.Windows.Shell.JumpList _jumpList;
        private static readonly string FavoritesFile = Path.Combine(Application.StartupPath, "favorites.json");
        private List<FavoriteGame> _favorites = new List<FavoriteGame>();
        private System.Windows.Forms.Timer _jumpListWatcher;
        private ToolStripMenuItem _addFavMenuItem;
        private ToolStripMenuItem _removeFavMenuItem;
        private ToolStripMenuItem _renameFavMenuItem;

        public Form1()
        {

            InitializeComponent();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Columns.Clear();
            listView1.Columns.Add("Name", 400);
        }

        public Form1(System.Windows.Application wpfApp, string[] startupArgs) : this()
        {
            _wpfApp = wpfApp ?? (System.Windows.Application.Current ?? new System.Windows.Application());
            _startupArgs = startupArgs ?? Array.Empty<string>();

            InitJumpList();
            LoadFavorites();
            InitFavoritesContext();
            AddManageFavoritesTask();
            StartJumpListWatcher();

            this.Shown += (s, e) =>
            {
                if (_startupArgs.Length > 0)
                {
                    LaunchGameFromPath(_startupArgs[0]);
                }
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (ConnectToConsole())
            {
                connction2.Text = "ACTIVE";
                connction2.ForeColor = Color.Green;
                PullXboxInfo();
            }
            else
            {
                connction2.Text = "FAILED";
                connction2.ForeColor = Color.Red;
            }
        }

        #region JumpList&ContextMenu
        public class FavoriteGame
        {
            public string Name { get; set; }
            public string Path { get; set; }
        }
        private void InitJumpList()
        {
            _jumpList = System.Windows.Shell.JumpList.GetJumpList(_wpfApp);
            if (_jumpList == null)
            {
                _jumpList = new System.Windows.Shell.JumpList();
                System.Windows.Shell.JumpList.SetJumpList(_wpfApp, _jumpList);
            }

            _jumpList.ShowFrequentCategory = false;
            _jumpList.ShowRecentCategory = false;

            _jumpList.JumpItemsRemovedByUser += JumpList_JumpItemsRemovedByUser;

            _jumpList.Apply();
        }

        private void StartJumpListWatcher()
        {
            _jumpListWatcher = new System.Windows.Forms.Timer { Interval = 1500 };
            _jumpListWatcher.Tick += (s, e) =>
            {
                try { _jumpList?.Apply(); } catch { }
            };
            _jumpListWatcher.Start();
        }

        private void JumpList_JumpItemsRemovedByUser(object sender, System.Windows.Shell.JumpItemsRemovedEventArgs e)
        {
            bool changed = false;

            foreach (var removed in e.RemovedItems.OfType<System.Windows.Shell.JumpTask>())
            {
                var removedPath = removed.Arguments?.Trim('"');
                if (string.IsNullOrWhiteSpace(removedPath)) continue;

                var match = _favorites.FirstOrDefault(f =>
                    f.Path.Equals(removedPath, StringComparison.OrdinalIgnoreCase));

                if (match != null)
                {
                    _favorites.Remove(match);
                    changed = true;
                }
            }

            if (changed)
            {
                SaveFavorites();
            }
        }

        private void InitFavoritesContext()
        {
            var cms = new ContextMenuStrip();

            _addFavMenuItem = new ToolStripMenuItem("Add to Favorites");
            _addFavMenuItem.Click += (s, e) => AddSelectedToFavorites();
            cms.Items.Add(_addFavMenuItem);

            _removeFavMenuItem = new ToolStripMenuItem("Remove from Favorites");
            _removeFavMenuItem.Click += (s, e) => RemoveSelectedFromFavorites();
            cms.Items.Add(_removeFavMenuItem);

            _renameFavMenuItem = new ToolStripMenuItem("Rename Favorite");
            _renameFavMenuItem.Click += (s, e) =>
            {
                if (listView1.SelectedItems.Count == 0) return;
                string fullPath = listView1.SelectedItems[0].Tag?.ToString();
                if (!string.IsNullOrEmpty(fullPath))
                    RenameFavorite(fullPath);
            };
            cms.Items.Add(_renameFavMenuItem);

            listView1.ContextMenuStrip = cms;
            cms.Opening += ContextMenu_Opening;
        }

        private void AddSelectedToFavorites()
        {
            if (listView1.SelectedItems.Count == 0) return;

            var selected = listView1.SelectedItems[0];
            string fullPath = selected.Tag?.ToString();
            if (string.IsNullOrWhiteSpace(fullPath)) return;

            string displayName = PromptForName(selected.Text);

            var fav = new FavoriteGame { Name = displayName, Path = fullPath };
            _favorites.Add(fav);
            SaveFavorites();

            AddToJumpList(displayName, fullPath);
        }

        private void RemoveSelectedFromFavorites()
        {
            if (listView1.SelectedItems.Count == 0) return;

            string fullPath = listView1.SelectedItems[0].Tag?.ToString();
            if (string.IsNullOrWhiteSpace(fullPath)) return;

            // ✅ Remove from favorites list
            var fav = _favorites.FirstOrDefault(f =>
                f.Path.Equals(fullPath, StringComparison.OrdinalIgnoreCase));
            if (fav != null)
            {
                _favorites.Remove(fav);
                SaveFavorites();
            }

            // ✅ Rebuild JumpList so it matches the updated list
            RebuildJumpList(_favorites);
        }


        public void LaunchGameFromPath(string fullPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fullPath)) return;
                if (xbCon == null || !activeConnection)
                {
                    if (!ConnectToConsole2())
                    {
                        MessageBox.Show("Failed to connect to console.");
                        return;
                    }
                }

                string directoryPath = fullPath.Substring(0, fullPath.LastIndexOf("\\"));
                xbCon.Reboot(fullPath, directoryPath, null, XboxRebootFlags.Title);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to launch: " + ex.Message);
            }
        }

        private void AddToJumpList(string name, string fullPath)
        {
            var task = new System.Windows.Shell.JumpTask
            {
                Title = name,
                Arguments = "\"" + fullPath + "\"",
                Description = "Launch " + name,
                CustomCategory = "Favorites",
                ApplicationPath = Application.ExecutablePath,
                IconResourcePath = Application.ExecutablePath,
                IconResourceIndex = 0
            };

            _jumpList.JumpItems.Add(task);
            _jumpList.Apply();
        }

        public static void RebuildJumpList(List<FavoriteGame> favorites)
        {
            var wpfApp = System.Windows.Application.Current ?? new System.Windows.Application();
            var jumpList = System.Windows.Shell.JumpList.GetJumpList(wpfApp);
            if (jumpList == null)
            {
                jumpList = new System.Windows.Shell.JumpList();
                System.Windows.Shell.JumpList.SetJumpList(wpfApp, jumpList);
            }

            jumpList.JumpItems.Clear();

            foreach (var fav in favorites)
            {
                var task = new System.Windows.Shell.JumpTask
                {
                    Title = fav.Name,
                    Arguments = "\"" + fav.Path + "\"",
                    Description = "Launch " + fav.Name,
                    CustomCategory = "Favorites",
                    ApplicationPath = Application.ExecutablePath,
                    IconResourcePath = Application.ExecutablePath,
                    IconResourceIndex = 0
                };

                jumpList.JumpItems.Add(task);
            }

            jumpList.Apply();
        }

        private string PromptForName(string defaultName)
        {
            using (var prompt = new Form())
            {
                prompt.Width = 300;
                prompt.Height = 150;
                prompt.Text = "Rename Favorite";
                prompt.StartPosition = FormStartPosition.CenterScreen;

                var textBox = new TextBox()
                {
                    Left = 20,
                    Top = 20,
                    Width = 240,
                    Text = defaultName
                };

                var okButton = new Button()
                {
                    Text = "OK",
                    Left = 100,
                    Width = 80,
                    Top = 60,
                    DialogResult = DialogResult.OK
                };

                prompt.Controls.Add(textBox);
                prompt.Controls.Add(okButton);
                prompt.AcceptButton = okButton;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : defaultName;
            }
        }

        private void SaveFavorites()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_favorites, Formatting.Indented);
                File.WriteAllText(FavoritesFile, json);
            }
            catch { }
        }

        private void LoadFavorites()
        {
            try
            {
                if (File.Exists(FavoritesFile))
                {
                    string json = File.ReadAllText(FavoritesFile);
                    _favorites = JsonConvert.DeserializeObject<List<FavoriteGame>>(json) ?? new List<FavoriteGame>();
                }
                else
                {
                    _favorites = new List<FavoriteGame>();
                }
            }
            catch
            {
                _favorites = new List<FavoriteGame>();
            }

            RebuildJumpList(_favorites);

            var currentItems = _jumpList.JumpItems.OfType<System.Windows.Shell.JumpTask>()
                .Select(j => j.Arguments.Trim('"'))
                .ToList();

            var removed = _favorites.Where(f => !currentItems.Contains(f.Path, StringComparer.OrdinalIgnoreCase)).ToList();
            if (removed.Any())
            {
                foreach (var r in removed)
                    _favorites.Remove(r);

                SaveFavorites();
            }
        }

        private void AddManageFavoritesTask()
        {
            var task = new System.Windows.Shell.JumpTask
            {
                Title = "Manage Favorites",
                Arguments = "--manage",
                Description = "Rename or remove favorites",
                CustomCategory = "Favorites",
                ApplicationPath = Application.ExecutablePath
            };

            _jumpList.JumpItems.Add(task);
            _jumpList.Apply();
        }

        private void RenameFavorite(string fullPath)
        {
            var fav = _favorites.FirstOrDefault(f => f.Path.Equals(fullPath, StringComparison.OrdinalIgnoreCase));
            if (fav == null) return;

            string newName = PromptForName(fav.Name);
            fav.Name = newName;
            SaveFavorites();

            _jumpList.JumpItems.Clear();
            foreach (var f in _favorites)
                AddToJumpList(f.Name, f.Path);
            _jumpList.Apply();
        }

        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                _addFavMenuItem.Enabled = false;
                _removeFavMenuItem.Enabled = false;
                _renameFavMenuItem.Enabled = false;
                return;
            }

            var fullPath = listView1.SelectedItems[0].Tag?.ToString() ?? "";

            // must be a .xex file
            if (!fullPath.EndsWith(".xex", StringComparison.OrdinalIgnoreCase))
            {
                _addFavMenuItem.Enabled = false;
                _removeFavMenuItem.Enabled = false;
                _renameFavMenuItem.Enabled = false;
                return;
            }

            // check if this .xex is already favorited
            bool isFavorited = _favorites.Any(f =>
                f.Path.Equals(fullPath, StringComparison.OrdinalIgnoreCase));

            _addFavMenuItem.Enabled = !isFavorited;
            _removeFavMenuItem.Enabled = isFavorited;
            _renameFavMenuItem.Enabled = isFavorited;
        }

        #endregion

        #region FileExplorer

        private void LoadAllDrives()
        {
            listView1.Items.Clear();

            try
            {
                var drives = xbCon.Drives.Split(',');
                foreach (var drive in drives)
                {
                    ListViewItem item = new ListViewItem(drive + ":\\");
                    item.Tag = drive + ":\\";
                    listView1.Items.Add(item);
                }

                currentPath = "Drives";
                labelCurrentPath.Text = currentPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load drives: " + ex.Message);
            }
        }

        private void LoadDirectoryFiles(string path)
        {
            listView1.Items.Clear();
            currentPath = path;

            try
            {
                IXboxFiles files = xbCon.DirectoryFiles(path);

                // turn into a list and sort: directories first, then files
                var sortedFiles = files.Cast<IXboxFile>()
                                       .OrderBy(f => !f.IsDirectory)   // folders (false) before files (true)
                                       .ThenBy(f => f.Name)            // then alphabetical
                                       .ToList();

                foreach (IXboxFile file in sortedFiles)
                {
                    string fullPath = file.Name;
                    string displayName = Path.GetFileName(fullPath.TrimEnd('\\'));
                    if (string.IsNullOrEmpty(displayName))
                        displayName = fullPath;

                    ListViewItem item = new ListViewItem(displayName);
                    item.Tag = fullPath; // store full path
                    listView1.Items.Add(item);
                }

                // ✅ update label with current path
                labelCurrentPath.Text = currentPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load directory: " + ex.Message);
            }
        }

        // double click logic for file explorer
        private void listView1_DoubleClick_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1) return;

            ListViewItem selectedItem = (ListViewItem)listView1.SelectedItems[0];
            string fullPath = selectedItem.Tag.ToString(); // ✅ always the correct path

            // If folder → load it
            try
            {
                IXboxFiles files = xbCon.DirectoryFiles(fullPath);
                LoadDirectoryFiles(fullPath);
                return;
            }
            catch
            {
                // Not a folder → maybe file
            }

            // If .xex → launch it
            if (fullPath.EndsWith(".xex", StringComparison.OrdinalIgnoreCase))
            {
                string directoryPath = fullPath.Substring(0, fullPath.LastIndexOf("\\"));

                var result = MessageBox.Show($"Launch {Path.GetFileName(fullPath)}?", "Confirm Launch", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        xbCon.Reboot(fullPath, directoryPath, null, XboxRebootFlags.Title);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Launch failed: " + ex.Message);
                    }
                }
            }
        }

        // back button for file explorer
        private void button4_Click(object sender, EventArgs e)
        {
            // If we're at drives root → reload drives
            if (string.IsNullOrEmpty(currentPath) || currentPath == "Drives")
            {
                LoadAllDrives();
                return;
            }

            // If we're at the root of a drive → go back to drives list
            if (currentPath.EndsWith(":\\"))
            {
                LoadAllDrives();
                return;
            }

            // Otherwise → go up one level
            try
            {
                string parentPath = Path.GetDirectoryName(currentPath.TrimEnd('\\')) + "\\";

                if (string.IsNullOrEmpty(parentPath) || parentPath.Length <= 2)
                {
                    LoadAllDrives();
                }
                else
                {
                    LoadDirectoryFiles(parentPath);
                }
            }
            catch
            {
                LoadAllDrives();
            }
        }

        #endregion

        #region XboxStuff
        public static bool ConnectToConsole()
        {
            if (activeConnection && xbCon.DebugTarget.IsDebuggerConnected(out var debuggerName, out var userName))
            {
                MessageBox.Show("Connection to " + xbCon.Name + " already established!");
                return true;
            }
            try
            {
                xbManager = (XboxManager)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("A5EB45D8-F3B6-49B9-984A-0D313AB60342")));
                xbCon = xbManager.OpenConsole(xbManager.DefaultConsole);
                var ConnectionCode = xbCon.OpenConnection(null);
                var xboxConnection = xbCon.OpenConnection(null);

                if (!xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
                {
                    xbCon.DebugTarget.ConnectAsDebugger("DMON", XboxDebugConnectFlags.Force);
                }

                activeConnection = xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName);
                return activeConnection;
            }
            catch (Exception)
            {
                MessageBox.Show("Could not connect to console: " + xbManager.DefaultConsole);
                return false;
            }
        }

        public static bool ConnectToConsole2()
        {
            try
            {
                if (activeConnection && xbCon != null &&
                    xbCon.DebugTarget.IsDebuggerConnected(out _, out _))
                {
                    return true;
                }

                if (xbManager == null)
                {
                    xbManager = (XboxManager)Activator.CreateInstance(
                        Marshal.GetTypeFromCLSID(new Guid("A5EB45D8-F3B6-49B9-984A-0D313AB60342")));
                }

                xbCon = xbManager.OpenConsole(xbManager.DefaultConsole);
                xbCon.OpenConnection(null);

                if (!xbCon.DebugTarget.IsDebuggerConnected(out _, out _))
                {
                    xbCon.DebugTarget.ConnectAsDebugger("Xbox Toolbox", XboxDebugConnectFlags.Force);
                }

                activeConnection = xbCon.DebugTarget.IsDebuggerConnected(out _, out _);
                return activeConnection;
            }
            catch
            {
                activeConnection = false;
                xbCon = null;
                return false;
            }
        }

        public void PullXboxInfo()
        {
            try
            {
                var procInfo = xbCon.RunningProcessInfo;
                path2.Text = !string.IsNullOrEmpty(procInfo.ProgramName) ? procInfo.ProgramName : "Unknown";
                name2.Text = xbCon.Name ?? "Unknown";
                type2.Text = xbCon.ConsoleType.ToString();

                try
                {
                    string cpuKey = xbCon.GetCPUKey();
                    cpuKeyBox.Text = cpuKey;
                }
                catch
                {
                    cpuKeyBox.Text = "Unknown";
                }
            }
            catch
            {
                name2.Text = "Error pulling info";
                type2.Text = "Error pulling info";
                path2.Text = "Error pulling info";
                cpuKeyBox.Text = "Error pulling info";
            }
        }


        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectToConsole();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadAllDrives();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            cpuKeyBox.UseSystemPasswordChar = !cpuKeyBox.UseSystemPasswordChar;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cpuKeyBox.Text) && cpuKeyBox.Text != "Unknown" && cpuKeyBox.Text != "Error pulling info")
            {
                Clipboard.SetText(cpuKeyBox.Text);
                MessageBox.Show("CPU Key copied to clipboard!");
            }
        }
    }
}
