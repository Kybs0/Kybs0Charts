using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Kybs0Charts
{
    public abstract class ChartBase:UserControl
    {
        public override void OnApplyTemplate()
        {
            TopGrid = (Grid)Template.FindName(nameof(TopGrid), this);
            LeftGrid = (Grid)Template.FindName(nameof(LeftGrid), this);
            RightGrid = (Grid)Template.FindName(nameof(RightGrid), this);
            BottomGrid = (Grid)Template.FindName(nameof(BottomGrid), this);

            MainGridXLines = (Grid)Template.FindName(nameof(MainGridXLines), this);
            MainGridYLines = (Grid)Template.FindName(nameof(MainGridYLines), this);
            MainGridAxisX = (Grid)Template.FindName(nameof(MainGridAxisX), this);
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
    }
}
