using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendadorCitasOnline.Models
{
    public class Cita
    {
        // ID único para la cita
        public int ID { get; set; }

        // Fecha y hora de inicio de la cita
        public DateTime Inicio { get; set; }

        // Fecha y hora de finalización de la cita
        public DateTime Fin { get; set; }

        // Estado de la cita (puede ser utilizado para manejar si la cita está confirmada, cancelada, etc.)
        public string Estado { get; set; }

        // Cédula del usuario asociada a la cita
        public long CedulaUsuario { get; set; }

        // Propiedad de navegación para acceder al usuario asociado a la cita
        public Usuario Usuario { get; set; }

        // ID del especialista asociado a la cita
        public int IDEspecialista { get; set; }

        // Propiedad de navegación para acceder al especialista asociado a la cita
        public Especialista Especialista { get; set; }

        // ID del servicio asociado a la cita
        public int IDServicio { get; set; }

        // Propiedad de navegación para acceder al servicio asociado a la cita
        public Servicio Servicio { get; set; }
    }
}