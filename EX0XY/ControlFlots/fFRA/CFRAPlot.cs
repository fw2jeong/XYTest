using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottPlot;
using System.Windows.Forms;
using System.Drawing;

namespace ControlFlots.fFRA
{   
   
    /// <summary>
    /// 주파수,위상에 대한 데이터 디스플레이를 도우는 클래스.
    /// </summary>
    public partial class CFRA
    {
        public ScottPlot.FormsPlot formsPlot;

        private FRAHandler Current;
        private FRAHandler Reference;

        public CData Data;
        public CDisplay Display;
        public CTool Tool;
        public CInfo Info;


        public CFRA(ScottPlot.FormsPlot _formsPlot)
        {
            formsPlot = _formsPlot;
            formsPlot.Reset();
            Current = new FRAHandler(formsPlot, Source.Current);
            Reference = new FRAHandler(formsPlot, Source.Reference);

            Data = new CData(this);
            Display = new CDisplay(this);
            Tool = new CTool(this);
            Info = new CInfo(this);
            InitFRAPlot();
        }
       

        /// <summary>
        /// 모든 설정이 끝난 후, 호출 시 설정이 적용.
        /// </summary>
        /// <param name="opt"></param>
        public void UpdatePlot(bool opt =false)
        {
            try
            {
                if (formsPlot.InvokeRequired)
                {
                    formsPlot.Invoke(new MethodInvoker(delegate()
                    {
                        formsPlot.Refresh(opt);
                    }));
                }
                else
                {
                    formsPlot.Refresh(opt);
                }
            }
            catch { }    
        }

        private void InitFRAPlot()
        {
            formsPlot.Plot.XAxis.Label("Frequency [Hz]");
            formsPlot.Plot.YAxis.Label("Magnitude [dB]");
            formsPlot.Plot.YAxis2.Label("Phase [deg]");

            formsPlot.Plot.XAxis.MinorLogScale(true);
            formsPlot.Plot.XAxis.MajorGrid(true);
            formsPlot.Plot.XAxis.MinorGrid(true);

            formsPlot.Plot.XAxis.IsVisible = true;
            formsPlot.Plot.XAxis2.IsVisible = true;
            formsPlot.Plot.YAxis.IsVisible = false;
            formsPlot.Plot.YAxis2.IsVisible = false;
            formsPlot.Plot.YAxis2.Ticks(false);

            formsPlot.Plot.SetAxisLimits(0, 4);

            formsPlot.Plot.XAxis.TickLabelFormat(Graphstyle.LogFormat);
            //Current.MagAxis.TickLabelFormat(style.MagFormat);
            //Reference.MagAxis.TickLabelFormat(style.MagFormat);
            //Current.PhaAxis.TickLabelFormat(style.PhaFormat);
            //Reference.PhaAxis.TickLabelFormat(style.PhaFormat);
            //formsPlot.Plot.YAxis.TickLabelFormat(style.MagFormat);

        }


        /// <summary>
        /// 현재 이 라이브러리의 버전 리턴.
        /// </summary>
        /// <param name="PrintConsole">콘솔 리턴 Enable</param>
        /// <returns></returns>
        public string GetVersion(bool PrintConsole = false)
        {
            string version = "v2.0.1";
            string date = "22.03.02";
            string writer = "LeeJeonghoon";

            string s = "version" + version + "\n" + "date " + date + "\n" + writer + "\n";
            if (PrintConsole)
            {
                Console.WriteLine(s);
            }
            return s;
        }

        private class FRAHandler
        {
            public double[] frequency = new double[2000];
            public double[] LogFrequency = new double[2000];
            public double[] magnitude = new double[2000];
            public double[] phase = new double[2000];


            public ScottPlot.Plottable.ScatterPlot MagScott;
            public ScottPlot.Plottable.ScatterPlot PhaScott;
            public ScottPlot.Renderable.Axis MagAxis;
            public ScottPlot.Renderable.Axis PhaAxis;

            ScottPlot.FormsPlot formsPlot;
            Source me;
            public int dataIndex;
            public FRAHandler(ScottPlot.FormsPlot formsPlot, Source source)
            {
                this.formsPlot = formsPlot;

                if (source == Source.Current)
                {
                    MagScott = formsPlot.Plot.AddScatter(LogFrequency, magnitude, Color.Blue, markerSize: 5);
                    PhaScott = formsPlot.Plot.AddScatter(LogFrequency, phase, Color.Red, markerSize: 5);

                    MagAxis = formsPlot.Plot.AddAxis(ScottPlot.Renderable.Edge.Left, 2);
                    PhaAxis = formsPlot.Plot.AddAxis(ScottPlot.Renderable.Edge.Right, 3);

                    MagScott.XAxisIndex = 0;
                    MagScott.YAxisIndex = 2;
                    PhaScott.XAxisIndex = 0;
                    PhaScott.YAxisIndex = 3;

                    MagScott.IsVisible = false;
                    PhaScott.IsVisible = false;
                    MagAxis.IsVisible = true;
                    PhaAxis.IsVisible = true;

                    MagAxis.Label("Magnitude [dB]");
                    PhaAxis.Label("Phase [deg]");

                    MagAxis.Color(Color.Blue);
                    PhaAxis.Color(Color.Red);
                    me = Source.Current;
                }
                else
                {
                    MagScott = formsPlot.Plot.AddScatter(LogFrequency, magnitude, Color.Blue, markerSize: 5);
                    PhaScott = formsPlot.Plot.AddScatter(LogFrequency, phase, Color.Red, markerSize: 5);

                    MagAxis = formsPlot.Plot.AddAxis(ScottPlot.Renderable.Edge.Left, 4);
                    PhaAxis = formsPlot.Plot.AddAxis(ScottPlot.Renderable.Edge.Right, 5);

                    MagScott.XAxisIndex = 0;
                    MagScott.YAxisIndex = 4;
                    PhaScott.XAxisIndex = 0;
                    PhaScott.YAxisIndex = 5;

                    MagScott.IsVisible = false;
                    PhaScott.IsVisible = false;
                    MagAxis.IsVisible = false;
                    PhaAxis.IsVisible = false;

                    MagAxis.Label("Magnitude [dB]");
                    PhaAxis.Label("Phase [deg]");

                    MagAxis.Color(Color.Blue);
                    PhaAxis.Color(Color.Red);
                    me = Source.Reference;
                }
                MagScott.MaxRenderIndex = 0;
                PhaScott.MaxRenderIndex = 0;

            }

            /// <summary>
            /// 데이터를 입력합니다. 
            /// </summary>
            /// <param name="fre"></param>
            /// <param name="mag"></param>
            /// <param name="pha"></param>
            /// <returns></returns>
            public PLOT_STATUS Input(double fre, double mag, double pha)
            {
                //if (fre < 0 || frequency.Length < dataIndex)
                //{
                //    return Result.NACK;
                //}
                if(frequency.Length < dataIndex)
                {
                    return PLOT_STATUS.PT_OVERFLOW;
                }

                frequency[dataIndex] = fre;
                LogFrequency[dataIndex] = Math.Log10(fre);
                magnitude[dataIndex] = mag;
                phase[dataIndex] = pha;




                MagScott.MaxRenderIndex = dataIndex;
                PhaScott.MaxRenderIndex = dataIndex;

                dataIndex++;

                return PLOT_STATUS.PT_OK;
            }

            public void Clear()
            {
                dataIndex = 0;
                Array.Clear(frequency, 0, frequency.Length);
                Array.Clear(LogFrequency, 0, LogFrequency.Length);
                Array.Clear(magnitude, 0, magnitude.Length);
                Array.Clear(phase, 0, phase.Length);

                MagScott.MaxRenderIndex = dataIndex;
                PhaScott.MaxRenderIndex = dataIndex;
            }


            /// <summary>
            /// 선의 보임여부를 선택합니다.
            /// </summary>
            /// <param name="select"></param>
            /// <param name="isVisible"></param>
            public void LineDisplay(DataType select, bool isVisible = false)
            {
                if (select == DataType.Magnitude)
                {
                    MagScott.IsVisible = isVisible;

                }
                else
                {
                    PhaScott.IsVisible = isVisible;
                }
            }
            /// <summary>
            /// 선의 색을 변경합니다.
            /// </summary>
            /// <param name="select"></param>
            /// <param name="color"></param>
            public void LineColor(DataType select, Color color)
            {
                if (select == DataType.Magnitude)
                {
                    MagScott.Color = color;
                }
                else
                {
                    PhaScott.Color = color;
                }
            }
            /// <summary>
            /// 축의 보임여부를 선택합니다.
            /// </summary>
            /// <param name="select"></param>
            /// <param name="isVisible"></param>
            public void AxisDisplay(DataType select, bool isVisible = false)
            {
                if (select == DataType.Magnitude)
                {
                    MagAxis.IsVisible = isVisible;
                }
                else
                {
                    PhaAxis.IsVisible = isVisible;
                }
            }
            /// <summary>
            /// 축의 색을 변경합니다.
            /// </summary>
            /// <param name="select"></param>
            /// <param name="color"></param>
            public void AxisColor(DataType select, Color color)
            {
                if (select == DataType.Magnitude)
                {
                    MagAxis.Color(color);
                }
                else
                {
                    PhaAxis.Color(color);
                }
            }

        }
      
    
    }
    
    


}
