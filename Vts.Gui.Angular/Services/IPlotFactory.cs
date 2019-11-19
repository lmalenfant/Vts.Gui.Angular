namespace Vts.Api.Services
{
    public interface IPlotFactory
    {
        string GetPlot(PlotType plotType, IPlotParameters plotParameters);
    }
}
