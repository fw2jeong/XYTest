using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ControlFlots;
namespace ControlFlots.fXY
{

    public partial class CXY
    {
        /// <summary>
        /// 데이터 처리를 위한 기능을 모은 클래스.
        /// </summary>
        public class CData
        {
            CXY XYPlot;
            XYHandler Up;
            XYHandler Down;

            public CData(CXY XYPlot)
            {
                this.XYPlot = XYPlot;
                Up = XYPlot.Up;
                Down = XYPlot.Down;
            }

            /// <summary>
            /// 데이터를 입력합니다. 한번에 (채널,Source)에 2000개의 데이터를 넣을 수 있다.
            /// 그 이상의 데이터가 들어오면 PLOT_OVERFLOW 리턴.
            /// </summary>
            /// <param name="ch">채널 선택</param>
            /// <param name="X">X 값</param>
            /// <param name="Y">Y 값</param>
            /// <param name="source">Up,Down 영역을 구분하기 위한 열거형</param>
            public PLOT_STATUS Input(CH ch, Source source, double X, double Y)
            {
                int index;

                if (source == Source.Current)
                {
                    index = Up.index[(int)ch];
                    if (Up.xArray[(int)ch].Length < index)
                    {
                        return PLOT_STATUS.PT_OVERFLOW;
                    }
                    Up.xArray[(int)ch][index] = X;
                    Up.yArray[(int)ch][index] = Y;
                    Up.index[(int)ch]++;
                }
                else
                {
                    
                    index = Down.index[(int)ch];
                    if (Down.xArray[(int)ch].Length < index)
                    {
                        return PLOT_STATUS.PT_OVERFLOW;
                    }
                    Down.xArray[(int)ch][index] = X;
                    Down.yArray[(int)ch][index] = Y;
                    Down.index[(int)ch]++;
                }
                return PLOT_STATUS.PT_OK;
            }
            /// <summary>
            /// 특정 채널과 영역에 저장된 데이터를 삭제.
            /// </summary>
            /// <param name="ch">채널 선택</param>
            /// <param name="source">영역을 구분하기 위한 열거형 null 입력시 Up,Down 모두 클리어</param>
            public void Clear(CH ch, Source? source = null)
            {
                if (source == null)
                {
                    Up.index[(int)ch] = 0;
                    Down.index[(int)ch] = 0;
                    Array.Clear(Up.xArray[(int)ch], 0, Up.xArray[(int)ch].Length);
                    Array.Clear(Up.yArray[(int)ch], 0, Up.yArray[(int)ch].Length);
                    Array.Clear(Down.xArray[(int)ch], 0, Down.xArray[(int)ch].Length);
                    Array.Clear(Down.yArray[(int)ch], 0, Down.yArray[(int)ch].Length);
                }
                else
                {
                    if ((Source)source == Source.Current)
                    {
                        Up.index[(int)ch] = 0;
                        Array.Clear(Up.xArray[(int)ch], 0, Up.xArray[(int)ch].Length);
                        Array.Clear(Up.yArray[(int)ch], 0, Up.yArray[(int)ch].Length);
                    }
                    else
                    {
                        Down.index[(int)ch] = 0;
                        Array.Clear(Down.xArray[(int)ch], 0, Down.xArray[(int)ch].Length);
                        Array.Clear(Down.yArray[(int)ch], 0, Down.yArray[(int)ch].Length);
                    }
                }
            }
        }
    }

}