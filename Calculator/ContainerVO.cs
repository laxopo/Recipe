using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Recipe.Calculator
{
    public class ContainerVO
    {
        public const int HorizontalGap = 10;
        public const int VerticalGap = 10;

        public Control Container { get; set; }
        public List<VisualObject> VisualObjects { get; set; } = new List<VisualObject>();
        public int X { get; set; }
        public int Y { get; set; }
        public int RegX { get; set; }
        public int RegY { get; set; }
        public int Height { get; set; }

        public ContainerVO(Control container)
        {
            Container = container;
        }

        public void Clear()
        {
            Container.Controls.Clear();
            Container.Height = 0;
            X = 0;
            Y = 0;
            RegX = 0;
            RegY = 0;
            Height = 0;
        }

        public void RemoveExtras()
        {
            var rem = VisualObjects.FindAll(x => x.Extra);
            rem.ForEach(x => Container.Controls.Remove(x.Container));
            VisualObjects.RemoveAll(x => x.Extra);

            X = RegX;
            Y = RegY;
        }

        public void AddResRange(List<Resource> resources)
        {
            resources.ForEach(x => AddRes(x));
        }

        public void AddRes(Resource res)
        {
            var vo = new VisualObject(res);
            VisualObjects.Add(vo);

            if (Height < vo.Container.Height)
            {
                Height = vo.Container.Height;
            }

            if (X + vo.Container.Width > Container.Width)
            {
                //new line
                X = 0;
                Y += Height + VerticalGap;
            }

            vo.Container.Location = new Point(X, Y);

            if (Container.Height < vo.Container.Bottom)
            {
                Container.Height = vo.Container.Bottom;
            }

            Container.Controls.Add(vo.Container);

            X += vo.Container.Width + HorizontalGap;

            if (!vo.Extra)
            {
                RegX = X;
                RegY = Y;
            }
        }

        public void UpdateVOs()
        {
            VisualObjects.ForEach(x => x.UpdateVO());
        }
    }
}
