using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgendadorCitasOnline.Models
{
    public class Servicio
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(150)]
        public string NombreServicio { get; set; }

        public virtual ICollection<Cita> Citas { get; set; }
    }
}
