namespace testapp
{
    partial class ManageFavoritesForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listBoxFavorites;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Button buttonRemove;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.listBoxFavorites = new System.Windows.Forms.ListBox();
            this.buttonRename = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxFavorites
            // 
            this.listBoxFavorites.FormattingEnabled = true;
            this.listBoxFavorites.Location = new System.Drawing.Point(12, 12);
            this.listBoxFavorites.Size = new System.Drawing.Size(260, 160);
            this.listBoxFavorites.TabIndex = 0;
            // 
            // buttonRename
            // 
            this.buttonRename.Location = new System.Drawing.Point(12, 185);
            this.buttonRename.Size = new System.Drawing.Size(120, 30);
            this.buttonRename.Text = "Rename";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(152, 185);
            this.buttonRemove.Size = new System.Drawing.Size(120, 30);
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // ManageFavoritesForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 231);
            this.Controls.Add(this.listBoxFavorites);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.buttonRemove);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Favorites";
            this.ResumeLayout(false);
        }
    }
}
