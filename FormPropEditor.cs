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
    public partial class FormPropEditor : Form
    {
        private Editor.ItemObject CurrentIObj;
        private Config config;
        private bool loaded = false;
        private readonly OpacityForm opacity;
        private string gBoxQtyText;

        private List<Control> enableManual = new List<Control>();

        public FormPropEditor(Config config)
        {
            InitializeComponent();
            this.config = config;
            opacity = new OpacityForm(this, checkBoxOpa);
            gBoxQtyText = groupBoxQuantity.Text;

            int bx = Width - ClientRectangle.Width;
            int by = Height - ClientRectangle.Height;
            Width = groupBoxIO.Right + groupBoxQuantity.Left + bx;
            Height = groupBoxLinks.Bottom + groupBoxQuantity.Left + by;
            groupBoxMechProps.Location = groupBoxQuantity.Location;

            radioButtonAuto.Tag = Editor.ItemObject.Type.Auto;
            radioButtonInput.Tag = Editor.ItemObject.Type.Input;
            radioButtonOutput.Tag = Editor.ItemObject.Type.Output;

            enableManual = new List<Control>() {
                checkBoxOpa,
                groupBoxQuantity,
                groupBoxIO
            };
        }

        /*Common*/

        public void LoadItem(bool reload)
        {
            bool en = Editor.Engine.CurrentVObj != null;

            //Enabling
            foreach (Control ctrl in Controls)
            {
                if (enableManual.Contains(ctrl))
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
                    labelID.Text = "";
                    checkBoxPublic.Checked = false;
                    GBoxQuantityUpdate(false);
                    GBoxExternalUpdate(false);
                    listBoxLinksInput.SelectedItem = null;
                    listBoxLinksOutput.SelectedItem = null;
                    listBoxLinksInput.Items.Clear();
                    listBoxLinksOutput.Items.Clear();
                    comboBoxTypes_SetValue((Library.Item.ItemType)(-1));
                    CurrentIObj = null;
                }

                return;
            }

            var loadItem = Editor.Engine.CurrentVObj.Tag as Editor.ItemObject;

            if (!reload && loadItem == CurrentIObj)
            {
                return;
            }

            listBoxLinksInput.SelectedItem = null;
            listBoxLinksOutput.SelectedItem = null;
            CurrentIObj = loadItem;

            labelID.Text = CurrentIObj.ID.ToString();
            textBoxName.Text = CurrentIObj.Item.Name;
            GBoxQuantityUpdate(true);
            GBoxExternalUpdate(true);

            comboBoxTypes_SetValue(CurrentIObj.Item.Type);

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
            if (Editor.Engine.selectedIObjs.Count > 0)
            {
                foreach (var iobj in Editor.Engine.selectedIObjs)
                {
                    if (!confirm && (iobj.LinkInTags.Count > 0 || iobj.LinkOutTags.Count > 0))
                    {
                        string ending;
                        if (Editor.Engine.selectedIObjs.Count == 1)
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

                Editor.Engine.RetraceArea();
                LoadItem(true);
            }
        }

        private void TypingFilterInteger(KeyPressEventArgs e, object textBox = null)
        {
            bool pass;

            if (e.KeyChar == '-' && textBox != null)
            {
                var tb = textBox as TextBox;

                if (tb.Text.Length > 0)
                {
                    pass = !tb.Text.Contains('-') && tb.SelectionStart == 0;
                }
                else
                {
                    pass = true;
                }
            }
            else
            {
                pass = char.IsDigit(e.KeyChar);
            }

            if (!char.IsControl(e.KeyChar) && !pass)
            {
                e.Handled = true;
            }
        }

        /*Form*/

        private void PropEditor_Load(object sender, EventArgs e)
        {
            if (config.Loaded)
            {
                Location = config.ToolPosition.PropEditor;
            }

            LoadItem(false);

            loaded = true;
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

        /*Name & Type*/

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxName_Leave(null, null);
            }
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            if (CurrentIObj == null || CurrentIObj.Item.Name == textBoxName.Text)
            {
                return;
            }

            CurrentIObj.Item.Name = textBoxName.Text;

            Editor.VisualObject.Constructor.VOTextUpdate(CurrentIObj);
            Editor.Engine.SelectVO(CurrentIObj, false);
        }

        private void comboBoxTypes_SetValue(Library.Item.ItemType type)
        {
            comboBoxTypes.SelectedIndexChanged -= new EventHandler(comboBoxTypes_SelectedIndexChanged);
            comboBoxTypes.Text = Enum.GetName(typeof(Library.Item.ItemType), type);
            comboBoxTypes.SelectedIndexChanged += new EventHandler(comboBoxTypes_SelectedIndexChanged);

            if (type == Library.Item.ItemType.Fluid)
            {
                CurrentIObj.QuantityIn *= 1000;
                CurrentIObj.QuantityOut *= 1000;
            }
            else if (CurrentIObj.Item.Type == Library.Item.ItemType.Fluid)
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

            if (type == Library.Item.ItemType.Mechanism)
            {
                groupBoxQuantity.Visible = false;
                groupBoxIO.Visible = false;
                groupBoxMechProps.Visible = true;
                CurrentIObj.Public = false;
                checkBoxPublic.Checked = false;

                textBoxEnergy.Text = CurrentIObj.Item.Energy.ToString();
                textBoxTime.Text = CurrentIObj.Item.Time.ToString();
                comboBoxEU.Text = CurrentIObj.Item.EU;
            }
            else
            {
                groupBoxMechProps.Visible = false;
                groupBoxQuantity.Visible = true;
                groupBoxIO.Visible = true;

                GBoxQuantityUpdate(true);
                GBoxExternalUpdate(true);
            }
        }

        private void comboBoxTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = (Library.Item.ItemType)comboBoxTypes.SelectedIndex;
            if (type == CurrentIObj.Item.Type)
            {
                return;
            }

            CurrentIObj.Item.Type = type;

            Editor.VisualObject.Constructor.ItemTypeStyleUpdate(CurrentIObj);
        }

        /*Links*/

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

                    Editor.Engine.Changed = true;
                    Calculator.CEngine.IsActual = false;
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

                    Editor.Engine.Changed = true;
                    Calculator.CEngine.IsActual = false;
                }

                foreach (var item in listToDelete)
                {
                    listBoxLinksOutput.Items.Remove(item);
                }
                iobj.LinkOutHLs.Clear();
            }

            GBoxQuantityUpdate(true);
            GBoxExternalUpdate(true);
        }

        private void ListSelectAll(ListBox list, bool selection)
        {
            for (int index = 0; index < list.Items.Count; index++)
            {
                list.SetSelected(index, selection);
            }
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
            Editor.Engine.RetraceArea();
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
                Editor.Engine.RetraceArea();
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
            Editor.Engine.RetraceArea();
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
                Editor.Engine.RetraceArea();
            }
        }

        private void buttonDeleteLinks_Click(object sender, EventArgs e)
        {
            EnableLSIC_Handlers(false);
            DeleteLinks(CurrentIObj, false, Editor.ItemObject.LinkType.Both);
            EnableLSIC_Handlers(true);
            Editor.Engine.RetraceArea();
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

        /*Quantity*/
        
        private int IObjQtyUpdate(TextBox source, int property)
        {
            int src = Convert.ToInt32(source.Text);

            if (property != src)
            {
                Editor.Engine.Changed = true;
                Calculator.CEngine.IsActual = false;
            }

            return src;
        }

        private void EnableTBTC_Hanlders(bool enable)
        {
            if (enable)
            {
                textBoxQtyIn.TextChanged += new EventHandler(textBoxQtyIn_TextChanged);
                textBoxQtyOut.TextChanged += new EventHandler(textBoxQtyOut_TextChanged);
                textBoxInjected.TextChanged += new EventHandler(textBoxInjected_TextChanged);
            }
            else
            {
                textBoxQtyIn.TextChanged -= new EventHandler(textBoxQtyIn_TextChanged);
                textBoxQtyOut.TextChanged -= new EventHandler(textBoxQtyOut_TextChanged);
                textBoxInjected.TextChanged -= new EventHandler(textBoxInjected_TextChanged);
            }
        }

        private void TextBoxQtyUpdate(bool enable)
        {
            EnableTBTC_Hanlders(false);
            if (enable)
            {
                if (CurrentIObj.LinkInTags.Count > 0)
                {
                    textBoxQtyIn.Enabled = true;
                    textBoxQtyIn.Text = CurrentIObj.QuantityIn.ToString();
                }
                else
                {
                    textBoxQtyIn.Enabled = false;
                    textBoxQtyIn.Text = "";
                }

                if(CurrentIObj.LinkOutTags.Count > 0)
                {
                    textBoxQtyOut.Enabled = true;
                    textBoxInjected.Enabled = true;
                    textBoxQtyOut.Text = CurrentIObj.QuantityOut.ToString();
                }
                else
                {
                    textBoxQtyOut.Enabled = false;
                    textBoxInjected.Enabled = false;
                    textBoxQtyOut.Text = "";
                    textBoxInjected.Text = "";
                }
            }
            else
            {
                textBoxQtyIn.Text = "";
                textBoxQtyOut.Text = "";
                textBoxInjected.Text = "";
            }
            EnableTBTC_Hanlders(true);
        }

        private void GBoxQuantityUpdate(bool enable)
        {
            groupBoxQuantity.Text = gBoxQtyText;
            if (enable)
            {
                var units = CurrentIObj.Item.GetUnits();
                if (units != "")
                {
                    groupBoxQuantity.Text += " [" + units + "]";
                }
            }

            bool en = enable && CurrentIObj.Item.Type != Library.Item.ItemType.Mechanism;
            groupBoxQuantity.Enabled = en;
            TextBoxQtyUpdate(en);
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

            CurrentIObj.QuantityIn = IObjQtyUpdate(textBoxQtyIn, CurrentIObj.QuantityIn);
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

            CurrentIObj.QuantityOut = IObjQtyUpdate(textBoxQtyOut, (int)CurrentIObj.QuantityOut);
        }

        private void textBoxInjected_KeyPress(object sender, KeyPressEventArgs e)
        {
            TypingFilterInteger(e);
        }

        private void textBoxInjected_TextChanged(object sender, EventArgs e)
        {
            if (textBoxInjected.Text.Length == 0)
            {
                return;
            }
        }

        /*I/O*/

        private void GBoxExternalUpdate(bool enable)
        {
            if (CurrentIObj.Item.Type != Library.Item.ItemType.Mechanism && enable)
            {
                groupBoxIO.Enabled = enable;
                radioButtonAuto.Enabled = enable;

                switch (CurrentIObj.IOType)
                {
                    case Editor.ItemObject.Type.Auto:
                        radioButtonAuto.Checked = true;
                        break;

                    case Editor.ItemObject.Type.Input:
                        radioButtonInput.Checked = true;
                        break;

                    case Editor.ItemObject.Type.Output:
                        radioButtonOutput.Checked = true;
                        break;
                }

                if (enable)
                {
                    radioButtonInput.Enabled = CurrentIObj.LinkOutTags.Count > 0;
                    radioButtonOutput.Enabled = CurrentIObj.LinkInTags.Count > 0;
                }
                else
                {
                    radioButtonInput.Enabled = false;
                    radioButtonOutput.Enabled = false;
                }
            }
            else
            {
                groupBoxIO.Enabled = false;
                foreach (var ct in groupBoxIO.Controls)
                {
                    if (!(ct is RadioButton))
                    {
                        continue;
                    }

                    var ctrl = ct as RadioButton;

                    ctrl.Enabled = false;
                    ctrl.Checked = false;
                }
            }
        }

        private void RBEnabledHandler(object sender)
        {
            var rb = sender as RadioButton;

            if (!rb.Enabled && rb.Checked)
            {
                rb.Checked = false;
                radioButtonAuto.CheckedChanged -= new EventHandler(radioButtonAuto_CheckedChanged);
                radioButtonAuto.Checked = true;
                radioButtonAuto.CheckedChanged += new EventHandler(radioButtonAuto_CheckedChanged);
            }
        }

        private void RBCheckedUpdate(object sender)
        {
            var activeRB = sender as RadioButton;

            if (activeRB.Checked)
            {
                foreach (var ctrl in groupBoxIO.Controls)
                {
                    if (!(ctrl is RadioButton) || ctrl == sender as RadioButton)
                    {
                        continue;
                    }

                    (ctrl as RadioButton).Checked = false;
                }

                IObjExtUpdate();
            }
        }

        private void IObjExtUpdate()
        {
            foreach (var ct in groupBoxIO.Controls)
            {
                if (!(ct is RadioButton))
                {
                    continue;
                }

                var ctrl = ct as RadioButton;

                if (ctrl.Checked)
                {
                    var ext = (Editor.ItemObject.Type)ctrl.Tag;
                    if (CurrentIObj.IOType != ext)
                    {
                        CurrentIObj.IOType = ext;
                        Editor.Engine.Changed = true;
                        Calculator.CEngine.IsActual = false;
                        return;
                    }
                }
            }
        }

        private void radioButtonAuto_CheckedChanged(object sender, EventArgs e)
        {
            RBCheckedUpdate(sender);
        }

        private void radioButtonInput_CheckedChanged(object sender, EventArgs e)
        {
            RBCheckedUpdate(sender);
        }

        private void radioButtonOutput_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonOutput.Checked)
            {
                checkBoxPublic.Enabled = true;
                checkBoxPublic.Checked = CurrentIObj.Public;
            }
            else
            {
                checkBoxPublic.Enabled = false;
                checkBoxPublic.Checked = false;
                CurrentIObj.Public = false;
            }

            RBCheckedUpdate(sender);
        }

        private void radioButtonOutput_EnabledChanged(object sender, EventArgs e)
        {
            RBEnabledHandler(sender);
        }

        private void radioButtonInput_EnabledChanged(object sender, EventArgs e)
        {
            RBEnabledHandler(sender);
        }

        private void checkBoxPublic_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxPublic.Focused)
            {
                return;
            }

            CurrentIObj.Public = checkBoxPublic.Checked;
            Editor.Engine.Changed = true;
            Calculator.CEngine.IsActual = false;
        }

        /*Mech*/

        private void textBoxEnergy_KeyPress(object sender, KeyPressEventArgs e)
        {
            TypingFilterInteger(e, sender);
        }

        private void textBoxTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            TypingFilterInteger(e);
        }

        private void textBoxEnergy_TextChanged(object sender, EventArgs e)
        {
            if (!textBoxEnergy.Focused || textBoxEnergy.Text.Length == 0 || textBoxEnergy.Text == "-")
            {
                return;
            }

            CurrentIObj.Item.Energy = Convert.ToInt32(textBoxEnergy.Text);
            Editor.Engine.Changed = true;
            Calculator.CEngine.IsActual = false;
        }

        private void textBoxTime_TextChanged(object sender, EventArgs e)
        {
            if (!textBoxTime.Focused || textBoxTime.Text.Length == 0)
            {
                return;
            }

            CurrentIObj.Item.Time = Convert.ToInt32(textBoxTime.Text);
            Editor.Engine.Changed = true;
            Calculator.CEngine.IsActual = false;
        }

        private void comboBoxEU_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxEU.Focused)
            {
                return;
            }

            if (comboBoxEU.SelectedIndex == -1)
            {
                CurrentIObj.Item.EU = null;
                return;
            }

            CurrentIObj.Item.EU = comboBoxEU.Text;
            Editor.Engine.Changed = true;
            Calculator.CEngine.IsActual = false;
        }
    }
}
