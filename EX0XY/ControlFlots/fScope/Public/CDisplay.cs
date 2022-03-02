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
        /// 차트의 디스플레이에 대한 기능을 모아놓은 클래스.
        /// </summary>
        public class CDisplay
        {
            CScopePlot_Private ScopePlot;
            CScopeHandler Current;
            CScopeHandler Reference;
            public CDisplay(CScopePlot_Private ScopePlot)
            {
                this.ScopePlot = ScopePlot;
                Current = ScopePlot.Current;
                Reference = ScopePlot.Reference;

            }

            /// <summary>
            /// 데이터의 보임여부와 색을 설정
            /// </summary>
            /// <param name="ch"></param>
            /// <param name="source"></param>
            /// <param name="color"></param>
            /// <param name="isVisible"></param>
            public void Line(CH ch, Source source = Source.Current, Color? color = null, bool? isVisible = null)
            {

                CScopeHandler.CDisplay.CLine LineHandler;

                LineHandler = (source == Source.Current) ? Current.Display.Line : Reference.Display.Line;

                if (color != null)
                {
                    LineHandler.SetColor(ch, (Color)color);
                }
                if (isVisible != null)
                {
                    LineHandler.SetVisible(ch, (bool)isVisible);
                }
            }

            /// <summary>
            /// 축의 보임여부와 색을 설정
            /// </summary>
            /// <param name="ch"></param>
            /// <param name="source"></param>
            /// <param name="color"></param>
            /// <param name="isVisible"></param>
            public void Axis(CH ch, Source source = Source.Current, Color? color = null, bool? isVisible = null)
            {
                CScopeHandler.CDisplay.CAxis AxisHandler;

                AxisHandler = (source == Source.Current) ? Current.Display.Axis : Reference.Display.Axis;

                if (color != null)
                {
                    AxisHandler.SetColor(ch, (Color)color);
                }
                if (isVisible != null)
                {
                    AxisHandler.SetVisible(ch, (bool)isVisible);
                }
            }
            /// <summary>
            /// y축방향의 데이터들의 스케일을 설정
            /// </summary>
            /// <param name="ch"></param>
            /// <param name="source"></param>
            /// <param name="scale"></param>
            /// <param name="offset"></param>
            public void SetVirtical(CH ch, Source source = Source.Current, double? scale = null, double? offset = null)
            {
                CScopeHandler Handler;

                Handler = (source == Source.Current) ? Current : Reference;

                if (scale != null)
                {
                    Handler.Display.Virtical.SetScale(ch, (int)scale);
                }
                if (offset != null)
                {
                    double nowOffset = Handler.GetOffset(ch);
                    double nextOffset = (double)offset;
                    double diffOffset = nextOffset - nowOffset;
                    Handler.Display.Virtical.SetOffset(ch, (double)diffOffset);
                }
            }

            /// <summary>
            /// 차트 왼쪽 위 Bode를 활성화.
            /// </summary>
            /// <param name="isVisible"></param>
            public void ChannelInfoBode(bool isVisible)
            {
                ScopePlot.Info.EnableAnnotation(isVisible);
            }
            
            /// <summary>
            /// Legend 활성화 
            /// </summary>
            /// <param name="isVisible"></param>
           public void Legend(bool isVisible)
            {
                ScopePlot.EnableLable(isVisible);
            }



        }
    }

}
