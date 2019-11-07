using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mwfga.Subscriptions
{
    public class DpiChangeSubscription : NativeWindow
    {
        public DpiChangeSubscription(Control parent)
        {
            if (parent.IsHandleCreated)
                Parent_HandleCreated(parent, EventArgs.Empty);
            else
                parent.HandleCreated += Parent_HandleCreated;

            parent.HandleDestroyed += Parent_HandleDestroyed;
        }

        private void Parent_HandleCreated(object sender, EventArgs e)
        {
            AssignHandle(((Control)sender).Handle);
        }

        private void Parent_HandleDestroyed(object sender, EventArgs e)
        {
            ReleaseHandle();
        }

        public event EventHandler<GreatDpiChangeEventArgs> DpiChanging;
        public event EventHandler<GreatDpiChangeEventArgs> DpiChanged;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == GreatMessages.WM_DPICHANGED)
            {
                var e = new GreatDpiChangeEventArgs(new Size((ushort)(m.WParam.ToInt64() >> 16), (ushort)m.WParam.ToInt64()));

                DpiChanging?.Invoke(this, e);
                base.WndProc(ref m);
                DpiChanged?.Invoke(this, e);
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}