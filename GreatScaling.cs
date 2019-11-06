using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ToppyMcTopface
{
    public class GreatScaling
    {
        private readonly List<(Control control, float factor)> fontScaling = new List<(Control control, float factor)>();
        private readonly Form form;

        public GreatScaling(Form form)
        {
            this.form = form;
        }

        public void PrepareFontScaling(Control control)
        {
            fontScaling.Add((control, control.Font.SizeInPoints / form.Font.SizeInPoints));
        }

        public void ScaleAfterDpiChange()
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
    }
}