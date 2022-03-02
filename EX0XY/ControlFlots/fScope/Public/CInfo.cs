using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ControlFlots.fScope.Private;

namespace ControlFlots.fScope
{
    public partial class CScope
    {

        /// <summary>
        /// 차트의 입력된 데이터와 각 차트의 설정 정보 리턴.
        /// </summary>
        public class CInfo
        {
            CScopePlot_Private ScopePlot;

            public CInfo(CScopePlot_Private ScopePlot)
            {
                this.ScopePlot = ScopePlot;
            }

            /// <summary>
            /// 현재 모드의 정보를 리턴.
            /// </summary>
            /// <returns></returns>
            public DisplayMode GetDisplayMode()
            {
                return ScopePlot.Data.mode;
            }
            /// <summary>
            ///  특정 채널,영역에 해당하는 그래프 활성화 정보 리턴.
            /// </summary>
            /// <param name="ch"></param>
            /// <param name="source"></param>
            /// <returns></returns>
            public bool GetLineVisible(CH ch, Source source)
            {
                return GetHandler(source).Display.Line.GetVisible(ch);
            }

            /// <summary>
            /// 현재 채널,영역에 해당하는 그래프 색을 리턴.
            /// </summary>
            /// <param name="ch"></param>
            /// <param name="source"></param>
            /// <returns></returns>
            public Color GetLineColor(CH ch, Source source)
            {
                return GetHandler(source).Display.Line.GetColor(ch);
            }
            /// <summary>
            /// 현재 채널,영역에 해당하는 축의 활성화 정보 리턴.
            /// </summary>
            /// <param name="ch"></param>
            /// <param name="source"></param>
            /// <returns></returns>
            public bool GetAxisVisible(CH ch, Source source)
            {
                return GetHandler(source).Display.Axis.GetVisible(ch);
            }
            /// <summary>
            /// 현재 채널,영역에 해당하는 축의 색을 리턴.
            /// </summary>
            /// <param name="ch"></param>
            /// <param name="source"></param>
            /// <returns></returns>
            public Color GetAxisColor(CH ch, Source source)
            {
                return GetHandler(source).Display.Axis.GetColor(ch);
            }
            /// <summary>
            /// 시간축의 스케일을 리턴.
            /// </summary>
            /// <returns></returns>
            public double GetHorizontalScale()
            {
                return ScopePlot.Info.GetTimeScale();
            }
         
            /// <summary>
            /// 시간축의 주파수를 리턴.
            /// </summary>
            /// <returns></returns>
            public double GetFrequncy()
            {
                return ScopePlot.Info.GetFrequncy();
            }
         
            /// <summary>
            /// 시간축에 저장된 데이터를 리턴.
            /// </summary>
            /// <returns></returns>
            public double[] GetTime()
            {
                return ScopePlot.Info.GetTime();
            }
            /// <summary>
            /// 입력된 데이터를 리턴.
            /// </summary>
            /// <param name="ch"></param>
            /// <param name="source"></param>
            /// <returns></returns>
            public double[] GetData(CH ch, Source source)
            {
                return GetHandler(source).GetData(ch);
            }

            /// <summary>
            /// 내부 배열의 크기를 리턴.
            /// </summary>
            /// <returns></returns>
            public int GetArraySize()
            {
                return ScopePlot.Info.GetArraySize();
            }



            #region   private
            private CScopeHandler GetHandler(Source source)
            {
                if (source == Source.Current)
                {
                    return ScopePlot.Current;
                }
                else
                {
                    return ScopePlot.Reference;
                }
            }
            #endregion

        }

    }

}
