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

        public partial class CStorage
        {
            public CCondition[] Condition = new CCondition[6];

            CScopeHandler ScopeHandler;
            ScottPlot.FormsPlot formsPlot;

            static double[] mainY = new double[2];

            public CStorage(ScottPlot.FormsPlot formsPlot, CScopeHandler ScopeHandler)
            {
                this.formsPlot = formsPlot;
                this.ScopeHandler = ScopeHandler;

                for (int i = 0; i < 6; i++)
                {
                    Condition[i] = new CCondition();
                }

                mainY[0] = 0;
                mainY[1] = 1;

            }

            public void Save()
            {
                mainY[0] = formsPlot.Plot.GetAxisLimits().YMin;
                mainY[1] = formsPlot.Plot.GetAxisLimits().YMax;

                for (int i = 0; i < 6; i++)
                {
                    Condition[i].ymin = formsPlot.Plot.GetAxisLimits(0, 2 + i + 6 * (int)ScopeHandler.me).YMin;
                    Condition[i].ymax = formsPlot.Plot.GetAxisLimits(0, 2 + i + 6 * (int)ScopeHandler.me).YMax;
                    Condition[i].Line.display = ScopeHandler.chHandler[i].IsVisible;
                    Condition[i].Axis.display = ScopeHandler.yAxis[i].IsVisible;
                    Condition[i].Line.color = ScopeHandler.chHandler[i].Color;
                    Condition[i].Axis.color = ScopeHandler.GetAxisColor((CH)i);
                }

            }

            public void Load()
            {
                formsPlot.Plot.SetAxisLimits(yMin: mainY[0], yMax: mainY[1]);

                for (int i = 0; i < 6; i++)
                {
                    formsPlot.Plot.SetAxisLimits(yMin: Condition[i].ymin, yMax: Condition[i].ymax, xAxisIndex: 0, yAxisIndex: 2 + i + 6 * (int)ScopeHandler.me);

                    // 저장되어 있는 Line 설정 Load 
                    ScopeHandler.chHandler[i].IsVisible = Condition[i].Line.display;
                    ScopeHandler.chHandler[i].Color = Condition[i].Line.color;

                    // 저장되어 있는 Aixs 설정 Load
                    ScopeHandler.yAxis[i].IsVisible = Condition[i].Axis.display;
                    ScopeHandler.yAxis[i].Color(Condition[i].Axis.color);
                }

            }




            public class CCondition
            {
                public double ymin, ymax;
                public Component Line;
                public Component Axis;

                public CCondition()
                {
                    ymin = -1;
                    ymax = 1;
                    Line = new Component();
                    Axis = new Component();
                }

                public class Component
                {
                    public bool display;
                    public Color color;
                    public Component()
                    { }
                }
            }
        }
    }


}
