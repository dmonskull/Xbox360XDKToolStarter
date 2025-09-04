namespace testapp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.test1 = new System.Windows.Forms.Button();
            this.reconnect = new System.Windows.Forms.Button();
            this.connection = new System.Windows.Forms.Label();
            this.connction2 = new System.Windows.Forms.Label();
            this.path2 = new System.Windows.Forms.Label();
            this.path = new System.Windows.Forms.Label();
            this.scanbtn = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.backbtn = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.Label();
            this.name2 = new System.Windows.Forms.Label();
            this.type2 = new System.Windows.Forms.Label();
            this.type = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCurrentPath = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.cpuKeyBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // test1
            // 
            this.test1.Location = new System.Drawing.Point(15, 226);
            this.test1.Name = "test1";
            this.test1.Size = new System.Drawing.Size(118, 32);
            this.test1.TabIndex = 0;
            this.test1.Text = "Test Button";
            this.test1.UseVisualStyleBackColor = true;
            this.test1.Click += new System.EventHandler(this.button1_Click);
            // 
            // reconnect
            // 
            this.reconnect.Location = new System.Drawing.Point(12, 34);
            this.reconnect.Name = "reconnect";
            this.reconnect.Size = new System.Drawing.Size(121, 23);
            this.reconnect.TabIndex = 1;
            this.reconnect.Text = "Reconnect";
            this.reconnect.UseVisualStyleBackColor = true;
            this.reconnect.Click += new System.EventHandler(this.button2_Click);
            // 
            // connection
            // 
            this.connection.AutoSize = true;
            this.connection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connection.Location = new System.Drawing.Point(12, 9);
            this.connection.Name = "connection";
            this.connection.Size = new System.Drawing.Size(64, 13);
            this.connection.TabIndex = 2;
            this.connection.Text = "Connection:";
            // 
            // connction2
            // 
            this.connction2.AutoSize = true;
            this.connction2.Location = new System.Drawing.Point(82, 9);
            this.connction2.Name = "connction2";
            this.connction2.Size = new System.Drawing.Size(31, 13);
            this.connction2.TabIndex = 3;
            this.connction2.Text = "IDLE";
            // 
            // path2
            // 
            this.path2.AutoSize = true;
            this.path2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.path2.Location = new System.Drawing.Point(93, 116);
            this.path2.Name = "path2";
            this.path2.Size = new System.Drawing.Size(0, 12);
            this.path2.TabIndex = 6;
            // 
            // path
            // 
            this.path.AutoSize = true;
            this.path.Location = new System.Drawing.Point(12, 116);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(75, 13);
            this.path.TabIndex = 7;
            this.path.Text = "Running Path:";
            // 
            // scanbtn
            // 
            this.scanbtn.Location = new System.Drawing.Point(371, 272);
            this.scanbtn.Name = "scanbtn";
            this.scanbtn.Size = new System.Drawing.Size(94, 24);
            this.scanbtn.TabIndex = 9;
            this.scanbtn.Text = "Scan";
            this.scanbtn.UseVisualStyleBackColor = true;
            this.scanbtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(371, 25);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(210, 228);
            this.listView1.TabIndex = 10;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(368, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "File Explorer";
            // 
            // backbtn
            // 
            this.backbtn.Location = new System.Drawing.Point(487, 272);
            this.backbtn.Name = "backbtn";
            this.backbtn.Size = new System.Drawing.Size(94, 24);
            this.backbtn.TabIndex = 12;
            this.backbtn.Text = "Back";
            this.backbtn.UseVisualStyleBackColor = true;
            this.backbtn.Click += new System.EventHandler(this.button4_Click);
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(12, 71);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(79, 13);
            this.name.TabIndex = 13;
            this.name.Text = "Console Name:";
            // 
            // name2
            // 
            this.name2.AutoSize = true;
            this.name2.Location = new System.Drawing.Point(93, 71);
            this.name2.Name = "name2";
            this.name2.Size = new System.Drawing.Size(0, 13);
            this.name2.TabIndex = 14;
            // 
            // type2
            // 
            this.type2.AutoSize = true;
            this.type2.Location = new System.Drawing.Point(93, 94);
            this.type2.Name = "type2";
            this.type2.Size = new System.Drawing.Size(0, 13);
            this.type2.TabIndex = 16;
            // 
            // type
            // 
            this.type.AutoSize = true;
            this.type.Location = new System.Drawing.Point(12, 94);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(75, 13);
            this.type.TabIndex = 15;
            this.type.Text = "Console Type:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(193, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem1.Text = "contextMenuFavorites";
            // 
            // labelCurrentPath
            // 
            this.labelCurrentPath.AutoSize = true;
            this.labelCurrentPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentPath.Location = new System.Drawing.Point(369, 257);
            this.labelCurrentPath.Name = "labelCurrentPath";
            this.labelCurrentPath.Size = new System.Drawing.Size(0, 12);
            this.labelCurrentPath.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(178, 161);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 25);
            this.button1.TabIndex = 19;
            this.button1.Text = "Copy CPU Key";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "CPU Key:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(71, 161);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 25);
            this.button2.TabIndex = 22;
            this.button2.Text = "Show CPU Key";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // cpuKeyBox
            // 
            this.cpuKeyBox.Location = new System.Drawing.Point(71, 135);
            this.cpuKeyBox.Name = "cpuKeyBox";
            this.cpuKeyBox.ReadOnly = true;
            this.cpuKeyBox.Size = new System.Drawing.Size(232, 20);
            this.cpuKeyBox.TabIndex = 23;
            this.cpuKeyBox.UseSystemPasswordChar = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 404);
            this.Controls.Add(this.cpuKeyBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelCurrentPath);
            this.Controls.Add(this.type2);
            this.Controls.Add(this.type);
            this.Controls.Add(this.name2);
            this.Controls.Add(this.name);
            this.Controls.Add(this.backbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.scanbtn);
            this.Controls.Add(this.path);
            this.Controls.Add(this.path2);
            this.Controls.Add(this.connction2);
            this.Controls.Add(this.connection);
            this.Controls.Add(this.reconnect);
            this.Controls.Add(this.test1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button test1;
        private System.Windows.Forms.Button reconnect;
        private System.Windows.Forms.Label connection;
        private System.Windows.Forms.Label connction2;
        private System.Windows.Forms.Label path2;
        private System.Windows.Forms.Label path;
        private System.Windows.Forms.Button scanbtn;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button backbtn;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label name2;
        private System.Windows.Forms.Label type2;
        private System.Windows.Forms.Label type;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label labelCurrentPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox cpuKeyBox;
    }
}

