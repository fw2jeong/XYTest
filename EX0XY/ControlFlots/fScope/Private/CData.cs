using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace ControlFlots.fScope.Private
{
    public partial class CData
    {
        CScopePlot_Private ScopePlot;

        public DisplayMode mode;
        public ModeFlag Flag;
        public CTrigger Trigger;
        int dataLen = 0;
        int storageLen = 0 ;
        private bool[] TriggerChkBit = new bool[6];


        public CData(CScopePlot_Private ScopePlot)
        {
            this.ScopePlot = ScopePlot;
            Flag = new ModeFlag();
            mode = DisplayMode.Roll;
            for (int i = 0; i < 6;i++ )
            {
                TriggerChkBit[i] = false;
            }

            Trigger = new CTrigger(ScopePlot);
            
        }
        public PLOT_STATUS Input(CH ch, double[] data = null)
        {
            storageLen = ScopePlot.arraySize;
            dataLen = data.Length;

            if (storageLen <= dataLen)
            {
                Trace.WriteLine("inputData over flow");
                return PLOT_STATUS.PT_OVERFLOW;
            }   
            

            if (mode == DisplayMode.Roll)
            {
                ScopePlot.Current.chHandler[(int)ch].MinRenderIndex = 0;

                //트리거 검사 ok 
                if(Flag.triggerSensing && (ch == Trigger.GetIndexY()))
                {
                    TriggerSensing(data,ch);
                }

                TriggerChkBit[(int)ch] = false;
                //// 다음 데이터를 받으면 오버플로우가 일어나는지 확인
                if (ScopePlot.Current.index[(int)ch] + dataLen > storageLen)
                {

                    //데이터를 초기화
                    Array.Clear(ScopePlot.Current.TempChData[(int)ch], 0, ScopePlot.Info.GetArraySize());
                    ScopePlot.Current.index[(int)ch] = 0;
                    if (Flag.pause1CycleFlag)
                    {
                        TriggerChkBit[(int)ch] = true;
                    }
                    
                }
                
                //입력데이터를 저장
                Array.Copy(data, 0, ScopePlot.Current.TempChData[(int)ch], ScopePlot.Current.index[(int)ch], dataLen);
            
                //현재 저장된 데이터의 갯수를 업데이트
                ScopePlot.Current.index[(int)ch] += dataLen;
                // 다음 데이터를 받으면 오버플로우가 일어나는지 확인
                
                

              
            }
            else if(mode  == DisplayMode.Normal)
            {
               

                for (int i = 0; i < storageLen - dataLen; i++)
                {
                    ScopePlot.Current.TempChData[(int)ch][i] = ScopePlot.Current.TempChData[(int)ch][i + dataLen];
                }
                for (int i = 0; i < dataLen; i++)
                {
                    ScopePlot.Current.TempChData[(int)ch][i + storageLen - dataLen] = data[i];
                }
            }

            VsyncUpdate(ch);

            return PLOT_STATUS.PT_OK;
        }

        public void SaveInReference(CH currenctCh, CH referenceCh)
        {
            ScopePlot.Reference.chHandler[(int)referenceCh].MinRenderIndex = ScopePlot.Current.chHandler[(int)currenctCh].MinRenderIndex;
            ScopePlot.Reference.chHandler[(int)referenceCh].MaxRenderIndex = ScopePlot.Current.chHandler[(int)currenctCh].MaxRenderIndex;
            ScopePlot.Reference.scale[(int)referenceCh] = ScopePlot.Current.scale[(int)currenctCh];
            ScopePlot.Reference.offset[(int)referenceCh] = ScopePlot.Current.offset[(int)currenctCh];


            Array.Copy(ScopePlot.Current.chData[(int)currenctCh], ScopePlot.Reference.chData[(int)referenceCh], ScopePlot.Current.chData[(int)currenctCh].Length);

            ScopePlot.formsPlot.Plot.SetAxisLimits(ScopePlot.formsPlot.Plot.GetAxisLimits(0, (int)currenctCh + 2), xAxisIndex: 0, yAxisIndex: (int)referenceCh + 2 + 6);

        }



        public void TempClear(CH ch)
        {
            ScopePlot.Reference.index[(int)ch] = 0;
            Array.Clear(ScopePlot.Current.TempChData[(int)ch], 0, ScopePlot.Current.TempChData[(int)ch].Length);
        }
        public void ChClear(CH ch)
        {
            ScopePlot.Current.index[(int)ch] = 0;
            Array.Clear(ScopePlot.Current.chData[(int)ch], 0, ScopePlot.Current.chData[(int)ch].Length);
        }
      
        private void VsyncUpdate(CH ch)
        {
            if (ScopePlot.Current.chHandler[(int)ch].IsVisible)
            {
                if (!Flag.syncLock)
                {

                    if (mode == DisplayMode.Roll)
                    {
                        if (Flag.triggerRepeat)
                        {

                            if (((TriggerChkBit[(int)ch])) && Flag.pause1CycleFlag)
                            {
                                Flag.pause[(int)ch] = true;
                                ScopePlot.ControlPanelForm.LblState(false);
                                ScopePlot.ControlPanelForm.LblState(false);
                                Flag.first = true;
                            }

                            if (!Flag.first)
                            {
                                if (!Flag.pause[(int)ch])
                                {
                                    if (ScopePlot.Current.index[(int)ch] == 0)
                                    {
                                        ScopePlot.Current.chHandler[(int)ch].MaxRenderIndex = 0;
                                    }
                                    else
                                    {
                                        ScopePlot.Current.chHandler[(int)ch].MaxRenderIndex = ScopePlot.Current.index[(int)ch] - 1;
                                    }
                                    Array.Copy(ScopePlot.Current.TempChData[(int)ch], ScopePlot.Current.chData[(int)ch], storageLen);
                                }
                            }
                            else
                            {
                                if (!Flag.pause[(int)ch])
                                {
                                    if (ScopePlot.Current.index[(int)ch] + dataLen > storageLen)
                                    {

                                        ScopePlot.Current.chHandler[(int)ch].MaxRenderIndex = ScopePlot.Current.index[(int)ch] - 1;

                                        Array.Copy(ScopePlot.Current.TempChData[(int)ch], ScopePlot.Current.chData[(int)ch], storageLen);

                                    }
                                }
                            }


                        }
                        else
                        {
                            if ((TriggerChkBit[(int)ch]) && Flag.pause1CycleFlag)
                            {
                                Flag.pause[(int)ch] = true;
                                ScopePlot.ControlPanelForm.LblState(false);
                            }


                            if (!Flag.pause[(int)ch])
                            {
                                if (ScopePlot.Current.index[(int)ch] == 0)
                                {
                                    ScopePlot.Current.chHandler[(int)ch].MaxRenderIndex = 0;
                                }
                                else
                                {
                                    ScopePlot.Current.chHandler[(int)ch].MaxRenderIndex = ScopePlot.Current.index[(int)ch] - 1;
                                }
                                Array.Copy(ScopePlot.Current.TempChData[(int)ch], ScopePlot.Current.chData[(int)ch], storageLen);
                            }

                        }
                    }
                    else if(mode == DisplayMode.Normal)
                    {
                        ScopePlot.Current.chHandler[(int)ch].MinRenderIndex = 0;
                        ScopePlot.Current.chHandler[(int)ch].MaxRenderIndex = storageLen - 1;
                        Array.Copy(ScopePlot.Current.TempChData[(int)ch], ScopePlot.Current.chData[(int)ch], ScopePlot.Current.TempChData[(int)ch].Length);
                    }

                }
                    
            }
        }

        /// <summary>
        /// 데이터 입력방식 중 Normal 방식과 Roll을 선택합니다.
        /// </summary>
        /// <param name="mode">Normal,Roll</param>
        public void ModeChange(DisplayMode mode)
        {
            this.mode = mode;
        }
        //Temp -> Display 의 데이터 이동을 즉각 멈춥니다. 
        public void Pause(bool flag)
        {
            //Flag.pause[(int)ch] = flag;
            Flag.syncLock = flag;
        }


        public void TriggerSingle()
        {
            Pause_1cycle(false);
            Flag.trigger = true;
            Flag.triggerSensing = true;
            Flag.triggerRepeat = false;
            Trigger.SetVisible(true);
        }
        public void TriggerRpt()
        {

            Pause_1cycle(false);
            Flag.trigger = true;
            Flag.triggerSensing = true;
            Flag.triggerRepeat = true;
            Trigger.SetVisible(true);
        }

        public void TriggerOff()
        {
            Flag.trigger = false;
            Flag.triggerSensing = false;
            Flag.triggerRepeat = false;
            Flag.pause1CycleFlag = false;
            Flag.first = false;

            for (int i = 0; i < 6; i++)
            {
                Flag.pause[i] = false;
            }


        }

        private void Pause_1cycle(bool pause)
        {
            Flag.pause1CycleFlag = pause;
            for (int i = 0; i < 6; i++)
            {
                Flag.pause[i] = false;
            }
            
        }
       



        private void TriggerSensing(double[] inputData,CH ch)
        {
            int InputLen = inputData.Length;
            double triggerY =  Trigger.GetY();


            if (Flag.trigger)
            {
                if (Flag.upTrigger)
                {
                    for (int i = 1; i < InputLen - 1; i++)
                    {
                        if ((inputData[i - 1] <= triggerY) && (inputData[i + 1] >= triggerY))
                        {
                            if(!Flag.triggerRepeat)
                            {
                                Flag.trigger = false;
                            
                            }
                            Pause_1cycle(true);

                            break;
                        }
                    }
                }
                else if (Flag.downTrigger)
                {
                    for (int i = 1; i < InputLen - 1; i++)
                    {
                        if ((inputData[i - 1] >= triggerY) && (inputData[i + 1] <= triggerY))
                        {
                            if (!Flag.triggerRepeat)
                            {
                                Flag.trigger = false;

                            }

                            Pause_1cycle(true);

                            break;
                        }
                    }
                }
            }
        }

        #region 미사용 함수
        /// <summary>
        /// shift 가 양이면 오른쪽 음이면 왼쪽으로 쉬프트
        /// </summary>
        /// <param name="array"></param>
        /// <param name="shift"></param>
        public void Shift(int shift,CH ch)
        {
            //int arrayLen = array.Length;
            int TempLen = ScopePlot.Current.TempChData[(int)ch].Length;

            ScopePlot.Current.index[(int)ch] += shift;

            if(shift >0)
            {
                for (int i = 0; i < TempLen - shift; i++)
                {
                    ScopePlot.Current.TempChData[(int)ch][i] = ScopePlot.Current.TempChData[(int)ch][i + shift];
                }
            }
            else if(shift <0)
            {
                shift = -shift;
                for (int i = TempLen - 1; i < shift + 1; i--)
                {
                    ScopePlot.Current.TempChData[(int)ch][i] = ScopePlot.Current.TempChData[(int)ch][i - shift];
                }
            }


        }

        public void LShift(CH ch,int shift)
        {
            int TempLen = ScopePlot.Current.TempChData[(int)ch].Length;
            
            ScopePlot.Current.index[(int)ch] -= shift;
            if (ScopePlot.Current.index[(int)ch] <=0)
            {
                ScopePlot.Current.index[(int)ch] = 0;
            }

            for (int i = TempLen -1; i < shift + 1; i--)
            {
                ScopePlot.Current.TempChData[(int)ch][i] = ScopePlot.Current.TempChData[(int)ch][i - shift];
                ScopePlot.Current.TempChData[(int)ch][i - shift] = 0;
            }

          
        }

        public void RShift(CH ch,int shift)
        {
            int TempLen = ScopePlot.Current.TempChData[(int)ch].Length;

            ScopePlot.Current.index[(int)ch] += shift;

            if (ScopePlot.Current.index[(int)ch] >= 0)
            {
                ScopePlot.Current.index[(int)ch] = TempLen;
            }
            for (int i = 0; i < TempLen - shift; i++)
            {
                ScopePlot.Current.TempChData[(int)ch][i + shift] = ScopePlot.Current.TempChData[(int)ch][i ];
                ScopePlot.Current.TempChData[(int)ch][i ] = 0;
            }

        }

        #endregion 

    }

   

    /// <summary>
    /// Trigger Flag 처리를 위한 클래스
    /// </summary>
    public class ModeFlag
    {
        public bool syncLock = false;
        
        public bool pause1CycleFlag =false;

        public bool[] pause = new bool[6];
        public bool trigger;
        public bool triggerSensing;
        
        public bool upTrigger;
        public bool downTrigger;

        public double triggerY;


        public bool first = false;

        public bool triggerRepeat;


        public ModeFlag()
        {
            for (int i = 0; i < 6;i++ )
            {
                pause[i] = false;
            }
            trigger = true;
            triggerSensing = false;
            upTrigger = false;  
            downTrigger = false;
            triggerY = 0;
            triggerRepeat = false;

        }
    }
}
