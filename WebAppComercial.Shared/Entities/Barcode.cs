using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.Entities
{
    public class Barcode
    {
        public int Id { get; set; }
        public Product Product { get; set; } = null!;

        public int ProductId { get; set; }

        [Display(Name = "Código de Barras")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Code { get; set; }
    }
}
