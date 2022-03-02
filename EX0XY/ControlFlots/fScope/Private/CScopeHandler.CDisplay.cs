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
        public class CDisplay
        {
            CScopePlot_Private ScopePlot;
            CScopeHandler ScopeHandler;

            public CLine Line;
            public CAxis Axis;
            public CVirtical Virtical;

            public CDisplay(CScopePlot_Private ScopePlot, CScopeHandler ScopeHandler)
            {
                this.ScopePlot = ScopePlot;
                this.ScopeHandler = ScopeHandler;
                Line = new CLine(ScopeHandler);
                Axis = new CAxis(ScopeHandler);
                Virtical = new CVirtical(ScopeHandler);
            }

            /// <summary>
            /// 특정 채널의 데이터를 검사하여 
            /// 주어진 스케일과 오프셋을 자동으로 계산하여 적용시킵니다.
            /// </summary>
            /// <param name="ch">채널 선택</param>
            public void Auto(CH ch)
            {
                double xMin = double.MaxValue;
                double xMax = double.MinValue;

                double yMax = double.MinValue;
                double yMin = double.MaxValue;


                xMin = ScopePlot.time[0];
                xMax = ScopePlot.time[ScopePlot.time.Length - 1];

                yMax = ScopeHandler.GetMaxData(ch);
                yMin = ScopeHandler.GetMinData(ch);


                if (xMin >= xMax)
                {
                    xMin = 0;
                    xMax = 1;
                }
                if (yMin >= yMax)
                {
                    yMin = -TimeScale.InitAxisMag;
                    yMax = TimeScale.InitAxisMag;
                }

                double tempOffset = (yMax + yMin) / 2;
                double range = (yMax - yMin) / 2;

                double tempScale = 1;

                if (range > 10000)
                {
                    tempScale = 10000;
                }
                else if (range > 5000)
                {
                    tempScale = 5000;
                }
                else if (range > 2000)
                {
                    tempScale = 2000;
                }
                else if (range > 1000)
                {
                    tempScale = 1000;
                }
                else if (range > 500)
                {
                    tempScale = 500;
                }
                else if (range > 200)
                {
                    tempScale = 200;
                }
                else if (range > 100)
                {
                    tempScale = 100;
                }
                else if (range > 50)
                {
                    tempScale = 50;
                }
                else if (range > 20)
                {
                    tempScale = 20;
                }
                else
                {
                    tempScale = 10;
                }

                ScopeHandler.scale[(int)ch] = tempScale;
                ScopeHandler.offset[(int)ch] = -tempOffset;

                ScopeHandler.formsPlot.Plot.SetAxisLimits(xMin, xMax, -TimeScale.InitAxisMag, TimeScale.InitAxisMag);
                ScopeHandler.formsPlot.Plot.SetAxisLimits(xMin, xMax, tempOffset - 5 * tempScale, tempOffset + 5 * tempScale
                    , 0, (int)ch + 2 + 6 * (int)ScopeHandler.me);


            }


            public class CLine
            {
                CScopeHandler ScopeHandler;
                public CLine(CScopeHandler ScopeHandler)
                {
                    this.ScopeHandler = ScopeHandler;
                }
                #region   라인의 색 Get,Set
                public void SetColor(CH ch, Color color) { ScopeHandler.chHandler[(int)ch].Color = color; }
                public Color GetColor(CH ch) { return ScopeHandler.chHandler[(int)ch].Color; }
                public void SetVisible(CH ch, bool isVisible) { ScopeHandler.chHandler[(int)ch].IsVisible = isVisible; }
                public bool GetVisible(CH ch) { return ScopeHandler.chHandler[(int)ch].IsVisible; }
                #endregion
            }

            public class CAxis
            {
                CScopeHandler ScopeHandler;
                public CAxis(CScopeHandler ScopeHandler)
                {
                    this.ScopeHandler = ScopeHandler;
                }
                #region   축의 색 Get,Set
                public Color GetColor(CH ch) { return ScopeHandler.axisColor[(int)ch]; }
                public void SetColor(CH ch, Color color) { ScopeHandler.yAxis[(int)ch].Color(color); ScopeHandler.axisColor[(int)ch] = color; }
                public bool GetVisible(CH ch) { return ScopeHandler.yAxis[(int)ch].IsVisible; }
                public void SetVisible(CH ch, bool isVisible) { ScopeHandler.yAxis[(int)ch].IsVisible = isVisible; }
                #endregion
            }

            public class CVirtical
            {
                CScopeHandler ScopeHandler;
                public CVirtical(CScopeHandler ScopeHandler)
                {
                    this.ScopeHandler = ScopeHandler;
                }
                /// <summary>
                /// 특정 채널의 스케일을 변경합니다.
                /// </summary>
                /// <param name="ch">채널 선택</param>
                /// <param name="value">스케일 값 code/blanck </param>
                public void SetScale(CH ch, int value)
                {
                    ScopeHandler.scale[(int)ch] = value;

                    double ymid = ScopeHandler.formsPlot.Plot.GetAxisLimits(yAxisIndex: (int)ch + 2 + 6 * (int)ScopeHandler.me).YCenter;
                    double yRadius = value * 5;
                    double newYMin = ymid - yRadius;
                    double newYMax = ymid + yRadius;
                    ScopeHandler.formsPlot.Plot.SetAxisLimits(yMin: newYMin, yMax: newYMax, xAxisIndex: 0, yAxisIndex: (int)ch + 2 + 6 * (int)ScopeHandler.me);
                }
                /// <summary>
                /// 특정 채널의 오프셋을 변경합니다.
                /// </summary>
                /// <param name="ch">채널 선택</param>
                /// <param name="offset">오프셋 값</param>
                public void SetOffset(CH ch, double offset)
                {
                    double ymin, ymax;
                    ScopeHandler.offset[(int)ch] += offset;
                    ymin = ScopeHandler.formsPlot.Plot.GetAxisLimits(0, (int)ch + 2 + 6 * (int)ScopeHandler.me).YMin;
                    ymax = ScopeHandler.formsPlot.Plot.GetAxisLimits(0, (int)ch + 2 + 6 * (int)ScopeHandler.me).YMax;

                    double yMid = (ymin + ymax) / 2;
                    double yRadius = (ymax - ymin) / 2;

                    double newYMin, newYMax;

                    newYMin = yMid - yRadius - offset;
                    newYMax = yMid + yRadius - offset;

                   

                    ScopeHandler.formsPlot.Plot.SetAxisLimits(yMin: newYMin, yMax: newYMax, xAxisIndex: 0, yAxisIndex: (int)ch + 2 + 6 * (int)ScopeHandler.me);

                }

            }


            #region  미사용 클래스

            #endregion


        }
    }
}
