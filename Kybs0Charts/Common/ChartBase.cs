using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kybs0Charts
{
    public abstract class ChartBase:UserControl
    {
        public ChartBase()
        {
            Loaded += ChartBase_Loaded;
        }

        private void ChartBase_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= ChartBase_Loaded;
        }

        public override void OnApplyTemplate()
        {
            TopGrid = (Grid)Template.FindName(nameof(TopGrid), this);
            LeftGrid = (Grid)Template.FindName(nameof(LeftGrid), this);
            RightGrid = (Grid)Template.FindName(nameof(RightGrid), this);
            BottomGrid = (Grid)Template.FindName(nameof(BottomGrid), this);

            MainGridXLines = (Grid)Template.FindName(nameof(MainGridXLines), this);
            MainGridYLines = (Grid)Template.FindName(nameof(MainGridYLines), this);
            MainGridAxisX = (Grid)Template.FindName(nameof(MainGridAxisX), this);

            this.UpdateLayout();
            InitTemplateViewContent();
        }
        protected virtual void InitTemplateViewContent()
        {

        }

        #region 布局

        /// <summary>
        /// 图表头部区域<br/>
        /// <remarks>如果试图在模板应用之前获取此值，则为 null。</remarks><br/>
        /// <remarks>请注意：如果尚未显示之前，或者刚显示时处于 Collapse 的状态，则会保持此值为 null。</remarks>
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        protected Grid TopGrid;
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        protected Grid LeftGrid;
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        protected Grid RightGrid;
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        protected Grid BottomGrid;

        /// <summary>
        /// Y轴水平方向的分割线
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        protected Grid MainGridYLines;
        /// <summary>
        /// X轴竖直方向的分割线
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        protected Grid MainGridXLines;

        /// <summary>
        /// 主体内容区域-X轴竖直显示的数据
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        protected Grid MainGridAxisX;

        #endregion

        #region 内部方法

        /// <summary>
        /// 设置Y轴方向间隔以及分割线
        /// </summary>
        protected void SetYIntervalsAndLines(AxisYSegmentMode axisYSegmentMode)
        {
            if (axisYSegmentMode.Titles.Count > 0)
            {
                int gridRows = axisYSegmentMode.Titles.Count - 1;
                for (int i = 0; i < gridRows; i++)
                {
                    LeftGrid.RowDefinitions.Add(new RowDefinition());
                    MainGridYLines.RowDefinitions.Add(new RowDefinition());
                }
                int index = 0;
                foreach (var title in axisYSegmentMode.Titles)
                {
                    var textblock = new TextBlock();
                    textblock.Text = title.Name;
                    textblock.Foreground = axisYSegmentMode.ForeGround;
                    textblock.HorizontalAlignment = HorizontalAlignment.Right;
                    textblock.Height = title.LabelHeight;
                    if (index < gridRows)
                    {
                        textblock.VerticalAlignment = VerticalAlignment.Bottom;
                        textblock.Margin = new Thickness(0, 0, 5, -title.LabelHeight / 2);//因为设置在行底部还不够,必须往下移
                        Grid.SetRow(textblock, gridRows - index - 1);
                    }
                    else
                    {
                        textblock.VerticalAlignment = VerticalAlignment.Top;
                        textblock.Margin = new Thickness(0, -title.LabelHeight / 2, 5, 0);//最后一个,设置在顶部
                        Grid.SetRow(textblock, 0);
                    }
                    LeftGrid.Children.Add(textblock);

                    var border = new Border();
                    border.Height = title.LineHeight;
                    border.BorderBrush = title.LineBrush;
                    double thickness = Convert.ToDouble(title.LineHeight) / 2;
                    border.BorderThickness = new Thickness(0, thickness, 0, thickness);
                    if (index < gridRows)
                    {
                        border.VerticalAlignment = VerticalAlignment.Bottom;
                        border.Margin = new Thickness(0, 0, 0, -thickness);//因为设置在行底部还不够,必须往下移
                        Grid.SetRow(border, gridRows - index - 1);
                    }
                    else
                    {
                        border.VerticalAlignment = VerticalAlignment.Top;
                        border.Margin = new Thickness(0, -thickness, 0, 0);//最后一个,设置在顶部
                        Grid.SetRow(border, 0);
                    }
                    MainGridYLines.Children.Add(border);
                    index++;
                }
            }
        }

        #endregion
    }
}
