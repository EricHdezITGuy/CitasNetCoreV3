// Importando las bibliotecas necesarias
using Microsoft.AspNetCore.Mvc;
using AgendadorCitasOnline.Data;
using System.Linq;
using AgendadorCitasOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendadorCitasOnline.Controllers
{
    // Definición de la clase controladora de citas
    public class CitasController : Controller
    {
        // Variable privada para interactuar con la base de datos
        private readonly ApplicationDbContext _context;

        // Constructor del controlador que inicializa la variable _context con el contexto de la base de datos
        public CitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción que muestra la lista de todas las citas
        public IActionResult Index()
        {
            // Consulta la base de datos para obtener todas las citas
            var citas = _context.Citas.ToList();
            // Devuelve la vista Index con la lista de citas
            return View(citas);
        }

        // Acción que muestra el formulario para crear una nueva cita
        public IActionResult Crear()
        {
            // Carga la lista de especialistas y servicios para mostrarlos en un dropdown en la vista
            ViewBag.Especialistas = _context.Especialistas.Select(e => new
            {
                Id = e.ID,
                NombreCompleto = e.Nombre + " " + e.Apellido
            }).ToList();
            ViewBag.Servicios = _context.Servicios.Select(s => new
            {
                Id = s.ID,
                NombreServicio = s.NombreServicio
            }).ToList();
            // Devuelve la vista Crear
            return View();
        }

        // Acción que procesa el formulario de creación de cita
        [HttpPost]
        public IActionResult Crear(Cita cita)
        {
            // Comprueba si los datos del formulario son válidos
            if (ModelState.IsValid)
            {
                // Calcula la hora de finalización de la cita sumando 30 minutos a la hora de inicio
                cita.Fin = cita.Inicio.AddMinutes(30);

                // Agrega la nueva cita a la base de datos y la guarda
                _context.Citas.Add(cita);
                _context.SaveChanges();

                // Redirige al usuario a la página de lista de citas después de guardar la cita
                return RedirectToAction("Index");
            }

            // Si los datos no son válidos, recarga las listas desplegables y muestra el formulario nuevamente
            ViewBag.Especialistas = _context.Especialistas.Select(e => new
            {
                Id = e.ID,
                NombreCompleto = e.Nombre + " " + e.Apellido
            }).ToList();
            ViewBag.Servicios = _context.Servicios.Select(s => new
            {
                Id = s.ID,
                NombreServicio = s.NombreServicio
            }).ToList();
            return View(cita);
        }

        // Acción para mostrar la vista de la cita (en desarrollo)
        public IActionResult Ver()
        {
            return View();
        }

        // Acción para mostrar los detalles de una cita específica
        [HttpPost]
        public IActionResult DetalleCita(int citaId)
        {
            // Busca la cita en la base de datos usando el ID proporcionado
            Cita cita = _context.Citas.FirstOrDefault(c => c.ID == citaId);
            // Si no se encuentra la cita, muestra un mensaje de error
            if (cita == null)
            {
                return View("Error", "Cita no encontrada");
            }
            // Si se encuentra la cita, muestra sus detalles
            return View(cita);
        }
    }
}