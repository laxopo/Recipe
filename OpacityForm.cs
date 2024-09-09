using System;
using System.Windows.Forms;

namespace Recipe
{
    public class OpacityForm
    {
        private Form form;
        private CheckBox checkEnable;
        private System.Timers.Timer timer;
        private double counter;

        private const double interval = 16.7;
        private const double timSec = 1000 / interval;

        public OpacityForm(Form form, CheckBox enable)
        {
            this.form = form;
            form.MouseEnter += new EventHandler(Form_MouseEnter);

            checkEnable = enable;
            counter = 0;

            timer = new System.Timers.Timer(16.7);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Tick);
        }


        private double Opacity
        {
            get
            {
                double opa = 0;
                form.Invoke(new MethodInvoker(delegate () {
                     opa = form.Opacity;
                }));

                return opa;
            }

            set
            {
                form.Invoke(new MethodInvoker(delegate () {
                    form.Opacity = value;
                }));
            }
        }

        private void Timer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool contain = false;
            
            try
            {
                form.Invoke(new MethodInvoker(delegate () {
                    contain = form.ClientRectangle.Contains(form.PointToClient(Control.MousePosition));
                }));
            }
            catch
            {
                timer.Stop();
                return;
            }
            

            if (contain)
            {
                Opacity = 1.0;
                counter = 0;
            }
            else
            {
                counter++;
                if (counter > timSec / 2)
                {
                    Opacity = 1 - 0.5 * ((counter - timSec / 2) / timSec);
                }
                if (Opacity <= 0.5)
                {
                    Opacity = 0.5;
                    timer.Stop();
                }
            }
        }

        private void Form_MouseEnter(object sender, EventArgs e)
        {
            timer.Enabled = checkEnable.Checked;
            Opacity = 1.0;
            counter = 0;
        }
    }
}
