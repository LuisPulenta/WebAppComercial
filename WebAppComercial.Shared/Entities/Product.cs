using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppComercial.Shared.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Producto")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Category Category { get; set; } = new Category();

        [Display(Name = "Iva")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Iva Iva { get; set; } = new Iva();

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Price { get; set; }

        [Display(Name = "Notas")]
        [MaxLength(255, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Remarks { get; set; } = null!;

        public string Image { get; set; } = null!;

        [Display(Name = "Unidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Measure Measure { get; set; } = new Measure();

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Medida")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Quantity { get; set; }

    }
}
