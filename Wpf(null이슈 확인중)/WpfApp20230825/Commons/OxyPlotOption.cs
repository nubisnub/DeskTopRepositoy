using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System.Collections.Generic;

namespace WpfApp20230825.Commons
{
    public class OxyPlotOption
    {
        private readonly PlotModel plotModel = new PlotModel();
        private IList<OxyColor> colorList=default!;
        private int colorListIndex = default!;

        public OxyPlotOption(string title)
        {
            plotModel = new PlotModel { Title = title };
        }

        public void SetAxis_X(string Xname)
        {
            PlotModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = Xname,
                StringFormat = "yyyy-MM-dd",
                MajorGridlineStyle = LineStyle.Solid,
                IsZoomEnabled = false,
            });
        }

        public void SetAxis_Y(string Yname)
        {
            PlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = Yname,
                Maximum = 100,
                Minimum = 0,
                IsZoomEnabled = false,
            });
        }

        public void SetRegend()
        {
            Legend legend = new Legend
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightTop,
                LegendOrientation = LegendOrientation.Vertical,
            };
            plotModel.Legends.Add(legend);
        }
       
        public void AddLineSeriesDataPoints(string title, IEnumerable<DataPoint> dataPoints)
        {
            OxyColor color = colorList == null ? OxyColors.LightBlue : colorList[colorListIndex];
            LineSeries lineSeries = new LineSeries
            {
                Title = title,
                Color = color,
                MarkerStroke = color,
                StrokeThickness = 2,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
            };

            lineSeries.Points.AddRange(dataPoints);

            PlotModel.Series.Add(lineSeries);
        }
     


        public PlotModel PlotModel => plotModel;
    }
}
