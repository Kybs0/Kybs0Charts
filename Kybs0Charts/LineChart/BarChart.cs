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
    public class LineChart : ChartBase
    {
        static LineChart()
        {
            // 覆盖基类的默认样式，重新提供一个新的默认样式。
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(LineChart), new FrameworkPropertyMetadata(typeof(LineChart)));
        }

        protected override void InitTemplateViewContent()
        {
            LeftGrid.Width = AxisYSegment.Width;
            BottomGrid.Height = AxisX.Height;
            SetYIntervalsAndLines(AxisYSegment);
            SetXIntervalsAndLines(AxisXSegment);
            SetAxisXDatas();
        }

        private void SetXIntervalsAndLines(AxisSegmentMode axisYSegment)
        {
            if (axisYSegment==null)
            {
                var axisXModel = AxisX;
                if (axisXModel.Datas.Count > 0)
                {
                    //使用数据添加X轴坐标
                    int count = axisXModel.Datas.Count;
                    for (int i = 0; i < count + 1; i++)
                    {
                        BottomGrid.ColumnDefinitions.Add(new ColumnDefinition());
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
                        index++;
                    }
                }
            }
        }

        #region 属性

        #region YSegement
        public AxisSegmentMode AxisYSegment
        {
            get => (AxisSegmentMode)GetValue(AxisYSegmentProperty);
            set => SetValue(AxisYSegmentProperty, value);
        }
        public static readonly DependencyProperty AxisYSegmentProperty = DependencyProperty.Register("AxisYSegment",
            typeof(AxisSegmentMode), typeof(LineChart),
            new PropertyMetadata(new AxisSegmentMode()));

        #endregion

        #region XSegement
        public AxisSegmentMode AxisXSegment
        {
            get => (AxisSegmentMode)GetValue(AxisXSegmentProperty);
            set => SetValue(AxisXSegmentProperty, value);
        }
        public static readonly DependencyProperty AxisXSegmentProperty = DependencyProperty.Register("AxisXSegment",
            typeof(AxisSegmentMode), typeof(LineChart),
            new PropertyMetadata(default(AxisSegmentMode)));

        #endregion

        #region AxisX数据

        public AxisXModel AxisX
        {
            get => (AxisXModel)GetValue(AxisXProperty);
            set => SetValue(AxisXProperty, value);
        }
        public static readonly DependencyProperty AxisXProperty = DependencyProperty.Register("AxisX",
            typeof(AxisXModel), typeof(LineChart),
            new PropertyMetadata(new AxisXModel()));

        #endregion

        public new Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }
        public new static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush",
            typeof(Brush), typeof(LineChart),
            new PropertyMetadata(Brushes.Black));

        public new Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        public new static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness",
            typeof(Thickness), typeof(LineChart),
            new PropertyMetadata(new Thickness(1.0, 0.0, 1.0, 1.0)));

        public double HeaderHeight
        {
            get => (double)GetValue(HeaderHeightProperty);
            set => SetValue(HeaderHeightProperty, value);
        }
        public static readonly DependencyProperty HeaderHeightProperty = DependencyProperty.Register("HeaderHeight",
            typeof(double), typeof(LineChart), new PropertyMetadata(20.0));

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header",
            typeof(string), typeof(LineChart), new PropertyMetadata());

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
                    MainGridAxisX.ColumnDefinitions.Add(new ColumnDefinition());
                }
                double maxYValue = AxisYSegment.SegmentItems.Max(i => i.Value);
                int index = 0;
                foreach (var data in axisXModel.Datas)
                {
                    //主内容
                    double textBlockWidth = data.LabelWidth;
                    var barItem = GenerateBarItem(data, axisXModel.ForeGround, maxYValue, textBlockWidth);
                    Grid.SetColumn(barItem, index);
                    MainGridAxisX.Children.Add(barItem);
                    index++;
                }
            }
        }

        private Grid GenerateBarItem(AxisXDataModel data, Brush foregroundBrush, double maxYValue, double textBlockWidth)
        {
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(maxYValue - data.Value, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(data.Value, GridUnitType.Star) });

            var tbl = new TextBlock();
            tbl.Height = 15;
            tbl.Margin = new Thickness(0, -16, 0, 0);
            tbl.Text = data.Value.ToString(CultureInfo.InvariantCulture);
            tbl.Foreground = foregroundBrush;
            tbl.HorizontalAlignment = HorizontalAlignment.Center;
            tbl.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(tbl,1);
            grid.Children.Add(tbl);

            var rectangle = new Rectangle();
            rectangle.Width = data.BarWidth;
            rectangle.Fill = BrushHelper.GetLinearBrush(data.FillBrush, data.FillEndBrush);
            rectangle.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(rectangle, 1);
            grid.Children.Add(rectangle);

            grid.Margin = new Thickness(0, 0, -textBlockWidth / 2, 0);
            grid.HorizontalAlignment = HorizontalAlignment.Right;
            return grid;
        }

        #endregion
    }
}
