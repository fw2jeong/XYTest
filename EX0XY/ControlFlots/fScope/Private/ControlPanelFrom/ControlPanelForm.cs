using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

//22.02.09 LeeJeongHoon revision
namespace ControlFlots.fScope.Private
{
    public partial class ControlPanelForm : Form
    {
        CScopePlot_Private ScopePlot;
        bool InitBool = false;

        public ControlPanelForm(CScopePlot_Private _ScopePlot)
        {
            InitializeComponent();
            ScopePlot = _ScopePlot;

            #region  Initialize
            //
            chk_PlotDisplayMode.Checked = (ScopePlot.Data.mode == DisplayMode.Normal) ? false : true;

            // Horizotal Groupbox     

            // Trigger Gropbox;
            cbo_TriggerMode.SelectedIndex = 0;
            cbo_TriggerChannel.SelectedIndex = 0;


            //WaveFormSave Groupbox 
            cbo_WaveFormSave.SelectedIndex = 0;

            // Refernce Groupbox  :OK
            cbo_CurWaveform.SelectedIndex = 1;
            cbo_TargetRef.SelectedIndex = 2;


            // Waveform Groupbox 
            ManageCheckGroupBox(chk_VisibleCH1, grp_WaveformCH1);
            cbo_VertScaleCH1.SelectedIndex = 6;
            // 채널 별 추가
            ManageCheckGroupBox(chk_VisibleCH2, grp_WaveformCH2);
            cbo_VertScaleCH2.SelectedIndex = 6;
            ManageCheckGroupBox(chk_VisibleCH3, grp_WaveformCH3);
            cbo_VertScaleCH3.SelectedIndex = 6;
            ManageCheckGroupBox(chk_VisibleCH4, grp_WaveformCH4);
            cbo_VertScaleCH4.SelectedIndex = 6;
            ManageCheckGroupBox(chk_VisibleCH5, grp_WaveformCH5);
            cbo_VertScaleCH5.SelectedIndex = 6;
            ManageCheckGroupBox(chk_VisibleCH6, grp_WaveformCH6);
            cbo_VertScaleCH6.SelectedIndex = 6;
            //

            // Reference Waveform Groupbox 
            ManageCheckGroupBox(chk_RefVisibleCH1, grp_RefWaveformCH1);
            cbo_RefVertScaleCH1.SelectedIndex = 6;
            // 채널 별 추가
            ManageCheckGroupBox(chk_RefVisibleCH2, grp_RefWaveformCH2);
            cbo_RefVertScaleCH2.SelectedIndex = 6;
            ManageCheckGroupBox(chk_RefVisibleCH3, grp_RefWaveformCH3);
            cbo_RefVertScaleCH3.SelectedIndex = 6;
            ManageCheckGroupBox(chk_RefVisibleCH4, grp_RefWaveformCH4);
            cbo_RefVertScaleCH4.SelectedIndex = 6;
            ManageCheckGroupBox(chk_RefVisibleCH5, grp_RefWaveformCH5);
            cbo_RefVertScaleCH5.SelectedIndex = 6;
            ManageCheckGroupBox(chk_RefVisibleCH6, grp_RefWaveformCH6);
            cbo_RefVertScaleCH6.SelectedIndex = 6;
            //
            //현재 Normal 모드 는 비활성화
            //chk_PlotDisplayMode.Enabled = false;
            grp_Trigger.Enabled = chk_PlotDisplayMode.Checked;
            Initialize();

            InitBool = true;
            ScopePlot.UpdatePlot(true);
            #endregion
        }




        #region Initialize Form

        public void Initialize()
        {
            #region   Horizontal Scale 초기화 
            int temp = (int)ScopePlot.Info.GetTimeScale();
            switch (temp)
            {
                case 50:
                    cbo_HorizScale.SelectedIndex = 0;
                    break;
                case 100:
                    cbo_HorizScale.SelectedIndex = 1;
                    break;
                case 200:
                    cbo_HorizScale.SelectedIndex = 2;
                    break;
                case 500:
                    cbo_HorizScale.SelectedIndex = 3;
                    break;
                case 1000:
                    cbo_HorizScale.SelectedIndex = 4;
                    break;
                default:
                    break;
            }
            #endregion

            #region Virtical Offset 초기화 (Current,Reference) ch1~ch6
            nud_VertPosCH1.Value = (decimal)ScopePlot.Current.GetOffset(CH.ch1);
            nud_VertPosCH2.Value = (decimal)ScopePlot.Current.GetOffset(CH.ch2);
            nud_VertPosCH3.Value = (decimal)ScopePlot.Current.GetOffset(CH.ch3);
            nud_VertPosCH4.Value = (decimal)ScopePlot.Current.GetOffset(CH.ch4);
            nud_VertPosCH5.Value = (decimal)ScopePlot.Current.GetOffset(CH.ch5);
            nud_VertPosCH6.Value = (decimal)ScopePlot.Current.GetOffset(CH.ch6);

            //Reference nud Initialize
            nud_RefVertPosCH1.Value = (decimal)ScopePlot.Reference.GetOffset(CH.ch1);
            nud_RefVertPosCH2.Value = (decimal)ScopePlot.Reference.GetOffset(CH.ch2);
            nud_RefVertPosCH3.Value = (decimal)ScopePlot.Reference.GetOffset(CH.ch3);
            nud_RefVertPosCH4.Value = (decimal)ScopePlot.Reference.GetOffset(CH.ch4);
            nud_RefVertPosCH5.Value = (decimal)ScopePlot.Reference.GetOffset(CH.ch5);
            nud_RefVertPosCH6.Value = (decimal)ScopePlot.Reference.GetOffset(CH.ch6);
            
            #endregion

            #region   Virtical Scale 초기화 (Current,Reference) ch1~ch6
            for (int i = 0; i < 6; i++)
            {
                VirticalScaleIndexToComboBox((CH)i, Source.Current).SelectedIndex = VirticalScaleToIndex((CH)i, Source.Current);
                VirticalScaleIndexToComboBox((CH)i, Source.Reference).SelectedIndex = VirticalScaleToIndex((CH)i, Source.Reference);
            }
            #endregion

            #region Channel CheckBox 초기화 (Current,Reference) ch1~ch6
            chk_VisibleCH1.Checked = ScopePlot.Current.Display.Line.GetVisible(CH.ch1);
            chk_VisibleCH2.Checked = ScopePlot.Current.Display.Line.GetVisible(CH.ch2);
            chk_VisibleCH3.Checked = ScopePlot.Current.Display.Line.GetVisible(CH.ch3);
            chk_VisibleCH4.Checked = ScopePlot.Current.Display.Line.GetVisible(CH.ch4);
            chk_VisibleCH5.Checked = ScopePlot.Current.Display.Line.GetVisible(CH.ch5);
            chk_VisibleCH6.Checked = ScopePlot.Current.Display.Line.GetVisible(CH.ch6);

            chk_RefVisibleCH1.Checked = ScopePlot.Reference.Display.Line.GetVisible(CH.ch1);
            chk_RefVisibleCH2.Checked = ScopePlot.Reference.Display.Line.GetVisible(CH.ch2);
            chk_RefVisibleCH3.Checked = ScopePlot.Reference.Display.Line.GetVisible(CH.ch3);
            chk_RefVisibleCH4.Checked = ScopePlot.Reference.Display.Line.GetVisible(CH.ch4);
            chk_RefVisibleCH5.Checked = ScopePlot.Reference.Display.Line.GetVisible(CH.ch5);
            chk_RefVisibleCH6.Checked = ScopePlot.Reference.Display.Line.GetVisible(CH.ch6);
            #endregion

            #region Panel Color 초기화 (Current,Reference) ch1~ch6
            pnl_ColorCH1.BackColor = ScopePlot.Current.Display.Line.GetColor(CH.ch1);
            pnl_ColorCH2.BackColor = ScopePlot.Current.Display.Line.GetColor(CH.ch2);
            pnl_ColorCH3.BackColor = ScopePlot.Current.Display.Line.GetColor(CH.ch3);
            pnl_ColorCH4.BackColor = ScopePlot.Current.Display.Line.GetColor(CH.ch4);
            pnl_ColorCH5.BackColor = ScopePlot.Current.Display.Line.GetColor(CH.ch5);
            pnl_ColorCH6.BackColor = ScopePlot.Current.Display.Line.GetColor(CH.ch6);

            pnl_RefColorCH1.BackColor = ScopePlot.Reference.Display.Line.GetColor(CH.ch1);
            pnl_RefColorCH2.BackColor = ScopePlot.Reference.Display.Line.GetColor(CH.ch2);
            pnl_RefColorCH3.BackColor = ScopePlot.Reference.Display.Line.GetColor(CH.ch3);
            pnl_RefColorCH4.BackColor = ScopePlot.Reference.Display.Line.GetColor(CH.ch4);
            pnl_RefColorCH5.BackColor = ScopePlot.Reference.Display.Line.GetColor(CH.ch5);
            pnl_RefColorCH6.BackColor = ScopePlot.Reference.Display.Line.GetColor(CH.ch6);
            
            #endregion

        }
        #region Virtical Scale 초기화 처리를 위한 함수
        /// <summary>
        /// 입력한 채널의 스케일에 해당하는 Form인덱스를 가져옵니다.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private int VirticalScaleToIndex(CH ch, Source source)
        {
            int TempScale;
            if(source == Source.Current)
            {
                TempScale = Convert.ToInt32(ScopePlot.Current.GetScale(ch));

            }
            else
            {
                TempScale = Convert.ToInt32(ScopePlot.Reference.GetScale(ch));

            }
            
            switch (TempScale)
            {
                case 10:
                    return 0;
                case 20:
                    return 1;
                case 50:
                    return 2;
                case 100:
                    return 3;
                case 200:
                    return 4;
                case 500:
                    return 5;
                case 1000:
                    return 6;
                case 2000:
                    return 7;
                case 5000:
                    return 8;
                case 10000:
                    return 9;
                default:
                    return 0;
            }
        }


        /// <summary>
        /// 특정채널에 해당하는 콤보박스 수직 스케일의 초기값을 리턴
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private ComboBox VirticalScaleIndexToComboBox(CH ch, Source source)
        {
            ComboBox outCombo;

            if (source == Source.Current)
            {
                switch (ch)
                {
                    case CH.ch1:
                        outCombo = cbo_VertScaleCH1;
                        break;

                    case CH.ch2:
                        outCombo = cbo_VertScaleCH2;
                        break;
                    case CH.ch3:
                        outCombo = cbo_VertScaleCH3;
                        break;
                    case CH.ch4:
                        outCombo = cbo_VertScaleCH4;
                        break;
                    case CH.ch5:
                        outCombo = cbo_VertScaleCH5;
                        break;
                    default:
                        outCombo = cbo_VertScaleCH6;
                        break;
                }
            }
            else
            {
                switch (ch)
                {
                    case CH.ch1:
                        outCombo = cbo_RefVertScaleCH1;
                        break;

                    case CH.ch2:
                        outCombo = cbo_RefVertScaleCH2;
                        break;
                    case CH.ch3:
                        outCombo = cbo_RefVertScaleCH3;
                        break;
                    case CH.ch4:
                        outCombo = cbo_RefVertScaleCH4;
                        break;
                    case CH.ch5:
                        outCombo = cbo_RefVertScaleCH5;
                        break;
                    default:
                        outCombo = cbo_RefVertScaleCH6;
                        break;
                }
            }
            return outCombo;
        }
        #endregion

        #endregion

        #region Check Box Event 처리
        /// <summary>
        /// 체크박스의 이벤트를 처리해줍니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_VisibleCH_CheckedChanged(object sender, EventArgs e)
        {
            string chkName = ((CheckBox)sender).Name;

            switch (chkName)
            {

                //Waveform chk Process
                case "chk_VisibleCH1":
                    ManageCheckGroupBox(chk_VisibleCH1, grp_WaveformCH1);
                    break;
                case "chk_VisibleCH2":
                    ManageCheckGroupBox(chk_VisibleCH2, grp_WaveformCH2);
                    break;
                case "chk_VisibleCH3":
                    ManageCheckGroupBox(chk_VisibleCH3, grp_WaveformCH3);
                    break;
                case "chk_VisibleCH4":
                    ManageCheckGroupBox(chk_VisibleCH4, grp_WaveformCH4);
                    break;
                case "chk_VisibleCH5":
                    ManageCheckGroupBox(chk_VisibleCH5, grp_WaveformCH5);
                    break;
                case "chk_VisibleCH6":
                    ManageCheckGroupBox(chk_VisibleCH6, grp_WaveformCH6);
                    break;
                //Reference Waveform chk Process
                case "chk_RefVisibleCH1":
                    ManageCheckGroupBox(chk_RefVisibleCH1, grp_RefWaveformCH1);
                    break;
                case "chk_RefVisibleCH2":
                    ManageCheckGroupBox(chk_RefVisibleCH2, grp_RefWaveformCH2);
                    break;
                case "chk_RefVisibleCH3":
                    ManageCheckGroupBox(chk_RefVisibleCH3, grp_RefWaveformCH3);
                    break;
                case "chk_RefVisibleCH4":
                    ManageCheckGroupBox(chk_RefVisibleCH4, grp_RefWaveformCH4);
                    break;
                case "chk_RefVisibleCH5":
                    ManageCheckGroupBox(chk_RefVisibleCH5, grp_RefWaveformCH5);
                    break;
                case "chk_RefVisibleCH6":
                    ManageCheckGroupBox(chk_RefVisibleCH6, grp_RefWaveformCH6);
                    break;
                
                default:
                    break;

                  
            }

            switch (chkName)
            {
                //Waveform chk Process
                case "chk_VisibleCH1":
                    ScopePlot.Current.Display.Line.SetVisible(CH.ch1,chk_VisibleCH1.Checked);
                    break;
                case "chk_VisibleCH2":
                    ScopePlot.Current.Display.Line.SetVisible(CH.ch2, chk_VisibleCH2.Checked);
                    break;
                case "chk_VisibleCH3":
                    ScopePlot.Current.Display.Line.SetVisible(CH.ch3, chk_VisibleCH3.Checked);
                    break;
                case "chk_VisibleCH4":
                    ScopePlot.Current.Display.Line.SetVisible(CH.ch4, chk_VisibleCH4.Checked);
                    break;
                case "chk_VisibleCH5":
                    ScopePlot.Current.Display.Line.SetVisible(CH.ch5, chk_VisibleCH5.Checked);
                    break;
                case "chk_VisibleCH6":
                    ScopePlot.Current.Display.Line.SetVisible(CH.ch6, chk_VisibleCH6.Checked);
                    break;
                //Reference Waveform chk Process

                case "chk_RefVisibleCH1":
                    ScopePlot.Reference.Display.Line.SetVisible(CH.ch1, chk_RefVisibleCH1.Checked);
                    break;
                case "chk_RefVisibleCH2":
                    ScopePlot.Reference.Display.Line.SetVisible(CH.ch2, chk_RefVisibleCH2.Checked);
                    break;
                case "chk_RefVisibleCH3":
                    ScopePlot.Reference.Display.Line.SetVisible(CH.ch3, chk_RefVisibleCH3.Checked);
                    break;
                case "chk_RefVisibleCH4":
                    ScopePlot.Reference.Display.Line.SetVisible(CH.ch4, chk_RefVisibleCH4.Checked);
                    break;
                case "chk_RefVisibleCH5":
                    ScopePlot.Reference.Display.Line.SetVisible(CH.ch5, chk_RefVisibleCH5.Checked);
                    break;
                case "chk_RefVisibleCH6":
                    ScopePlot.Reference.Display.Line.SetVisible(CH.ch6, chk_RefVisibleCH6.Checked);
                    break;
                default:
                    break;
            }
            ScopePlot.UpdatePlot();
            
        }



        private void ManageCheckGroupBox(CheckBox chk, GroupBox grp)
        {
            // Make sure the CheckBox isn't in the GroupBox.
            // This will only happen the first time.
            if (chk.Parent == grp)
            {
                // Reparent the CheckBox so it's not in the GroupBox.
                grp.Parent.Controls.Add(chk);

                // Adjust the CheckBox's location.
                chk.Location = new Point(
                    chk.Left + grp.Left,
                    chk.Top + grp.Top);

                // Move the CheckBox to the top of the stacking order.
                chk.BringToFront();
            }

            // Enable or disable the GroupBox.
            grp.Enabled = chk.Checked;
        }
        #endregion
        
        #region Virtical Offset Event 처리

        private Object lockObject = new object();

        private void nud_VertPosCH_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;

            double nowOffset;
            double nextOffset;
            double diffOffset;

            switch(nud.Name)
            {
                ////Waveform Vertical Offset Process
                case "nud_VertPosCH1":
                    nowOffset = ScopePlot.Current.GetOffset(CH.ch1);
                    nextOffset = (double)nud.Value;
                    diffOffset = nextOffset - nowOffset;
                    ScopePlot.Current.Display.Virtical.SetOffset(CH.ch1, diffOffset);
                    break;
                case "nud_VertPosCH2":
                    nowOffset = ScopePlot.Current.GetOffset(CH.ch2);
                    nextOffset = (double)nud.Value;
                    diffOffset = nextOffset - nowOffset;
                    ScopePlot.Current.Display.Virtical.SetOffset(CH.ch2,diffOffset);
                    break;
                case "nud_VertPosCH3":
                    nowOffset = ScopePlot.Current.GetOffset(CH.ch3);
                    nextOffset = (double)nud.Value;
                    diffOffset = nextOffset - nowOffset;
                    ScopePlot.Current.Display.Virtical.SetOffset(CH.ch3,diffOffset);
                    break;
                case "nud_VertPosCH4":
                    nowOffset = ScopePlot.Current.GetOffset(CH.ch4);
                    nextOffset = (double)nud.Value;
                    diffOffset = nextOffset - nowOffset;
                    ScopePlot.Current.Display.Virtical.SetOffset(CH.ch4,diffOffset);
                    break;
                case "nud_VertPosCH5":
                    nowOffset = ScopePlot.Current.GetOffset(CH.ch5);
                    nextOffset = (double)nud.Value;
                    diffOffset = nextOffset - nowOffset;
                    ScopePlot.Current.Display.Virtical.SetOffset(CH.ch5,diffOffset);
                    break;
                case "nud_VertPosCH6":
                    nowOffset = ScopePlot.Current.GetOffset(CH.ch6);
                    nextOffset = (double)nud.Value;
                    diffOffset = nextOffset - nowOffset;
                    ScopePlot.Current.Display.Virtical.SetOffset(CH.ch6,diffOffset);
                    break;

                ////Reference Waveform Vertical Offset Process
                case "nud_RefVertPosCH1":
                    nowOffset = ScopePlot.Reference.GetOffset(CH.ch1);
                    nextOffset = (double)nud.Value;
                    diffOffset = nextOffset - nowOffset;
                    ScopePlot.Reference.Display.Virtical.SetOffset(CH.ch1, diffOffset);
                    break;
                case "nud_RefVertPosCH2":
                    nowOffset = ScopePlot.Reference.GetOffset(CH.ch2);
                    nextOffset = (double)nud.Value;
                    diffOffset = nextOffset - nowOffset;
                    ScopePlot.Reference.Display.Virtical.SetOffset(CH.ch2, diffOffset);
                    break;
                case "nud_RefVertPosCH3":
                    nowOffset = ScopePlot.Reference.GetOffset(CH.ch3);
                    nextOffset = (double)nud.Value;
                    diffOffset = nextOffset - nowOffset;
                    ScopePlot.Reference.Display.Virtical.SetOffset(CH.ch3, diffOffset);
                    break;
                case "nud_RefVertPosCH4":
                    nowOffset = ScopePlot.Reference.GetOffset(CH.ch4);
                    nextOffset = (double)nud.Value;
                    diffOffset = nextOffset - nowOffset;
                    ScopePlot.Reference.Display.Virtical.SetOffset(CH.ch4, diffOffset);
                    break;
                case "nud_RefVertPosCH5":
                    nowOffset = ScopePlot.Reference.GetOffset(CH.ch5);
                    nextOffset = (double)nud.Value;
                    diffOffset = nextOffset - nowOffset;
                    ScopePlot.Reference.Display.Virtical.SetOffset(CH.ch5, diffOffset);
                    break;
                case "nud_RefVertPosCH6":
                    nowOffset = ScopePlot.Reference.GetOffset(CH.ch6);
                    nextOffset = (double)nud.Value;
                    diffOffset = nextOffset - nowOffset;
                    ScopePlot.Reference.Display.Virtical.SetOffset(CH.ch6, diffOffset);
                    break;

                default:
                    break;
            }
            ScopePlot.UpdatePlot();
            

        }
        #endregion
        
        #region Virtical Scale  Event 처리
        private void cbo_VertScaleCH_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;

            if (InitBool != true)
            {
                return;
            }

            if (ComboToIndex(cbo) < 6)
            {
                ScopePlot.Current.Display.Virtical.SetScale((CH)ComboToIndex(cbo), IndexToScale(cbo.SelectedIndex)); 
            }
            else
            {

                ScopePlot.Reference.Display.Virtical.SetScale((CH)ComboToIndex(cbo) -6, IndexToScale(cbo.SelectedIndex)); 

            }
            ScopePlot.UpdatePlot();
            
        }

        private int ComboToIndex(ComboBox cbo)
        {
            switch (cbo.Name)
            {
                case "cbo_VertScaleCH1":
                    return 0;
                case "cbo_VertScaleCH2":
                    return 1;
                case "cbo_VertScaleCH3":
                    return 2;
                case "cbo_VertScaleCH4":
                    return 3;
                case "cbo_VertScaleCH5":
                    return 4;
                case "cbo_VertScaleCH6":
                    return 5;
                case "cbo_RefVertScaleCH1":
                    return 6;                                    //cbo_RefVertScaleCH3
                case "cbo_RefVertScaleCH2":
                    return 7;
                case "cbo_RefVertScaleCH3":
                    return 8;
                case "cbo_RefVertScaleCH4":
                    return 9;
                case "cbo_RefVertScaleCH5":
                    return 10;
                case "cbo_RefVertScaleCH6":
                    return 11;
                default:
                    return 0;
            }

        }
        /// <summary>
        /// 폼의 콤보박스에 해당하는 스케일 값을 가져옵니다.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int IndexToScale(int index)
        {
            switch (index)
            {
                case 0:
                    return 10;
                case 1:
                    return 20;
                case 2:
                    return 50;
                case 3:
                    return 100;
                case 4:
                    return 200;
                case 5:
                    return 500;
                case 6:
                    return 1000;
                case 7:
                    return 2000;
                case 8:
                    return 5000;
                case 9:
                    return 10000;
                default:
                    return 10;
            }
        }
        #endregion

        #region  시간 스케일 Event 처리
        private void cbo_HorizScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InitBool == false)
            {
                return;
            }

            int temp = cbo_HorizScale.SelectedIndex;
            int timeGrid = 50;

            switch (temp)
            {
                case 0:
                    timeGrid = 50;
                    break;
                case 1:
                    timeGrid = 100;
                    break;
                case 2:
                    timeGrid = 200;
                    break;
                case 3:
                    timeGrid = 500;
                    break;
                case 4:
                default:
                    timeGrid = 1000;
                    break;

            }

            ScopePlot.ReDefine(timeGrid);
            ScopePlot.UpdatePlot();
            
        
        }
        #endregion

        #region 임시 저장 Event 처리
        private void ReferenceSave()
        {
            int currentCh = cbo_CurWaveform.SelectedIndex;
            int referenceCh = cbo_TargetRef.SelectedIndex;

            ScopePlot.Data.SaveInReference((CH)currentCh, (CH)referenceCh);

            switch (referenceCh)
            {
                case 0:
                    chk_RefVisibleCH1.Checked = true;
                    nud_RefVertPosCH1.Value = (decimal)ScopePlot.Current.offset[currentCh];

                    break;
                case 1:
                    chk_RefVisibleCH2.Checked = true;
                    nud_RefVertPosCH2.Value = (decimal)ScopePlot.Current.offset[currentCh];

                    break;
                case 2:
                    chk_RefVisibleCH3.Checked = true;
                    nud_RefVertPosCH3.Value = (decimal)ScopePlot.Current.offset[currentCh];

                    break;
                case 3:
                    chk_RefVisibleCH4.Checked = true;
                    nud_RefVertPosCH4.Value = (decimal)ScopePlot.Current.offset[currentCh];

                    break;
                case 4:
                    chk_RefVisibleCH5.Checked = true;
                    nud_RefVertPosCH5.Value = (decimal)ScopePlot.Current.offset[currentCh];

                    break;
                case 5:
                    chk_RefVisibleCH6.Checked = true;
                    nud_RefVertPosCH6.Value = (decimal)ScopePlot.Current.offset[currentCh];

                    break;
                default:
                    break;
            }
            Initialize();
        }
        #endregion

        #region 라인 색변경 Event 처리
        private void Click_ColorChange(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;

            if (cod_ColorSelect.ShowDialog() == DialogResult.OK)
            {
                panel.BackColor = cod_ColorSelect.Color;

                switch (panel.Name)
                {
                    case "pnl_ColorCH1":
                        ScopePlot.Current.Display.Line.SetColor(CH.ch1, cod_ColorSelect.Color);
                        break;
                    case "pnl_ColorCH2":
                        ScopePlot.Current.Display.Line.SetColor(CH.ch2, cod_ColorSelect.Color);
                        break;
                    case "pnl_ColorCH3":
                        ScopePlot.Current.Display.Line.SetColor(CH.ch3, cod_ColorSelect.Color);
                        break;
                    case "pnl_ColorCH4":
                        ScopePlot.Current.Display.Line.SetColor(CH.ch4, cod_ColorSelect.Color);
                        break;
                    case "pnl_ColorCH5":
                        ScopePlot.Current.Display.Line.SetColor(CH.ch5, cod_ColorSelect.Color);
                        break;
                    case "pnl_ColorCH6":
                        ScopePlot.Current.Display.Line.SetColor(CH.ch6, cod_ColorSelect.Color);
                        break;

                    case "pnl_RefColorCH1":
                        ScopePlot.Reference.Display.Line.SetColor(CH.ch1, cod_ColorSelect.Color);
                        break;
                    case "pnl_RefColorCH2":
                        ScopePlot.Reference.Display.Line.SetColor(CH.ch2, cod_ColorSelect.Color);
                        break;
                    case "pnl_RefColorCH3":
                        ScopePlot.Reference.Display.Line.SetColor(CH.ch3, cod_ColorSelect.Color);
                        break;
                    case "pnl_RefColorCH4":
                        ScopePlot.Reference.Display.Line.SetColor(CH.ch4, cod_ColorSelect.Color);
                        break;
                    case "pnl_RefColorCH5":
                        ScopePlot.Reference.Display.Line.SetColor(CH.ch5, cod_ColorSelect.Color);
                        break;
                    case "pnl_RefColorCH6":
                        ScopePlot.Reference.Display.Line.SetColor(CH.ch6, cod_ColorSelect.Color);
                        break;
                    default:
                        break;
                }
                ScopePlot.UpdatePlot();

            }


        }
        #endregion


        #region Normal 모드 Roll 모드 변경 체크박스 Event 처리 현재 Normal 모드는 비활성화
        private void chk_PlotDisplayMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_PlotDisplayMode.Checked == true)
            {
                ScopePlot.Data.ModeChange(DisplayMode.Roll);
            }
            else
            {
                ScopePlot.Data.ModeChange(DisplayMode.Normal);
            }

            grp_Trigger.Enabled = chk_PlotDisplayMode.Checked;

        }
        #endregion

        #region Text File  저장
        private void WaveFormSave()
        {

            //파일 이름 기입
            DateTime dtNow = DateTime.Now;  //현재 시간 얻기(지역 변수 처리)
            string fileName = txt_FileSave.Text ?? "";
            if (fileName == "")
            {
                MessageBox.Show("File name is null");
                return;
            }

            string filePath;

            filePath = "ScopeSaveFile\\" + dtNow.ToString("yyMMdd_HH_mm_ss_") + fileName + ".txt";


            //정보 사항 기입
            string[] InfomationData = new string[3];
            InfomationData[0] = "Sampling Rate :\t" + Convert.ToString(ScopePlot.Info.GetFrequncy());

            System.IO.File.AppendAllLines(filePath, InfomationData);

            int cbo_WaveFormSaveIndex = cbo_WaveFormSave.SelectedIndex;

            double[] time = ScopePlot.Info.GetTime();
            double[][] CurrentData = new double[6][];
            double[][] ReferenceData = new double[6][];

            bool[] CurrentEnable = new bool[6];
            bool[] ReferenceEnable = new bool[6];

            int[] CurrentIndex = new int[6];
            int[] ReferenceIndex = new int[6];

            
            for (int i = 0; i < 6; i++)
            {
                CurrentEnable[i] = ScopePlot.Current.Display.Line.GetVisible((CH)i);
                ReferenceEnable[i] = ScopePlot.Reference.Display.Line.GetVisible((CH)i);
                CurrentIndex[i] = 0;
                ReferenceIndex[i] = 0;
            }

            for (int i = 0; i < 6; i++)
            {
                CurrentData[i] = ScopePlot.Current.GetData((CH)i);
                ReferenceData[i] = ScopePlot.Reference.GetData((CH)i);
            }
            int startIndex = GetVisibleIndexMin(CH.ch1, Source.Current);
            int endIndex = GetVisibleIndexMax(CH.ch1, Source.Current);


            for (int i = 0; i < 6; i++)
            {
                if(GetVisibleIndexMin((CH)i, Source.Current) < startIndex)
                {
                    startIndex = GetVisibleIndexMin((CH)i, Source.Current);
                }
                if (GetVisibleIndexMin((CH)i, Source.Reference) < startIndex)
                {
                    startIndex = GetVisibleIndexMin((CH)i, Source.Reference);
                }
                
                CurrentIndex[i] = GetVisibleIndexMax((CH)i, Source.Current);
                if (CurrentIndex[i] > endIndex)
                {
                    endIndex = GetVisibleIndexMax((CH)i, Source.Current);
                    
                }

                ReferenceIndex[i] = GetVisibleIndexMax((CH)i, Source.Reference);
                if( ReferenceIndex[i] > endIndex)
                {
                    endIndex = GetVisibleIndexMax((CH)i, Source.Reference);
                    
                }
            }
        

            string[] str = new string[endIndex - startIndex + 2];

            str[0] = "Time[ms]";

            //채널이 선택되지 않은 경우 활성된 채널 저장
            if (cbo_WaveFormSave.SelectedIndex == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (CurrentEnable[i] == true)
                    {
                        str[0] += "\t" + "Current_" + ((Convert.ToString((CH)i)).ToUpper());
                    }
                }
                for (int i = 0; i < 6; i++)
                {
                    if (ReferenceEnable[i] == true)
                    {
                        str[0] += "\t" + "Reference_" + ((Convert.ToString((CH)i)).ToUpper());
                    }
                }

                for (int i = startIndex + 1; i < endIndex + 2;i++ )
                {
                    str[i] = Convert.ToString(time[i - 1]);
                }
                for (int i = startIndex + 1; i < endIndex + 2 ; i++)
                {
                    for (int j = 0; j < 6;j++ )
                    {
                        if (CurrentEnable[j] == true)
                        {
                            if (i < CurrentIndex[j] + 1)
                            {
                                str[i] += "\t" + Convert.ToString(CurrentData[j][i - 1]);
                            }
                            else
                            {
                                str[i] += "\t";
                            }
                        }
                        
                    }
                    for (int j = 0; j < 6; j++)
                    {
                        if (ReferenceEnable[j] == true)
                        {
                            if (i < ReferenceIndex[j] + 1)
                            {
                                str[i] += "\t" + Convert.ToString(ReferenceData[j][i - 1]);
                            }
                            else
                            {
                                str[i] += "\t";
                            }
                        }
                    }


                }

            }
            else
            {
                startIndex = GetVisibleIndexMin((CH)(cbo_WaveFormSave.SelectedIndex - 1), Source.Current);
                endIndex = GetVisibleIndexMin((CH)(cbo_WaveFormSave.SelectedIndex - 1), Source.Current);
                str = new string[endIndex - startIndex + 2];
                str[0] += "\t" + "Current_" + ((Convert.ToString((CH)(cbo_WaveFormSave.SelectedIndex - 1))).ToUpper());
                
                for (int i = startIndex + 1; i < endIndex + 2; i++)
                {
                    str[i] = Convert.ToString(time[i - 1]);
                    str[i] += "\t" + Convert.ToString(CurrentData[cbo_WaveFormSave.SelectedIndex - 1][i - 1]);
                }
            }






            System.IO.File.AppendAllLines(filePath, str);

            


        }

        #endregion
        //저장 배열크기 재설정

        private int GetVisibleIndexMin(CH ch, Source source)
        {
            if (source == Source.Current)
            {
                return ScopePlot.Current.chHandler[(int)ch].MinRenderIndex;
            }
            else
            {
                return ScopePlot.Reference.chHandler[(int)ch].MinRenderIndex;
            }
        }
        private int GetVisibleIndexMax(CH ch, Source source)
        {
            if (source == Source.Current)
            {
                return ScopePlot.Current.chHandler[(int)ch].MaxRenderIndex;
            }
            else
            {
                return ScopePlot.Reference.chHandler[(int)ch].MaxRenderIndex;
            }
        }


        #region  Trigger 및 Pause 처리
        private void button_Click(object sender, EventArgs e)
        {
            Button bttm = (Button)sender;
            string bttmName = bttm.Name;

            switch(bttmName)
            {
                case "btn_ReferenceSave":
                    ReferenceSave();
                    break;
                case "btn_WaveFormSave":
                    WaveFormSave();
                    break;
                case "btn_TriggerExe":
                    ExeStop(); 
                    //bool LockState = ScopePlot.Data.Flag.pause1CycleFlag;
 
                    ////현재 트리거가 걸린 상태이면
                    //if(LockState)
                    //{
                    //    //트리거를 풀어준다. 
                    //    ScopePlot.Data.TriggerOff(true);

                    //}
                    ////현재 트리거가 걸리지 않은 상태이면
                    //else
                    //{
                    //    //즉각 멈추어 버린다.
                    //    for(int i=0;i<6;i++)
                    //    {
                    //        ScopePlot.Data.Pause((CH)i, true);
                    //    }

                    //}
                    //Flag.pause1CycleFlag
                    //ScopePlot.Data.Flag.pause1CycleFlag

                    
                    break;
          
                case "btn_Pause":
                    Pause();
                    //ScopePlot.Data.Flag.syncLock = !ScopePlot.Data.Flag.syncLock;

                    break;
               
                    
                default:
                    break;
            }
            ScopePlot.UpdatePlot();
        }


        public void LblState(bool state)
        {
            if (this.lbl_TriggerState.InvokeRequired)
            {
                this.lbl_TriggerState.Invoke(new MethodInvoker(delegate()
                {
                    if (state)
                    {
                        this.lbl_TriggerState.Text = "실행 중";
                    }
                    else
                    {
                        this.lbl_TriggerState.Text = "정지";
                    }
                    
                }));
            }
            else
            {
                if (state)
                {
                    this.lbl_TriggerState.Text = "실행 중";
                }
                else
                {
                    this.lbl_TriggerState.Text = "정지";
                }
            }
        
        }
        private void Stop()
        {
            //bool LockState = !ScopePlot.Data.Flag.syncLock;
            ScopePlot.Data.Pause(true);

        }

        private void Pause()
        {
            bool LockState = !ScopePlot.Data.Flag.syncLock;
            ScopePlot.Data.Pause(LockState);
            if(!LockState)
            {
                for (int i = 0; i < 6; i++)
                {
                    ScopePlot.Data.TempClear((CH)i);
                    ScopePlot.Data.ChClear((CH)i);
                    ScopePlot.Current.index[i] = 0;

                }
            }
            
        }

        //bool toggleBit = false;
        private void ExeStop()
        {
            //ScopePlot.Data.TriggerOff();
            //if (lbl_TriggerState.Text == "실행 중")
            //{
            //    Stop();
            //    //ScopePlot.Data.TriggerOff();

            //}
            //else
            if (lbl_TriggerState.Text == "정지")
            {
                ScopePlot.Data.TriggerOff();
                if (cbo_TriggerMode.SelectedIndex == 1) //단일 모드
                {
                    ScopePlot.Data.Trigger.SetVisible(true);
                    ScopePlot.Data.Trigger.SetIndexY((CH)(cbo_TriggerChannel.SelectedIndex));

                    if (rdo_TriggerUp.Checked)
                    {
                        ScopePlot.Data.Flag.upTrigger = true;
                    }
                    else
                    {
                        ScopePlot.Data.Flag.downTrigger = true;
                    }
                    ScopePlot.Data.TriggerSingle();
                }
                else if (cbo_TriggerMode.SelectedIndex == 2) //반복 모드
                {
                    ScopePlot.Data.Trigger.SetVisible(true);
                    ScopePlot.Data.Trigger.SetIndexY((CH)(cbo_TriggerChannel.SelectedIndex));


                    if (rdo_TriggerUp.Checked)
                    {
                        ScopePlot.Data.Flag.upTrigger = true;
                    }
                    else
                    {
                        ScopePlot.Data.Flag.downTrigger = true;
                    }

                    ScopePlot.Data.TriggerRpt();
                }
                lbl_TriggerState.Text = "실행 중";

            }


            
            for (int i = 0; i < 6; i++)
            {
                ScopePlot.Data.TempClear((CH)i);
                ScopePlot.Data.ChClear((CH)i);
                ScopePlot.Current.index[i] = 0;

            }
        }



        private void TriggerSettingChange(object sender, EventArgs e)
        {
            int mode = cbo_TriggerMode.SelectedIndex;
            int channel = cbo_TriggerChannel.SelectedIndex;

            switch (mode)
            {
                //트리거 Off
                case 0:
                    //lbl_TriggerState.Text = "";
                    ScopePlot.Data.Trigger.SetVisible(false);
                    ScopePlot.Data.TriggerOff();
                    break;
                //트리거 단일 모드
                case 1:
                    //lbl_TriggerState.Text = "실행 중";
                    LblState(true);
                    ScopePlot.Data.Trigger.SetVisible(true);
                    ScopePlot.Data.Trigger.SetIndexY((CH)channel); 

                    if (rdo_TriggerUp.Checked)
                    {
                        ScopePlot.Data.Flag.upTrigger = true;
                    }
                    else
                    {
                        ScopePlot.Data.Flag.downTrigger = true;
                    }
                    ScopePlot.Data.TriggerSingle();
                    break;
                    //트리거 반복모드
                case 2: //
                    //lbl_TriggerState.Text = "실행 중";
                    LblState(true);
                    ScopePlot.Data.Trigger.SetVisible(true);
                    ScopePlot.Data.Trigger.SetIndexY((CH)channel); 


                    if (rdo_TriggerUp.Checked)
                    {
                        ScopePlot.Data.Flag.upTrigger = true;
                    }
                    else
                    {
                        ScopePlot.Data.Flag.downTrigger = true;
                    }

                    ScopePlot.Data.TriggerRpt();

                    break;
                default:
                    break;
            }
        }



        #endregion


    }
}
