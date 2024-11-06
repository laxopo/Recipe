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
    public partial class FormCalculator : Form
    {
        private int scLarge, scSmall;

        public FormCalculator()
        {
            InitializeComponent();
            Calculator.CEngine.Initialize(ClearData, UpdateData, panelInputsContainer, panelOutputsContainer);
            scLarge = vScrollBarInputs.LargeChange;
            scSmall = vScrollBarInputs.SmallChange;
        }

        /*Main*/

        private void ClearData()
        {
            //clear records
            listBoxTrees.Items.Clear();
            panelInputsContainer.Controls.Clear();
            panelOutputsContainer.Controls.Clear();
            panelInputsContainer.Top = 0;
            panelOutputsContainer.Top = 0;
        }

        private void UpdateData()
        {
            //add trees to list
            Calculator.CEngine.Forest.ForEach(x => listBoxTrees.Items.Add(x.Name));
        }

        private void FormCalculator_MouseEnter(object sender, EventArgs e)
        {
            Calculator.CEngine.Update();
        }

        private void FormCalculator_Load(object sender, EventArgs e)
        {
            Calculator.CEngine.Update();
        }

        /*Controls*/

        private void listBoxTrees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTrees.SelectedIndex == -1)
            {
                return;
            }

            var tree = Calculator.CEngine.Forest[listBoxTrees.SelectedIndex];

            Calculator.CEngine.GenerateVOs(tree);
            //Calculator.CEngine.InitCalc(tree);
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (listBoxTrees.SelectedIndex == -1)
            {
                return;
            }

            var tree = Calculator.CEngine.Forest[listBoxTrees.SelectedIndex];

            Calculator.CEngine.Calculate(tree, radioButtonCalcOut.Checked);

        }

        private void radioButtonCalcIn_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonCalcOut.Checked = !radioButtonCalcIn.Checked;
        }

        private void radioButtonCalcOut_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonCalcIn.Checked = !radioButtonCalcOut.Checked;
        }

        /*Containers*/

        private void ContainerFit(VScrollBar scrollBar, Panel window, Panel container)
        {
            scrollBar.Minimum = window.Height;

            if (container.Height < window.Height)
            {
                scrollBar.Enabled = false;
                return;
            }

            scrollBar.Enabled = true;
            scrollBar.Maximum = container.Height + scLarge;

            scrollBar.Value = scrollBar.Minimum;
        }

        private void ContScroll(VScrollBar vScroll, int delta)
        {
            if (!vScroll.Enabled)
            {
                return;
            }

            if (delta != 0)
            {
                int val = vScroll.Value - (delta / 120) * scSmall;

                if (val < vScroll.Minimum)
                {
                    val = vScroll.Minimum;
                }

                if (val > vScroll.Maximum - scLarge)
                {
                    val = vScroll.Maximum - scLarge;
                }

                vScroll.Value = val;
            }
        }

        //Inputs
        private void panelInputsContainer_ControlAdded(object sender, ControlEventArgs e)
        {
            ContainerFit(vScrollBarInputs, panelInputsWindow, panelInputsContainer);
        }

        private void panelInputsContainer_ControlRemoved(object sender, ControlEventArgs e)
        {
            ContainerFit(vScrollBarInputs, panelInputsWindow, panelInputsContainer);
        }

        private void panelInputsContainer_MouseWheel(object sender, MouseEventArgs e)
        {
            ContScroll(vScrollBarInputs, e.Delta);
        }

        private void vScrollBarInputs_ValueChanged(object sender, EventArgs e)
        {
            panelInputsContainer.Top = vScrollBarInputs.Minimum - vScrollBarInputs.Value;
        }

        //Outputs
        private void panelOutputsContainer_ControlAdded(object sender, ControlEventArgs e)
        {
            ContainerFit(vScrollBarOutputs, panelOutputsWindow, panelOutputsContainer);
        }

        private void panelOutputsContainer_ControlRemoved(object sender, ControlEventArgs e)
        {
            ContainerFit(vScrollBarOutputs, panelOutputsWindow, panelOutputsContainer);
        }

        private void panelOutputsContainer_MouseWheel(object sender, MouseEventArgs e)
        {
            ContScroll(vScrollBarOutputs, e.Delta);
        }

        private void FormCalculator_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void vScrollBarOutputs_ValueChanged(object sender, EventArgs e)
        {
            panelOutputsContainer.Top = vScrollBarOutputs.Minimum - vScrollBarOutputs.Value;
        }
    }
}
