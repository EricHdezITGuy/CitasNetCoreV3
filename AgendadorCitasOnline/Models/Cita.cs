using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendadorCitasOnline.Models
{
    public class Cita
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public TimeSpan Hora { get; set; }

        [Required]
        public string Estado { get; set; }

        [ForeignKey("Usuario")]
        public long CedulaUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("Especialista")]
        public int IDEspecialista { get; set; }
        public virtual Especialista Especialista { get; set; }

        [ForeignKey("Servicio")]
        public int IDServicio { get; set; }
        public virtual Servicio Servicio { get; set; }
    }
}