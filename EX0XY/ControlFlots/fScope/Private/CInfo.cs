using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFlots.fScope.Private
{
    public class CInfo
    {
        CScopePlot_Private ScopePlot;
        ScottPlot.Plottable.Annotation InfoAnnotation;

        public CInfo(CScopePlot_Private ScopePlot)
        {
            this.ScopePlot = ScopePlot;
            InfoAnnotation = new ScottPlot.Plottable.Annotation
            {
                Background = true,
                Shadow =false,
                IsVisible = true,
                XAxisIndex = 0,
                YAxisIndex = 0,
                
            };
            InfoAnnotation.Font.Size = 10;
            InfoAnnotation.Font.Bold = true;

            ScopePlot.formsPlot.Plot.Add(InfoAnnotation);

        }
        #region 왼쪽 위 상단 Scale,Offset 주석 처리 
        public string InfoNowString()
        {
           
            string total = "";

            total += "CH".PadRight(4);
            total += "| Source".PadRight(15);
            total += "| Scale".PadRight(12);
            total += "| " + "Offset".PadRight(8);

            for (int i = 0; i < 6; i++)
            {
                if (ScopePlot.Current.chHandler[i].IsVisible)
                {
                    total += "\n" + Convert.ToString((CH)i).PadRight(4) + "| Current".PadRight(15)
                        + "| " + Convert.ToString(ScopePlot.Current.GetScale((CH)i)).PadRight(12)
                        + "| " + Convert.ToString(ScopePlot.Current.GetOffset((CH)i)).PadRight(8);
                }
            }
            for (int i = 0; i < 6; i++)
            {
                if (ScopePlot.Reference.chHandler[i].IsVisible)
                {
                    total += "\n" + Convert.ToString((CH)i).PadRight(4) + "| Reference".PadRight(15)
                        + "| " + Convert.ToString(ScopePlot.Reference.GetOffset((CH)i)).PadRight(12)
                        + "| " + Convert.ToString(ScopePlot.Reference.GetOffset((CH)i)).PadRight(8);
                }
            }
            InfoAnnotation.Label = total;

            return total;
        }
        /// <summary>
        /// 외부에서 왼쪽 위 주석을 온오프합니다. 
        /// </summary>
        /// <param name="isVisible"></param>
        public void EnableAnnotation(bool isVisible)
        {
             
            InfoAnnotation.IsVisible = isVisible;

        }

        public void AfterClearAnnotation()
        {
            ScopePlot.formsPlot.Plot.Add(InfoAnnotation);
        }

        #endregion


        public void EnableAnnotation()
        {

        }

        public double GetTimeScale() { return ScopePlot.timeScale; }
        public double GetFrequncy() { return ScopePlot.frequency; }
        public int GetArraySize() { return ScopePlot.arraySize; }


        /// <summary>
        /// 시간 데이터를 출력합니다.
        /// </summary>
        /// <returns></returns>
        public double[] GetTime() { return ScopePlot.time; }
        /// <summary>
        /// (영역,채널)에 해당하는 데이터를 출력합니다.
        /// </summary>
        /// <param name="ch">채널 선택</param>
        /// <param name="source">영역 선택</param>
        /// <returns>리턴 데이터</returns>
        public double[] GetData(CH ch, Source source)
        {
            double[] result;
            result = (source == Source.Current) ? ScopePlot.Current.GetData(ch) : ScopePlot.Reference.GetData(ch);

            return result;

        }



    }

}
