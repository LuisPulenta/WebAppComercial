using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppComercial.Shared.Entities
{
    public class Iva
    {
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,3)")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        [Display(Name = "%")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Percentage { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
