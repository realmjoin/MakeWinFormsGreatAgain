using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mwfga
{
    /*
     * This is based on the excellent code by msvasilis.kons and Stu Yarrow:
     * https://developercommunity.visualstudio.com/content/problem/262330/high-dpi-support-in-windows-forms.html
     * 
     * Win10 1607+ only
     */
    public static class DpiUtils
    {
        [DllImport("user32.dll")]
        public static extern int GetDpiForSystem();

        [DllImport("user32.dll")]
        public static extern int GetDpiForWindow(IntPtr hWnd);

        public static void InitPerMonitorDpi(Form form)
        {
            var reportedDpi = form.DeviceDpi;
            var trueDpi = GetDpiForWindow(form.Handle);

            if (reportedDpi == trueDpi)
                return;

            var wParam = (trueDpi << 16) | (trueDpi & 0xffff);
            var dpiRatio = trueDpi / (double)reportedDpi;
            var suggestedBounds = new SuggestedBoundsRect
            {
                Left = form.Location.X,
                Top = form.Location.Y,
                Right = form.Location.X + (int)(form.Width * dpiRatio),
                Bottom = form.Location.Y + (int)(form.Height * dpiRatio)
            };

            var ptr = IntPtr.Zero;

            try
            {
                ptr = Marshal.AllocHGlobal(Marshal.SizeOf(suggestedBounds));
                Marshal.StructureToPtr(suggestedBounds, ptr, false);
                form.SendMessage(GreatMessages.WM_DPICHANGED, wParam, ptr);
            }
            catch
            {
            }
            finally
            {
                if (ptr != IntPtr.Zero) Marshal.FreeHGlobal(ptr);
            }
        }

        private struct SuggestedBoundsRect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}
