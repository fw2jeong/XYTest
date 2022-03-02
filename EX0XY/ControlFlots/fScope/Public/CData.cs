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
        /// 데이터 처리를 위한 클래스
        /// </summary>
        public class CData
        {
            CScopePlot_Private ScopePlot;

            public CData(CScopePlot_Private ScopePlot)
            {
                this.ScopePlot = ScopePlot;
            }



            /// <summary>
            /// 특정 채널에 데이터를 입력.
            /// </summary>
            /// <param name="ch">채널 선택</param>
            /// <param name="data">입력 데이터</param>
            public PLOT_STATUS Input(CH ch, double[] data = null)
            {
                return ScopePlot.Data.Input(ch, data);
            }


            /// <summary>
            /// 입력된 데이터를 모두 지웁니다.
            /// </summary>
            /// <param name="ch">채널 선택</param>
            /// <param name="source">영역 선택</param>
            public void Clear(CH ch, Source source)
            {
                if (source == Source.Current)
                {
                    ScopePlot.Data.ChClear(ch);
                }
                else
                {
                    ScopePlot.Data.TempClear(ch);
                }
            }
            /// <summary>
            /// 모드를 선택.
            /// Roll : 데이터가 왼쪽에서 오른쪽으로 흐르며 화면에 데이터가 모두 차면 Clear후 다시 차기시작
            /// Normal : 데이터가 오른쪽에서 왼쪽으로 흐르며 오실리스코프를 모방 
            /// </summary>
            /// <param name="mode"></param>
            public void ModeChange(DisplayMode mode)
            {
                ScopePlot.Data.ModeChange(mode);
            }

        }
    }

}
