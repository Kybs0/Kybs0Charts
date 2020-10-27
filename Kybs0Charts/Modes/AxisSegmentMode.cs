using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Kybs0Charts
{
    public class AxisSegmentMode : BindingPropertyBase
    {
        private double _width = 20;
        /// <summary>
        /// 宽度
        /// </summary>
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        private Brush _foreGround = Brushes.Black;
        /// <summary>
        /// 字体颜色
        /// </summary>
        public Brush ForeGround
        {
            get { return _foreGround; }
            set
            {
                _foreGround = value;
                OnPropertyChanged();
            }
        }

        private double _labelHeight = 15;
        /// <summary>
        /// 左侧标题栏-单个标题高度
        /// </summary>
        public double LabelHeight
        {
            get { return _labelHeight; }
            set
            {
                _labelHeight = value;
                OnPropertyChanged();
            }
        }
        private double _lineHeight = 0.2;
        /// <summary>
        /// GridLine高度
        /// </summary>
        public double LineHeight
        {
            get { return _lineHeight; }
            set
            {
                _lineHeight = value;
                OnPropertyChanged();
            }
        }


        private Brush _lineBrush = Brushes.Blue;
        /// <summary>
        /// Bar填充颜色
        /// </summary>
        public Brush LineBrush
        {
            get { return _lineBrush; }
            set
            {
                _lineBrush = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 标题列表
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DefinedCollection<AxisSegmentItem> SegmentItems { get; set; } = new DefinedCollection<AxisSegmentItem>();
    }
}
