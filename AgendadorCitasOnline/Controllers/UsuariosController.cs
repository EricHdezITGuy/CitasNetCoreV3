using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AgendadorCitasOnline.Models;
using AgendadorCitasOnline.Data;

public class UsuariosController : Controller
{
    // Variable para el contexto de la base de datos
    private readonly ApplicationDbContext _context;

    // Constructor que inicializa la variable _context con el contexto de la base de datos
    public UsuariosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Acción asíncrona para mostrar la lista de usuarios
    public async Task<IActionResult> Index()
    {
        // Obtiene todos los usuarios de la base de datos de manera asíncrona
        var usuarios = await _context.Usuarios.ToListAsync();

        // Envía la lista de usuarios a la vista de manera asíncrona
        return View(usuarios);
    }
}