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

        public void LoadItem(bool reload)
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
                    TextBoxQtyUpdate(false);
                    listBoxLinksInput.SelectedItem = null;
                    listBoxLinksOutput.SelectedItem = null;
                    listBoxLinksInput.Items.Clear();
                    listBoxLinksOutput.Items.Clear();
                    comboBoxTypes_SetValue((Library.Item.Type)(-1));
                    CurrentIObj = null;
                }

                return;
            }

            var loadItem = Editor.Editor.CurrentVObj.Tag as Editor.ItemObject;

            if (!reload && loadItem == CurrentIObj)
            {
                return;
            }

            listBoxLinksInput.SelectedItem = null;
            listBoxLinksOutput.SelectedItem = null;
            CurrentIObj = loadItem;

            textBoxName.Text = CurrentIObj.Item.Name;
            TextBoxQtyUpdate(true);

            comboBoxTypes_SetValue(CurrentIObj.Item.ItemType);

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
            bool confirm = false;
            if (Editor.Editor.selectedIObjs.Count > 0)
            {
                foreach (var iobj in Editor.Editor.selectedIObjs)
                {
                    if (!confirm && (iobj.LinkInTags.Count > 0 || iobj.LinkOutTags.Count > 0))
                    {
                        string ending;
                        if (Editor.Editor.selectedIObjs.Count == 1)
                        {
                            ending = "this item?";
                        }
                        else
                        {
                            ending = "these items?";
                        }

                        if (MessageBox.Show("Remove all links related with " + ending, "Unlink",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                        {
                            return;
                        }

                        confirm = true;
                    }

                    DeleteLinks(iobj, true, Editor.ItemObject.LinkType.Both);
                }

                Editor.Editor.RetraceArea();
                LoadItem(true);
            }
        }

        private void TypingFilterInteger(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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

        private void EnableTBTC_Hanlders(bool enable)
        {
            if (enable)
            {
                textBoxQtyIn.TextChanged += new EventHandler(textBoxQtyIn_TextChanged);
                textBoxQtyOut.TextChanged += new EventHandler(textBoxQtyOut_TextChanged);
            }
            else
            {
                textBoxQtyIn.TextChanged -= new EventHandler(textBoxQtyIn_TextChanged);
                textBoxQtyOut.TextChanged -= new EventHandler(textBoxQtyOut_TextChanged);
            }
        }

        private void DeleteLinks(Editor.ItemObject iobj, bool all, Editor.ItemObject.LinkType lists)
        {
            var linksToDelete = new List<Editor.ItemObject>();
            var listToDelete = new List<object>();

            //begin
            if (lists == Editor.ItemObject.LinkType.Input || lists == Editor.ItemObject.LinkType.Both)
            {
                if (all)
                {
                    linksToDelete.AddRange(iobj.LinkInTags);
                }
                else
                {
                    foreach (int index in listBoxLinksInput.SelectedIndices)
                    {
                        linksToDelete.Add(iobj.LinkInTags[index]);
                        listToDelete.Add(listBoxLinksInput.Items[index]);
                    }
                }

                foreach (var beg in linksToDelete)
                {
                    beg.LinksOut.Remove(iobj.ID);
                    beg.LinkOutTags.Remove(iobj);
                    iobj.LinkInTags.Remove(beg);
                    iobj.LinksIn.Remove(beg.ID);

                    Editor.Editor.Changed = true;
                }

                foreach (var item in listToDelete)
                {
                    listBoxLinksInput.Items.Remove(item);
                }
                iobj.LinkInHLs.Clear();
            }
            
            //end
            if (lists == Editor.ItemObject.LinkType.Output || lists == Editor.ItemObject.LinkType.Both)
            {
                linksToDelete = new List<Editor.ItemObject>();
                listToDelete = new List<object>();

                if (all)
                {
                    linksToDelete.AddRange(iobj.LinkOutTags);
                }
                else
                {
                    foreach (int index in listBoxLinksOutput.SelectedIndices)
                    {
                        linksToDelete.Add(iobj.LinkOutTags[index]);
                        listToDelete.Add(listBoxLinksOutput.Items[index]);
                    }
                }

                foreach (var end in linksToDelete)
                {
                    end.LinkInTags.Remove(iobj);
                    end.LinksIn.Remove(iobj.ID);
                    iobj.LinkOutTags.Remove(end);
                    iobj.LinksOut.Remove(end.ID);

                    Editor.Editor.Changed = true;
                }

                foreach (var item in listToDelete)
                {
                    listBoxLinksOutput.Items.Remove(item);
                }
                iobj.LinkOutHLs.Clear();
            }
            
        }

        /**/

        private void ListSelectAll(ListBox list, bool selection)
        {
            for (int index = 0; index < list.Items.Count; index++)
            {
                list.SetSelected(index, selection);
            }
        }

        /**/

        private void PropEditor_Load(object sender, EventArgs e)
        {
            if (config.Loaded)
            {
                Location = config.ToolPosition.PropEditor;
            }

            LoadItem(false);

            loaded = true;
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxName_Leave(null, null);
            }
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            CurrentIObj.Item.Name = textBoxName.Text;

            Editor.VisualObject.VisualObject.VOTextUpdate(CurrentIObj);
            Editor.Editor.SelectVO(CurrentIObj, false);
        }

        private void comboBoxTypes_SetValue(Library.Item.Type type)
        {
            comboBoxTypes.SelectedIndexChanged -= new EventHandler(comboBoxTypes_SelectedIndexChanged);
            comboBoxTypes.Text = Enum.GetName(typeof(Library.Item.Type), type);
            comboBoxTypes.SelectedIndexChanged += new EventHandler(comboBoxTypes_SelectedIndexChanged);
        }

        private void comboBoxTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = (Library.Item.Type)comboBoxTypes.SelectedIndex;
            if (type == CurrentIObj.Item.ItemType)
            {
                return;
            }

            if (type == Library.Item.Type.Fluid)
            {
                CurrentIObj.QuantityIn *= 1000;
                CurrentIObj.QuantityOut *= 1000;
            }
            else if (CurrentIObj.Item.ItemType == Library.Item.Type.Fluid)
            {
                CurrentIObj.QuantityIn /= 1000;
                if (CurrentIObj.QuantityIn < 1)
                {
                    CurrentIObj.QuantityIn = 1;
                }
                
                CurrentIObj.QuantityOut /= 1000;
                if (CurrentIObj.QuantityOut < 1)
                {
                    CurrentIObj.QuantityOut = 1;
                }
            }

            CurrentIObj.Item.ItemType = type;

            TextBoxQtyUpdate(true);

            Editor.VisualObject.VisualObject.ItemTypeStyleUpdate(CurrentIObj);
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
            else if (e.KeyCode == Keys.Delete)
            {
                EnableLSIC_Handlers(false);
                DeleteLinks(CurrentIObj, false, Editor.ItemObject.LinkType.Input);
                listBoxLinksInput_SelectedIndexChanged(null, null);
                EnableLSIC_Handlers(true);
                Editor.Editor.RetraceArea();
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
            else if (e.KeyCode == Keys.Delete)
            {
                EnableLSIC_Handlers(false);
                DeleteLinks(CurrentIObj, false, Editor.ItemObject.LinkType.Output);
                listBoxLinksInput_SelectedIndexChanged(null, null);
                EnableLSIC_Handlers(true);
                Editor.Editor.RetraceArea();
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
            DeleteLinks(CurrentIObj, false, Editor.ItemObject.LinkType.Both);
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

        private void textBoxQtyIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            TypingFilterInteger(e);
        }

        private void textBoxQtyIn_TextChanged(object sender, EventArgs e)
        {
            if (textBoxQtyIn.Text.Length == 0)
            {
                return;
            }

            CurrentIObj.QuantityIn = Convert.ToInt32(textBoxQtyIn.Text);
        }

        private void textBoxQtyOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            TypingFilterInteger(e);
        }

        private void textBoxQtyOut_TextChanged(object sender, EventArgs e)
        {
            if (textBoxQtyOut.Text.Length == 0)
            {
                return;
            }

            CurrentIObj.QuantityOut = Convert.ToInt32(textBoxQtyOut.Text);
        }

        private void TextBoxQtyUpdate(bool enable)
        {
            EnableTBTC_Hanlders(false);
            if (CurrentIObj.Item.ItemType == Library.Item.Type.Mechanism || !enable)
            {
                textBoxQtyIn.Text = "";
                textBoxQtyIn.Enabled = false;
                textBoxQtyOut.Text = "";
                textBoxQtyOut.Enabled = false;
            }
            else
            {
                textBoxQtyIn.Text = CurrentIObj.QuantityIn.ToString();
                textBoxQtyIn.Enabled = true;
                textBoxQtyOut.Text = CurrentIObj.QuantityOut.ToString();
                textBoxQtyOut.Enabled = true;
            }
            EnableTBTC_Hanlders(true);
        }

        private void textBoxQtyIn_EnabledChanged(object sender, EventArgs e)
        {

        }
    }
}
