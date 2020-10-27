using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kybs0Charts
{
    public class BarChart : ChartBase
    {
        static BarChart()
        {
            // 覆盖基类的默认样式，重新提供一个新的默认样式。
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(BarChart), new FrameworkPropertyMetadata(typeof(BarChart)));
        }

        protected override void InitTemplateViewContent()
        {
            LeftGrid.Width = AxisY.Width;
            BottomGrid.Height = AxisX.Height;
            SetYIntervalsAndLines();
            SetAxisXDatas();
        }

        #region 属性

        public new Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }
        public new static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush",
            typeof(Brush), typeof(BarChart),
            new PropertyMetadata(Brushes.Black));

        public new Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        public new static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness",
            typeof(Thickness), typeof(BarChart),
            new PropertyMetadata(new Thickness(1.0, 0.0, 1.0, 1.0)));

        public AxisYModel AxisY
        {
            get => (AxisYModel)GetValue(AxisYProperty);
            set => SetValue(AxisYProperty, value);
        }
        public static readonly DependencyProperty AxisYProperty = DependencyProperty.Register("AxisY",
            typeof(AxisYModel), typeof(BarChart),
            new PropertyMetadata(new AxisYModel()));

        public AxisXModel AxisX
        {
            get => (AxisXModel)GetValue(AxisXProperty);
            set => SetValue(AxisXProperty, value);
        }
        public static readonly DependencyProperty AxisXProperty = DependencyProperty.Register("AxisX",
            typeof(AxisXModel), typeof(BarChart),
            new PropertyMetadata(new AxisXModel()));

        public double HeaderHeight
        {
            get => (double)GetValue(HeaderHeightProperty);
            set => SetValue(HeaderHeightProperty, value);
        }
        public static readonly DependencyProperty HeaderHeightProperty = DependencyProperty.Register("HeaderHeight",
            typeof(double), typeof(BarChart), new PropertyMetadata(20.0));

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header",
            typeof(string), typeof(BarChart), new PropertyMetadata());

        #endregion

        #region 内部方法

        /// <summary>
        /// 设置X轴竖直方向的排列数据显示
        /// </summary>
        private void SetAxisXDatas()
        {
            var axisXModel = AxisX;
            if (axisXModel.Datas.Count > 0)
            {
                int count = axisXModel.Datas.Count;
                for (int i = 0; i < count + 1; i++)
                {
                    BottomGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    MainGridAxisX.ColumnDefinitions.Add(new ColumnDefinition());
                }
                int index = 0;
                foreach (var data in axisXModel.Datas)
                {
                    //底部
                    var textblock = new TextBlock();
                    textblock.Text = data.Name;
                    textblock.Foreground = axisXModel.ForeGround;
                    textblock.VerticalAlignment = VerticalAlignment.Top;
                    textblock.TextAlignment = TextAlignment.Center;
                    textblock.HorizontalAlignment = HorizontalAlignment.Right;
                    double textBlockWidth = data.LabelWidth;
                    textblock.Width = data.LabelWidth;
                    textblock.Margin = new Thickness(0, 5, -textBlockWidth / 2, 0);
                    Grid.SetColumn(textblock, index);
                    BottomGrid.Children.Add(textblock);


                    //主内容
                    var stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Vertical;

                    var tbl = new TextBlock();
                    tbl.Height = 15;
                    tbl.Margin = new Thickness(0, 0, 0, 5);
                    tbl.Text = data.Value.ToString(CultureInfo.InvariantCulture);
                    tbl.Foreground = axisXModel.ForeGround;
                    tbl.HorizontalAlignment = HorizontalAlignment.Center;
                    stackPanel.Children.Add(tbl);

                    var rectangle = new Rectangle();
                    rectangle.Width = data.BarWidth;
                    double maxValue = AxisY.Titles.Max(i => i.Value);
                    var headerHeight = Math.Max(HeaderHeight,TopGrid.ActualHeight);
                    rectangle.Height = (data.Value / maxValue) * (this.Height - BottomGrid.Height - headerHeight);
                    var linearBrush = new LinearGradientBrush()
                    {
                        StartPoint = new Point(1, 0),
                        EndPoint = new Point(1, 1),
                        GradientStops = new GradientStopCollection() {
                            new GradientStop()
                            {
                                Color = data.FillBrush, Offset = 0
                            }, new GradientStop()
                            {
                                Color = data.FillEndBrush, Offset = 1
                            }
                        }
                    };
                    rectangle.Fill = linearBrush;
                    rectangle.HorizontalAlignment = HorizontalAlignment.Center;

                    stackPanel.Children.Add(rectangle);
                    stackPanel.Margin = new Thickness(0, 0, -textBlockWidth / 2, 0);
                    stackPanel.VerticalAlignment = VerticalAlignment.Bottom;
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Right;
                    Grid.SetColumn(stackPanel, index);
                    MainGridAxisX.Children.Add(stackPanel);
                    index++;
                }
            }
        }
        /// <summary>
        /// 设置Y轴方向间隔以及分割线
        /// </summary>
        private void SetYIntervalsAndLines()
        {
            var axisYModel = AxisY;
            if (axisYModel.Titles.Count > 0)
            {
                int gridRows = axisYModel.Titles.Count - 1;
                for (int i = 0; i < gridRows; i++)
                {
                    LeftGrid.RowDefinitions.Add(new RowDefinition());
                    MainGridYLines.RowDefinitions.Add(new RowDefinition());
                }
                int index = 0;
                foreach (var title in axisYModel.Titles)
                {
                    var textblock = new TextBlock();
                    textblock.Text = title.Name;
                    textblock.Foreground = axisYModel.ForeGround;
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
                    Grid.SetColumn(border, 0);
                    Grid.SetColumnSpan(border, AxisX.Datas.Count + 1);
                    MainGridYLines.Children.Add(border);
                    index++;
                }
            }
        }
        /// <summary>
        /// 设置分行
        /// </summary>
        /// <param name="leftGrid"></param>
        /// <param name="count"></param>
        private void SetGridRowDefinitions(Grid leftGrid, int count)
        {
            for (int i = 0; i < count; i++)
            {
                leftGrid.RowDefinitions.Add(new RowDefinition());
            }
        }

        #endregion
    }
}
