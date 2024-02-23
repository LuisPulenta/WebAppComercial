using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.DTOs
{
    public class StoreproductDTO
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public int ProductId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Stock")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public double Stock { get; set; } = 0!;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Mínimo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public double Minimum { get; set; } = 0!;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Máximo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public double Maximum { get; set; } = 0!;

        [Display(Name = "Días de Repos.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Replacementdays { get; set; } = 0!;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Mínimo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public double Minimumquantity { get; set; } = 0!;
    }
}