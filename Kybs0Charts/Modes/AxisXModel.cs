using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Kybs0Charts
{
    public class AxisXModel : BindingPropertyBase
    {
        private double _labelWidth = 20;
        /// <summary>
        /// 底部标签-单个宽度
        /// </summary>
        public double LabelWidth
        {
            get { return _labelWidth; }
            set { _labelWidth = value; OnPropertyChanged(); }
        }
        private double _marginWidth = 20;
        /// <summary>
        /// Bar间隔宽度
        /// </summary>
        public double MarginWidth
        {
            get { return _marginWidth; }
            set { _marginWidth = value; OnPropertyChanged(); }
        }
        private double _height = 20;
        /// <summary>
        /// 高度
        /// </summary>
        public double Height
        {
            get
            {
                return _height;
            }
            set { _height = value; OnPropertyChanged(); }
        }

        private Brush _foreground = Brushes.Black;
        /// <summary>
        /// 字体颜色
        /// </summary>
        public Brush Foreground
        {
            get { return _foreground; }
            set { _foreground = value; OnPropertyChanged(); }
        }
        private double _barWidth = 30;
        /// <summary>
        /// Bar宽度
        /// </summary>
        public double BarWidth
        {
            get { return _barWidth; }
            set { _barWidth = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 数据
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DefinedCollection<AxisXDataModel> Datas { get; set; } = new DefinedCollection<AxisXDataModel>();
    }
}
