using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace testapp
{
    public partial class ManageFavoritesForm : Form
    {
        private List<Form1.FavoriteGame> _favorites;
        private readonly string FavoritesFile = Path.Combine(Application.StartupPath, "favorites.json");

        public ManageFavoritesForm()
        {
            InitializeComponent();
            LoadFavorites();
        }

        private void LoadFavorites()
        {
            try
            {
                if (File.Exists(FavoritesFile))
                {
                    string json = File.ReadAllText(FavoritesFile);
                    _favorites = JsonConvert.DeserializeObject<List<Form1.FavoriteGame>>(json) ?? new List<Form1.FavoriteGame>();
                }
                else
                {
                    _favorites = new List<Form1.FavoriteGame>();
                }
            }
            catch
            {
                _favorites = new List<Form1.FavoriteGame>();
            }

            listBoxFavorites.Items.Clear();
            foreach (var fav in _favorites)
            {
                listBoxFavorites.Items.Add(fav.Name);
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

        private void buttonRename_Click(object sender, EventArgs e)
        {
            if (listBoxFavorites.SelectedIndex < 0) return;

            var fav = _favorites[listBoxFavorites.SelectedIndex];

            string newName = PromptForName(fav.Name);
            if (!string.IsNullOrWhiteSpace(newName))
            {
                fav.Name = newName;
                SaveFavorites();
                Form1.RebuildJumpList(_favorites);
                LoadFavorites();
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxFavorites.SelectedIndex < 0) return;

            _favorites.RemoveAt(listBoxFavorites.SelectedIndex);
            SaveFavorites();
            Form1.RebuildJumpList(_favorites);
            LoadFavorites();
        }

        private string PromptForName(string defaultName)
        {
            using (var prompt = new Form())
            {
                prompt.Width = 300;
                prompt.Height = 150;
                prompt.Text = "Rename Favorite";

                var textBox = new TextBox() { Left = 20, Top = 20, Width = 240, Text = defaultName };
                var okButton = new Button() { Text = "OK", Left = 100, Width = 80, Top = 60, DialogResult = DialogResult.OK };

                prompt.Controls.Add(textBox);
                prompt.Controls.Add(okButton);
                prompt.AcceptButton = okButton;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : defaultName;
            }
        }
    }
}
