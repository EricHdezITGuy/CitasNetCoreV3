using System;
using System.ComponentModel.DataAnnotations;

namespace AgendadorCitasOnline.Models
{
    public class Servicio
    {
        [Key]
        public int ID { get; set; }
        public string NombreServicio { get; set; }

        public virtual ICollection<Cita> Citas { get; set; }
    }

}