namespace DeweyDecApplication
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelHomeDesktop = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnFindCallNum = new System.Windows.Forms.Button();
            this.btnIdentifyAreas = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnGamePage = new System.Windows.Forms.Button();
            this.btnHomePage = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelTitle.SuspendLayout();
            this.panelHomeDesktop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(143)))), ((int)(((byte)(175)))));
            this.panelMenu.Controls.Add(this.btnFindCallNum);
            this.panelMenu.Controls.Add(this.btnIdentifyAreas);
            this.panelMenu.Controls.Add(this.btnExit);
            this.panelMenu.Controls.Add(this.btnGamePage);
            this.panelMenu.Controls.Add(this.btnHomePage);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(305, 643);
            this.panelMenu.TabIndex = 1;
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.BurlyWood;
            this.panelLogo.Controls.Add(this.label2);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(305, 100);
            this.panelLogo.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(27, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Dewey Decimal Application";
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.AntiqueWhite;
            this.panelTitle.Controls.Add(this.lblTitle);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(305, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(857, 100);
            this.panelTitle.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(237, 34);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(445, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THE DEWEY DECIMAL SYSTEM";
            // 
            // panelHomeDesktop
            // 
            this.panelHomeDesktop.BackgroundImage = global::DeweyDecApplication.Properties.Resources.ancient_egypt_styled_dewey_decimal_system;
            this.panelHomeDesktop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelHomeDesktop.Controls.Add(this.pictureBox1);
            this.panelHomeDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHomeDesktop.Location = new System.Drawing.Point(305, 100);
            this.panelHomeDesktop.Name = "panelHomeDesktop";
            this.panelHomeDesktop.Size = new System.Drawing.Size(857, 543);
            this.panelHomeDesktop.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DeweyDecApplication.Properties.Resources.Dewey_Decimal_System_Poster_1024_x_661;
            this.pictureBox1.Location = new System.Drawing.Point(92, 79);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(678, 406);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnFindCallNum
            // 
            this.btnFindCallNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(143)))), ((int)(((byte)(175)))));
            this.btnFindCallNum.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFindCallNum.FlatAppearance.BorderSize = 0;
            this.btnFindCallNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindCallNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindCallNum.ForeColor = System.Drawing.Color.Black;
            this.btnFindCallNum.Image = global::DeweyDecApplication.Properties.Resources.icons8_search_50;
            this.btnFindCallNum.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFindCallNum.Location = new System.Drawing.Point(0, 280);
            this.btnFindCallNum.Name = "btnFindCallNum";
            this.btnFindCallNum.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnFindCallNum.Size = new System.Drawing.Size(305, 60);
            this.btnFindCallNum.TabIndex = 5;
            this.btnFindCallNum.Text = "      Find Call Number";
            this.btnFindCallNum.UseVisualStyleBackColor = false;
            this.btnFindCallNum.Click += new System.EventHandler(this.btnFindCallNum_Click);
            // 
            // btnIdentifyAreas
            // 
            this.btnIdentifyAreas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(143)))), ((int)(((byte)(175)))));
            this.btnIdentifyAreas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnIdentifyAreas.FlatAppearance.BorderSize = 0;
            this.btnIdentifyAreas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIdentifyAreas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIdentifyAreas.ForeColor = System.Drawing.Color.Black;
            this.btnIdentifyAreas.Image = global::DeweyDecApplication.Properties.Resources.icons8_puzzle_matching_50;
            this.btnIdentifyAreas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIdentifyAreas.Location = new System.Drawing.Point(0, 220);
            this.btnIdentifyAreas.Name = "btnIdentifyAreas";
            this.btnIdentifyAreas.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnIdentifyAreas.Size = new System.Drawing.Size(305, 60);
            this.btnIdentifyAreas.TabIndex = 4;
            this.btnIdentifyAreas.Text = "      Identifying Areas";
            this.btnIdentifyAreas.UseVisualStyleBackColor = false;
            this.btnIdentifyAreas.Click += new System.EventHandler(this.btnIdentifyAreas_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(143)))), ((int)(((byte)(175)))));
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Image = global::DeweyDecApplication.Properties.Resources.icons8_logout_50;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(0, 577);
            this.btnExit.Name = "btnExit";
            this.btnExit.Padding = new System.Windows.Forms.Padding(12, 0, 0, 8);
            this.btnExit.Size = new System.Drawing.Size(305, 66);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnGamePage
            // 
            this.btnGamePage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(143)))), ((int)(((byte)(175)))));
            this.btnGamePage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGamePage.FlatAppearance.BorderSize = 0;
            this.btnGamePage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGamePage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGamePage.ForeColor = System.Drawing.Color.Black;
            this.btnGamePage.Image = global::DeweyDecApplication.Properties.Resources.icons8_books_50;
            this.btnGamePage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGamePage.Location = new System.Drawing.Point(0, 160);
            this.btnGamePage.Name = "btnGamePage";
            this.btnGamePage.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnGamePage.Size = new System.Drawing.Size(305, 60);
            this.btnGamePage.TabIndex = 2;
            this.btnGamePage.Text = "       Replacing Books";
            this.btnGamePage.UseVisualStyleBackColor = false;
            this.btnGamePage.Click += new System.EventHandler(this.btnGamePage_Click);
            // 
            // btnHomePage
            // 
            this.btnHomePage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(143)))), ((int)(((byte)(175)))));
            this.btnHomePage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHomePage.FlatAppearance.BorderSize = 0;
            this.btnHomePage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHomePage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHomePage.ForeColor = System.Drawing.Color.Black;
            this.btnHomePage.Image = global::DeweyDecApplication.Properties.Resources.icons8_home_50;
            this.btnHomePage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHomePage.Location = new System.Drawing.Point(0, 100);
            this.btnHomePage.Name = "btnHomePage";
            this.btnHomePage.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnHomePage.Size = new System.Drawing.Size(305, 60);
            this.btnHomePage.TabIndex = 1;
            this.btnHomePage.Text = "     Home";
            this.btnHomePage.UseVisualStyleBackColor = false;
            this.btnHomePage.Click += new System.EventHandler(this.btnHomePage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(208)))), ((int)(((byte)(226)))));
            this.ClientSize = new System.Drawing.Size(1162, 643);
            this.Controls.Add(this.panelHomeDesktop);
            this.Controls.Add(this.panelTitle);
            this.Controls.Add(this.panelMenu);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            this.panelHomeDesktop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnHomePage;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnGamePage;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelHomeDesktop;
        private System.Windows.Forms.Button btnIdentifyAreas;
        private System.Windows.Forms.Button btnFindCallNum;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

