using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendadorCitasOnline.Models
{
    public class Especialista
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(150)]
        public string Apellido { get; set; }

        [Required]
        [MaxLength(150)]
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        public virtual ICollection<Cita> Citas { get; set; }
    }
}
