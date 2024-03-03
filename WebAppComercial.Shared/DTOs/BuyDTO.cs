using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.DTOs
{
    public class BuyDTO
    {
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Proveedor")]
        public int SupplierId { get; set; }


        [Display(Name = "Almacén")]
        public int StoreId { get; set; }

        public string? Store { get; set; }

        public string? Supplier { get; set; }

        public int? Items { get; set; }
    }
}