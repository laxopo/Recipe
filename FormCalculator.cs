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
        }

        private List<Calculator.Tree> trees = new List<Calculator.Tree>();
        private List<Calculator.VisualObject> visualObjects = new List<Calculator.VisualObject>();


        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            //clear records
            listBoxTrees.Items.Clear();
            trees.Clear();

            //scan for trees
            Calculator.Calculator.ScanTrees(trees);

            //add trees to list
            trees.ForEach(x => listBoxTrees.Items.Add(x.Name));
        }

        private void listBoxTrees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTrees.SelectedIndex == -1)
            {
                return;
            }

            var tree = trees[listBoxTrees.SelectedIndex];

            visualObjects = Calculator.Calculator.GenerateVOs(tree, groupBoxInput, groupBoxOutput);

        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            var tree = trees[listBoxTrees.SelectedIndex];

            Calculator.Calculator.Calculate(tree);

            visualObjects.ForEach(x => x.UpdateVO());
        }
    }
}
