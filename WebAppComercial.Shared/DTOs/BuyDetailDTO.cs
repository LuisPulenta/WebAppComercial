using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppComercial.Shared.DTOs
{
    public class BuyDetailDTO
    {
        public int Id { get; set; }

        [Display(Name = "N° Compra")]
        public int BuyId { get; set; }

        [Display(Name = "Producto")]
        public int ProductId { get; set; }

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
        public int MoveId { get; set; }

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