using System;
using System.ComponentModel.DataAnnotations;

namespace AgendadorCitasOnline.Models
{
    public class Usuario
    {
        [Key]
        public long Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }

        public virtual ICollection<Cita> Citas { get; set; }
    }

}