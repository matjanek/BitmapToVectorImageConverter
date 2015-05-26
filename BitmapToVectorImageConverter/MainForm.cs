using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BitmapToVectorImageConverter.ImageComparison;

namespace BitmapToVectorImageConverter
{
    public partial class MainForm : Form
    {
        private Bitmap _modelImage;
        private List<Bitmap> _imagesLoaded;
        public MainForm()
        {
            InitializeComponent();
        }

        private void openModelImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openModelImageDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _modelImage = new Bitmap(openModelImageDialog.FileName);
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
            }
            if (_modelImage != null)
            {
                modelImagePictureBox.Image = _modelImage;
                modelImagePictureBox.Visible = true;
                modelImagePictureBoxLabel.Text = "Model image";
                modelImagePictureBoxLabel.ForeColor = Color.Black;
            }
            else
            {
                modelImagePictureBox.Visible = false;
                modelImagePictureBoxLabel.Text = "No model image loaded!";
                modelImagePictureBoxLabel.ForeColor = Color.Red;
            }
        }

        private void openImagesForComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openImagesDialog.ShowDialog() == DialogResult.OK)
            {
                _imagesLoaded = new List<Bitmap>();
                imagesLoadedListBox.Items.Clear();
                foreach (var file in openImagesDialog.FileNames)
                {
                    try
                    {
                        _imagesLoaded.Add(new Bitmap(file));
                        imagesLoadedListBox.Items.Add(file);
                    }
                    catch (Exception ex)
                    {
                        ShowError(ex);
                    }
                }
                if (imagesLoadedListBox.Items.Count > 0 && _imagesLoaded.Count > 0)
                {
                    imagesLoadedListBox.SelectedIndex = 0;
                    imageLoadedPictureBox.Image = _imagesLoaded[0];
                    imageLoadedPictureBox.Visible = true;
                }
                else
                {
                    imageLoadedPictureBox.Image = null;
                    imageLoadedPictureBox.Visible = false;
                }
            }
        }

        private void ShowError(Exception ex)
        {
            const string title = "Error";
            var error = "An error occured while loading file.\nError: " + ex.Message;
            MessageBox.Show(error, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void imagesLoadedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = imagesLoadedListBox.SelectedIndex;
            if (index >= 0 && index < _imagesLoaded.Count)
            {
                imageLoadedPictureBox.Image = _imagesLoaded[index];
                imageLoadedPictureBox.Visible = true;
            }
            else
            {
                imageLoadedPictureBox.Image = null;
                imageLoadedPictureBox.Visible = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void compareButton_Click(object sender, EventArgs e)
        {
            saveResultsFileDialog.ShowDialog();
            compareButton.Enabled = false;
            progressLabel.Visible = true;
            if (saveResultsFileDialog.FileName != "")
            {
                try
                {
                    _modelImage = new Bitmap(openModelImageDialog.FileName);
                    ImageComparer IC = new ImageComparer(_modelImage, _imagesLoaded, imagesLoadedListBox.Items);
                    var progress = new Progress<int>(percent =>
                    {
                        progressLabel.Text = "Progress: " + percent + "%";
                    });
                    string comparisonResults = await Task.Run(() => IC.Compare(progress));
                    comparisonStatisticsTextBox.Text = comparisonResults;
                    StreamWriter file = new StreamWriter(saveResultsFileDialog.FileName);
                    file.WriteLine(comparisonResults);
                    file.Close();
                }
                catch (Exception ex)
                {
                    const string title = "Error";
                    var error = "An error occured while comparing images.\nError: " + ex.Message;
                    MessageBox.Show(error, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            compareButton.Enabled = true;
            progressLabel.Visible = false;
        }
    }
}
