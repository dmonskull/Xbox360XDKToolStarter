using System;
using System.Windows.Forms;
using System.Windows; // <-- for WPF Application

namespace testapp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            var wpfApp = System.Windows.Application.Current ?? new System.Windows.Application();

            if (args.Length > 0)
            {
                if (args[0] == "--manage")
                {
                    System.Windows.Forms.Application.Run(new ManageFavoritesForm());
                    return;
                }
                else
                {
                    var tempForm = new Form1(wpfApp, args);
                    tempForm.LaunchGameFromPath(args[0]);
                    return;
                }
            }

            System.Windows.Forms.Application.Run(new Form1(wpfApp, args));
        }

    }
}
