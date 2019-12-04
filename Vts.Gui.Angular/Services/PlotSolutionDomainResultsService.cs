﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Vts.Common;
using Vts.Extensions;
using Vts.Api.Data;
using Vts.Api.Models;
using Vts.Factories;

namespace Vts.Api.Services
{
    public class PlotSolutionDomainResultsService : IPlotResultsService
    {
        private readonly ILogger<PlotSolutionDomainResultsService> _logger;

        public PlotSolutionDomainResultsService(ILogger<PlotSolutionDomainResultsService> logger)
        {
            _logger = logger;
        }

        public string Plot(IPlotParameters plotParameters)
        {
            var msg = "";
            var parameters = (SolutionDomainPlotParameters) plotParameters;
            var fs = parameters.ForwardSolverType;
            var op = parameters.OpticalProperties;
            var xaxis = parameters.XAxis;
            var noise = parameters.NoiseValue;
            var independentAxis = parameters.IndependentAxes.Label;
            var independentValue = parameters.IndependentAxes.Value;
            var independentValues = xaxis.AsEnumerable().ToArray();
            IEnumerable<double> doubleResults;
            IEnumerable<Complex> complexResults;
            double[] xs;
            IEnumerable<Point> xyPoints, xyPointsReal, xyPointsImag;
            PlotData plotData, plotDataReal, plotDataImag;
            Plots plot;
            try
            {
                switch (parameters.SolutionDomain)
                {
                    case SolutionDomainType.ROfRho:
                        doubleResults = ROfRho(fs, op, xaxis, noise);
                        xs = independentValues;
                        xyPoints = xs.Zip(doubleResults, (x, y) => new Point(x, y));
                        plotData = new PlotData {Data = xyPoints, Label = "ROfRho"};
                        plot = new Plots
                        {
                            Id = "ROfRho", Detector = "R(ρ)", Legend = "R(ρ)", XAxis = "ρ", YAxis = "Reflectance",
                            PlotList = new List<PlotDataJson>()
                        };
                        plot.PlotList.Add(new PlotDataJson
                        {
                            Data = plotData.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                            Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp
                        });
                        msg = JsonConvert.SerializeObject(plot);
                        break;
                    case SolutionDomainType.ROfRhoAndTime:
                        if (independentAxis == "t")
                        {
                            var time = independentValue;
                            doubleResults = ROfRhoAndTime(fs, op, xaxis, time, noise);
                            xs = independentValues;
                            xyPoints = xs.Zip(doubleResults, (x, y) => new Point(x, y));
                            plotData = new PlotData {Data = xyPoints, Label = "ROfRhoAndTime"};
                            plot = new Plots
                            {
                                Id = "ROfRhoAndTimeFixedTime", Detector = "R(ρ,time)", Legend = "R(ρ,time)",
                                XAxis = "ρ", YAxis = "Reflectance", PlotList = new List<PlotDataJson>()
                            };
                            plot.PlotList.Add(new PlotDataJson
                            {
                                Data = plotData.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                                Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + time
                            });
                            msg = JsonConvert.SerializeObject(plot);
                        }
                        else
                        {
                            var rho = independentValue;
                            doubleResults = ROfRhoAndTime(fs, op, rho, xaxis, noise);
                            xs = independentValues.ToArray(); // the Skip(1) is giving inverse problems
                            xyPoints = xs.Zip(doubleResults, (x, y) => new Point(x, y));
                            plotData = new PlotData {Data = xyPoints, Label = "ROfRhoAndTime"};
                            plot = new Plots
                            {
                                Id = "ROfRhoAndTimeFixedRho", Detector = "R(ρ,time)", Legend = "R(ρ,time)",
                                XAxis = "Time", YAxis = "Reflectance", PlotList = new List<PlotDataJson>()
                            };
                            plot.PlotList.Add(new PlotDataJson
                            {
                                Data = plotData.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                                Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ρ=" + rho
                            });
                            msg = JsonConvert.SerializeObject(plot);
                        }
                        break;
                    case SolutionDomainType.ROfRhoAndFt:
                        if (independentAxis == "ft")
                        {
                            var rho = xaxis;
                            var ft = independentValue;
                            complexResults = ROfRhoAndFt(fs, op, rho, ft, noise);
                            xs = independentValues;
                            xyPointsReal = xs.Zip(complexResults, (x, y) => new Point(x, y.Real));
                            xyPointsImag = xs.Zip(complexResults, (x, y) => new Point(x, y.Imaginary));
                            plotDataReal = new PlotData {Data = xyPointsReal, Label = "ROfRhoAndFt"};
                            plotDataImag = new PlotData {Data = xyPointsImag, Label = "ROfRhoAndFt"};
                            plot = new Plots
                            {
                                Id = "ROfRhoAndFtFixedFt", Detector = "R(ρ,ft)", Legend = "R(ρ,ft)", XAxis = "ρ",
                                YAxis = "Reflectance", PlotList = new List<PlotDataJson>()
                            };
                            plot.PlotList.Add(new PlotDataJson
                            {
                                Data = plotDataReal.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                                Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ft=" + ft + "(real)"
                            });
                            plot.PlotList.Add(new PlotDataJson
                            {
                                Data = plotDataImag.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                                Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ft=" + ft + "(imag)"
                            });
                            msg = JsonConvert.SerializeObject(plot);
                        }
                        else
                        {
                            var rho = independentValue;
                            complexResults = ROfRhoAndFt(fs, op, rho, xaxis, noise);
                            xs = independentValues;
                            xyPointsReal = xs.Zip(complexResults, (x, y) => new Point(x, y.Real));
                            xyPointsImag = xs.Zip(complexResults, (x, y) => new Point(x, y.Imaginary));
                            var realPlot = new PlotData {Data = xyPointsReal, Label = "ROfRhoAndFt"};
                            var imagPlot = new PlotData {Data = xyPointsImag, Label = "ROfRhoAndFt"};
                            var rhoPlot = new Plots
                            {
                                Id = "ROfRhoAndFtFixedRho", Detector = "R(ρ,ft)", Legend = "R(ρ,ft)", XAxis = "ft",
                                YAxis = "Reflectance", PlotList = new List<PlotDataJson>()
                            };
                            rhoPlot.PlotList.Add(new PlotDataJson
                            {
                                Data = realPlot.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                                Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ρ=" + rho + "(real)"
                            });
                            rhoPlot.PlotList.Add(new PlotDataJson
                            {
                                Data = imagPlot.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                                Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ρ=" + rho + "(imag)"
                            });
                            msg = JsonConvert.SerializeObject(rhoPlot);
                        }
                        break;
                    case SolutionDomainType.ROfFx:
                        doubleResults = ROfFx(fs, op, xaxis, noise);
                        xs = independentValues;
                        xyPoints = xs.Zip(doubleResults, (x, y) => new Point(x, y));
                        plotData = new PlotData {Data = xyPoints, Label = "ROfFx"};
                        plot = new Plots
                        {
                            Id = "ROfFx", Detector = "R(fx)", Legend = "R(fx)", XAxis = "fx", YAxis = "Reflectance",
                            PlotList = new List<PlotDataJson>()
                        };
                        plot.PlotList.Add(new PlotDataJson
                        {
                            Data = plotData.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                            Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp
                        });
                        msg = JsonConvert.SerializeObject(plot);
                        break;
                    case SolutionDomainType.ROfFxAndTime:
                        if (independentAxis == "t")
                        {
                            var time = independentValue;
                            doubleResults = ROfFxAndTime(fs, op, xaxis, time, noise);
                            xs = independentValues;
                            xyPoints = xs.Zip(doubleResults, (x, y) => new Point(x, y));
                            plotData = new PlotData {Data = xyPoints, Label = "ROfFxAndTime"};
                            plot = new Plots
                            {
                                Id = "ROfFxAndTimeFixedTime", Detector = "R(fx,time)", Legend = "R(fx,time)",
                                XAxis = "fx", YAxis = "Reflectance", PlotList = new List<PlotDataJson>()
                            };
                            plot.PlotList.Add(new PlotDataJson
                            {
                                Data = plotData.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                                Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + time
                            });
                            msg = JsonConvert.SerializeObject(plot);
                        }
                        else
                        {
                            var fx = independentValue;
                            doubleResults = ROfFxAndTime(fs, op, fx, xaxis, noise);
                            xs = independentValues;
                            xyPoints = xs.Zip(doubleResults, (x, y) => new Point(x, y));
                            plotData = new PlotData {Data = xyPoints, Label = "ROfFxAndTime"};
                            plot = new Plots
                            {
                                Id = "ROfFxAndTimeFixedFx", Detector = "R(fx,time)", Legend = "R(fx,time)",
                                XAxis = "Time", YAxis = "Reflectance", PlotList = new List<PlotDataJson>()
                            };
                            plot.PlotList.Add(new PlotDataJson
                            {
                                Data = plotData.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                                Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ρ=" + fx
                            });
                            msg = JsonConvert.SerializeObject(plot);
                        }
                        break;
                    case SolutionDomainType.ROfFxAndFt:
                        if (independentAxis == "ft")
                        {
                            var ft = independentValue;
                            complexResults = ROfFxAndFt(fs, op, xaxis, ft, noise);
                            xs = independentValues;
                            xyPointsReal = xs.Zip(complexResults, (x, y) => new Point(x, y.Real));
                            xyPointsImag = xs.Zip(complexResults, (x, y) => new Point(x, y.Imaginary));
                            plotDataReal = new PlotData {Data = xyPointsReal, Label = "ROfFxAndFt"};
                            plotDataImag = new PlotData {Data = xyPointsImag, Label = "ROfFxAndFt"};
                            plot = new Plots
                            {
                                Id = "ROfFxAndFtFixedFt", Detector = "R(fx,ft)", Legend = "R(fx,ft)", XAxis = "fx",
                                YAxis = "Reflectance", PlotList = new List<PlotDataJson>()
                            };
                            plot.PlotList.Add(new PlotDataJson
                            {
                                Data = plotDataReal.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                                Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ft=" + ft + "(real)"
                            });
                            plot.PlotList.Add(new PlotDataJson
                            {
                                Data = plotDataImag.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                                Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ft=" + ft + "(imag)"
                            });
                            msg = JsonConvert.SerializeObject(plot);
                        }
                        else
                        {
                            var fx = independentValue;
                            complexResults = ROfFxAndFt(fs, op, fx, xaxis, noise);
                            xs = independentValues;
                            xyPointsReal = xs.Zip(complexResults, (x, y) => new Point(x, y.Real));
                            xyPointsImag = xs.Zip(complexResults, (x, y) => new Point(x, y.Imaginary));
                            plotDataReal = new PlotData {Data = xyPointsReal, Label = "ROfFxAndFt"};
                            plotDataImag = new PlotData {Data = xyPointsImag, Label = "ROfFxAndFt"};
                            plot = new Plots
                            {
                                Id = "ROfFxAndFtFixedFx", Detector = "R(fx,ft)", Legend = "R(fx,ft)", XAxis = "ft",
                                YAxis = "Reflectance", PlotList = new List<PlotDataJson>()
                            };
                            plot.PlotList.Add(new PlotDataJson
                            {
                                Data = plotDataReal.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                                Label = fs.ToString() + " μa=" + op.Mua + " μs'=" + op.Musp + " fx=" + fx + "(real)"
                            });
                            plot.PlotList.Add(new PlotDataJson
                            {
                                Data = plotDataImag.Data.Select(item => new List<double> {item.X, item.Y}).ToList(),
                                Label = fs.ToString() + " μa=" + op.Mua + " μs'=" + op.Musp + " fx=" + fx + "(imag)"
                            });
                            msg = JsonConvert.SerializeObject(plot);
                        }
                        break;
                }
                return msg;
            }
            catch (Exception e)
            {
                _logger.LogError("An error occurred: {Message}", e.Message);
                throw;
            }
        }

        private IEnumerable<double> ROfRho(ForwardSolverType fst, OpticalProperties op, DoubleRange rho, double noise)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var fs = SolverFactory.GetForwardSolver(fst);
            if (noise > 0.0)
            {
                return fs.ROfRho(ops, rhos).AddNoise(noise);
            }
            return fs.ROfRho(ops, rhos);
        }

        private IEnumerable<double> ROfRhoAndTime(ForwardSolverType fst, OpticalProperties op, DoubleRange rho, double time, double noise)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var times = time.AsEnumerable();
            var fs = SolverFactory.GetForwardSolver(fst);
            if (noise > 0.0)
            {
                return fs.ROfRhoAndTime(ops, rhos, times).AddNoise(noise);
            }
            return fs.ROfRhoAndTime(ops, rhos, times);
        }

        private IEnumerable<double> ROfRhoAndTime(ForwardSolverType fst, OpticalProperties op, double rho, DoubleRange time, double noise)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var times = time.AsEnumerable();
            var fs = SolverFactory.GetForwardSolver(fst);
            if (noise > 0.0)
            {
                return fs.ROfRhoAndTime(ops, rhos, times).AddNoise(noise);
            }
            return fs.ROfRhoAndTime(ops, rhos, times);
        }

        private IEnumerable<Complex> ROfRhoAndFt(ForwardSolverType fst, OpticalProperties op, DoubleRange rho, double ft, double noise)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var fts = ft.AsEnumerable();
            var fs = SolverFactory.GetForwardSolver(fst);
            var results = fs.ROfRhoAndFt(ops, rhos, fts);
            if (noise > 0.0)
            {
                var realsWithNoise = DataExtensions.AddNoise(results.Select(r => r.Real).ToList(), noise);
                var imagsWithNoise = DataExtensions.AddNoise(results.Select(i => i.Imaginary).ToList(), noise);
                IEnumerable<Complex> resultsWithNoise = realsWithNoise.Zip(imagsWithNoise, (a, b) => new Complex(a,b));
                return resultsWithNoise;
            }
            return fs.ROfRhoAndFt(ops, rhos, fts);
        }

        private IEnumerable<Complex> ROfRhoAndFt(ForwardSolverType fst, OpticalProperties op, double rho, DoubleRange ft, double noise)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var fts = ft.AsEnumerable();
            var fs = SolverFactory.GetForwardSolver(fst);
            var results = fs.ROfRhoAndFt(ops, rhos, fts);
            if (noise > 0.0)
            {
                var realsWithNoise = DataExtensions.AddNoise(results.Select(r => r.Real).ToList(), noise);
                var imagsWithNoise = DataExtensions.AddNoise(results.Select(i => i.Imaginary).ToList(), noise);
                IEnumerable<Complex> resultsWithNoise = realsWithNoise.Zip(imagsWithNoise, (a, b) => new Complex(a, b));
                return resultsWithNoise;
            }
            return fs.ROfRhoAndFt(ops, rhos, fts);
        }

        private IEnumerable<double> ROfFx(ForwardSolverType fst, OpticalProperties op, DoubleRange fx, double noise)
        {
            try
            {
                var ops = op.AsEnumerable();
                var fxs = fx.AsEnumerable();
                var fs = SolverFactory.GetForwardSolver(fst);
                if (noise > 0.0)
                {
                    return fs.ROfFx(ops, fxs).AddNoise(noise);
                }
                return fs.ROfFx(ops, fxs);
            }
            catch (Exception e)
            {
                throw new Exception("Error in call to ROfFx: " + e.Message + "values fst: " + fst + ", op: " + op + ", rho:" + fx + " source: " + e.Source + " inner: " + e.InnerException);
            }
        }

        private IEnumerable<double> ROfFxAndTime(ForwardSolverType fst, OpticalProperties op, DoubleRange fx, double time, double noise)
        {
            var ops = op.AsEnumerable();
            var fxs = fx.AsEnumerable();
            var times = time.AsEnumerable();
            var fs = SolverFactory.GetForwardSolver(fst);
            if (noise > 0.0)
            {
                return fs.ROfFxAndTime(ops, fxs, times).AddNoise(noise);
            }
            return fs.ROfFxAndTime(ops, fxs, times);
        }

        private IEnumerable<double> ROfFxAndTime(ForwardSolverType fst, OpticalProperties op, double fx, DoubleRange time, double noise)
        {
            var ops = op.AsEnumerable();
            var fxs = fx.AsEnumerable();
            var times = time.AsEnumerable();
            var fs = SolverFactory.GetForwardSolver(fst);
            if (noise > 0.0)
            {
                return fs.ROfFxAndTime(ops, fxs, times).AddNoise(noise);
            }
            return fs.ROfFxAndTime(ops, fxs, times);
        }

        private IEnumerable<Complex> ROfFxAndFt(ForwardSolverType fst, OpticalProperties op, DoubleRange fx, double ft, double noise)
        {
            var ops = op.AsEnumerable();
            var fxs = fx.AsEnumerable();
            var fts = ft.AsEnumerable();
            var fs = SolverFactory.GetForwardSolver(fst);
            var results = fs.ROfRhoAndFt(ops, fxs, fts);
            if (noise > 0.0)
            {
                var realsWithNoise = DataExtensions.AddNoise(results.Select(r => r.Real).ToList(), noise);
                var imagsWithNoise = DataExtensions.AddNoise(results.Select(i => i.Imaginary).ToList(), noise);
                IEnumerable<Complex> resultsWithNoise = realsWithNoise.Zip(imagsWithNoise, (a, b) => new Complex(a, b));
                return resultsWithNoise;
            }
            return fs.ROfFxAndFt(ops, fxs, fts);
        }

        private IEnumerable<Complex> ROfFxAndFt(ForwardSolverType fst, OpticalProperties op, double fx, DoubleRange ft, double noise)
        {
            var ops = op.AsEnumerable();
            var fxs = fx.AsEnumerable();
            var fts = ft.AsEnumerable();
            var fs = SolverFactory.GetForwardSolver(fst);
            var results = fs.ROfRhoAndFt(ops, fxs, fts);
            if (noise > 0.0)
            {
                var realsWithNoise = DataExtensions.AddNoise(results.Select(r => r.Real).ToList(), noise);
                var imagsWithNoise = DataExtensions.AddNoise(results.Select(i => i.Imaginary).ToList(), noise);
                IEnumerable<Complex> resultsWithNoise = realsWithNoise.Zip(imagsWithNoise, (a, b) => new Complex(a, b));
                return resultsWithNoise;
            }
            return fs.ROfFxAndFt(ops, fxs, fts);
        }
    }
}
