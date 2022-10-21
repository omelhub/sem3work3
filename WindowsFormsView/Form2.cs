using BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace WindowsFormsView
{
    public partial class Form2 : Form
    {
        public Form2(Logic logic)
        {
            InitializeComponent();
            logic2 = logic;
            DrawGraph();
        }

        Logic logic2;

        private void DrawGraph()
        {
            GraphPane pane = zedGraphControl1.GraphPane;

            pane.YAxis.Scale.MajorStep = 1;
            pane.YAxis.Scale.MinorStep = 1;
            pane.YAxis.Title.Text = "Количество студентов";

            pane.XAxis.Scale.MajorStep = 1;
            pane.XAxis.Scale.MinorStep = 1;
            pane.XAxis.Title.Text = "Специализации";

            pane.CurveList.Clear();

            Random r = new Random();

            var distributionOfSpecialties = logic2.DistributionOfSpecialties();

            foreach (var key in distributionOfSpecialties.Keys)
            {
                pane.AddBar(key, null, new double[] { distributionOfSpecialties[key] }, Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256)));
            }

            pane.BarSettings.MinBarGap = 1.5f;

            zedGraphControl1.AxisChange();
            Invalidate();
        }
    }
}
