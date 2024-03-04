using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.Entities
{
    public class Sale
    {
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Customer Customer { get; set; } = null!;

        [Display(Name = "Almacén")]
        public int StoreId { get; set; }

        [Display(Name = "Almacén")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Store Store { get; set; } = null!;

        public ICollection<SaleDetail>? SaleDetails { get; set; }
    }
}
