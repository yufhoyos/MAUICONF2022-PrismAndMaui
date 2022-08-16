using PrismAndMauiApp1.Services.Implementation;
using PrismAndMauiApp1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAndMauiApp1.Services
{
    public abstract class ApiServiceBase
    {
        private string ControllerEndPoint;
        private const string UrlUnsplashAPI = "https://api.unsplash.com/";
        private const string UnsplashAccessKey = "pYtvF2n-T-9sJxd-kLEs9nZaS6UF73RXVLw56_KCEV8";

        protected string EndPoint
        {
            get
            {
                return UrlUnsplashAPI + ControllerEndPoint;
            }
        }

        protected IHttpRequestService HttpRequestService { get; set; }

        public ApiServiceBase(IHttpRequestService httpRequestService, 
            string controllerEndpoint)
        {
            //Dependencies
            HttpRequestService = httpRequestService;
            HttpRequestService.AuthenticationKey = UnsplashAccessKey;
            //Init Endpoint            
            ControllerEndPoint = controllerEndpoint;
        }

        public void ShowLoading(string title = "Cargando...")
        {
            
        }

        public void HideLoading()
        {
            
        }

        public void DisplayAlertAsync(string title, string message, string okText)
        {
          
        }
    }
}
