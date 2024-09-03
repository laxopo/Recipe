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
    public partial class PropEditor : Form
    {
        private Editor.ItemObject CurrentIObj;
        private Config config;
        private bool loaded = false;
        private FormOpacity opacity;

        public PropEditor(Config config)
        {
            InitializeComponent();
            this.config = config;
            opacity = new FormOpacity(this, checkBoxOpa);
        }

        /**/

        public void LoadItem()
        {
            bool en = Editor.Editor.CurrentVObj != null;

            //Enabling
            foreach (Control ctrl in Controls)
            {
                if (ctrl == checkBoxOpa)
                {
                    continue;
                }

                ctrl.Enabled = en;
            }

            if (!en)
            {
                //Clear the controls
                if (CurrentIObj != null)
                {
                    textBoxName.Text = "";
                    listBoxLinksInput.SelectedItem = null;
                    listBoxLinksOutput.SelectedItem = null;
                    listBoxLinksInput.Items.Clear();
                    listBoxLinksOutput.Items.Clear();
                    CurrentIObj = null;
                }

                return;
            }

            var loadItem = Editor.Editor.CurrentVObj.Tag as Editor.ItemObject;

            if (loadItem == CurrentIObj)
            {
                return;
            }

            listBoxLinksInput.SelectedItem = null;
            listBoxLinksOutput.SelectedItem = null;
            CurrentIObj = loadItem;

            textBoxName.Text = CurrentIObj.Item.Name;

            comboBoxTypes.Text = Enum.GetName(typeof(Library.Item.Type), CurrentIObj.Item.ItemType);

            listBoxLinksInput.Items.Clear();
            foreach (var link in CurrentIObj.LinkInTags)
            {
                listBoxLinksInput.Items.Add(link.Item.Name);
            }

            listBoxLinksOutput.Items.Clear();
            foreach (var link in CurrentIObj.LinkOutTags)
            {
                listBoxLinksOutput.Items.Add(link.Item.Name);
            }
        }

        /**/

        private void ItemProperties_Load(object sender, EventArgs e)
        {
            if (config.Loaded)
            {
                Location = config.ToolPosition.PropEditor;
            }

            LoadItem();

            loaded = true;
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CurrentIObj.Item.Name = textBoxName.Text;

                Editor.VisualObject.VisualObject.VOTextUpdate(CurrentIObj);
                Editor.Editor.SelectVO(CurrentIObj, true);
            }
        }

        private void comboBoxTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentIObj.Item.ItemType = (Library.Item.Type)comboBoxTypes.SelectedIndex;

            Editor.VisualObject.VisualObject.ItemTypeStyleUpdate(CurrentIObj);
            Editor.Editor.SelectVO(CurrentIObj, true);
        }

        int indexIn = -1, indexOut = -1;
        private void listBoxLinksInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBoxLinksInput.SelectedIndex;
            if (indexIn == index)
            {
                indexIn = -1;
                index = -1;
                listBoxLinksInput.SelectedItem = null;
            }

            indexIn = index;

            CurrentIObj.LinkInHLs.Clear();
            if (index != -1)
            {
                Editor.ItemObject link = CurrentIObj.LinkInTags[indexIn];
                CurrentIObj.LinkInHLs.Add(link);
            }
            Editor.Editor.RetraceArea();
        }

        private void listBoxLinksOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBoxLinksOutput.SelectedIndex;
            if (indexOut == index)
            {
                indexOut = -1;
                index = -1;
                listBoxLinksOutput.SelectedItem = null;
            }

            indexOut = index;

            CurrentIObj.LinkOutHLs.Clear();
            if (indexOut != -1)
            {
                Editor.ItemObject link = CurrentIObj.LinkOutTags[index];
                CurrentIObj.LinkOutHLs.Add(link);
            }
            Editor.Editor.RetraceArea();
        }

        private void listBoxLinksInput_MouseClick(object sender, MouseEventArgs e)
        {
            listBoxLinksOutput.SelectedItem = null;
        }

        private void listBoxLinksOutput_MouseClick(object sender, MouseEventArgs e)
        {
            listBoxLinksInput.SelectedItem = null;
        }

        private void PropEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void PropEditor_Move(object sender, EventArgs e)
        {
            if (loaded)
            {
                config.ToolPosition.PropEditor = Location;
            }    
        }

        private void buttonDeleteLink_Click(object sender, EventArgs e)
        {
            int index = listBoxLinksInput.SelectedIndex;
            if (index != -1)
            {
                CurrentIObj.LinkInHLs.Clear();
                Editor.ItemObject beg = CurrentIObj.LinkInTags[index];
                beg.LinkOutTags.Remove(CurrentIObj);
                CurrentIObj.LinkInTags.RemoveAt(index);
                listBoxLinksInput.Items.RemoveAt(index);

                Editor.Editor.Changed = true;
            }

            index = listBoxLinksOutput.SelectedIndex;
            if (index != -1)
            {
                CurrentIObj.LinkOutHLs.Clear();
                Editor.ItemObject end = CurrentIObj.LinkOutTags[index];
                end.LinkInTags.Remove(CurrentIObj);
                CurrentIObj.LinkOutTags.RemoveAt(index);
                listBoxLinksOutput.Items.RemoveAt(index);

                Editor.Editor.Changed = true;
            }

            Editor.Editor.RetraceArea();
        }
    }
}
