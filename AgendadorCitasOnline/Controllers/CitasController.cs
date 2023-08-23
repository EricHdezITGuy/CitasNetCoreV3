using Microsoft.AspNetCore.Mvc;
using AgendadorCitasOnline.Data;
using System.Linq;
using AgendadorCitasOnline.Models;
using Microsoft.EntityFrameworkCore;
using System; // Para el manejo de excepciones

namespace AgendadorCitasOnline.Controllers
{
    public class CitasController : Controller
    {
        // Contexto de la base de datos.
        private readonly ApplicationDbContext _context;

        // Constructor que inicializa el contexto de la base de datos.
        public CitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Función que muestra una lista de todas las citas.
        public IActionResult Index()
        {
            var citas = _context.Citas.Include(c => c.IDEspecialista).Include(c => c.Servicio).ToList();
            return View(citas);
        }

        // Función que muestra el formulario para crear una nueva cita.
        public IActionResult Crear()
        {
            CargarViewData();
            return View();
        }

        // Función que muestra detalles de una cita
        public IActionResult Ver()
        {
            CargarViewData();

            // Mensajes de éxito y error.
            if (TempData["Success"] != null) {
                ViewBag.SuccessMessage = TempData["Success"];
            }

            if (TempData["Error"] != null)
            {
                ViewBag.ErrorMessage = TempData["Error"];
            }

            return View();
        }

        // Acción POST que obtiene detalles de una cita específica.
        [HttpPost]
        public IActionResult Cita(int citaID)
        {
            var cita = _context.Citas.Find (citaID);
            if (cita == null)
            {
                TempData["Error"] = "El número de cita indicado no existe, por favor intente de nuevo.";
                return RedirectToAction("Ver", "Citas");

            }
            CargarViewData();
            Cita newCita = new Cita
            {

                Inicio = cita.Inicio,
                Fin = cita.Fin,
                Estado = cita.Estado,
                CedulaUsuario = cita.CedulaUsuario,
                IDEspecialista = cita.IDEspecialista,
                IDServicio = cita.IDServicio,
                ID = cita.ID
            };
            ViewBag.especialista = _context.Especialistas.Find(cita.IDEspecialista);
            ViewBag.usuario = _context.Usuarios.Find(cita.CedulaUsuario);
            ViewBag.cancelable = cita.Estado != "Cancelada" & DateTime.Now.AddDays(1) < newCita.Inicio;
            return View(newCita);
        }

        // Acción POST que cancela una cita.
        [HttpPost]
        public IActionResult Cancelar(int id)
        {
            var cita = _context.Citas.Find(id);
            if (cita == null)
            {
                return NotFound();
            }
            cita.Estado = "Cancelada";
            _context.Update(cita);
            _context.SaveChanges();
            TempData["Success"] = $"Su cita {id} se canceló exitosamente.";
            return RedirectToAction("Ver", "Citas");
        }

        // Acción POST que crea una nueva cita.
        [HttpPost]
        public IActionResult Crear(Cita cita, string FechaInicio, string HoraInicio, string NombreUsuario, string ApellidoUsuario, string CorreoElectronico, int CedulaUsuario)
        {
            // Combina la fecha y hora para crear un objeto DateTime completo
            var fechaCompleta = DateTime.Parse($"{FechaInicio} {HoraInicio}");
            cita.Inicio = fechaCompleta;

            // Validaciones manuales primero
            if (cita.Inicio.Date < DateTime.Now.AddDays(7).Date)
            {
                ModelState.AddModelError("FechaInicio", "Por favor, selecciona una fecha luego de los próximos 7 días.");
            }
            if (cita.Inicio.DayOfWeek == DayOfWeek.Saturday || cita.Inicio.DayOfWeek == DayOfWeek.Sunday)
            {
                ModelState.AddModelError("FechaInicio", "Por favor, selecciona un día entre Lunes y Viernes.");
            }
            if (cita.Inicio.Hour < 7 || (cita.Inicio.Hour >= 16 || (cita.Inicio.Hour == 15 && cita.Inicio.Minute > 30)))
            {
                ModelState.AddModelError("HoraInicio", "Por favor, selecciona una hora entre las 7:00am y las 3:30pm.");
            }

            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Creación del usuario
                        var newUser = new Usuario
                        {
                            Cedula = CedulaUsuario,
                            Nombre = NombreUsuario,  // Llamando el dato desde Crear.cshtml
                            Apellido = ApellidoUsuario, // Lo mismo aquí
                            CorreoElectronico = CorreoElectronico // Y aquí
                        };

                        _context.Usuarios.Add(newUser);
                        _context.SaveChanges();

                        // Asegurarse que la cita tenga la referencia al usuario recién creado
                        cita.CedulaUsuario = newUser.Cedula;

                        // Creación de la cita
                        cita.Fin = cita.Inicio.AddMinutes(30);
                        var newCita = new Cita
                        {

                            Inicio = cita.Inicio,
                            Fin = cita.Inicio.AddMinutes(30),
                            Estado = cita.Estado,
                            CedulaUsuario = newUser.Cedula,
                            IDEspecialista = cita.IDEspecialista,
                            IDServicio = cita.IDServicio
                        };

                        _context.Citas.Add(newCita);
                        _context.SaveChanges();

                        transaction.Commit();

                        ViewBag.SuccessMessage = $"Cita agendada exitosamente! Tu ID de cita es {newCita.ID}.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                        ViewBag.ErrorMessage = "Ha ocurrido un error al guardar la cita. Por favor, intenta de nuevo.";
                    }
                }
            }

            CargarViewData();
            return View(cita);
        }

        // Metod0 auxiliar que carga datos relacionados con Especialistas y Servicios para las vistas.
        private void CargarViewData()
        {
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
        }
    }
}