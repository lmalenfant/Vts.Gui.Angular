using Vts.Common;

namespace Vts.Api.Services
{
    public interface IPlotResultsService
    {
        string Plot(IPlotParameters plotParameters);
        
    }
}
