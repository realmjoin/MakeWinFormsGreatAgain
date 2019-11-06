using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mwfga
{
    public class GreatScaling
    {
        private readonly List<(Control control, float factor)> fontScaling = new List<(Control control, float factor)>();
        private readonly Form form;
        private readonly Size formMinimumSize;
        private readonly Size formMaximumSize;

        public GreatScaling(Form form)
        {
            this.form = form;
            formMinimumSize = form.MinimumSize;
            formMaximumSize = form.MaximumSize;
        }

        public void PrepareFontScaling(Control control)
        {
            fontScaling.Add((control, control.Font.SizeInPoints / form.Font.SizeInPoints));
        }

        public void ScaleFontAfterDpiChange()
        {
            foreach (var (control, factor) in fontScaling)
            {
                var oldFont = control.Font;

                using (oldFont)
                {
                    control.Font = new Font(oldFont.FontFamily, factor * form.Font.SizeInPoints, oldFont.Style, GraphicsUnit.Point, oldFont.GdiCharSet, oldFont.GdiVerticalFont);
                }
            }
        }

        public void ClearFormSizeConstraintsBeforeDpiChange()
        {
            form.MinimumSize = default;
            form.MaximumSize = default;
        }

        public void ScaleFormSizeConstraintsAfterDpiChange()
        {
            if (formMinimumSize != default)
                form.MinimumSize = new Size(form.LogicalToDeviceUnits(formMinimumSize.Width), form.LogicalToDeviceUnits(formMinimumSize.Height));

            if (formMaximumSize != default)
                form.MaximumSize = new Size(form.LogicalToDeviceUnits(formMaximumSize.Width), form.LogicalToDeviceUnits(formMaximumSize.Height));
        }
    }
}