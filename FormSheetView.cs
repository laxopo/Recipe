using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recipe
{
    public partial class FormSheetView : Form
    {
        public FormSheetView(PictureBox area, Config config)
        {
            InitializeComponent();
            Area = area;

            labelPath.Text = Area.ImageLocation;

            comboBoxLayout.Items.AddRange(modeNames);
            comboBoxLayout.Text = modeText[Area.SizeMode];
            this.config = config;
        }

        private PictureBox Area;
        private Config config;
        private bool loaded;

        private static string[] modeNames = 
        {
            "Normal",
            "Stretch",
            "Center",
            "Zoom"
        };

        private static Dictionary<int, PictureBoxSizeMode> modeOption = new Dictionary<int, PictureBoxSizeMode>
        {
            { 0, PictureBoxSizeMode.Normal },
            { 1, PictureBoxSizeMode.StretchImage },
            { 2, PictureBoxSizeMode.CenterImage },
            { 3, PictureBoxSizeMode.Zoom }
        };

        private static Dictionary<PictureBoxSizeMode, string> modeText = new Dictionary<PictureBoxSizeMode, string>
        {
            { PictureBoxSizeMode.Normal, modeNames[0] },
            { PictureBoxSizeMode.StretchImage, modeNames[1] },
            { PictureBoxSizeMode.CenterImage, modeNames[2] },
            { PictureBoxSizeMode.Zoom, modeNames[3] }
        };

        private void buttonOpenImg_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Area.ImageLocation = openFileDialog.FileName;
                }
                catch
                {
                    MessageBox.Show("Error during loading a picture", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                labelPath.Text = openFileDialog.FileName;;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Area.ImageLocation = null;
            labelPath.Text = "";
            Editor.EEngine.RetraceArea();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            Area.SizeMode = modeOption[comboBoxLayout.SelectedIndex];
            Editor.EEngine.RetraceArea();
        }

        private void SheetView_Load(object sender, EventArgs e)
        {
            checkBoxBI.Checked = config.VObjStyle.IconHasBorder;
            checkBoxBL.Checked = config.VObjStyle.LabelHasBorder;
            loaded = true;
        }

        private void checkBoxBI_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                config.VObjStyle.IconBorderEnable(checkBoxBI.Checked);

                foreach (Control control in Area.Controls)
                {
                    if (control.Name == Editor.VisualObject.Constructor.IconName)
                    {
                        (control as PictureBox).BorderStyle = config.VObjStyle.IconBorder;
                    }
                }
            }
        }

        private void checkBoxBL_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                config.VObjStyle.LabelBorderEnable(checkBoxBL.Checked);

                foreach (Control control in Area.Controls)
                {
                    if (control.Name == Editor.VisualObject.Constructor.LabelName)
                    {
                        (control as Label).BorderStyle = config.VObjStyle.LabelBorder;
                    }
                }
            }
        }
    }
}
