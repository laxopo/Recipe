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
    public partial class FormRename : Form
    {
        public string ItemName;

        public FormRename(string itemName)
        {
            InitializeComponent();

            textBoxName.Text = itemName;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            ItemName = textBoxName.Text;
        }

        private void buttonApply_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
