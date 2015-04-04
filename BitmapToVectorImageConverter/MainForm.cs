using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitmapToVectorImageConverter
{
    public partial class MainForm : Form
    {
        private Bitmap _inputImage;
        private Bitmap _vectorImageForComparison;
        private Bitmap _outputImage;
        public MainForm()
        {
            InitializeComponent();
        }

        private void openInputFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openInputImageDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _inputImage = new Bitmap(openInputImageDialog.FileName);
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
            }
            if (_inputImage != null)
            {
                inputPictureBox.Image = _inputImage;
                inputPictureBox.Visible = true;
                inputPictureBoxLabel.Text = "Input image";
                inputPictureBoxLabel.ForeColor = Color.Black;
                convertButton.Enabled = true;
            }
            else
            {
                inputPictureBox.Visible = false;
                inputPictureBoxLabel.Text = "No input image loaded!";
                inputPictureBoxLabel.ForeColor = Color.Red;
                convertButton.Enabled = false;
            }
        }

        private void openVectorImageForComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openVectorImageForComparisonFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _vectorImageForComparison = new Bitmap(openVectorImageForComparisonFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
            }
            if (_vectorImageForComparison != null)
            {
                vectorImageForComparisonPictureBox.Image = _vectorImageForComparison;
                vectorImageForComparisonPictureBox.Visible = true;
                vectorImageForComparisonPictureBoxLabel.Text = "Vector image for comparison";
                vectorImageForComparisonPictureBoxLabel.ForeColor = Color.Black;
            }
            else
            {
                vectorImageForComparisonPictureBox.Visible = false;
                vectorImageForComparisonPictureBoxLabel.Text = "No vector image for comparison loaded!";
                vectorImageForComparisonPictureBoxLabel.ForeColor = Color.Red;
            }
        }

        private void ShowError(Exception ex)
        {
            const string title = "Error";
            String error = "An error occured while loading file. Please try again.\nError: " + ex.Message;
            MessageBox.Show(error, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            if (_inputImage != null)
            {
                //TODO start konwersji obrazka z _inputImage z zadanymi parametrami
                /* 
                 * otrzymujemy _outputImage
                 * aktualnie przepisuje _inputImage do _outputImage bez zmian
                 */
                _outputImage = new Bitmap(_inputImage);
                if (_outputImage != null)
                {
                    outputPictureBox.Image = _inputImage;
                    outputPictureBox.Visible = true;
                    outputPictureBoxLabel.Text = "Output image";
                    outputPictureBoxLabel.ForeColor = Color.Black;
                }
                else
                {
                    outputPictureBox.Visible = false;
                    outputPictureBoxLabel.Text = "No output image generated!";
                    outputPictureBoxLabel.ForeColor = Color.Red;
                }
            }
        }
    }
}
