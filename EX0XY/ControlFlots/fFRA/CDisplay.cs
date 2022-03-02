using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ControlFlots.fFRA
{
    public partial class CFRA
    {
        /// <summary>
        /// 디스플레이 처리를 위한 기능을 모은 클래스.
        /// </summary>
        public class CDisplay
        {
            CFRA FRAPlot;
            public CDisplay(CFRA FRAPlot)
            {
                this.FRAPlot = FRAPlot;
            }

            /// <summary>
            /// 보여주는 화면을 특정 좌표구간으로 설정. 
            /// fra모드에서는 초기에 설정해주면 됩니다.
            /// </summary>
            /// <param name="frequncyXMin">보이는 주파수 최소 값</param>
            /// <param name="frequncyXMax">보이는 주파수 최대 값</param>
            /// <param name="magnitudeMin">보이는 크기 최소 값</param>
            /// <param name="magnitudeMax">보이는 크기 최대 값</param>
            /// <param name="phaseMin">보이는 위상 최소 값</param>
            /// <param name="phaseMax">보이는 위상 최대 값</param>
            public void Set(double frequncyXMin, double frequncyXMax, double magnitudeMin, double magnitudeMax, double phaseMin, double phaseMax)
            {
                FRAPlot.formsPlot.Plot.SetAxisLimits(Math.Log10(frequncyXMin), Math.Log10(frequncyXMax), magnitudeMin, magnitudeMax, 0, 2);     //
                FRAPlot.formsPlot.Plot.SetAxisLimits(Math.Log10(frequncyXMin), Math.Log10(frequncyXMax), phaseMin, phaseMax, 0, 3);
                Match(Source.Current);
            }

            /// <summary>
            // 특정 영역으로 축을 설정.
            /// </summary>
            /// <param name="standardSource"></param>
            private void Match(Source standardSource = Source.Current)
            {
                if (standardSource == Source.Current)
                {
                    FRAPlot.formsPlot.Plot.SetAxisLimits(FRAPlot.formsPlot.Plot.GetAxisLimits(0, 2), 0, 4);
                    FRAPlot.formsPlot.Plot.SetAxisLimits(FRAPlot.formsPlot.Plot.GetAxisLimits(0, 3), 0, 5);
                }
                else
                {
                    FRAPlot.formsPlot.Plot.SetAxisLimits(FRAPlot.formsPlot.Plot.GetAxisLimits(0, 4), 0, 2);
                    FRAPlot.formsPlot.Plot.SetAxisLimits(FRAPlot.formsPlot.Plot.GetAxisLimits(0, 5), 0, 3);
                }
            }

            /// <summary>
            /// 특정 Line의 색과 보임여부를 설정.
            /// </summary>
            /// <param name="select"></param>
            /// <param name="source"></param>
            /// <param name="color"></param>
            /// <param name="isvisible"></param>
            public void Line(DataType select, Source source, Color? color, bool? isvisible)
            {
                if (color != null)
                {
                    if (source == Source.Current)
                    {
                        FRAPlot.Current.LineColor(select, (Color)color);
                    }
                    else
                    {
                        FRAPlot.Reference.LineColor(select, (Color)color);
                    }

                }

                if (isvisible != null)
                {
                    if (source == Source.Current)
                    {
                        FRAPlot.Current.LineDisplay(select, (bool)isvisible);
                    }
                    else
                    {
                        FRAPlot.Reference.LineDisplay(select, (bool)isvisible);
                    }
                }
                FRAPlot.UpdatePlot();

            }
            /// <summary>
            /// 특정 축의 색과 보임여부를 설정.
            /// </summary>
            /// <param name="select"></param>
            /// <param name="source"></param>
            /// <param name="color"></param>
            /// <param name="isvisible"></param>
            public void Axis(DataType select, Source source, Color? color, bool? isvisible)
            {
                if (color != null)
                {
                    if (source == Source.Current)
                    {
                        FRAPlot.Current.AxisColor(select, (Color)color);
                    }
                    else
                    {
                        FRAPlot.Reference.AxisColor(select, (Color)color);
                    }

                }

                if (isvisible != null)
                {
                    if (source == Source.Current)
                    {
                        FRAPlot.Current.AxisDisplay(select, (bool)isvisible);
                    }
                    else
                    {
                        FRAPlot.Reference.AxisDisplay(select, (bool)isvisible);
                    }
                }
                FRAPlot.UpdatePlot();
            }
        }
    }

}
