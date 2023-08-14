using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AgendadorCitasOnline.Models;

namespace AgendadorCitasOnline.Controllers
{
    public class HomeController : Controller
    {
        // Variable para la gestión de registros
        private readonly ILogger<HomeController> _logger;

        // Constructor para inicializar la variable _logger con el registrador
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Acción para mostrar la página de inicio
        public IActionResult Index()
        {
            return View();
        }

        // Acción para mostrar la página de privacidad
        public IActionResult Privacy()
        {
            return View();
        }

        // Acción para mostrar la página de error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Creación de una nueva instancia del modelo de error
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
