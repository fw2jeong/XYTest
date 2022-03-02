using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ControlFlots.fScope.Private
{
    public partial class CScopeHandler
    {
        //CScopePlot ScopePlot;

        public double[] time;
        //Roll 이전 데이터
        //public double[][] preData = new double[6][];
        //Roll 현재 데이터
        public double[][] chData = new double[6][];
        //Roll 현재 임시 저장 데이터
        public double[][] TempChData = new double[6][];
        //각 채널의 데이터 채워진 위치 저장을 위한 Index
        public int[] index = new int[6];

        public ScottPlot.Plottable.SignalPlotXY[] chHandler = new ScottPlot.Plottable.SignalPlotXY[6];
        public ScottPlot.Renderable.Axis[] yAxis = new ScottPlot.Renderable.Axis[6];

        public double[] scale = new double[6];
        public double[] offset = new double[6];

        public Color[] axisColor = new Color[6];

 
        

        public Source me;

        public ScottPlot.FormsPlot formsPlot;
        
        public CStorage Storage;
        public CDisplay Display;

        #region 생성자
        public CScopeHandler(CScopePlot_Private ScopePlot, ScottPlot.FormsPlot formsPlot, Source source)
        {
            this.time = ScopePlot.time;
            this.formsPlot = formsPlot;
            me = source;
            // time 재정의해주세요.

            if (me == Source.Current)
            {
                for (int i = 0; i < 6; i++)
                {
                    string s = "Current_" + Convert.ToString((CH)i);

                    chData[i] = new double[InitValue.VOLUME];
                    chHandler[i] = formsPlot.Plot.AddSignalXY(time, chData[i], label: s);
                    yAxis[i] = formsPlot.Plot.AddAxis(ScottPlot.Renderable.Edge.Left, i + 2);
                    chHandler[i].XAxisIndex = 0;
                    chHandler[i].YAxisIndex = i + 2;
                    yAxis[i].IsVisible = false;
                    chHandler[i].IsVisible = false;

                    //preData[i] = new double[InitValue.VOLUME];
                    TempChData[i] = new double[InitValue.VOLUME];
                }

            }
            else if (me == Source.Reference)
            {
                for (int i = 0; i < 6; i++)
                {

                    string s = "Reference_" + Convert.ToString((CH)i);

                    chData[i] = new double[InitValue.VOLUME];
                    chHandler[i] = formsPlot.Plot.AddSignalXY(time, chData[i], label: s);
                    yAxis[i] = formsPlot.Plot.AddAxis(ScottPlot.Renderable.Edge.Right, i + 8);
                    chHandler[i].XAxisIndex = 0;
                    chHandler[i].YAxisIndex = i + 8;
                    yAxis[i].IsVisible = false;
                    chHandler[i].IsVisible = false;

                    //preData[i] = new double[InitValue.VOLUME];
                    TempChData[i] = new double[InitValue.VOLUME];
                }
            }
           

            for (int i = 0; i < 6; i++)
            {
                axisColor[i] = Color.Black;
                scale[i] = 2000;
                offset[i] = 0;
                index[i] = 0;
            }
            Storage = new CStorage(formsPlot, this);
            //Info = new CInfo(this);
            Display = new CDisplay(ScopePlot, this);
        }
        #endregion

        #region Scale,Offset,Data Get
        public double GetOffset(CH ch) { return offset[(int)ch]; }
        public double GetScale(CH ch) { return scale[(int)ch]; }
        public double[] GetData(CH ch) { return chData[(int)ch]; }
        #endregion


        #region 축,라인 Get,Set
        //축 컬러 Get,Set
        public Color GetAxisColor(CH ch) { return axisColor[(int)ch]; }
        public void SetAxisColor(CH ch, Color color) { yAxis[(int)ch].Color(color); axisColor[(int)ch] = color; }

        //축 디스플레이 Get,Set
        public bool GetAxisDisplay(CH ch) { return yAxis[(int)ch].IsVisible; }
        public void SetAxisDisplay(CH ch, bool isVisible) { yAxis[(int)ch].IsVisible = isVisible; }

        //라인 컬러 Get,Set
        public Color GetLineColor(CH ch) { return chHandler[(int)ch].Color; }
        public void SetLineColor(CH ch, Color color) { chHandler[(int)ch].Color = color; }

        //라인 디스플레이 Get,Set
        public bool GetLineDisplay(CH ch) { return chHandler[(int)ch].IsVisible; }
        public void SetLineDisplay(CH ch, bool isVisible) { chHandler[(int)ch].IsVisible = isVisible; }
        #endregion

        /// <summary>
        /// 채널에 저장되어 있는 최대 값을 반환합니다.
        /// </summary>
        /// <param name="ch">채널 선택</param>
        /// <returns></returns>
        public double GetMaxData(CH ch)
        {
            double maxData = double.MinValue;
            for (int i = chHandler[(int)ch].MinRenderIndex; i < chHandler[(int)ch].MaxRenderIndex; i++)
            {
                if (maxData < chData[(int)ch][i])
                {
                    maxData = chData[(int)ch][i];
                }
            }
            return maxData;
        }
        /// <summary>
        /// 채널에 저장되어 있는 최소 값을 반환합니다.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public double GetMinData(CH ch)
        {
            double minData = double.MaxValue;
            for (int i = chHandler[(int)ch].MinRenderIndex; i < chHandler[(int)ch].MaxRenderIndex; i++)
            {
                if (minData > chData[(int)ch][i])
                {
                    minData = chData[(int)ch][i];
                }
            }
            return minData;
        }


    }
}
