using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Profiling
{
    class ChartBuilder : IChartBuilder
    {
        public Control Build(List<ExperimentResult> result)
        {
            var chart = new Chart();
            var classGraph = new Series();
            var structGraph = new Series();
            chart.ChartAreas.Add(new ChartArea());
            foreach (var measure in result)
            {
                classGraph.Points.Add(new DataPoint(measure.Size, measure.ClassResult));
                structGraph.Points.Add(new DataPoint(measure.Size, measure.StructResult));
            }
            classGraph.ChartType = SeriesChartType.FastLine;
            classGraph.Color = Color.Orange;
            classGraph.MarkerBorderWidth = 5;
            structGraph.ChartType = SeriesChartType.FastLine;
            structGraph.Color = Color.Green;
            structGraph.MarkerBorderWidth = 5;
            chart.Series.Add(classGraph);
            chart.Series.Add(structGraph);
            chart.ChartAreas[0].AxisX.Title = "Measure size";
            chart.ChartAreas[0].AxisY.Title = "Time Result";
            chart.Legends.Add("");
            chart.Series[0].LegendText = "Class";
            chart.Series[1].LegendText = "Struct";
            return chart;
        }
    }
}
