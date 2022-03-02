using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlFlots.fFRA
{
    public partial class CFRA
    {
        /// <summary>
        /// 차트의 추가적인 기능을 모아놓은 클래스.
        /// </summary>
        public partial class CTool
        {
            CFRA FRAPlot;
            public CMouse Mouse;
            public CTool(CFRA _FRAPlot)
            {
                FRAPlot = _FRAPlot;
                Mouse = new CMouse(FRAPlot);
            }

            /// <summary>
            /// Current 영역에 있는 FRA데이터를 Reference영역으로 이동.
            /// </summary>
            public void SaveInReference()
            {
                int Temp = FRAPlot.Current.dataIndex;

                FRAPlot.formsPlot.Plot.SetAxisLimits(FRAPlot.formsPlot.Plot.GetAxisLimits(0, 2), 0, 4);
                FRAPlot.formsPlot.Plot.SetAxisLimits(FRAPlot.formsPlot.Plot.GetAxisLimits(0, 3), 0, 5);

                FRAPlot.Reference.dataIndex = Temp - 1;
                Array.Copy(FRAPlot.Current.frequency, FRAPlot.Reference.frequency, Temp);
                Array.Copy(FRAPlot.Current.magnitude, FRAPlot.Reference.magnitude, Temp);
                Array.Copy(FRAPlot.Current.phase, FRAPlot.Reference.phase, Temp);
                Array.Copy(FRAPlot.Current.LogFrequency, FRAPlot.Reference.LogFrequency, Temp);
                FRAPlot.Reference.MagScott.MaxRenderIndex = Temp - 1;
                FRAPlot.Reference.PhaScott.MaxRenderIndex = Temp - 1;

            }

            /// <summary>
            /// 마우스와 관련된 기능을 제공하는 클래스
            /// </summary>
            public class CMouse
            {
                CFRA FRAPlot;

                private ScottPlot.Plottable.Crosshair CrossHair;
                private double LastHighlightedIndex = -1;

                private ScottPlot.Plottable.Annotation annotationHandler;

                public CMouse(CFRA FRAPlot)
                {
                    this.FRAPlot = FRAPlot;
                    InitDottedLine();
                    FRAPlot.formsPlot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UpdateDottedLine);
                    FRAPlot.formsPlot.MouseEnter += new System.EventHandler(this.DottedLineEnter);
                    FRAPlot.formsPlot.MouseLeave += new System.EventHandler(this.DottedLineLeave);

                    annotationHandler = FRAPlot.formsPlot.Plot.AddAnnotation("init", 10, 10);
                    annotationHandler.XAxisIndex = 0;
                    annotationHandler.YAxisIndex = 2;
                    annotationHandler.IsVisible = false;

                }
                
                bool DottedLineEnable = false;
                /// <summary>
                /// 점선을 사용할 것인지 여부를 정합니다. 
                /// 화면에 마우스를 올릴시 점선이 보입니다. 점선의 위치는 화면 위 데이터에 맞추어지며,  
                /// 왼쪽 위에 Bode에 데이터의 값을 출력해줍니다.
                /// </summary>
                /// <param name="on"></param>
                public void DottedLine(bool on)
                {
                    DottedLineEnable = on;
                    if (on == false)
                    {
                        CrossHair.IsVisible = false;
                    }
                }
                /// <summary>
                /// 마우스가 위치한 곳의 주파수 데이터를 리턴
                /// </summary>
                /// <param name="source"></param>
                /// <returns></returns>
                public double ReadFrequency(Source source = Source.Current)
                {
                    if (source == Source.Current)
                    {
                        return FRAPlot.Current.frequency[ReadIndex()];
                    }
                    else
                    {
                        return FRAPlot.Reference.frequency[ReadIndex()];
                    }
                }

                /// <summary>
                /// 마우스가 위치한 곳의 크기 데이터를 리턴
                /// </summary>
                /// <param name="source"></param>
                /// <returns></returns>
                public double ReadMagnitude(Source source = Source.Current)
                {
                    if (source == Source.Current)
                    {
                        return FRAPlot.Current.magnitude[ReadIndex()];
                    }
                    else
                    {
                        return FRAPlot.Reference.magnitude[ReadIndex()];
                    }
                }
                /// <summary>
                /// 마우스가 위한 곳의 위상 데이터를 리턴
                /// </summary>
                /// <param name="source"></param>
                /// <returns></returns>
                public double ReadPhase(Source source = Source.Current)
                {
                    if (source == Source.Current)
                    {
                        return FRAPlot.Current.phase[ReadIndex()];
                    }
                    else
                    {
                        return FRAPlot.Reference.phase[ReadIndex()];
                    }
                }
                #region   private 
                /// <summary>
                /// 점선을 사용할거면 호출해주어야합니다. 내부 호출
                /// </summary>
                private void InitDottedLine()
                {
                    CrossHair = FRAPlot.formsPlot.Plot.AddCrosshair(0, 0);
                    CrossHair.HorizontalLine.IsVisible = false;
                    CrossHair.IsVisible = false;
                    CrossHair.PositionLabel = false;
                }
                /// <summary>
                /// 마우스가 가르키는 배열의 인덱스 값을 리턴
                /// </summary>
                /// <returns></returns>
                private int ReadIndex()
                {
                    double temp1 = FRAPlot.formsPlot.GetMouseCoordinates().Item1;
                    double temp2 = FRAPlot.Current.MagScott.GetPointNearestX(temp1).Item1;
                    if (temp2 == 0)
                    {
                        temp2 = FRAPlot.Reference.MagScott.GetPointNearestX(temp1).Item1;
                    }

                    int index = Array.IndexOf(FRAPlot.Current.LogFrequency, temp2);
                    if (index == -1)
                    {
                        index = 0;
                    }
                    if (index == 0)
                    {
                        index = Array.IndexOf(FRAPlot.Reference.LogFrequency, temp2);
                    }
                    if (index == -1)
                    {
                        index = 0;
                    }

                    return index;
                }
                private void DottedLineEnter(object sender, EventArgs e)
                {
                    if (DottedLineEnable == false)
                        return;
                    annotationHandler.IsVisible = true;
                    CrossHair.IsVisible = true;
                    FRAPlot.UpdatePlot();
                }

                private void DottedLineLeave(object sender, EventArgs e)
                {
                    if (DottedLineEnable == false)
                        return;
                    annotationHandler.IsVisible = false;
                    CrossHair.IsVisible = false;
                    FRAPlot.UpdatePlot();
                }

                private void UpdateDottedLine(object sender, MouseEventArgs e)
                {
                    if (LastHighlightedIndex != ReadIndex())
                    {
                        CrossHair.X = FRAPlot.Current.LogFrequency[ReadIndex()];
                        if (CrossHair.X == 0)
                        {
                            CrossHair.X = FRAPlot.Reference.LogFrequency[ReadIndex()];
                        }
                        AnnotationUpdate();
                        LastHighlightedIndex = ReadIndex();

                        FRAPlot.UpdatePlot();
                    }
                }

                private void AnnotationUpdate()
                {
                    string att = "";
                    att = "frequency : " + Convert.ToString(ReadFrequency(Source.Current));
                    if (ReadFrequency(Source.Current) == 0)
                    {
                        att = "frequency : " + Convert.ToString(ReadFrequency(Source.Reference));
                    }

                    if (FRAPlot.Current.MagScott.IsVisible)
                    {
                        att += "\n" + "Current_Mag :" + Convert.ToString(ReadMagnitude(Source.Current));
                    }
                    if (FRAPlot.Current.PhaScott.IsVisible)
                    {
                        att += "\n" + "Current_Pha :" + Convert.ToString(ReadPhase(Source.Current));
                    }

                    if (FRAPlot.Reference.MagScott.IsVisible)
                    {
                        att += "\n" + "Reference_Mag :" + Convert.ToString(ReadMagnitude(Source.Reference));
                    }
                    if (FRAPlot.Reference.PhaScott.IsVisible)
                    {
                        att += "\n" + "Reference_Pha :" + Convert.ToString(ReadPhase(Source.Reference));
                    }

                    annotationHandler.Label = att;

                }
                #endregion

            }

        }
    }
}
