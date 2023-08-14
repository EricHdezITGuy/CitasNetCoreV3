using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendadorCitasOnline.Models
{
    public class Usuario
    {
        [Key] // Atributo que indica que esta propiedad es la clave principal
        public long Cedula { get; set; }

        [Required] // Atributo que indica que esta propiedad es obligatoria
        [MaxLength(100)] // Longitud máxima del campo Nombre
        public string Nombre { get; set; }

        [Required] // Atributo que indica que esta propiedad es obligatoria
        [MaxLength(150)] // Longitud máxima del campo Apellido
        public string Apellido { get; set; }

        [Required] // Atributo que indica que esta propiedad es obligatoria
        [MaxLength(150)] // Longitud máxima del campo CorreoElectronico
        [EmailAddress] // Atributo que valida si el valor es una dirección de correo electrónico válida
        public string CorreoElectronico { get; set; }

        // Colección de citas relacionadas con este usuario
        public virtual ICollection<Cita> Citas { get; set; }
    }
}