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
        public FormCalculator()
        {
            InitializeComponent();
            Calculator.CEngine.Initialize(ClearData, UpdateData);
        }

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

        private void listBoxTrees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTrees.SelectedIndex == -1)
            {
                return;
            }

            var tree = Calculator.CEngine.Forest[listBoxTrees.SelectedIndex];

            Calculator.CEngine.GenerateVOs(tree, panelInputsContainer, panelOutputsContainer);

            ContainerFit(groupBoxInput);
            ContainerFit(groupBoxOutput);
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (listBoxTrees.SelectedIndex == -1)
            {
                return;
            }

            var tree = Calculator.CEngine.Forest[listBoxTrees.SelectedIndex];

            Calculator.CEngine.Calculate(tree);
        }

        private void ContainerFit(Control groupBox)
        {
            VScrollBar scrollBar = null;
            Panel container = null;
            Panel window = null;

            foreach (Control ctrl in groupBox.Controls)
            {
                if (ctrl.Name.Contains("Window"))
                {
                    window = ctrl as Panel;

                    foreach (Control ctr in window.Controls)
                    {
                        if(ctr.Name.Contains("Container"))
                        {
                            container = ctr as Panel;
                        }
                    }
                }
                else if (ctrl.Name.Contains("vScroll"))
                {
                    scrollBar = ctrl as VScrollBar;
                }
            }

            int lc = scrollBar.LargeChange;
            scrollBar.Minimum = window.Height;

            if (container.Height < window.Height)
            {
                vScrollBarInputs.Enabled = false;
                return;
            }

            scrollBar.Enabled = true;
            scrollBar.Maximum = container.Height + lc;
        }

        private void vScrollBarInputs_Scroll(object sender, ScrollEventArgs e)
        {
            panelInputsContainer.Top = vScrollBarInputs.Minimum - vScrollBarInputs.Value;
        }

        private void vScrollBarOutputs_Scroll(object sender, ScrollEventArgs e)
        {
            panelOutputsContainer.Top = vScrollBarOutputs.Minimum - vScrollBarOutputs.Value;
        }
    }
}
