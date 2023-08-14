using System.ComponentModel.DataAnnotations;

namespace AgendadorCitasOnline.Models
{
    public class Servicio
    {
        [Key] // Atributo que indica que esta propiedad es la clave principal
        public int ID { get; set; }

        [Required] // Atributo que indica que esta propiedad es obligatoria
        [MaxLength(150)] // Longitud máxima del campo NombreServicio
        public string NombreServicio { get; set; }

        // Colección de citas relacionadas con este servicio
        public virtual ICollection<Cita> Citas { get; set; }
    }
}