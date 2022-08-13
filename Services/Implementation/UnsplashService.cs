using PrismAndMauiApp1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAndMauiApp1.Services.Implementation;

public class UnsplashService : ApiServiceBase, IUnsplashService
{
    public UnsplashService(IHttpRequestService httpRequestService)
        :base(httpRequestService, "photos/")
    {

    }

    public async Task<List<object>> GetFotos(string Category)
    {
        try
        {
            return await base.HttpRequestService.GetAsync<List<object>>(base.EndPoint).ConfigureAwait(false);
        }
        catch (HttpRequestException)
        {
            DisplayAlertAsync("Error", "Problemas de Conexion a la Red", "Ok");
            return null;
        }
        catch (Exception ex)
        {
            DisplayAlertAsync("Error", ex.Message, "Ok");
            return null;
        }
    }
}
