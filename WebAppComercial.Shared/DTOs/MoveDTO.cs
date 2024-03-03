using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppComercial.Shared.DTOs
{
    public class MoveDTO
    {
        public int Id { get; set; }

        [Display(Name = "Almacén")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int StoreId { get; set; }


        [Display(Name = "Producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ProductId { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }

        [Display(Name = "Documento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Entrada")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public double Entrance { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Salida")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public double Exit { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Saldo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public double Balance { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Ultimo Costo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal LastCost { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Costo Promedio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal AverageCost { get; set; }
    }
}
