using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GZIJ20241303.Models
{
    public class Proveedor
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [StringLength(100, ErrorMessage = "Máximo 100 carácteres")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "El campo Producto es requerido")]
        public string Producto { get; set; }

        [Display(Name = "Fecha de registro")]
        public DateTime FechaRegistro { get; set; }

        [Display(Name = "Teléfono")]
        [Phone(ErrorMessage = "Digíte un número de teléfono valido")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Digíte un correo electrónico valido")]
        [Display(Name = "Correo Electrónico")]
        [StringLength(200)]
        public string CorreoElectronico { get; set; }

        public IList<DireccionesProveedor> DireccionesProveedors { get; set; }
    }
}
