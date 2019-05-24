﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vts.Common;
using Vts.Extensions;
using System.IO;
using Newtonsoft.Json.Linq;
using Vts.Web.Data;
using Newtonsoft.Json;
using Vts.IO;

namespace Vts.Gui.Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VtsController : ControllerBase
    {
        // GET: api/Vts
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Vts
        [HttpPost]
        public string Post([FromBody] dynamic Value)
        {
            var action = "getdata";

            switch (action)
            {
                case "getdata":
                try
                {
                    dynamic vtsSettings = Value;
                    var sd = vtsSettings["solutionDomain"];
                    var fs = vtsSettings["forwardSolverEngine"];
                    var msg = "";
                    var op = new OpticalProperties((double)vtsSettings["opticalProperties"]["mua"], (double)vtsSettings["opticalProperties"]["mus"], (double)vtsSettings["opticalProperties"]["g"], (double)vtsSettings["opticalProperties"]["n"]);
                    if (sd == "rofrho")
                    {
                        var rho = new DoubleRange((double)vtsSettings["range"]["startValue"], (double)vtsSettings["range"]["endValue"], (int)vtsSettings["range"]["numberValue"]);
                        IEnumerable<double> results = null;
                        results = ROfRho(Enum.Parse(typeof(ForwardSolverType), fs.ToString()), op, rho);
                        var independentValues = rho.AsEnumerable().ToArray();
                        var rhos = independentValues.Skip(1).Zip(independentValues.Take(independentValues.Length - 1), (first, second) => (first + second) / 2).ToArray();

                        var rOfRhoPoints = rhos.Zip(results, (x, y) => new Point(x, y));
                        var rOfRhoPlot = new PlotData { Data = rOfRhoPoints, Label = "ROfRho" };
                        var rhoPlot = new Plots { id = "ROfRho", detector = "R(ρ)", legend = "R(ρ)", xaxis = "ρ", yaxis = "Reflectance", plotlist = new List<PlotDataJson>() };
                        rhoPlot.plotlist.Add(new PlotDataJson { data = rOfRhoPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp });
                        msg = JsonConvert.SerializeObject(rhoPlot);
                    }
                    else if (sd == "rofrhoandt")
                    {
                        IEnumerable<double> results = null;
                        var independentAxis = vtsSettings["independentAxes"]["label"];
                        if (independentAxis == "t")
                        {
                            var rho = new DoubleRange(double.Parse(vtsSettings["range"]["startValue"]), double.Parse(vtsSettings["range"]["endValue"]), int.Parse(vtsSettings["range"]["numberValue"]));
                            var time = double.Parse(vtsSettings["independentAxes"]["value"]);
                            results = ROfRhoAndTime(Enum.Parse(typeof(ForwardSolverType), fs), op, rho, time);
                            var independentValues = rho.AsEnumerable().ToArray();
                            var rhos = independentValues.Skip(1).Zip(independentValues.Take(independentValues.Length - 1), (first, second) => (first + second) / 2).ToArray();
                            var rOfRhoPoints = rhos.Zip(results, (x, y) => new Point(x, y));
                            var rOfRhoPlot = new PlotData { Data = rOfRhoPoints, Label = "ROfRhoAndTime" };
                            var rhoPlot = new Plots { id = "ROfRhoAndTimeFixedTime", detector = "R(ρ,time)", legend = "R(ρ,time)", xaxis = "ρ", yaxis = "Reflectance", plotlist = new List<PlotDataJson>() };
                            rhoPlot.plotlist.Add(new PlotDataJson { data = rOfRhoPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + time });
                            msg = JsonConvert.SerializeObject(rhoPlot);
                        }
                        else
                        {
                            var time = new DoubleRange(double.Parse(vtsSettings["range"]["startValue"]), double.Parse(vtsSettings["range"]["endValue"]), int.Parse(vtsSettings["range"]["numberValue"]));
                            var rho = double.Parse(vtsSettings["independentAxes"]["value"]);
                            results = ROfRhoAndTime(Enum.Parse(typeof(ForwardSolverType), fs), op, rho, time);
                            var independentValues = time.AsEnumerable().ToArray();
                            var times = independentValues.Skip(1).Zip(independentValues.Take(independentValues.Length - 1), (first, second) => (first + second) / 2).ToArray();
                            var dataPoints = times.Zip(results, (x, y) => new Point(x, y));
                            var dataPlot = new PlotData { Data = dataPoints, Label = "ROfRhoAndTime" };
                            var rhoPlot = new Plots { id = "ROfRhoAndTimeFixedRho", detector = "R(ρ,time)", legend = "R(ρ,time)", xaxis = "Time", yaxis = "Reflectance", plotlist = new List<PlotDataJson>() };
                            rhoPlot.plotlist.Add(new PlotDataJson { data = dataPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ρ=" + rho });
                            msg = JsonConvert.SerializeObject(rhoPlot);
                        }
                    }
                    else if (sd == "rofrhoandft")
                    {
                        IEnumerable<Complex> results;
                        var independentAxis = vtsSettings["independentAxes"]["label"];
                        if (independentAxis == "ft")
                        {
                            var rho = new DoubleRange(double.Parse(vtsSettings["range"]["startValue"]), double.Parse(vtsSettings["range"]["endValue"]), int.Parse(vtsSettings["range"]["numberValue"]));
                            var ft = double.Parse(vtsSettings["independentAxes"]["value"]);
                            results = ROfRhoAndFt(Enum.Parse(typeof(ForwardSolverType), fs), op, rho, ft);
                            var independentValues = rho.AsEnumerable().ToArray();
                            var rhos = independentValues.Skip(1).Zip(independentValues.Take(independentValues.Length - 1), (first, second) => (first + second) / 2).ToArray();
                            var realPoints = rhos.Zip(results, (x, y) => new Point(x, y.Real));
                            var imagPoints = rhos.Zip(results, (x, y) => new Point(x, y.Imaginary));
                            var realPlot = new PlotData { Data = realPoints, Label = "ROfRhoAndFt" };
                            var imagPlot = new PlotData { Data = imagPoints, Label = "ROfRhoAndFt" };
                            var rhoPlot = new Plots { id = "ROfRhoAndFtFixedFt", detector = "R(ρ,ft)", legend = "R(ρ,ft)", xaxis = "ρ", yaxis = "Reflectance", plotlist = new List<PlotDataJson>() };
                            rhoPlot.plotlist.Add(new PlotDataJson { data = realPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + ft + "(real)" });
                            rhoPlot.plotlist.Add(new PlotDataJson { data = imagPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + ft + "(imag)" });
                            msg = JsonConvert.SerializeObject(rhoPlot);
                        }
                        else
                        {
                            var ft = new DoubleRange(double.Parse(vtsSettings["range"]["startValue"]), double.Parse(vtsSettings["range"]["endValue"]), int.Parse(vtsSettings["range"]["numberValue"]));
                            var rho = double.Parse(vtsSettings["independentAxes"]["value"]);
                            results = ROfRhoAndFt(Enum.Parse(typeof(ForwardSolverType), fs), op, rho, ft);
                            var independentValues = ft.AsEnumerable().ToArray();
                            var times = independentValues.Skip(1).Zip(independentValues.Take(independentValues.Length - 1), (first, second) => (first + second) / 2).ToArray();
                            var realPoints = times.Zip(results, (x, y) => new Point(x, y.Real));
                            var imagPoints = times.Zip(results, (x, y) => new Point(x, y.Imaginary));
                            var realPlot = new PlotData { Data = realPoints, Label = "ROfRhoAndFt" };
                            var imagPlot = new PlotData { Data = imagPoints, Label = "ROfRhoAndFt" };
                            var rhoPlot = new Plots { id = "ROfRhoAndFtFixedRho", detector = "R(ρ,ft)", legend = "R(ρ,ft)", xaxis = "Time", yaxis = "Reflectance", plotlist = new List<PlotDataJson>() };
                            rhoPlot.plotlist.Add(new PlotDataJson { data = realPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ρ=" + rho + "(real)" });
                            rhoPlot.plotlist.Add(new PlotDataJson { data = imagPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ρ=" + rho + "(imag)" });
                            msg = JsonConvert.SerializeObject(rhoPlot);
                        }
                    }
                    else if (sd == "roffx")
                    {
                        var fx = new DoubleRange(double.Parse(vtsSettings["range"]["startValue"]), double.Parse(vtsSettings["range"]["endValue"]), int.Parse(vtsSettings["range"]["numberValue"]));
                        IEnumerable<double> results = null;
                        results = ROfFx(Enum.Parse(typeof(ForwardSolverType), fs.ToString()), op, fx);
                        var independentValues = fx.AsEnumerable().ToArray();
                        var fxs = independentValues.Skip(1).Zip(independentValues.Take(independentValues.Length - 1), (first, second) => (first + second) / 2).ToArray();

                        var rOfFxPoints = fxs.Zip(results, (x, y) => new Point(x, y));
                        var rOfFxPlot = new PlotData { Data = rOfFxPoints, Label = "ROfFx" };
                        var fxPlot = new Plots { id = "ROfFx", detector = "R(fx)", legend = "R(fx)", xaxis = "fx", yaxis = "Reflectance", plotlist = new List<PlotDataJson>() };
                        fxPlot.plotlist.Add(new PlotDataJson { data = rOfFxPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp });
                        msg = JsonConvert.SerializeObject(fxPlot);
                    }
                    else if (sd == "roffxandt")
                    {
                        IEnumerable<double> results = null;
                        var independentAxis = vtsSettings["independentAxes"]["label"];
                        if (independentAxis == "t")
                        {
                            var fx = new DoubleRange(double.Parse(vtsSettings["range"]["startValue"]), double.Parse(vtsSettings["range"]["endValue"]), int.Parse(vtsSettings["range"]["numberValue"]));
                            var time = double.Parse(vtsSettings["independentAxes"]["value"]);
                            results = ROfFxAndTime(Enum.Parse(typeof(ForwardSolverType), fs), op, fx, time);
                            var independentValues = fx.AsEnumerable().ToArray();
                            var fxs = independentValues.Skip(1).Zip(independentValues.Take(independentValues.Length - 1), (first, second) => (first + second) / 2).ToArray();
                            var rOfFxPoints = fxs.Zip(results, (x, y) => new Point(x, y));
                            var rOfFxPlot = new PlotData { Data = rOfFxPoints, Label = "ROfFxAndTime" };
                            var fxPlot = new Plots { id = "ROfFxAndTimeFixedTime", detector = "R(fx,time)", legend = "R(fx,time)", xaxis = "fx", yaxis = "Reflectance", plotlist = new List<PlotDataJson>() };
                            fxPlot.plotlist.Add(new PlotDataJson { data = rOfFxPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + time });
                            msg = JsonConvert.SerializeObject(fxPlot);
                        }
                        else
                        {
                            var time = new DoubleRange(double.Parse(vtsSettings["range"]["startValue"]), double.Parse(vtsSettings["range"]["endValue"]), int.Parse(vtsSettings["range"]["numberValue"]));
                            var fx = double.Parse(vtsSettings["independentAxes"]["value"]);
                            results = ROfFxAndTime(Enum.Parse(typeof(ForwardSolverType), fs), op, fx, time);
                            var independentValues = time.AsEnumerable().ToArray();
                            var times = independentValues.Skip(1).Zip(independentValues.Take(independentValues.Length - 1), (first, second) => (first + second) / 2).ToArray();
                            var dataPoints = times.Zip(results, (x, y) => new Point(x, y));
                            var dataPlot = new PlotData { Data = dataPoints, Label = "ROfFxAndTime" };
                            var fxPlot = new Plots { id = "ROfFxAndTimeFixedFx", detector = "R(fx,time)", legend = "R(fx,time)", xaxis = "Time", yaxis = "Reflectance", plotlist = new List<PlotDataJson>() };
                            fxPlot.plotlist.Add(new PlotDataJson { data = dataPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " ρ=" + fx });
                            msg = JsonConvert.SerializeObject(fxPlot);
                        }
                    }
                    else if (sd == "roffxandft")
                    {
                        IEnumerable<Complex> results;
                        var independentAxis = vtsSettings["independentAxes"]["label"];
                        if (independentAxis == "ft")
                        {
                            var fx = new DoubleRange(double.Parse(vtsSettings["range"]["startValue"]), double.Parse(vtsSettings["range"]["endValue"]), int.Parse(vtsSettings["range"]["numberValue"]));
                            var ft = double.Parse(vtsSettings["independentAxes"]["value"]);
                            results = ROfFxAndFt(Enum.Parse(typeof(ForwardSolverType), fs), op, fx, ft);
                            var independentValues = fx.AsEnumerable().ToArray();
                            var fxs = independentValues.Skip(1).Zip(independentValues.Take(independentValues.Length - 1), (first, second) => (first + second) / 2).ToArray();
                            var realPoints = fxs.Zip(results, (x, y) => new Point(x, y.Real));
                            var imagPoints = fxs.Zip(results, (x, y) => new Point(x, y.Imaginary));
                            var realPlot = new PlotData { Data = realPoints, Label = "ROfFxAndFt" };
                            var imagPlot = new PlotData { Data = imagPoints, Label = "ROfFxAndFt" };
                            var fxPlot = new Plots { id = "ROfFxAndFtFixedFt", detector = "R(fx,ft)", legend = "R(fx,ft)", xaxis = "fx", yaxis = "Reflectance", plotlist = new List<PlotDataJson>() };
                            fxPlot.plotlist.Add(new PlotDataJson { data = realPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + ft + "(real)" });
                            fxPlot.plotlist.Add(new PlotDataJson { data = imagPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " t=" + ft + "(imag)" });
                            msg = JsonConvert.SerializeObject(fxPlot);
                        }
                        else
                        {
                            var ft = new DoubleRange(double.Parse(vtsSettings["range"]["startValue"]), double.Parse(vtsSettings["range"]["endValue"]), int.Parse(vtsSettings["range"]["numberValue"]));
                            var fx = double.Parse(vtsSettings["independentAxes"]["value"]);
                            results = ROfFxAndFt(Enum.Parse(typeof(ForwardSolverType), fs), op, fx, ft);
                            var independentValues = ft.AsEnumerable().ToArray();
                            var times = independentValues.Skip(1).Zip(independentValues.Take(independentValues.Length - 1), (first, second) => (first + second) / 2).ToArray();
                            var realPoints = times.Zip(results, (x, y) => new Point(x, y.Real));
                            var imagPoints = times.Zip(results, (x, y) => new Point(x, y.Imaginary));
                            var realPlot = new PlotData { Data = realPoints, Label = "ROfFxAndFt" };
                            var imagPlot = new PlotData { Data = imagPoints, Label = "ROfFxAndFt" };
                            var fxPlot = new Plots { id = "ROfFxAndFtFixedFx", detector = "R(fx,ft)", legend = "R(fx,ft)", xaxis = "Time", yaxis = "Reflectance", plotlist = new List<PlotDataJson>() };
                            fxPlot.plotlist.Add(new PlotDataJson { data = realPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " fx=" + fx + "(real)" });
                            fxPlot.plotlist.Add(new PlotDataJson { data = imagPlot.Data.Select(item => new List<double> { item.X, item.Y }).ToList(), label = fs + " μa=" + op.Mua + " μs'=" + op.Musp + " fx=" + fx + "(imag)" });
                            msg = JsonConvert.SerializeObject(fxPlot);
                        }
                    }
                    return msg;
                }
                catch (Exception e)
                {
                    throw new Exception("Error during action: " + e.Message);
                }
                case "getfile":
                    string dataLocation = "Modeling/Resources/HankelData/";
                    var hankelPoints = (double[])FileIO.ReadArrayFromBinaryInResources<double>
                        (dataLocation + @"basepoints.dat", "Vts", 801);
                    return JsonConvert.SerializeObject(hankelPoints);
                default:
                    return "nothing happened";
            }
        }

        // GET: api/Vts/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT: api/Vts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private IEnumerable<double> ROfRho(ForwardSolverType fst, OpticalProperties op, DoubleRange rho)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            return fs.ROfRho(ops, rhos);
        }

        private IEnumerable<double> ROfRhoAndTime(ForwardSolverType fst, OpticalProperties op, DoubleRange rho, double time)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var times = time.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            return fs.ROfRhoAndTime(ops, rhos, times);
        }

        private IEnumerable<double> ROfRhoAndTime(ForwardSolverType fst, OpticalProperties op, double rho, DoubleRange time)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var times = time.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            return fs.ROfRhoAndTime(ops, rhos, times);
        }

        private IEnumerable<Complex> ROfRhoAndFt(ForwardSolverType fst, OpticalProperties op, DoubleRange rho, double ft)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var fts = ft.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            return fs.ROfRhoAndFt(ops, rhos, fts);
        }

        private IEnumerable<Complex> ROfRhoAndFt(ForwardSolverType fst, OpticalProperties op, double rho, DoubleRange ft)
        {
            var ops = op.AsEnumerable();
            var rhos = rho.AsEnumerable();
            var fts = ft.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            return fs.ROfRhoAndFt(ops, rhos, fts);
        }

        private IEnumerable<double> ROfFx(ForwardSolverType fst, OpticalProperties op, DoubleRange fx)
        {
            try
            {
                var ops = op.AsEnumerable();
                var rhos = fx.AsEnumerable();
                var fs = Factories.SolverFactory.GetForwardSolver(fst);
                return fs.ROfFx(ops, rhos);
            }
            catch (Exception e)
            {
                throw new Exception("Error in call to ROfFx: " + e.Message + "values fst: " + fst + ", op: " + op + ", rho:" + fx + " source: " + e.Source + " inner: " + e.InnerException);
            }
        }

        private IEnumerable<double> ROfFxAndTime(ForwardSolverType fst, OpticalProperties op, DoubleRange fx, double time)
        {
            var ops = op.AsEnumerable();
            var fxs = fx.AsEnumerable();
            var times = time.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            return fs.ROfFxAndTime(ops, fxs, times);
        }

        private IEnumerable<double> ROfFxAndTime(ForwardSolverType fst, OpticalProperties op, double fx, DoubleRange time)
        {
            var ops = op.AsEnumerable();
            var fxs = fx.AsEnumerable();
            var times = time.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            return fs.ROfFxAndTime(ops, fxs, times);
        }

        private IEnumerable<Complex> ROfFxAndFt(ForwardSolverType fst, OpticalProperties op, DoubleRange fx, double ft)
        {
            var ops = op.AsEnumerable();
            var fxs = fx.AsEnumerable();
            var fts = ft.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            return fs.ROfFxAndFt(ops, fxs, fts);
        }

        private IEnumerable<Complex> ROfFxAndFt(ForwardSolverType fst, OpticalProperties op, double fx, DoubleRange ft)
        {
            var ops = op.AsEnumerable();
            var fxs = fx.AsEnumerable();
            var fts = ft.AsEnumerable();
            var fs = Factories.SolverFactory.GetForwardSolver(fst);
            return fs.ROfFxAndFt(ops, fxs, fts);
        }
    }
}
