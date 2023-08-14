// Importando las bibliotecas necesarias
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgendadorCitasOnline.Models;
using AgendadorCitasOnline.Data;

// Definición de la clase controladora de especialistas
public class EspecialistasController : Controller
{
    // Variable privada para interactuar con la base de datos
    private readonly ApplicationDbContext _context;

    // Constructor del controlador que inicializa la variable _context con el contexto de la base de datos
    public EspecialistasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Acción que muestra la lista de todos los especialistas
    public IActionResult Index()
    {
        // Consulta la base de datos para obtener todos los especialistas
        var especialistas = _context.Especialistas.ToList();
        // Devuelve la vista Index con la lista de especialistas
        return View(especialistas);
    }
}
