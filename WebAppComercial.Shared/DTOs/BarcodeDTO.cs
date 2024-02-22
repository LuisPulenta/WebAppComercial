using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.DTOs
{
    public class BarcodeDTO
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Código de Barras")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Code { get; set; }
    }
}
