using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppComercial.Shared.Entities
{
    public class BuyDetail
    {
        public int Id { get; set; }


        [Display(Name = "N° Compra")]
        public int IdBuy { get; set; }

        [Display(Name = "N° Compra")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Buy Buy { get; set; } = null!;

        [Display(Name = "Producto")]
        public int IdProduct { get; set; }

        [Display(Name = "Producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Product Product { get; set; } = null!;

        [Display(Name = "Nombre")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Column(TypeName = "decimal(18,3)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Costo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Cost { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public double Quantity { get; set; }

        [Display(Name = "Kardex")]
        public int IdMove { get; set; }

        [Display(Name = "Kardex")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Move Move { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:P2}")]
        [Display(Name = "%IVA")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public double PercentageIVA { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}")]
        [Display(Name = "%Descuento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public double PercentageDiscount { get; set; }
    }
}
