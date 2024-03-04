using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.Entities
{
    public class SaleDetail
    {
        public int Id { get; set; }


        [Display(Name = "N° Venta")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Sale Sale { get; set; } = null!;

        [Display(Name = "N° Venta")]
        public int SaleId { get; set; }


        [Display(Name = "Producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Product Product { get; set; } = null!;
        [Display(Name = "Producto")]
        public int ProductId { get; set; }



        [Display(Name = "Nombre")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Column(TypeName = "decimal(18,3)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Price { get; set; }

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
