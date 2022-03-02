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
        /// 차트에 입력된 데이터의 정보를 리턴. 
        /// </summary>
        public class CInfo
        {
            CXY XYPlot;
            public CInfo(CXY XYPlot)
            {
                this.XYPlot = XYPlot;
            }

            /// <summary>
            /// 차트에 저장된 데이터를 모두 리턴 item1 : X, item2 : Y
            /// </summary>
            /// <param name="ch">채널 선택</param>
            /// <param name="source">영역 선택</param>
            /// <returns></returns>
            public Tuple<double[] , double[]> GetData(CH ch, Source source)
            {
                XYHandler XYHandler = (source == Source.Current) ? XYPlot.Up : XYPlot.Down;
                double[] X = XYHandler.xArray[(int)ch];
                double[] Y = XYHandler.yArray[(int)ch];
                var result = Tuple.Create<double[], double[]>(X, Y);
                return result;
            }
            /// <summary>
            /// 차트에 저장된 데이터의 인덱스를 리턴. 
            /// 인덱스의 의미는 저장된 데이터의 갯수.
            /// </summary>
            /// <param name="ch">채널 선택</param>
            /// <param name="source">영역 선택</param>
            /// <returns></returns>
            public int GetIndex(CH ch, Source source)
            {
                XYHandler XYHandler = (source == Source.Current) ? XYPlot.Up : XYPlot.Down;
                int index = XYHandler.index[(int)ch];
                return index;
            }
            /// <summary>
            /// 차트의 보여지는 데이터의 색을 리턴.
            /// </summary>
            /// <param name="ch">채널 선택</param>
            /// <param name="source">영역 선택</param>
            /// <returns></returns>
            public Color GetLineColor(CH ch, Source source)
            {
                XYHandler XYHandler = (source == Source.Current) ? XYPlot.Up : XYPlot.Down;
                return XYHandler.ScatterHandler[(int)ch].Color;
            }
            /// <summary>
            /// 차트의 데이터의 보임여부 상태를 리턴.
            /// </summary>
            /// <param name="ch">채널 선택</param>
            /// <param name="source">영역 선택</param>
            /// <returns></returns>
            public bool GetLineVisible(CH ch, Source source)
            {
                XYHandler XYHandler = (source == Source.Current) ? XYPlot.Up : XYPlot.Down;
                return XYHandler.ScatterHandler[(int)ch].IsVisible;
            }
            /// <summary>
            /// 차트의 축의 색을 리턴.
            /// </summary>
            /// <param name="ch">채널 선택</param>
            /// <param name="source">영역 선택</param>
            /// <returns></returns>
            public Color GetAixsColor(CH ch, Source source)
            {
                XYHandler XYHandler = (source == Source.Current) ? XYPlot.Up : XYPlot.Down;
                return XYHandler.axisColor[(int)ch];
            }
            /// <summary>
            /// 차트의 축의 보임여부 상태를 리턴.
            /// </summary>
            /// <param name="ch">채널 선택</param>
            /// <param name="source">영역 선택</param>
            /// <returns></returns>
            public bool GetAxisVisible(CH ch, Source source)
            {
                XYHandler XYHandler = (source == Source.Current) ? XYPlot.Up : XYPlot.Down;
                return XYHandler.yAxis[(int)ch].IsVisible;
            }

        }
    }
}
