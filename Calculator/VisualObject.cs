using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Recipe.Calculator
{
    public class VisualObject
    {
        public Resource Resource { get; set; }
        public Panel Container { get; set; }

        public const string voQuantity = "voQuantity";
        public const string voNote = "voNote";

        public VisualObject(Resource res)
        {
            Resource = res;

            Container = new Panel() {
                Size = new Size(0, 0),
                Tag = this
            };

            //Create controls
            var ct = Editor.VisualObject.Constructor.GenerateVO(res.ItemObject.Item, new Point(0, 0), false);

            if ((Resource.IOType == Resource.Type.Input &&
                Resource.ItemObject.IOType == Editor.ItemObject.Type.Input) ||
                (Resource.IOType == Resource.Type.Output &&
                Resource.ItemObject.IOType == Editor.ItemObject.Type.Output))
            {
                ct.Label.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            }

            var quantity = new TextBox() { 
                Name = voQuantity,
                Top = ct.Label.Bottom + 1,
                Width = 62,
                TextAlign = HorizontalAlignment.Center,
                Text = Resource.Amount.ToString(),
                MaxLength = 9,
                Enabled = res.IOType != Resource.Type.Constant
            };

            var noteText = "";
            if (res.IOType == Resource.Type.Constant)
            {
                noteText = "Constant";
            }

            var note = new Label() { 
                Name = voNote,
                Top = quantity.Bottom + 1,
                AutoSize = true,
                Text = noteText
            };

            ct.Icon.Click += new EventHandler(Icon_Click);
            quantity.KeyPress += new KeyPressEventHandler(Quantity_KeyPress);
            quantity.TextChanged += new EventHandler(Quantity_TextChanged);
            note.TextChanged += new EventHandler(Note_TextChanged);

            Container.Controls.AddRange(new Control[] {
                ct.Icon,
                ct.Label,
                quantity,
                note
            });

            //Set the container size
            foreach (Control ctrl in Container.Controls)
            {
                if (Container.Height < ctrl.Bottom)
                {
                    Container.Height = ctrl.Bottom;
                }
                if (Container.Width < ctrl.Width)
                {
                    Container.Width = ctrl.Width;
                }
            }

            //Locate all ctrls at the middle
            int centerX = Container.Width / 2;
            foreach (Control ctrl in Container.Controls)
            {
                ctrl.Left = centerX - ctrl.Width / 2;
            }

            /* Event handlers */

            void Icon_Click(object sender, EventArgs e)
            {
                SelectIO(Resource.ItemObject);
            }

            void Quantity_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            void Quantity_TextChanged(object sender, EventArgs e)
            {
                if (quantity.Text != "")
                {
                    Resource.Amount = Convert.ToInt32(quantity.Text);
                }
            }

            void Note_TextChanged(object sender, EventArgs e)
            {
                var w = note.CreateGraphics().MeasureString(note.Text, note.Font).ToSize().Width;
                note.Left = Container.Width / 2 - w / 2;
            }
        }

        public void UpdateVO()
        {
            switch (Resource.IOType)
            {
                case Resource.Type.Input:
                    var text = "";
                    if (Resource.Amount != 0)
                    {
                        text = "Res: " + Resource.Amount;
                    }

                    GetControl(voNote).Text = text;
                    break;

                case Resource.Type.Output:
                    GetControl(voQuantity).Text = Resource.Amount.ToString();
                    break;
            }
        }

        public static void SelectIO(Editor.ItemObject itemObject)
        {
            Editor.Engine.DeselectVOs();
            Editor.Engine.SelectVO(itemObject, false);
        }

        /**/

        private Control GetControl(string name)
        {
            foreach (Control ctrl in Container.Controls)
            {
                if (ctrl.Name == name)
                {
                    return ctrl;
                }
            }

            return null;
        }
    }
}

