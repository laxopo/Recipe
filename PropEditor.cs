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

        public void Unlink()
        {
            buttonLinksSelectAll_Click(null, null);
            buttonDeleteLinks_Click(null, null);
        }

        /**/

        private void EnableLSIC_Handlers(bool enable)
        {
            if (enable)
            {
                listBoxLinksInput.SelectedIndexChanged += new EventHandler(listBoxLinksInput_SelectedIndexChanged);
                listBoxLinksOutput.SelectedIndexChanged += new EventHandler(listBoxLinksOutput_SelectedIndexChanged);
            }
            else
            {
                listBoxLinksInput.SelectedIndexChanged -= new EventHandler(listBoxLinksInput_SelectedIndexChanged);
                listBoxLinksOutput.SelectedIndexChanged -= new EventHandler(listBoxLinksOutput_SelectedIndexChanged);
            }
        }

        private void ListSelectAll(ListBox list, bool selection)
        {
            for (int index = 0; index < list.Items.Count; index++)
            {
                list.SetSelected(index, selection);
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

        private void listBoxLinksInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            //HL selected links
            CurrentIObj.LinkInHLs.Clear();
            foreach (int index in listBoxLinksInput.SelectedIndices)
            {
                Editor.ItemObject link = CurrentIObj.LinkInTags[index];
                CurrentIObj.LinkInHLs.Add(link);
            }
            Editor.Editor.RetraceArea();
        }

        private void listBoxLinksInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                e.SuppressKeyPress = true;
                EnableLSIC_Handlers(false);
                if (e.KeyCode == Keys.A)
                {
                    ListSelectAll(listBoxLinksInput, true);
                    listBoxLinksInput_SelectedIndexChanged(null, null);
                }
                else if (e.KeyCode == Keys.D)
                {
                    ListSelectAll(listBoxLinksInput, false);
                    listBoxLinksInput_SelectedIndexChanged(null, null);
                }
                EnableLSIC_Handlers(true);
            }
        }

        private void listBoxLinksOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentIObj.LinkOutHLs.Clear();

            foreach (int index in listBoxLinksOutput.SelectedIndices)
            {
                Editor.ItemObject link = CurrentIObj.LinkOutTags[index];
                CurrentIObj.LinkOutHLs.Add(link);
            }
            Editor.Editor.RetraceArea();
        }

        private void listBoxLinksOutput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                e.SuppressKeyPress = true;
                EnableLSIC_Handlers(false);
                if (e.KeyCode == Keys.A)
                {
                    ListSelectAll(listBoxLinksOutput, true);
                    listBoxLinksOutput_SelectedIndexChanged(null, null);
                }
                else if (e.KeyCode == Keys.D)
                {
                    ListSelectAll(listBoxLinksOutput, false);
                    listBoxLinksOutput_SelectedIndexChanged(null, null);
                }
                EnableLSIC_Handlers(true);
            }
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

        private void buttonDeleteLinks_Click(object sender, EventArgs e)
        {
            EnableLSIC_Handlers(false);

            //begin
            var linksToDelete = new List<Editor.ItemObject>();
            var listToDelete = new List<object>();
            foreach (int index in listBoxLinksInput.SelectedIndices)
            {
                linksToDelete.Add(CurrentIObj.LinkInTags[index]);
                listToDelete.Add(listBoxLinksInput.Items[index]);
            }

            foreach (var beg in linksToDelete)
            {
                beg.LinksOut.Remove(CurrentIObj.ID);
                beg.LinkOutTags.Remove(CurrentIObj);
                CurrentIObj.LinkInTags.Remove(beg);
                CurrentIObj.LinksIn.Remove(beg.ID);

                Editor.Editor.Changed = true;
            }

            foreach (var item in listToDelete)
            {
                listBoxLinksInput.Items.Remove(item);
            }
            CurrentIObj.LinkInHLs.Clear();
            

            //end
            linksToDelete = new List<Editor.ItemObject>();
            listToDelete = new List<object>();
            foreach (int index in listBoxLinksOutput.SelectedIndices)
            {
                linksToDelete.Add(CurrentIObj.LinkOutTags[index]);
                listToDelete.Add(listBoxLinksOutput.Items[index]);
            }

            foreach (var end in linksToDelete)
            {
                end.LinkInTags.Remove(CurrentIObj);
                end.LinksIn.Remove(CurrentIObj.ID);
                CurrentIObj.LinkOutTags.Remove(end);
                CurrentIObj.LinksOut.Remove(end.ID);

                Editor.Editor.Changed = true;
            }

            foreach (var item in listToDelete)
            {
                listBoxLinksOutput.Items.Remove(item);
            }
            CurrentIObj.LinkOutHLs.Clear();

            EnableLSIC_Handlers(true);
            Editor.Editor.RetraceArea();
        }

        private void buttonLinksSelectAll_Click(object sender, EventArgs e)
        {
            EnableLSIC_Handlers(false);
            ListSelectAll(listBoxLinksInput, true);
            ListSelectAll(listBoxLinksOutput, true);
            EnableLSIC_Handlers(true);
            listBoxLinksInput_SelectedIndexChanged(null, null);
            listBoxLinksOutput_SelectedIndexChanged(null, null);
        }

        private void buttonLinksDeselectAll_Click(object sender, EventArgs e)
        {
            EnableLSIC_Handlers(false);
            ListSelectAll(listBoxLinksInput, false);
            ListSelectAll(listBoxLinksOutput, false);
            EnableLSIC_Handlers(true);
            listBoxLinksInput_SelectedIndexChanged(null, null);
            listBoxLinksOutput_SelectedIndexChanged(null, null);
        }
    }
}
