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
        /// 차트에 입력된 데이터의 정보를 리턴. 
        /// </summary>
        public class CInfo
        {
            CFRA FRAPlot;

            public CInfo(CFRA FRAPlot)
            {
                this.FRAPlot = FRAPlot;
            }


            /// <summary>
            /// 차트에 저장되어 있는 주파수 데이터 배열을 리턴
            /// </summary>
            /// <param name="source"></param>
            /// <returns></returns>
            public double[] Frequency(Source source = Source.Current)
            {
                double[] output = new double[1];
                if (source == Source.Current)
                {
                    output = FRAPlot.Current.frequency;
                }
                else
                {
                    output = FRAPlot.Reference.frequency;
                }
                return output;
            }

            /// <summary>
            /// 차트에 저장되어 있는 크기 데이터 배열을 리턴
            /// </summary>
            /// <param name="source"></param>
            /// <returns></returns>
            public double[] Magnitude(Source source = Source.Current)
            {
                double[] output = new double[1];
                if (source == Source.Current)
                {
                    output = FRAPlot.Current.magnitude;
                }
                else
                {
                    output = FRAPlot.Reference.magnitude;
                }
                return output;
            }

            /// <summary>
            /// 차트에 저장되어 있는 위상 데이터 배열을 리턴
            /// </summary>
            /// <param name="source"></param>
            /// <returns></returns>
            public double[] Phase(Source source = Source.Current)
            {
                double[] output = new double[1];
                if (source == Source.Current)
                {
                    output = FRAPlot.Current.phase;
                }
                else
                {
                    output = FRAPlot.Reference.phase;
                }
                return output;
            }
        }
    }

}
