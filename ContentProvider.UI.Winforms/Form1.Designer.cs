namespace ContentProvider.UI.Winforms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mediaPage = new System.Windows.Forms.TabPage();
            this.linkBox = new System.Windows.Forms.ListBox();
            this.picPanel = new System.Windows.Forms.Panel();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.mediaTitleLabel = new System.Windows.Forms.Label();
            this.downloadButton = new System.Windows.Forms.Button();
            this.browsePage = new System.Windows.Forms.TabPage();
            this.loadButton = new System.Windows.Forms.Button();
            this.manualButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.manualBox = new System.Windows.Forms.TextBox();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.moduleCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listingBox = new System.Windows.Forms.ListBox();
            this.browseBox = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.coverPicture = new System.Windows.Forms.PictureBox();
            this.mediaPage.SuspendLayout();
            this.picPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.browsePage.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.coverPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // mediaPage
            // 
            this.mediaPage.Controls.Add(this.linkBox);
            this.mediaPage.Controls.Add(this.picPanel);
            this.mediaPage.Controls.Add(this.mediaTitleLabel);
            this.mediaPage.Controls.Add(this.downloadButton);
            this.mediaPage.Location = new System.Drawing.Point(4, 30);
            this.mediaPage.Name = "mediaPage";
            this.mediaPage.Padding = new System.Windows.Forms.Padding(3);
            this.mediaPage.Size = new System.Drawing.Size(1068, 587);
            this.mediaPage.TabIndex = 1;
            this.mediaPage.Text = "Media";
            this.mediaPage.UseVisualStyleBackColor = true;
            // 
            // linkBox
            // 
            this.linkBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.linkBox.FormattingEnabled = true;
            this.linkBox.ItemHeight = 21;
            this.linkBox.Location = new System.Drawing.Point(6, 45);
            this.linkBox.Name = "linkBox";
            this.linkBox.Size = new System.Drawing.Size(382, 487);
            this.linkBox.TabIndex = 1;
            this.linkBox.SelectedIndexChanged += new System.EventHandler(this.linkBox_SelectedIndexChanged);
            // 
            // picPanel
            // 
            this.picPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picPanel.AutoScroll = true;
            this.picPanel.Controls.Add(this.axWindowsMediaPlayer1);
            this.picPanel.Controls.Add(this.picBox);
            this.picPanel.Location = new System.Drawing.Point(394, 45);
            this.picPanel.Name = "picPanel";
            this.picPanel.Size = new System.Drawing.Size(668, 544);
            this.picPanel.TabIndex = 5;
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(3, 3);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(668, 536);
            this.axWindowsMediaPlayer1.TabIndex = 5;
            this.axWindowsMediaPlayer1.Visible = false;
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(3, 3);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(662, 538);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            // 
            // mediaTitleLabel
            // 
            this.mediaTitleLabel.AutoSize = true;
            this.mediaTitleLabel.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediaTitleLabel.Location = new System.Drawing.Point(6, 6);
            this.mediaTitleLabel.Name = "mediaTitleLabel";
            this.mediaTitleLabel.Size = new System.Drawing.Size(0, 28);
            this.mediaTitleLabel.TabIndex = 4;
            // 
            // downloadButton
            // 
            this.downloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.downloadButton.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadButton.Location = new System.Drawing.Point(6, 549);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(382, 33);
            this.downloadButton.TabIndex = 2;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // browsePage
            // 
            this.browsePage.Controls.Add(this.coverPicture);
            this.browsePage.Controls.Add(this.loadButton);
            this.browsePage.Controls.Add(this.manualButton);
            this.browsePage.Controls.Add(this.label3);
            this.browsePage.Controls.Add(this.manualBox);
            this.browsePage.Controls.Add(this.searchBox);
            this.browsePage.Controls.Add(this.searchButton);
            this.browsePage.Controls.Add(this.label2);
            this.browsePage.Controls.Add(this.titleLabel);
            this.browsePage.Controls.Add(this.moduleCombo);
            this.browsePage.Controls.Add(this.label1);
            this.browsePage.Controls.Add(this.listingBox);
            this.browsePage.Controls.Add(this.browseBox);
            this.browsePage.Location = new System.Drawing.Point(4, 30);
            this.browsePage.Name = "browsePage";
            this.browsePage.Padding = new System.Windows.Forms.Padding(3);
            this.browsePage.Size = new System.Drawing.Size(1068, 587);
            this.browsePage.TabIndex = 0;
            this.browsePage.Text = "Browse";
            this.browsePage.UseVisualStyleBackColor = true;
            // 
            // loadButton
            // 
            this.loadButton.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.Location = new System.Drawing.Point(455, 6);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(92, 36);
            this.loadButton.TabIndex = 11;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // manualButton
            // 
            this.manualButton.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualButton.Location = new System.Drawing.Point(408, 81);
            this.manualButton.Name = "manualButton";
            this.manualButton.Size = new System.Drawing.Size(139, 30);
            this.manualButton.TabIndex = 10;
            this.manualButton.Text = "Enter";
            this.manualButton.UseVisualStyleBackColor = true;
            this.manualButton.Click += new System.EventHandler(this.manualButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 28);
            this.label3.TabIndex = 9;
            this.label3.Text = "Manual";
            // 
            // manualBox
            // 
            this.manualBox.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualBox.Location = new System.Drawing.Point(156, 83);
            this.manualBox.Name = "manualBox";
            this.manualBox.Size = new System.Drawing.Size(246, 29);
            this.manualBox.TabIndex = 8;
            // 
            // searchBox
            // 
            this.searchBox.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBox.Location = new System.Drawing.Point(156, 48);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(246, 29);
            this.searchBox.TabIndex = 5;
            // 
            // searchButton
            // 
            this.searchButton.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.Location = new System.Drawing.Point(408, 46);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(139, 30);
            this.searchButton.TabIndex = 7;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 28);
            this.label2.TabIndex = 6;
            this.label2.Text = "Search";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(553, 3);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(0, 28);
            this.titleLabel.TabIndex = 4;
            // 
            // moduleCombo
            // 
            this.moduleCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moduleCombo.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moduleCombo.FormattingEnabled = true;
            this.moduleCombo.Location = new System.Drawing.Point(94, 6);
            this.moduleCombo.Name = "moduleCombo";
            this.moduleCombo.Size = new System.Drawing.Size(355, 36);
            this.moduleCombo.TabIndex = 3;
            this.moduleCombo.SelectedIndexChanged += new System.EventHandler(this.moduleCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Module";
            // 
            // listingBox
            // 
            this.listingBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listingBox.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listingBox.FormattingEnabled = true;
            this.listingBox.ItemHeight = 21;
            this.listingBox.Location = new System.Drawing.Point(558, 150);
            this.listingBox.Name = "listingBox";
            this.listingBox.Size = new System.Drawing.Size(504, 424);
            this.listingBox.TabIndex = 1;
            this.listingBox.Click += new System.EventHandler(this.listingBox_DoubleClick);
            // 
            // browseBox
            // 
            this.browseBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.browseBox.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseBox.FormattingEnabled = true;
            this.browseBox.ItemHeight = 36;
            this.browseBox.Location = new System.Drawing.Point(6, 129);
            this.browseBox.Name = "browseBox";
            this.browseBox.Size = new System.Drawing.Size(546, 436);
            this.browseBox.TabIndex = 0;
            this.browseBox.Click += new System.EventHandler(this.browseBox_DoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.browsePage);
            this.tabControl1.Controls.Add(this.mediaPage);
            this.tabControl1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1076, 621);
            this.tabControl1.TabIndex = 0;
            // 
            // coverPicture
            // 
            this.coverPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.coverPicture.Location = new System.Drawing.Point(944, 9);
            this.coverPicture.Name = "coverPicture";
            this.coverPicture.Size = new System.Drawing.Size(118, 135);
            this.coverPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.coverPicture.TabIndex = 12;
            this.coverPicture.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 645);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "MediaExplorer";
            this.mediaPage.ResumeLayout(false);
            this.mediaPage.PerformLayout();
            this.picPanel.ResumeLayout(false);
            this.picPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.browsePage.ResumeLayout(false);
            this.browsePage.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.coverPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage mediaPage;
        private System.Windows.Forms.ListBox linkBox;
        private System.Windows.Forms.Panel picPanel;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Label mediaTitleLabel;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.TabPage browsePage;
        private System.Windows.Forms.Button manualButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox manualBox;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.ComboBox moduleCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listingBox;
        private System.Windows.Forms.ListBox browseBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.PictureBox coverPicture;


    }
}

