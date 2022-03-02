using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottPlot;
using System.Drawing;

namespace ControlFlots.fScope.Private
{
    public partial class CData
    {
        public class CTrigger
        {
            CScopePlot_Private ScopePlot;

            public ScottPlot.Plottable.HLine Handler;

            bool HandlerIsVisible;

            double HandlerY;
            int HandlerIndex;


            public CTrigger(CScopePlot_Private ScopePlot)
            {
                this.ScopePlot = ScopePlot;

                Handler = new ScottPlot.Plottable.HLine()
                {
                    Y = 0,
                    XAxisIndex = 0,
                    YAxisIndex = 2,
                    Color = Color.White,
                    DragEnabled = true,
                    IsVisible = false

                };

                ScopePlot.formsPlot.Plot.Add(Handler);


            }

            public void Save()
            {
                HandlerIsVisible = Handler.IsVisible;

                HandlerY = Handler.Y;
                HandlerIndex = Handler.YAxisIndex;


            }
            public void Load()
            {
                ScopePlot.formsPlot.Plot.Add(Handler);
                Handler.IsVisible = Handler.IsVisible;
                Handler.Y = HandlerY;
                Handler.YAxisIndex = HandlerIndex;
            }




            public double GetY() { return Handler.Y; }

            public CH GetIndexY() { return (CH)(Handler.YAxisIndex - 2); }
            public void SetIndexY(CH ch) { Handler.YAxisIndex = 2 + (int)ch; }


            public void SetPosition(double Y) { Handler.Y = Y; }

            public void SetVisible(bool isVisible) { Handler.IsVisible = isVisible; }
            public bool GetVisible() { return Handler.IsVisible; }


        }
    }

}