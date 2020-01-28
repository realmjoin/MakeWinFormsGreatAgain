using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Mwfga
{
    public static class WinFormsExtensions
    {
        public static void EnableClickThrough(this CreateParams cp)
        {
            cp.ExStyle |= GreatMessages.WS_EX_LAYERED | GreatMessages.WS_EX_TRANSPARENT;
        }

        public static void EnableComposited(this CreateParams cp)
        {
            cp.ExStyle |= GreatMessages.WS_EX_COMPOSITED;
        }

        public static void SetMaximizeBox(this CreateParams cp, bool enabled)
        {
            if (enabled) cp.ExStyle |= GreatMessages.WS_MAXIMIZEBOX;
            else cp.ExStyle &= ~GreatMessages.WS_MAXIMIZEBOX;
        }

        public static void SetMinimizeBox(this CreateParams cp, bool enabled)
        {
            if (enabled) cp.ExStyle |= GreatMessages.WS_MINIMIZEBOX;
            else cp.ExStyle &= ~GreatMessages.WS_MINIMIZEBOX;
        }

        public static void SetTopMost(this CreateParams cp, bool enabled)
        {
            if (enabled) cp.ExStyle |= GreatMessages.WS_EX_TOPMOST;
            else cp.ExStyle &= ~GreatMessages.WS_EX_TOPMOST;
        }

        public static void MoveFormFromChild(this Control child)
        {
            var form = child.FindForm();

            child.Capture = false;
            form.SendMessage(GreatMessages.WM_NCLBUTTONDOWN, GreatMessages.HT_CAPTION, null);
        }

        public static IntPtr SendMessage(this Control control, int msg, IntPtr wParam, StringBuilder lParam)
        {
            if (!control.IsHandleCreated || control.IsDisposed)
                return default;

            return GreatMessages.SendMessage(control.Handle, msg, wParam, lParam);
        }

        public static IntPtr SendMessage(this Control control, int msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam)
        {
            if (!control.IsHandleCreated || control.IsDisposed)
                return default;

            return GreatMessages.SendMessage(control.Handle, msg, wParam, lParam);
        }

        public static IntPtr SendMessage(this Control control, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam)
        {
            if (!control.IsHandleCreated || control.IsDisposed)
                return default;

            return GreatMessages.SendMessage(control.Handle, msg, wParam, lParam);
        }

        public static IntPtr SendMessage(this Control control, int msg, int wParam, ref IntPtr lParam)
        {
            if (!control.IsHandleCreated || control.IsDisposed)
                return default;

            return GreatMessages.SendMessage(control.Handle, msg, wParam, lParam);
        }

        public static IntPtr SendMessage(this Control control, int msg, int wParam, IntPtr lParam)
        {
            if (!control.IsHandleCreated || control.IsDisposed)
                return default;

            return GreatMessages.SendMessage(control.Handle, msg, wParam, lParam);
        }
    }
}
