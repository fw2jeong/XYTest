using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFlots.fFRA
{
    public partial class CFRA
    {   
        /// <summary>
        /// 데이터 처리를 위한 기능을 모은 클래스.
        /// </summary>
        public class CData
        {
            CFRA fraPlot;

            public CData(CFRA fraPlot)
            {
                this.fraPlot = fraPlot;
            }

            /// <summary>
            /// Current 영역에 (주파수,크기,위상)을 입력. 
            /// InitDataArray보다 넘치는 데이터를 입력하면 데이터가 입력되지 않습니다. 
            /// 다시 처음부터 데이터를 입력하려면 Clear매소드를 호출해 주어야합니다.
            /// </summary>
            /// <param name="frequency"></param>
            /// <param name="magnitude"></param>
            /// <param name="phase"></param>
            /// <returns></returns>
            public PLOT_STATUS Input(double frequency, double magnitude, double phase)
            {
                return fraPlot.Current.Input(frequency, magnitude, phase);
            }

            /// <summary>
            /// 특정 영역의 저장된 데이터를 제거합니다.
            /// </summary>
            /// <param name="source">영역 선택</param>
            public void Clear(Source source)
            {
                if (source == Source.Current)
                {
                    fraPlot.Current.Clear();
                }
                else
                {
                    fraPlot.Reference.Clear();
                }
            }


        }
    }

}
