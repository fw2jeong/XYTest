using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ControlFlots;

namespace ControlFlots.fXY
{

    public partial class CXY
    {
        /// <summary>
        /// 디스플레이 처리기능을 모은 클래스.
        /// </summary>
        public class CDisplay
        {
            CXY XYPlot;
            public CDisplay(CXY XYPlot)
            {
                this.XYPlot = XYPlot;
            }

            /// <summary>
            /// 그래프의 디스플레이를 설정
            /// </summary>
            /// <param name="ch">채널 설정</param>
            /// <param name="source">영역 설정</param>
            /// <param name="color">색 설정</param>
            /// <param name="isVisible">보임여부 설정</param>
            public void Line(CH ch, Source source, Color? color = null, bool? isVisible = null)
            {
                CXY.XYHandler XYHandler = (source == Source.Current) ? XYPlot.Up : XYPlot.Down;
                if (color != null)
                {
                    XYHandler.ScatterHandler[(int)ch].Color = (Color)color;
                }
                if (isVisible != null)
                {
                    XYHandler.ScatterHandler[(int)ch].IsVisible = (bool)isVisible;
                }
            }
            /// <summary>
            /// 축의 디스플레이를 설정
            /// </summary>
            /// <param name="ch">채널 설정</param>
            /// <param name="source">영역 설정</param>
            /// <param name="color">색 설정</param>
            /// <param name="isVisible">보임 여부 설정</param>
            public void Axis(CH ch, Source source, Color? color = null, bool? isVisible = null)
            {
                CXY.XYHandler XYHandler = (source == Source.Current) ? XYPlot.Up : XYPlot.Down;

                if (color != null)
                {
                    XYHandler.yAxis[(int)ch].Color((Color)color);
                }
                if (isVisible != null)
                {
                    XYHandler.yAxis[(int)ch].IsVisible = (bool)isVisible;
                }
            }

            /// <summary>
            /// 그래프 영역 설정
            /// </summary>
            /// <param name="xmin">Chart X 최소값</param>
            /// <param name="xmax">Chart X 최대값</param>
            /// <param name="ymin">Chart Y 최소값</param>
            /// <param name="ymax">Chart Y 최대값</param>
            public void Set(double xmin, double xmax, double ymin, double ymax)
            {
                XYPlot.formsPlot.Plot.SetAxisLimits(xmin, xmax, ymin, ymax, 0, 0);
                for (int i = 0; i < 12; i++)
                {
                    XYPlot.formsPlot.Plot.SetAxisLimits(xmin, xmax, ymin, ymax, 0, 2 + i);
                }

            }
        }
    }

}