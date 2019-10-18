using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Vts.Common;
using Vts.Extensions;
using Vts.Api.Data;
using Vts.Factories;

namespace Vts.Api.Services
{
    public class InverseSolverService
    {
        public string GetPlotData(dynamic values)
        {
            try
            {
                dynamic vtsSettings = values;
                var sd = vtsSettings["solutionDomain"];
                var ins = vtsSettings["inverseSolverEngine"];
                var msg = "";
                var igops = new OpticalProperties((double) vtsSettings["opticalProperties"]["mua"],
                    (double) vtsSettings["opticalProperties"]["musp"], (double) vtsSettings["opticalProperties"]["g"],
                    (double) vtsSettings["opticalProperties"]["n"]);
                var xaxis = new DoubleRange((double) vtsSettings["range"]["startValue"],
                    (double) vtsSettings["range"]["endValue"], (int) vtsSettings["range"]["numberValue"]);
                var independentValues = xaxis.AsEnumerable().ToArray();
                var independentAxis = vtsSettings["independentAxes"]["label"];
                // LM?: is independentAxisValue = constant independent axis value?
                var independentAxisValue = (double) vtsSettings["independentAxes"]["value"];
                var igopsArray = GetInitialGuessOpticalProperties(igops);
                var igparms = GetParametersInOrder(igopsArray, independentValues, sd, independentAxis, independentAxisValue);
                object[] igparmsConvert = igparms.Values.ToArray();
                var optpar = vtsSettings["optimizationParameters"];
                var optype = vtsSettings["optimizerType"];
                // get measured data from inverse solver analysis component
                var measuredPoints = vtsSettings["measuredData"]; // this is a JArray has form [[x1,y1],[x2,y2]...]
                List<double[]> measConvert = measuredPoints.ToObject<List<double[]>>(); // convert to list of double[]
                var meas = measConvert.Select(p => p.Last()).ToArray(); // get y value
                var lbs = new double[] {0, 0, 0, 0};
                var ubs = new double[]
                {
                    double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity
                };
                double[] fit = ComputationFactory.SolveInverse(
                    Enum.Parse(typeof(ForwardSolverType), ins.ToString()),
                    Enum.Parse(typeof(OptimizerType), optype.ToString()),
                    Enum.Parse(typeof(SolutionDomainType), sd.ToString(), true), // LM? add true
                    meas,
                    meas, // set standard deviation to measured to match WPF
                    Enum.Parse(typeof(InverseFitType), optpar.ToString()),
                    igparmsConvert,
                    lbs,
                    ubs);
                var fitops = ComputationFactory.UnFlattenOpticalProperties(fit);
                var fitparms = GetParametersInOrder(fitops, independentValues, sd, independentAxis, independentAxisValue);
                // temp substitute for common code in ForwardSolverService
                var ops = fitops.AsEnumerable();
                var rhos = xaxis.AsEnumerable();
                var fs = SolverFactory.GetForwardSolver(Enum.Parse(typeof(ForwardSolverType), ins.ToString()));
                IEnumerable<double> fitResults = fs.ROfRho(ops.First(), rhos);
                // LM? why didn't you call following in ForwardSolverService
                //var fitResults = ComputationFactory.ComputeReflectance(
                //    Enum.Parse(typeof(ForwardSolverType), ins.ToString()),
                //    Enum.Parse(typeof(SolutionDomainType), sd.ToString(), true),
                //    ForwardAnalysisType.R, fitparms.Values.ToArray());
                var fitDataPoints = rhos.Zip(fitResults, (x, y) => new Point(x, y));
                var fitPlot = new PlotData {Data = fitDataPoints, Label = "ROfRho"};
                var plot = new Plots
                {
                    Id = "ROfRho", Detector = "R(ρ)", Legend = "R(ρ)", XAxis = "ρ",
                    YAxis = "Reflectance", PlotList = new List<PlotDataJson>()
                };
                plot.PlotList.Add(new PlotDataJson
                {
                    Data = fitPlot.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                    Label = ins + " μa=" + fitops[0].Mua + " μs'=" + fitops[0].Musp
                }); 
                msg = JsonConvert.SerializeObject(plot);
                return msg;
            }
            catch (Exception e)
            {
                throw new Exception("Error during action: " + e.Message);
            }

            // this needs further development when add in wavelength refer to WPF code
            object GetInitialGuessOpticalProperties(OpticalProperties igops)
            {
                return new[] {igops};
            }

            // the following needs to change when Wavelength is added into independent variable list
            IDictionary<IndependentVariableAxis, object> GetParametersInOrder(
                object opticalProperties, double[] xs, string sd, string independentAxis, double independentValue)
            {
                // make list of independent vars with independent first then constant
                var listIndepVars = new List<IndependentVariableAxis>();
                var isConstant = independentAxis;
                if (sd == "rofrho")
                {
                    isConstant = "";
                    listIndepVars.Add(IndependentVariableAxis.Rho);
                }
                else if (sd == "rofrhoandt")
                {
                    if (independentAxis == "t")
                    {
                        listIndepVars.Add(IndependentVariableAxis.Rho);
                        listIndepVars.Add(IndependentVariableAxis.Time);
                    }

                    listIndepVars.Add(IndependentVariableAxis.Time);
                    listIndepVars.Add(IndependentVariableAxis.Rho);
                }
                else if (sd == "rofrhoandft")
                {
                    listIndepVars.Add(IndependentVariableAxis.Rho);
                    listIndepVars.Add(IndependentVariableAxis.Ft);
                }
                else if (sd == "roffx")
                {
                    isConstant = "";
                    listIndepVars.Add(IndependentVariableAxis.Fx);
                }
                else if (sd == "roffxandt")
                {
                    listIndepVars.Add(IndependentVariableAxis.Fx);
                    listIndepVars.Add(IndependentVariableAxis.Time);
                }
                else if (sd == "roffxandft")
                {
                    listIndepVars.Add(IndependentVariableAxis.Fx);
                    listIndepVars.Add(IndependentVariableAxis.Ft);
                }

                // get all parameters in order
                var allParameters =
                    from iva in listIndepVars
                    where iva != IndependentVariableAxis.Wavelength
                    //orderby GetParameterOrder(iva)
                    select new KeyValuePair<IndependentVariableAxis, object>(iva,
                        GetParameterValues(iva, isConstant, independentValue, xs));
                // OPs are always first in the list
                return
                    new KeyValuePair<IndependentVariableAxis, object>(IndependentVariableAxis.Wavelength,
                            opticalProperties)
                        .AsEnumerable()
                        .Concat(allParameters).ToDictionary();
            }

            int GetParameterOrder(IndependentVariableAxis axis)
            {
                switch (axis)
                {
                    case IndependentVariableAxis.Wavelength:
                        return 0;
                    case IndependentVariableAxis.Rho:
                        return 1;
                    case IndependentVariableAxis.Fx:
                        return 1;
                    case IndependentVariableAxis.Time:
                        return 2;
                    case IndependentVariableAxis.Ft:
                        return 2;
                    case IndependentVariableAxis.Z:
                        return 3;
                    default:
                        throw new ArgumentOutOfRangeException("axis");
                }
            }

            double[] GetParameterValues(IndependentVariableAxis axis, string isConstant, double independentValue,
                double[] xs)
            {
                if (isConstant != "")
                {
                    var positionIndex = 0; //hard-coded for now
                    switch (positionIndex)
                    {
                        case 0:
                        default:
                            return new[] {independentValue};
                        //case 1:
                        //return new[] { SolutionDomainTypeOptionVM.ConstantAxesVMs[1].AxisValue };
                        //case 2:
                        //    return new[] { SolutionDomainTypeOptionVM.ConstantAxisThreeValue };
                    }
                }
                else
                {
                    //var numAxes = axis.Count();
                    var numAxes = 1;
                    var positionIndex = 0; //hard-coded for now
                    //var positionIndex = SolutionDomainTypeOptionVM.IndependentVariableAxisOptionVM.SelectedValues.IndexOf(axis);
                    switch (numAxes)
                    {
                        case 1:
                        default:
                            //return AllRangeVMs[0].Values.ToArray();
                            return xs.ToArray();
                        //case 2:
                        //    switch (positionIndex)
                        //    {
                        //        case 0:
                        //        default:
                        //            return AllRangeVMs[1].Values.ToArray();
                        //        case 1:
                        //            return AllRangeVMs[0].Values.ToArray();
                        //    }
                        //case 3:
                        //    switch (positionIndex)
                        //    {
                        //        case 0:
                        //        default:
                        //            return AllRangeVMs[2].Values.ToArray();
                        //        case 1:
                        //            return AllRangeVMs[1].Values.ToArray();
                        //        case 2:
                        //            return AllRangeVMs[0].Values.ToArray();
                        //    }
                    }
                }
            }
        }

        public class InverseSolutionResult
        {
            // LM?: IDataPoint[][] for first item is in Wpf.Model
            public Point[] FitDataPoints { get; set; }
            //public double[][] FitDataPoints { get; set; }
            public OpticalProperties[] MeasuredOpticalProperties { get; set; }
            public OpticalProperties[] GuessOpticalProperties { get; set; }
            public OpticalProperties[] FitOpticalProperties { get; set; }
        }
        
    }
}
