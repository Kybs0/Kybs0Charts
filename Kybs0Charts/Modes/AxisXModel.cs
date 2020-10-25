using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Kybs0Charts
{
    public class AxisXModel
    {
        private double _labelWidth = 20;
        /// <summary>
        /// 底部标签-单个宽度
        /// </summary>
        public double LabelWidth
        {
            get { return _labelWidth; }
            set { _labelWidth = value; }
        }
        private double _marginWidth = 20;
        /// <summary>
        /// Bar间隔宽度
        /// </summary>
        public double MarginWidth
        {
            get { return _marginWidth; }
            set { _marginWidth = value; }
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
            set { _height = value; }
        }

        private Brush _foreGround = Brushes.Black;
        /// <summary>
        /// 字体颜色
        /// </summary>
        public Brush ForeGround
        {
            get { return _foreGround; }
            set { _foreGround = value; }
        }
        private double _barWidth = 30;
        /// <summary>
        /// Bar宽度
        /// </summary>
        public double BarWidth
        {
            get { return _barWidth; }
            set { _barWidth = value; }
        }
        DataModelCollection<AxisXDataModel> _datas = new DataModelCollection<AxisXDataModel>();
        /// <summary>
        /// 数据
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataModelCollection<AxisXDataModel> Datas
        {
            get { return _datas; }
            set { _datas = value; }
        }
    }
}
