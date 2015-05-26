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
            this.inputPictureBox = new System.Windows.Forms.PictureBox();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInputFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openVectorImageForComparisonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInputImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.inputPictureBoxLabel = new System.Windows.Forms.Label();
            this.algorithmPropertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.outputPictureBox = new System.Windows.Forms.PictureBox();
            this.outputPictureBoxLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.vectorImageForComparisonPictureBoxLabel = new System.Windows.Forms.Label();
            this.vectorImageForComparisonPictureBox = new System.Windows.Forms.PictureBox();
            this.openVectorImageForComparisonFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.percentageComparisonInputVectorLabel = new System.Windows.Forms.Label();
            this.percentageComparisonInputOutputLabel = new System.Windows.Forms.Label();
            this.percentageComparisonVectorOutputLabel = new System.Windows.Forms.Label();
            this.compareButton = new System.Windows.Forms.Button();
            this.ssimComparisonVectorOutputLabel = new System.Windows.Forms.Label();
            this.ssimComparisonInputOutputLabel = new System.Windows.Forms.Label();
            this.ssimComparisonInputVectorLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.inputPictureBox)).BeginInit();
            this.MenuStrip.SuspendLayout();
            this.algorithmPropertiesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vectorImageForComparisonPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // inputPictureBox
            // 
            this.inputPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputPictureBox.Location = new System.Drawing.Point(12, 57);
            this.inputPictureBox.Name = "inputPictureBox";
            this.inputPictureBox.Size = new System.Drawing.Size(300, 300);
            this.inputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.inputPictureBox.TabIndex = 0;
            this.inputPictureBox.TabStop = false;
            this.inputPictureBox.Visible = false;
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(984, 24);
            this.MenuStrip.TabIndex = 1;
            this.MenuStrip.Text = "MenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInputFileToolStripMenuItem,
            this.openVectorImageForComparisonToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openInputFileToolStripMenuItem
            // 
            this.openInputFileToolStripMenuItem.Name = "openInputFileToolStripMenuItem";
            this.openInputFileToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.openInputFileToolStripMenuItem.Text = "Open input raster image";
            this.openInputFileToolStripMenuItem.Click += new System.EventHandler(this.openInputFileToolStripMenuItem_Click);
            // 
            // openVectorImageForComparisonToolStripMenuItem
            // 
            this.openVectorImageForComparisonToolStripMenuItem.Name = "openVectorImageForComparisonToolStripMenuItem";
            this.openVectorImageForComparisonToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.openVectorImageForComparisonToolStripMenuItem.Text = "Open vector image for comparison";
            this.openVectorImageForComparisonToolStripMenuItem.Click += new System.EventHandler(this.openVectorImageForComparisonToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(256, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // openInputImageDialog
            // 
            this.openInputImageDialog.Filter = "Image Files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
            this.openInputImageDialog.Title = "Open input raster image";
            // 
            // inputPictureBoxLabel
            // 
            this.inputPictureBoxLabel.AutoSize = true;
            this.inputPictureBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.inputPictureBoxLabel.ForeColor = System.Drawing.Color.Red;
            this.inputPictureBoxLabel.Location = new System.Drawing.Point(77, 34);
            this.inputPictureBoxLabel.Name = "inputPictureBoxLabel";
            this.inputPictureBoxLabel.Size = new System.Drawing.Size(171, 20);
            this.inputPictureBoxLabel.TabIndex = 2;
            this.inputPictureBoxLabel.Text = "No input image loaded!";
            // 
            // algorithmPropertiesGroupBox
            // 
            this.algorithmPropertiesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.algorithmPropertiesGroupBox.Controls.Add(this.convertButton);
            this.algorithmPropertiesGroupBox.Location = new System.Drawing.Point(12, 549);
            this.algorithmPropertiesGroupBox.Name = "algorithmPropertiesGroupBox";
            this.algorithmPropertiesGroupBox.Size = new System.Drawing.Size(960, 100);
            this.algorithmPropertiesGroupBox.TabIndex = 3;
            this.algorithmPropertiesGroupBox.TabStop = false;
            this.algorithmPropertiesGroupBox.Text = "Algorithm properties";
            // 
            // convertButton
            // 
            this.convertButton.Enabled = false;
            this.convertButton.Location = new System.Drawing.Point(443, 71);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(75, 23);
            this.convertButton.TabIndex = 0;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // outputPictureBox
            // 
            this.outputPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputPictureBox.Location = new System.Drawing.Point(672, 57);
            this.outputPictureBox.Name = "outputPictureBox";
            this.outputPictureBox.Size = new System.Drawing.Size(300, 300);
            this.outputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.outputPictureBox.TabIndex = 4;
            this.outputPictureBox.TabStop = false;
            this.outputPictureBox.Visible = false;
            // 
            // outputPictureBoxLabel
            // 
            this.outputPictureBoxLabel.AutoSize = true;
            this.outputPictureBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.outputPictureBoxLabel.ForeColor = System.Drawing.Color.Red;
            this.outputPictureBoxLabel.Location = new System.Drawing.Point(719, 34);
            this.outputPictureBoxLabel.Name = "outputPictureBoxLabel";
            this.outputPictureBoxLabel.Size = new System.Drawing.Size(207, 20);
            this.outputPictureBoxLabel.TabIndex = 5;
            this.outputPictureBoxLabel.Text = "No output image generated!";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ssimComparisonVectorOutputLabel);
            this.groupBox1.Controls.Add(this.ssimComparisonInputOutputLabel);
            this.groupBox1.Controls.Add(this.ssimComparisonInputVectorLabel);
            this.groupBox1.Controls.Add(this.compareButton);
            this.groupBox1.Controls.Add(this.percentageComparisonVectorOutputLabel);
            this.groupBox1.Controls.Add(this.percentageComparisonInputOutputLabel);
            this.groupBox1.Controls.Add(this.percentageComparisonInputVectorLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 363);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(960, 180);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image comparison statistics";
            // 
            // vectorImageForComparisonPictureBoxLabel
            // 
            this.vectorImageForComparisonPictureBoxLabel.AutoSize = true;
            this.vectorImageForComparisonPictureBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.vectorImageForComparisonPictureBoxLabel.ForeColor = System.Drawing.Color.Red;
            this.vectorImageForComparisonPictureBoxLabel.Location = new System.Drawing.Point(348, 34);
            this.vectorImageForComparisonPictureBoxLabel.Name = "vectorImageForComparisonPictureBoxLabel";
            this.vectorImageForComparisonPictureBoxLabel.Size = new System.Drawing.Size(288, 20);
            this.vectorImageForComparisonPictureBoxLabel.TabIndex = 8;
            this.vectorImageForComparisonPictureBoxLabel.Text = "No vector image for comparison loaded!";
            // 
            // vectorImageForComparisonPictureBox
            // 
            this.vectorImageForComparisonPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vectorImageForComparisonPictureBox.Location = new System.Drawing.Point(342, 57);
            this.vectorImageForComparisonPictureBox.Name = "vectorImageForComparisonPictureBox";
            this.vectorImageForComparisonPictureBox.Size = new System.Drawing.Size(300, 300);
            this.vectorImageForComparisonPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.vectorImageForComparisonPictureBox.TabIndex = 7;
            this.vectorImageForComparisonPictureBox.TabStop = false;
            this.vectorImageForComparisonPictureBox.Visible = false;
            // 
            // openVectorImageForComparisonFileDialog
            // 
            this.openVectorImageForComparisonFileDialog.Title = "Open vector image for comparison";
            // 
            // percentageComparisonInputVectorLabel
            // 
            this.percentageComparisonInputVectorLabel.AutoSize = true;
            this.percentageComparisonInputVectorLabel.Location = new System.Drawing.Point(7, 20);
            this.percentageComparisonInputVectorLabel.Name = "percentageComparisonInputVectorLabel";
            this.percentageComparisonInputVectorLabel.Size = new System.Drawing.Size(193, 13);
            this.percentageComparisonInputVectorLabel.TabIndex = 0;
            this.percentageComparisonInputVectorLabel.Text = "Percentage comparison (input - vector):";
            // 
            // percentageComparisonInputOutputLabel
            // 
            this.percentageComparisonInputOutputLabel.AutoSize = true;
            this.percentageComparisonInputOutputLabel.Location = new System.Drawing.Point(7, 33);
            this.percentageComparisonInputOutputLabel.Name = "percentageComparisonInputOutputLabel";
            this.percentageComparisonInputOutputLabel.Size = new System.Drawing.Size(193, 13);
            this.percentageComparisonInputOutputLabel.TabIndex = 1;
            this.percentageComparisonInputOutputLabel.Text = "Percentage comparison (input - output):";
            // 
            // percentageComparisonVectorOutputLabel
            // 
            this.percentageComparisonVectorOutputLabel.AutoSize = true;
            this.percentageComparisonVectorOutputLabel.Location = new System.Drawing.Point(7, 46);
            this.percentageComparisonVectorOutputLabel.Name = "percentageComparisonVectorOutputLabel";
            this.percentageComparisonVectorOutputLabel.Size = new System.Drawing.Size(200, 13);
            this.percentageComparisonVectorOutputLabel.TabIndex = 2;
            this.percentageComparisonVectorOutputLabel.Text = "Percentage comparison (vector - output):";
            // 
            // compareButton
            // 
            this.compareButton.Location = new System.Drawing.Point(443, 151);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(75, 23);
            this.compareButton.TabIndex = 3;
            this.compareButton.Text = "Compare";
            this.compareButton.UseVisualStyleBackColor = true;
            this.compareButton.Click += new System.EventHandler(this.compareButton_Click);
            // 
            // ssimComparisonVectorOutputLabel
            // 
            this.ssimComparisonVectorOutputLabel.AutoSize = true;
            this.ssimComparisonVectorOutputLabel.Location = new System.Drawing.Point(7, 85);
            this.ssimComparisonVectorOutputLabel.Name = "ssimComparisonVectorOutputLabel";
            this.ssimComparisonVectorOutputLabel.Size = new System.Drawing.Size(171, 13);
            this.ssimComparisonVectorOutputLabel.TabIndex = 6;
            this.ssimComparisonVectorOutputLabel.Text = "SSIM comparison (vector - output):";
            // 
            // ssimComparisonInputOutputLabel
            // 
            this.ssimComparisonInputOutputLabel.AutoSize = true;
            this.ssimComparisonInputOutputLabel.Location = new System.Drawing.Point(7, 72);
            this.ssimComparisonInputOutputLabel.Name = "ssimComparisonInputOutputLabel";
            this.ssimComparisonInputOutputLabel.Size = new System.Drawing.Size(164, 13);
            this.ssimComparisonInputOutputLabel.TabIndex = 5;
            this.ssimComparisonInputOutputLabel.Text = "SSIM comparison (input - output):";
            // 
            // ssimComparisonInputVectorLabel
            // 
            this.ssimComparisonInputVectorLabel.AutoSize = true;
            this.ssimComparisonInputVectorLabel.Location = new System.Drawing.Point(7, 59);
            this.ssimComparisonInputVectorLabel.Name = "ssimComparisonInputVectorLabel";
            this.ssimComparisonInputVectorLabel.Size = new System.Drawing.Size(164, 13);
            this.ssimComparisonInputVectorLabel.TabIndex = 4;
            this.ssimComparisonInputVectorLabel.Text = "SSIM comparison (input - vector):";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.algorithmPropertiesGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.vectorImageForComparisonPictureBoxLabel);
            this.Controls.Add(this.vectorImageForComparisonPictureBox);
            this.Controls.Add(this.outputPictureBoxLabel);
            this.Controls.Add(this.outputPictureBox);
            this.Controls.Add(this.inputPictureBoxLabel);
            this.Controls.Add(this.inputPictureBox);
            this.Controls.Add(this.MenuStrip);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "MainForm";
            this.Text = "Bitmap To Vector Image Converter";
            ((System.ComponentModel.ISupportInitialize)(this.inputPictureBox)).EndInit();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.algorithmPropertiesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vectorImageForComparisonPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox inputPictureBox;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openInputImageDialog;
        private System.Windows.Forms.ToolStripMenuItem openInputFileToolStripMenuItem;
        private System.Windows.Forms.Label inputPictureBoxLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox algorithmPropertiesGroupBox;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.PictureBox outputPictureBox;
        private System.Windows.Forms.Label outputPictureBoxLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label vectorImageForComparisonPictureBoxLabel;
        private System.Windows.Forms.PictureBox vectorImageForComparisonPictureBox;
        private System.Windows.Forms.ToolStripMenuItem openVectorImageForComparisonToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openVectorImageForComparisonFileDialog;
        private System.Windows.Forms.Button compareButton;
        private System.Windows.Forms.Label percentageComparisonVectorOutputLabel;
        private System.Windows.Forms.Label percentageComparisonInputOutputLabel;
        private System.Windows.Forms.Label percentageComparisonInputVectorLabel;
        private System.Windows.Forms.Label ssimComparisonVectorOutputLabel;
        private System.Windows.Forms.Label ssimComparisonInputOutputLabel;
        private System.Windows.Forms.Label ssimComparisonInputVectorLabel;
    }
}

