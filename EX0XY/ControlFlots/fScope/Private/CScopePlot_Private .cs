using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ScottPlot;
using System.Windows.Forms;
using System.Diagnostics;
using ControlFlots;
////
//// V1.4.0 22.02.15 최신 버전 배포
//// 디자인 패턴 : Pacade Pattern 사용 
////
namespace ControlFlots.fScope.Private
{
    


    public class CScopePlot_Private
    {
        public double frequency;
        public double timeScale;
        public int arraySize;
        public double timeInterval;

        public double[] time = new double[InitValue.VOLUME];

        
        public ScottPlot.FormsPlot formsPlot;

        public CScopeHandler Current;
        public CScopeHandler Reference;

        public CData Data;
        public CInfo Info;

        public ControlPanelForm ControlPanelForm;

        public CScopePlot_Private(ScottPlot.FormsPlot formsPlot)
        {
            formsPlot.Reset();

            this.formsPlot = formsPlot;

            for (int i = 0; i < time.Length; i++)
            {
                time[i] = i;
            }
            
            Current = new CScopeHandler(this,formsPlot, Source.Current);
            Reference = new CScopeHandler(this,formsPlot, Source.Reference);

            Data = new CData(this);
            Info = new CInfo(this);

            formsPlot.Configuration.AllowDroppedFramesWhileDragging = false;
            formsPlot.Configuration.Zoom = false;
            formsPlot.Configuration.LockHorizontalAxis = true;
            formsPlot.Configuration.LeftClickDragPan = false;
            formsPlot.Configuration.RightClickDragZoom = false;
            formsPlot.Configuration.Quality = ScottPlot.Control.QualityMode.Low;
            formsPlot.Configuration.Pan = false;
            formsPlot.Configuration.DpiStretch = true;
            formsPlot.Configuration.UseRenderQueue = true;

            
            
            formsPlot.RightClicked += CustomRightClickEvent;
            Initialize();

            UpdatePlot();
        }

        #region 마우스 우 클릭 처리
        private void CustomRightClickEvent(object sender, EventArgs e)
        {
            ContextMenuStrip customMenu = new ContextMenuStrip();
            customMenu.Items.Add(new ToolStripMenuItem("Auto All Ch", null, new EventHandler(AddAuto)));
            customMenu.Items.Add(new ToolStripMenuItem("Adjust Data Setting Show", null, new EventHandler(AddSetting)));
            customMenu.Items.Add(new ToolStripMenuItem("Trigger Position to mouse point", null, new EventHandler(AddTrigger)));

            customMenu.Show(System.Windows.Forms.Cursor.Position);

        }
        private void AddTrigger(object sender, EventArgs e)
        {
            int TriggeIndex = (int)Data.Trigger.GetIndexY() + 2;
            Data.Trigger.SetPosition(formsPlot.GetMouseCoordinates(0, TriggeIndex).Item2);    //formsPlot.GetMouseCoordinates(0, TriggeIndex).Item1, 
            UpdatePlot();
        }
        private void AddSetting(object sender, EventArgs e)
        {
            Current.Storage.Save();
            Reference.Storage.Save();

            if (ControlPanelForm != null)
            {
                ControlPanelForm.Close();
            }
            ControlPanelForm = new ControlPanelForm(this);

            ControlPanelForm.Show();
            ControlPanelForm.BringToFront();
        }

        private void AddAuto(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                if (Current.chHandler[i].IsVisible)
                {
                    Current.Display.Auto((CH)i);
                }
                if (Reference.chHandler[i].IsVisible)
                {

                    Reference.Display.Auto((CH)i);
                }

            }
           
            SubFormUpdate();  

            UpdatePlot();
        }

        private void SubFormUpdate()
        {
            if (ControlPanelForm != null)
            {
                ControlPanelForm.Initialize();
            }
        }
        #endregion


        private void Initialize(TimeScale.GridInv initGridIv = TimeScale.GridInv._10ms, TimeScale.Mode InitMode = TimeScale.Mode.Mode1)
        {
            //formsPlot.Plot.YAxis.IsVisible = true;
            //Test 환경 
            var bnColor = System.Drawing.ColorTranslator.FromHtml("#2e3440");
            formsPlot.Plot.Style(figureBackground: bnColor, dataBackground: bnColor);
            formsPlot.Plot.Palette = ScottPlot.Palette.OneHalfDark;

            formsPlot.Plot.XLabel("Time[ms]");
            formsPlot.Plot.XAxis.MajorGrid(true, Color.Black, null, LineStyle.Dash);
            formsPlot.Plot.XAxis.Color(Color.White);

            //X축 라벨링 및 그리드 설정
            formsPlot.Plot.YAxis.MajorGrid(true, Color.Gray, null, lineStyle: LineStyle.Dash);
            formsPlot.Plot.YAxis.ManualTickSpacing(TimeScale.InitAxisMag / 5);
            formsPlot.Plot.YAxis.Grid(true);

            formsPlot.Plot.YAxis.Ticks(false);

            //Y축 라벨링 및 그리드 설정

            //NumberOfInputData = 10000;
            //GridInv = 100;
            ReDefine(100, 10000);
            //TimeScaleSetAndUpdate();
            //기본 Time Scale 설정

            //기준 축 설정    -10000 ~ 10000
            formsPlot.Plot.SetAxisLimits(yMin: -TimeScale.InitAxisMag, yMax: TimeScale.InitAxisMag);

            //서브 축 설정
            for (int i = 0; i < 12; i++)
            {
                formsPlot.Plot.SetAxisLimits(yMin: -TimeScale.InitAxisMag, yMax: TimeScale.InitAxisMag, yAxisIndex: 2 + i);
            }

            formsPlot.Plot.Legend();
            //범례 사용 

            Current.Storage.Save();
            Reference.Storage.Save();

            //Storage.Save();
            //기본 설정값 초기화 저장
        }


        private object lockObject = new object();

        bool updateChk = true;
        /// <summary>
        /// 차트를 업데이트 합니다.
        /// </summary>
        /// <param name="opt">최적화 여부 선택</param>
        public void UpdatePlot(bool opt = true)
        {

            if (updateChk)
            {
                Info.InfoNowString();
            
                lock(lockObject)
                { 
                    updateChk = false;
                }
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
                lock (lockObject)
                {
                    updateChk = true;
                }
            }

           
        }

        

        /// <summary>
        /// 내부 저장 배열 재 정의
        /// </summary>
        /// <param name="_timeScale"></param>
        /// Time 축 스케일 (ms 단위 1s = 1000ms) 
        /// <param name="_frequency"></param>
        /// 데이터의 주파수 (1초당 입력 데이터 수)
        public void ReDefine(double? _timeScale = null, double? _frequency = null)
        {
            Current.Storage.Save();
            Reference.Storage.Save();
            Data.Trigger.Save();
            formsPlot.Plot.Clear();
            for (int i = 0; i < 6;i++ )
            {
                Current.index[i] = 0;
                Reference.index[i] = 0;
                Current.chHandler[i].MinRenderIndex = 0;
                Current.chHandler[i].MaxRenderIndex = 0;
                Reference.chHandler[i].MinRenderIndex = 0;
                Reference.chHandler[i].MaxRenderIndex = 0;

            }

            timeScale = (_timeScale != null) ? (double)_timeScale : timeScale;
            frequency = (_frequency != null) ? (double)_frequency : frequency;

            //그리드 표시 적용
            formsPlot.Plot.XAxis.ManualTickSpacing((double)timeScale);


            timeInterval = 1 / (double)frequency * 1000;
            arraySize = (int)(((double)timeScale * (double)10 * (double)frequency) / 1000);

            double[] tempDouble = time;
            Array.Resize(ref tempDouble, arraySize);
            time = tempDouble;

            for (int i = 0; i < arraySize; i++)
            {
                time[i] = i * timeInterval;
            }

            for (int i = 0; i < 6; i++)
            {
                string s;
                
               
                s = "Current_" + Convert.ToString((CH)i);

                tempDouble = Current.chData[i];
                Array.Resize(ref tempDouble, arraySize);
                Current.chData[i] = tempDouble;

                tempDouble = Current.TempChData[i];
                Array.Resize(ref tempDouble, arraySize);
                Current.TempChData[i] = tempDouble;
                


                Current.chHandler[i] = formsPlot.Plot.AddSignalXY(time, Current.chData[i], label: s);
                Current.chHandler[i].XAxisIndex = 0;
                Current.chHandler[i].YAxisIndex = i + 2 ;
                Current.yAxis[i].IsVisible = false;
                Current.chHandler[i].IsVisible = false;


                s = "Reference_" + Convert.ToString((CH)i);

                tempDouble = Reference.chData[i];
                Array.Resize(ref tempDouble, arraySize);
                Reference.chData[i] = tempDouble;

                tempDouble = Reference.TempChData[i];
                Array.Resize(ref tempDouble, arraySize);
                Reference.TempChData[i] = tempDouble;


                Reference.chHandler[i] = formsPlot.Plot.AddSignalXY(time, Reference.chData[i], label: s);
                Reference.chHandler[i].XAxisIndex = 0;
                Reference.chHandler[i].YAxisIndex = i + 2 + 6 ;
                Reference.yAxis[i].IsVisible = false;
                Reference.chHandler[i].IsVisible = false;
            }

            double axisMin = 0;
            double axisMax = arraySize * (timeInterval);
            formsPlot.Plot.SetAxisLimits(xMin: axisMin, xMax: axisMax);

            Current.Storage.Load();
            Reference.Storage.Load();
            Data.Trigger.Load();
            Info.AfterClearAnnotation();

            UpdatePlot();

        }
        public void EnableLable(bool isVisible)
        {
            formsPlot.Plot.Legend(isVisible);
        }


    }
      


}