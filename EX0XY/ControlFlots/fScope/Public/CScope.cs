using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ControlFlots.fScope.Private;

namespace ControlFlots.fScope
{


    
    /// <summary>
    // 실시간 데이터를 ScottPlotlib을 이용하여 사용하는데 도움을 주는 클래스
    /// </summary>
    public partial class CScope 
    {
        private CScopePlot_Private ScopePlot;

        public CData Data;
        public CInfo Info;
        public CDisplay Display;

        public CScope(ScottPlot.FormsPlot formsPlot)
        {
            ScopePlot = new CScopePlot_Private(formsPlot);

            Data = new CData(ScopePlot);
            Info = new CInfo(ScopePlot);
            Display = new CDisplay(ScopePlot);
            
        }

        /// <summary>
        /// 모든 설정이 끝난후, 호출시 그래프에 적용.
        /// </summary>
        public void UpdatePlot()
        {
            ScopePlot.UpdatePlot();
        }


        /// <summary>
        /// 시간축 스케일과 데이터의 입력 주파수를 리턴
        /// </summary>
        /// <param name="scale">스케일 Time(ms)/box </param>
        /// <param name="frequency">주파수</param>
        public void TimeScaleInit(double ? scale = null, double? frequency = null)
        {
            ScopePlot.ReDefine(scale, frequency);
        }

        /// <summary>
        /// 현재 라이브러리의 정보를 리턴.
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            string Writer = "LeeJeongHoon\n";
            string Date = "22.03.02\n";
            string Version = "2.0.1\n";
            string Verification = "Yes\n";

            string output = "";
            output += "Writer" + Writer;
            output += "Date" + Date;
            output += "Version" + Version;
            output += "Verification" + Verification;

            return output;
            
        }
        
    }
    

}
