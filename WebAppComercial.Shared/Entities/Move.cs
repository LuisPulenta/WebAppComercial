using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppComercial.Shared.Entities
{
    public class Move
    {
        public int Id { get; set; }

        [Display(Name = "Almacén")]
        public int IdStore { get; set; }

        [Display(Name = "Almacén")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Store Store { get; set; } = null!;


        [Display(Name = "Producto")]
        public int IdProduct { get; set; }

        [Display(Name = "Producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Product Product { get; set; } = null!;

        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Documento")]
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
        public decimal Balance { get; set; }

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
