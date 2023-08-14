using System.ComponentModel.DataAnnotations;

namespace AgendadorCitasOnline.Models
{
    public class Especialista
    {
        [Key] // Atributo que indica que esta propiedad es la clave principal
        public int ID { get; set; }

        [Required] // Atributo que indica que esta propiedad es obligatoria
        [MaxLength(100)] // Longitud máxima del campo Nombre
        public string Nombre { get; set; }

        [Required] // Atributo que indica que esta propiedad es obligatoria
        [MaxLength(150)] // Longitud máxima del campo Apellido
        public string Apellido { get; set; }

        [Required] // Atributo que indica que esta propiedad es obligatoria
        [MaxLength(150)] // Longitud máxima del campo CorreoElectronico
        [EmailAddress] // Atributo que valida que la propiedad sea una dirección de correo electrónico válida
        public string CorreoElectronico { get; set; }

        // Colección de citas relacionadas con este especialista
        public virtual ICollection<Cita> Citas { get; set; }
    }
}