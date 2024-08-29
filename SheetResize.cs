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
    public partial class SheetResize : Form
    {
        public SheetResize(Size size)
        {
            InitializeComponent();
            buf = size;
            textBoxWidth.Text = size.Width.ToString();
            textBoxHeight.Text = size.Height.ToString();
        }

        public int dx;
        public int dy;
        public bool shift;


        private Size buf;

        private void buttonApply_Click(object sender, EventArgs e)
        {
            int w = 0, h = 0;
            string inv = "Invalid input";
            Size min = Editor.Editor.AreaSizeMin;
            Size max = Editor.Editor.AreaSizeMax;

            try
            {
                w = Convert.ToInt32(textBoxWidth.Text);
                h = Convert.ToInt32(textBoxHeight.Text);
            }
            catch
            {
                MessageBox.Show("Bad symbols in the text box(es).", inv, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (w < min.Width || w > max.Width || h < min.Height || h > max.Height)
            {
                MessageBox.Show("Value out of range. Size must be within \"" + min.Width + " x " + min.Height +
                    "\" and \"" + max.Width + " x " + max.Height + "\".", inv, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                dx = w - buf.Width;
                dy = h - buf.Height;
                DialogResult = DialogResult.OK;
            }
        }

        private void textBoxWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonApply_Click(sender, e);
            }
        }

        private void textBoxHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonApply_Click(sender, e);
            }
        }

        private void checkBoxShift_CheckedChanged(object sender, EventArgs e)
        {
            shift = checkBoxShift.Checked;
        }
    }
}
