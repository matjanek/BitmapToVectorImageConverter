namespace BitmapToVectorImageConverter
{
    partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openModelImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImagesForComparisonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImagesDialog = new System.Windows.Forms.OpenFileDialog();
            this.comparisonGroupBox = new System.Windows.Forms.GroupBox();
            this.progressLabel = new System.Windows.Forms.Label();
            this.comparisonStatisticsTextBox = new System.Windows.Forms.TextBox();
            this.compareButton = new System.Windows.Forms.Button();
            this.modelImagePictureBoxLabel = new System.Windows.Forms.Label();
            this.modelImagePictureBox = new System.Windows.Forms.PictureBox();
            this.openModelImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.openedImagesGroupBox = new System.Windows.Forms.GroupBox();
            this.imageLoadedPictureBox = new System.Windows.Forms.PictureBox();
            this.imagesLoadedListBox = new System.Windows.Forms.ListBox();
            this.saveResultsFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.comparisonGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modelImagePictureBox)).BeginInit();
            this.openedImagesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageLoadedPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(984, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openModelImageToolStripMenuItem,
            this.openImagesForComparisonToolStripMenuItem,
            this.toolStripSeparator,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openModelImageToolStripMenuItem
            // 
            this.openModelImageToolStripMenuItem.Name = "openModelImageToolStripMenuItem";
            this.openModelImageToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.openModelImageToolStripMenuItem.Text = "Open model image for comparison";
            this.openModelImageToolStripMenuItem.Click += new System.EventHandler(this.openModelImageToolStripMenuItem_Click);
            // 
            // openImagesForComparisonToolStripMenuItem
            // 
            this.openImagesForComparisonToolStripMenuItem.Name = "openImagesForComparisonToolStripMenuItem";
            this.openImagesForComparisonToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.openImagesForComparisonToolStripMenuItem.Text = "Open images for comparison";
            this.openImagesForComparisonToolStripMenuItem.Click += new System.EventHandler(this.openImagesForComparisonToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(257, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // openImagesDialog
            // 
            this.openImagesDialog.Filter = "Image Files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
            this.openImagesDialog.Multiselect = true;
            this.openImagesDialog.Title = "Open images for comparison";
            // 
            // comparisonGroupBox
            // 
            this.comparisonGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comparisonGroupBox.Controls.Add(this.progressLabel);
            this.comparisonGroupBox.Controls.Add(this.comparisonStatisticsTextBox);
            this.comparisonGroupBox.Controls.Add(this.compareButton);
            this.comparisonGroupBox.Location = new System.Drawing.Point(12, 463);
            this.comparisonGroupBox.Name = "comparisonGroupBox";
            this.comparisonGroupBox.Size = new System.Drawing.Size(960, 187);
            this.comparisonGroupBox.TabIndex = 6;
            this.comparisonGroupBox.TabStop = false;
            this.comparisonGroupBox.Text = "Image comparison statistics";
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(524, 163);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(54, 13);
            this.progressLabel.TabIndex = 5;
            this.progressLabel.Text = "Progress: ";
            this.progressLabel.Visible = false;
            // 
            // comparisonStatisticsTextBox
            // 
            this.comparisonStatisticsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comparisonStatisticsTextBox.Enabled = false;
            this.comparisonStatisticsTextBox.Location = new System.Drawing.Point(6, 19);
            this.comparisonStatisticsTextBox.Multiline = true;
            this.comparisonStatisticsTextBox.Name = "comparisonStatisticsTextBox";
            this.comparisonStatisticsTextBox.Size = new System.Drawing.Size(947, 133);
            this.comparisonStatisticsTextBox.TabIndex = 4;
            // 
            // compareButton
            // 
            this.compareButton.Location = new System.Drawing.Point(443, 158);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(75, 23);
            this.compareButton.TabIndex = 3;
            this.compareButton.Text = "Compare";
            this.compareButton.UseVisualStyleBackColor = true;
            this.compareButton.Click += new System.EventHandler(this.compareButton_Click);
            // 
            // modelImagePictureBoxLabel
            // 
            this.modelImagePictureBoxLabel.AutoSize = true;
            this.modelImagePictureBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.modelImagePictureBoxLabel.ForeColor = System.Drawing.Color.Red;
            this.modelImagePictureBoxLabel.Location = new System.Drawing.Point(223, 34);
            this.modelImagePictureBoxLabel.Name = "modelImagePictureBoxLabel";
            this.modelImagePictureBoxLabel.Size = new System.Drawing.Size(179, 20);
            this.modelImagePictureBoxLabel.TabIndex = 8;
            this.modelImagePictureBoxLabel.Text = "No model image loaded!";
            // 
            // modelImagePictureBox
            // 
            this.modelImagePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modelImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.modelImagePictureBox.Location = new System.Drawing.Point(12, 57);
            this.modelImagePictureBox.Name = "modelImagePictureBox";
            this.modelImagePictureBox.Size = new System.Drawing.Size(600, 400);
            this.modelImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.modelImagePictureBox.TabIndex = 7;
            this.modelImagePictureBox.TabStop = false;
            // 
            // openModelImageDialog
            // 
            this.openModelImageDialog.Filter = "Image Files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
            this.openModelImageDialog.Title = "Open vector image for comparison";
            // 
            // openedImagesGroupBox
            // 
            this.openedImagesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openedImagesGroupBox.Controls.Add(this.imageLoadedPictureBox);
            this.openedImagesGroupBox.Controls.Add(this.imagesLoadedListBox);
            this.openedImagesGroupBox.Location = new System.Drawing.Point(618, 34);
            this.openedImagesGroupBox.Name = "openedImagesGroupBox";
            this.openedImagesGroupBox.Size = new System.Drawing.Size(354, 423);
            this.openedImagesGroupBox.TabIndex = 9;
            this.openedImagesGroupBox.TabStop = false;
            this.openedImagesGroupBox.Text = "Images loaded";
            // 
            // imageLoadedPictureBox
            // 
            this.imageLoadedPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageLoadedPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageLoadedPictureBox.Location = new System.Drawing.Point(6, 117);
            this.imageLoadedPictureBox.Name = "imageLoadedPictureBox";
            this.imageLoadedPictureBox.Size = new System.Drawing.Size(340, 300);
            this.imageLoadedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageLoadedPictureBox.TabIndex = 8;
            this.imageLoadedPictureBox.TabStop = false;
            // 
            // imagesLoadedListBox
            // 
            this.imagesLoadedListBox.FormattingEnabled = true;
            this.imagesLoadedListBox.Location = new System.Drawing.Point(6, 20);
            this.imagesLoadedListBox.Name = "imagesLoadedListBox";
            this.imagesLoadedListBox.Size = new System.Drawing.Size(341, 95);
            this.imagesLoadedListBox.TabIndex = 0;
            this.imagesLoadedListBox.SelectedIndexChanged += new System.EventHandler(this.imagesLoadedListBox_SelectedIndexChanged);
            // 
            // saveResultsFileDialog
            // 
            this.saveResultsFileDialog.FileName = "results.csv";
            this.saveResultsFileDialog.Filter = "CSV(*.csv)|*.csv";
            this.saveResultsFileDialog.Title = "Save results to file";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.openedImagesGroupBox);
            this.Controls.Add(this.comparisonGroupBox);
            this.Controls.Add(this.modelImagePictureBoxLabel);
            this.Controls.Add(this.modelImagePictureBox);
            this.Controls.Add(this.menuStrip);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "MainForm";
            this.Text = "Bitmap To Vector Image Converter";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.comparisonGroupBox.ResumeLayout(false);
            this.comparisonGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modelImagePictureBox)).EndInit();
            this.openedImagesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageLoadedPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openImagesDialog;
        private System.Windows.Forms.ToolStripMenuItem openImagesForComparisonToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox comparisonGroupBox;
        private System.Windows.Forms.Label modelImagePictureBoxLabel;
        private System.Windows.Forms.PictureBox modelImagePictureBox;
        private System.Windows.Forms.ToolStripMenuItem openModelImageToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openModelImageDialog;
        private System.Windows.Forms.Button compareButton;
        private System.Windows.Forms.TextBox comparisonStatisticsTextBox;
        private System.Windows.Forms.GroupBox openedImagesGroupBox;
        private System.Windows.Forms.PictureBox imageLoadedPictureBox;
        private System.Windows.Forms.ListBox imagesLoadedListBox;
        private System.Windows.Forms.SaveFileDialog saveResultsFileDialog;
        private System.Windows.Forms.Label progressLabel;
    }
}

