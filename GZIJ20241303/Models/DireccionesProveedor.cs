using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GZIJ20241303.Models
{
    public class DireccionesProveedor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDireccion { get; set; }
        public int ProveedorId { get; set; }
        [ForeignKey("ProveedorId")]
        public Proveedor Proveedor { get; set; }

        [Required(ErrorMessage = "El campo Dirección es requerido")]
        [Display(Name = "Dirección")]
        [StringLength(200, ErrorMessage = "Máximo 200 carácteres")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo Ciudad es requerido")]
        [MaxLength(50, ErrorMessage = "Máximo 50 carácteres")]
        public string Ciudad { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "El campo País es requerido")]
        public string Pais { get; set; }

    }
}
