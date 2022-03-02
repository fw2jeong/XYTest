using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottPlot;
using ControlFlots;
using System.Drawing;
using System.Windows.Forms;
using ControlFlots;

namespace ControlFlots.fXY
{
    /// <summary>
    /// X/Y의 관계를 가지는 Sweep,Circle,Best AF Position을 만들기 위해 정의된 클래스
    /// </summary>
    public partial class CXY
    {

        public ScottPlot.FormsPlot formsPlot;
        XYHandler Up;
        XYHandler Down;

        public CData Data;
        public CDisplay Display;
        public CInfo Info;


        public CXY(ScottPlot.FormsPlot formsPlot)
        {
            this.formsPlot = formsPlot;
            formsPlot.Reset();

            Up = new XYHandler(formsPlot, Source.Current);
            Down = new XYHandler(formsPlot, Source.Reference);

            Data = new CData(this);
            Display = new CDisplay(this);
            Info = new CInfo(this);

        }

        /// <summary>
        /// 모든 설정이 끝난 후, 호출 시 설정이 적용.
        /// </summary>
        public void UpdatePlot()
        {
            for (int i = 0; i < 6;i++ )
            {
                Up.ScatterHandler[i].MaxRenderIndex = (Up.index[i] -1 > 0) ? Up.index[i] -1 :0 ;
                Down.ScatterHandler[i].MaxRenderIndex = (Down.index[i] - 1 >0) ? Down.index[i] -1 : 0;
            }

            if (formsPlot.InvokeRequired)
            {
                formsPlot.Invoke(new MethodInvoker(delegate()
                {
                    formsPlot.Refresh();
                }));
            }
            else
            {
                formsPlot.Refresh();
            }
        }
        /// <summary>
        /// 현재 이 라이브러리의 버전 리턴.
        /// </summary>
        /// <returns>Version Infomation</returns>
        public string GetVersion()
        {
            string Writer = "LeeJeongHoon\n";
            string Date = "22.03.02\n";
            string Version = "2.0.1\n";
            string Verification = "Yes\n";
            string ScottVersion = "4.1.33";

            string output = "";
            output += "Writer" + Writer;
            output += "Date" + Date;
            output += "Version" + Version;
            output += "Verification" + Verification;
            output += "ScottPlot.WinForms Library Version is \t" + ScottVersion;

            return output;
        }
        

        /// <summary>
        /// 하위 클래스의 사용을 도움 주기위해 선언한 클래스 사용자가 접근할 필요성이 없음.
        /// </summary>
        private class XYHandler
        {
            ScottPlot.FormsPlot formsPlot;
            public double [][] xArray =  new double[6][];
            public double [][] yArray =  new double[6][];
            public int[] index = new int[6];
            public Color[] axisColor = new Color[6];

            public ScottPlot.Plottable.ScatterPlot[] ScatterHandler = new ScottPlot.Plottable.ScatterPlot[6];
            public ScottPlot.Renderable.Axis[] yAxis = new ScottPlot.Renderable.Axis[6];

            Source me;
            public XYHandler(ScottPlot.FormsPlot formsPlot, Source source)
            {
                this.formsPlot = formsPlot;
                me = source;

                for(int i=0;i<6;i++)
                {
                    index[i] = 0;
                    xArray[i] = new double[20000];
                    yArray[i] = new double[20000];

                    yAxis[i] = formsPlot.Plot.AddAxis(ScottPlot.Renderable.Edge.Left, i + 2 + 6 * (int)source);
                    ScatterHandler[i] = formsPlot.Plot.AddScatter(xArray[i], yArray[i], lineStyle: ScottPlot.LineStyle.None);
                    ScatterHandler[i].LineStyle = LineStyle.None;

                    ScatterHandler[i].XAxisIndex = 0;
                    ScatterHandler[i].YAxisIndex = i + 2 + 6 * (int)source;
                    yAxis[i].IsVisible = false;
                    ScatterHandler[i].IsVisible = false;
                    axisColor[i] = Color.Black;
                }
                formsPlot.Refresh();
            }
        }

        

        
       

  }


       
     

}
