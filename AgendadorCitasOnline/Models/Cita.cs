using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendadorCitasOnline.Models
{
    public class Cita
    {
        // Identificador único para la cita.
        [Key]
        public int ID { get; set; }

        // Fecha y hora de inicio de la cita.
        [Required(ErrorMessage = "La fecha y hora de inicio son requeridas.")]
        public DateTime Inicio { get; set; }

        // Fecha y hora de finalización de la cita.
        [Required(ErrorMessage = "La fecha y hora de finalización son requeridas.")]
        public DateTime Fin { get; set; }

        // Estado actual de la cita (e.g., "Pendiente", "Cancelada", "Realizada").
        [Required]
        [StringLength(10, ErrorMessage = "El estado es inválido.")]
        public string Estado { get; set; } = "Pendiente";

        // Cédula del usuario que tiene la cita. Puede ser nula.
        [Range(1, int.MaxValue, ErrorMessage = "La cédula debe ser un número positivo.")]
        public int? CedulaUsuario { get; set; }

        // Relación con la entidad Usuario, basada en la cédula.
        [ForeignKey("CedulaUsuario")]
        public Usuario? Usuario { get; set; }

        // Identificador del especialista asociado a la cita.
        [Required(ErrorMessage = "El especialista es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del especialista debe ser un número positivo.")]
        public int IDEspecialista { get; set; }

        // Relación con la entidad Especialista, basada en el ID del especialista.
        [ForeignKey("IDEspecialista")]
        public Especialista? Especialista { get; set; }

        // Identificador del servicio para el cual se ha hecho la cita.
        [Required(ErrorMessage = "El servicio es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del servicio debe ser un número positivo.")]
        public int IDServicio { get; set; }

        // Relación con la entidad Servicio, basada en el ID del servicio.
        [ForeignKey("IDServicio")]
        public Servicio? Servicio { get; set; }
    }
}