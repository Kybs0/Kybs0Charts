using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Kybs0Charts
{
    public static class BrushHelper
    {
        public static Brush GetLinearBrush(Color fillBrush, Color fillEndBrush)
        {
            var linearBrush = new LinearGradientBrush()
            {
                StartPoint = new Point(1, 0),
                EndPoint = new Point(1, 1),
                GradientStops = new GradientStopCollection() {
                    new GradientStop()
                    {
                        Color = fillBrush, Offset = 0
                    }, new GradientStop()
                    {
                        Color = fillEndBrush, Offset = 1
                    }
                }
            };
            return linearBrush;
        }
    }
}
