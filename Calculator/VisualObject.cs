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
        public bool Extra { get; set; }
        public Type ResourceType { get; set; }

        public const string voQuantity = "voQuantity";
        public const string voNote = "voNote";

        public enum Type
        {
            Input,
            Output,
            Constant,
            ExtInput,
            ExtOutput
        }

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
                MaxLength = 9
            };


            var note = new Label() { 
                Name = voNote,
                Top = quantity.Bottom + 1,
                AutoSize = true
            };

            switch (res.IOType)
            {
                case Resource.Type.Input:
                    ResourceType = Type.Input;
                    quantity.Text = res.AmountOut.ToString();
                    break;

                case Resource.Type.Output:
                    ResourceType = Type.Output;
                    quantity.Text = res.AmountIn.ToString();
                    break;

                case Resource.Type.None:
                    Extra = true;
                    if (res.Insufficient)
                    {
                        if (res.Amount == 0)
                        {
                            ResourceType = Type.Constant;
                            quantity.Enabled = false;
                            quantity.Text = res.AmountOut.ToString();
                            note.Text = "Const.";
                        }
                        else
                        {
                            ResourceType = Type.ExtInput;
                            note.Text = "Insuf.";
                        }
                    }
                    else if (res.Amount > 0)
                    {
                        ResourceType = Type.ExtOutput;
                        note.Text = "Extra";
                    }
                    break;
            }

            bool hand = false;

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
                SelectVO();
            }

            
            void Quantity_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                else
                {
                    SelectVO();
                    hand = true;
                }
            }

            
            void Quantity_TextChanged(object sender, EventArgs e)
            {
                if (!hand)
                {
                    return;
                }
                hand = false;

                if (quantity.Text != "")
                {
                    Resource.Request = Convert.ToInt32(quantity.Text);
                    CEngine.SelectedTree.InitialRes = Resource;
                }
            }

            void Note_TextChanged(object sender, EventArgs e)
            {
                var w = note.CreateGraphics().MeasureString(note.Text, note.Font).ToSize().Width;
                note.Left = Container.Width / 2 - w / 2;
            }
        }

        public static void SelectIO(Editor.ItemObject itemObject)
        {
            Editor.Engine.DeselectVOs();
            Editor.Engine.SelectVO(itemObject, false);
        }

        public void SelectVO()
        {
            if (CEngine.SelectedVO != null)
            {
                CEngine.SelectedVO.Container.BorderStyle = BorderStyle.None;
            }

            CEngine.SelectedVO = this;
            Container.BorderStyle = BorderStyle.FixedSingle;
        }

        public void UpdateVO()
        {
            var qty = GetControl(voQuantity);

            switch (ResourceType)
            {
                case Type.Input:
                    qty.Text = (Resource.Injected - Resource.Amount).ToString();
                    break;

                case Type.Output:
                case Type.ExtOutput:
                    qty.Text = Resource.Amount.ToString();
                    break;

                case Type.Constant:
                    qty.Text = Resource.AmountOut.ToString();
                    break;

                case Type.ExtInput:
                    qty.Text = (-Resource.Amount).ToString();
                    break;
            }
        }

        public bool UpdateData()
        {
            switch (ResourceType)
            {
                case Type.Input:
                    //UNHANDLED
                    break;

                case Type.Output:
                    var qty = GetControl(voQuantity);
                    Resource.Request = Convert.ToInt32(qty.Text);
                    break;

                default:
                    return false;
            }

            return true;
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

