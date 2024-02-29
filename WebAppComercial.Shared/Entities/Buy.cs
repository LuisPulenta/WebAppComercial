using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.Entities
{
    public class Buy
    {
        public int Id { get; set; }
        
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Proveedor")]
        public int SupplierId { get; set; }

        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Supplier Supplier { get; set; } = null!;

        [Display(Name = "Almacén")]
        public int StoreId { get; set; }

        [Display(Name = "Almacén")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Store Store { get; set; } = null!;
        public ICollection<BuyDetail>? BuyDetails { get; set; }
    }
}
