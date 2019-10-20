using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Newtonsoft.Json;
using Vts.Common;
using Vts.Extensions;
using Vts.Api.Data;

namespace Vts.Api.Services
{
    public class PlotResults
    {
        public static string PlotBasedOnSolutionDomain(ForwardSolverType fs, string sd, DoubleRange xaxis, 
            OpticalProperties op, string independentAxis, double independentValue, double noise)
        {
            try
            {
                var msg = "";
                var independentValues = xaxis.AsEnumerable().ToArray();
                if (sd == "ROfRho")
                {
                    IEnumerable<double> results = null;
                    results = ROfRho(fs, op, xaxis, noise);
                    //var rhos = independentValues.Skip(1).Zip(independentValues.Take(independentValues.Length - 1), (first, second) => (first + second) / 2).ToArray();
                    var rhos = independentValues;
                    var rOfRhoPoints = rhos.Zip(results, (x, y) => new Point(x, y));
                    var rOfRhoPlot = new PlotData { Data = rOfRhoPoints, Label = "ROfRho" };
                    var rhoPlot = new Plots { Id = "ROfRho", Detector = "R(ρ)", Legend = "R(ρ)", XAxis = "ρ", YAxis = "Reflectance", PlotList = new List<PlotDataJson>() };
                    rhoPlot.PlotList.Add(new PlotDataJson { Data = rOfRhoPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp });
                    msg = JsonConvert.SerializeObject(rhoPlot);
                }
                else if (sd == "ROfRhoAndTime")
                {
                    IEnumerable<double> results = null;
                    if (independentAxis == "t")
                    {
                        var time = independentValue;
                        results = ROfRhoAndTime(fs, op, xaxis, time, noise);
                        var rhos = independentValues;
                        var rOfRhoPoints = rhos.Zip(results, (x, y) => new Point(x, y));
                        var rOfRhoPlot = new PlotData { Data = rOfRhoPoints, Label = "ROfRhoAndTime" };
                        var rhoPlot = new Plots { Id = "ROfRhoAndTimeFixedTime", Detector = "R(ρ,time)", Legend = "R(ρ,time)", XAxis = "ρ", YAxis = "Reflectance", PlotList = new List<PlotDataJson>() };
                        rhoPlot.PlotList.Add(new PlotDataJson { Data = rOfRhoPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + time });
                        msg = JsonConvert.SerializeObject(rhoPlot);
                    }
                    else
                    {
                        var rho = independentValue;
                        results = ROfRhoAndTime(fs, op, rho, xaxis, noise);
                        var times = independentValues.ToArray(); // the Skip(1) is giving inverse problems
                        var dataPoints = times.Zip(results, (x, y) => new Point(x, y));
                        var dataPlot = new PlotData { Data = dataPoints, Label = "ROfRhoAndTime" };
                        var rhoPlot = new Plots { Id = "ROfRhoAndTimeFixedRho", Detector = "R(ρ,time)", Legend = "R(ρ,time)", XAxis = "Time", YAxis = "Reflectance", PlotList = new List<PlotDataJson>() };
                        rhoPlot.PlotList.Add(new PlotDataJson { Data = dataPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ρ=" + rho });
                        msg = JsonConvert.SerializeObject(rhoPlot);
                    }
                }
                else if (sd == "ROfRhoAndFt")
                {
                    IEnumerable<Complex> results;
                    if (independentAxis == "ft")
                    {
                        var rho = xaxis;
                        var ft = independentValue;
                        results = ROfRhoAndFt(fs, op, rho, ft, noise);
                        var rhos = independentValues;
                        var realPoints = rhos.Zip(results, (x, y) => new Point(x, y.Real));
                        var imagPoints = rhos.Zip(results, (x, y) => new Point(x, y.Imaginary));
                        var realPlot = new PlotData { Data = realPoints, Label = "ROfRhoAndFt" };
                        var imagPlot = new PlotData { Data = imagPoints, Label = "ROfRhoAndFt" };
                        var rhoPlot = new Plots { Id = "ROfRhoAndFtFixedFt", Detector = "R(ρ,ft)", Legend = "R(ρ,ft)", XAxis = "ρ", YAxis = "Reflectance", PlotList = new List<PlotDataJson>() };
                        rhoPlot.PlotList.Add(new PlotDataJson { Data = realPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + ft + "(real)" });
                        rhoPlot.PlotList.Add(new PlotDataJson { Data = imagPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + ft + "(imag)" });
                        msg = JsonConvert.SerializeObject(rhoPlot);
                    }
                    else
                    {
                        var rho = independentValue;
                        results = ROfRhoAndFt(fs, op, rho, xaxis, noise);
                        var fts = independentValues;
                        var realPoints = fts.Zip(results, (x, y) => new Point(x, y.Real));
                        var imagPoints = fts.Zip(results, (x, y) => new Point(x, y.Imaginary));
                        var realPlot = new PlotData { Data = realPoints, Label = "ROfRhoAndFt" };
                        var imagPlot = new PlotData { Data = imagPoints, Label = "ROfRhoAndFt" };
                        var rhoPlot = new Plots { Id = "ROfRhoAndFtFixedRho", Detector = "R(ρ,ft)", Legend = "R(ρ,ft)", XAxis = "ft", YAxis = "Reflectance", PlotList = new List<PlotDataJson>() };
                        rhoPlot.PlotList.Add(new PlotDataJson { Data = realPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ρ=" + rho + "(real)" });
                        rhoPlot.PlotList.Add(new PlotDataJson { Data = imagPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ρ=" + rho + "(imag)" });
                        msg = JsonConvert.SerializeObject(rhoPlot);
                    }
                }
                else if (sd == "ROfFx")
                {
                    IEnumerable<double> results = null;
                    results = ROfFx(fs, op, xaxis, noise);
                    var fxs = independentValues;
                    var rOfFxPoints = fxs.Zip(results, (x, y) => new Point(x, y));
                    var rOfFxPlot = new PlotData { Data = rOfFxPoints, Label = "ROfFx" };
                    var fxPlot = new Plots { Id = "ROfFx", Detector = "R(fx)", Legend = "R(fx)", XAxis = "fx", YAxis = "Reflectance", PlotList = new List<PlotDataJson>() };
                    fxPlot.PlotList.Add(new PlotDataJson { Data = rOfFxPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp });
                    msg = JsonConvert.SerializeObject(fxPlot);
                }
                else if (sd == "ROfFxAndTime")
                {
                    IEnumerable<double> results = null;
                    if (independentAxis == "t")
                    {
                        var time = independentValue;
                        results = ROfFxAndTime(fs, op, xaxis, time, noise);
                        var fxs = independentValues;
                        var rOfFxPoints = fxs.Zip(results, (x, y) => new Point(x, y));
                        var rOfFxPlot = new PlotData { Data = rOfFxPoints, Label = "ROfFxAndTime" };
                        var fxPlot = new Plots { Id = "ROfFxAndTimeFixedTime", Detector = "R(fx,time)", Legend = "R(fx,time)", XAxis = "fx", YAxis = "Reflectance", PlotList = new List<PlotDataJson>() };
                        fxPlot.PlotList.Add(new PlotDataJson { Data = rOfFxPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + time });
                        msg = JsonConvert.SerializeObject(fxPlot);
                    }
                    else
                    {
                        var fx = independentValue;
                        results = ROfFxAndTime(fs, op, fx, xaxis, noise);
                        var fts = independentValues;
                        var dataPoints = fts.Zip(results, (x, y) => new Point(x, y));
                        var dataPlot = new PlotData { Data = dataPoints, Label = "ROfFxAndTime" };
                        var fxPlot = new Plots { Id = "ROfFxAndTimeFixedFx", Detector = "R(fx,time)", Legend = "R(fx,time)", XAxis = "Time", YAxis = "Reflectance", PlotList = new List<PlotDataJson>() };
                        fxPlot.PlotList.Add(new PlotDataJson { Data = dataPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ρ=" + fx });
                        msg = JsonConvert.SerializeObject(fxPlot);
                    }
                }
                else if (sd == "ROfFxAndFt")
                {
                    IEnumerable<Complex> results;
                    if (independentAxis == "ft")
                    {
                        var ft = independentValue;
                        results = ROfFxAndFt(fs, op, xaxis, ft, noise);
                        var fxs = independentValues;
                        var realPoints = fxs.Zip(results, (x, y) => new Point(x, y.Real));
                        var imagPoints = fxs.Zip(results, (x, y) => new Point(x, y.Imaginary));
                        var realPlot = new PlotData { Data = realPoints, Label = "ROfFxAndFt" };
                        var imagPlot = new PlotData { Data = imagPoints, Label = "ROfFxAndFt" };
                        var fxPlot = new Plots { Id = "ROfFxAndFtFixedFt", Detector = "R(fx,ft)", Legend = "R(fx,ft)", XAxis = "fx", YAxis = "Reflectance", PlotList = new List<PlotDataJson>() };
                        fxPlot.PlotList.Add(new PlotDataJson { Data = realPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + ft + "(real)" });
                        fxPlot.PlotList.Add(new PlotDataJson { Data = imagPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + ft + "(imag)" });
                        msg = JsonConvert.SerializeObject(fxPlot);
                    }
                    else
                    {
                        var fx = independentValue;
                        results = ROfFxAndFt(fs, op, fx, xaxis, noise);
                        var fts = independentValues;
                        var realPoints = fts.Zip(results, (x, y) => new Point(x, y.Real));
                        var imagPoints = fts.Zip(results, (x, y) => new Point(x, y.Imaginary));
                        var realPlot = new PlotData { Data = realPoints, Label = "ROfFxAndFt" };
                        var imagPlot = new PlotData { Data = imagPoints, Label = "ROfFxAndFt" };
                        var fxPlot = new Plots { Id = "ROfFxAndFtFixedFx", Detector = "R(fx,ft)", Legend = "R(fx,ft)", XAxis = "ft", YAxis = "Reflectance", PlotList = new List<PlotDataJson>() };
                        fxPlot.PlotList.Add(new PlotDataJson { Data = realPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs.ToString() + " μa=" + op.Mua + " μs'=" + op.Musp + " fx=" + fx + "(real)" });
                        fxPlot.PlotList.Add(new PlotDataJson { Data = imagPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), Label = fs.ToString() + " μa=" + op.Mua + " μs'=" + op.Musp + " fx=" + fx + "(imag)" });
                        msg = JsonConvert.SerializeObject(fxPlot);
                    }
                }
                return msg;
            }
            catch (Exception e)
            {
                throw new Exception("Error during action: " + e.Message);
            }
        }

        private static IEnumerable<double> ROfRho(ForwardSolverType fst, OpticalProperties op, DoubleRange rho, double noise)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            if (noise > 0.0)
            {
                return fs.ROfRho(ops, rhos).AddNoise(noise);
            }
            return fs.ROfRho(ops, rhos);
        }

        private static IEnumerable<double> ROfRhoAndTime(ForwardSolverType fst, OpticalProperties op, DoubleRange rho, double time, double noise)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var times = time.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            if (noise > 0.0)
            {
                return fs.ROfRhoAndTime(ops, rhos, times).AddNoise(noise);
            }
            return fs.ROfRhoAndTime(ops, rhos, times);
        }

        private static IEnumerable<double> ROfRhoAndTime(ForwardSolverType fst, OpticalProperties op, double rho, DoubleRange time, double noise)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var times = time.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            if (noise > 0.0)
            {
                return fs.ROfRhoAndTime(ops, rhos, times).AddNoise(noise);
            }
            return fs.ROfRhoAndTime(ops, rhos, times);
        }

        private static IEnumerable<Complex> ROfRhoAndFt(ForwardSolverType fst, OpticalProperties op, DoubleRange rho, double ft, double noise)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var fts = ft.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
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

        private static IEnumerable<Complex> ROfRhoAndFt(ForwardSolverType fst, OpticalProperties op, double rho, DoubleRange ft, double noise)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var fts = ft.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
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

        private static IEnumerable<double> ROfFx(ForwardSolverType fst, OpticalProperties op, DoubleRange fx, double noise)
        {
            try
            {
                var ops = op.AsEnumerable();
                var fxs = fx.AsEnumerable();
                var fs = Factories.SolverFactory.GetForwardSolver(fst);
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

        private static IEnumerable<double> ROfFxAndTime(ForwardSolverType fst, OpticalProperties op, DoubleRange fx, double time, double noise)
        {
            var ops = op.AsEnumerable();
            var fxs = fx.AsEnumerable();
            var times = time.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            if (noise > 0.0)
            {
                return fs.ROfFxAndTime(ops, fxs, times).AddNoise(noise);
            }
            return fs.ROfFxAndTime(ops, fxs, times);
        }

        private static IEnumerable<double> ROfFxAndTime(ForwardSolverType fst, OpticalProperties op, double fx, DoubleRange time, double noise)
        {
            var ops = op.AsEnumerable();
            var fxs = fx.AsEnumerable();
            var times = time.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            if (noise > 0.0)
            {
                return fs.ROfFxAndTime(ops, fxs, times).AddNoise(noise);
            }
            return fs.ROfFxAndTime(ops, fxs, times);
        }

        private static IEnumerable<Complex> ROfFxAndFt(ForwardSolverType fst, OpticalProperties op, DoubleRange fx, double ft, double noise)
        {
            var ops = op.AsEnumerable();
            var fxs = fx.AsEnumerable();
            var fts = ft.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
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

        private static IEnumerable<Complex> ROfFxAndFt(ForwardSolverType fst, OpticalProperties op, double fx, DoubleRange ft, double noise)
        {
            var ops = op.AsEnumerable();
            var fxs = fx.AsEnumerable();
            var fts = ft.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
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
