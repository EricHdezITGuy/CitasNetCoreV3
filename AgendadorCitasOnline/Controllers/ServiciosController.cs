using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgendadorCitasOnline.Models;
using AgendadorCitasOnline.Data;

public class ServiciosController : Controller
{
    // Variable para el contexto de la base de datos
    private readonly ApplicationDbContext _context;

    // Constructor que inicializa la variable _context con el contexto de la base de datos
    public ServiciosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Acción para mostrar la lista de servicios
    public IActionResult Index()
    {
        // Obtiene los servicios de la base de datos
        var servicios = _context.Servicios.ToList();

        // Envía la lista de servicios a la vista
        return View(servicios);
    }
}
