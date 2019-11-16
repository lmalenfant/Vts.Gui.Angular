using System;
using System.Collections.Generic;
using System.Linq;
using Vts.Common;
using Vts.Extensions;
using Vts.Api.Data;

namespace Vts.Api.Services
{
    public class PlotSpectralResultsService : IPlotResultsService
    {
        public string Plot(IPlotParameters plotParameters)
        {
            var msg = "";
            var parameters = (SpectralPlotParameters) plotParameters;
            var xaxis = parameters.XAxis;
            var plotType = parameters.PlotType;
            var independentValues = ((DoubleRange)xaxis).AsEnumerable().ToArray();
            IEnumerable<double> doubleResults;
            double[] xs;
            IEnumerable<Point> xyPoints, xyPointsReal, xyPointsImag;
            PlotData plotData, plotDataReal, plotDataImag;
            Plots plot;
            try
            {
                // CH: please disregard all that I've coded, I just guessed 
                switch (plotType)
                {
                    case SpectralPlotType.Mua:
                        //doubleResults = MuaSpectra(xaxis);
                        //xs = independentValues;
                        //xyPoints = xs.Zip(doubleResults, (x, y) => new Point(x, y));
                        //plotData = new PlotData {Data = xyPoints, Label = "μa spectra" };
                        //plot = new Plots
                        //{
                        //    Id = "Mua", Detector = "", Legend = "", XAxis = "lambda", YAxis = "μa [mm-1]",
                        //    PlotList = new List<PlotDataJson>()
                        //};
                        //plot.PlotList.Add(new PlotDataJson
                        //{
                        //    Data = plotData.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                        //    Label = "μa spectra"
                        //});
                        //msg = JsonConvert.SerializeObject(plot);
                        break;
                    case SpectralPlotType.Musp:
                        break;
                   
                }
                return msg;
            }
            catch (Exception e)
            {
                throw new Exception("Error during action: " + e.Message);
            }
        }
        //private IEnumerable<double> MuaSpectra(DoubleRange wavelength)
        //{
        //    return new IEnumerable<double>();
        //}
    }
}
