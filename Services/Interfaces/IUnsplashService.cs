using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAndMauiApp1.Services.Interfaces;

public interface IUnsplashService
{
    Task<List<Object>> GetFotos(string Category);
}
