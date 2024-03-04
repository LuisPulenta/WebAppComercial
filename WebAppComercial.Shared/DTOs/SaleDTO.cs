using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.DTOs
{
    public class SaleDTO
    {
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }


        [Display(Name = "Almacén")]
        public int StoreId { get; set; }

        public string? Store { get; set; }

        public string? Customer { get; set; }

        public int? Items { get; set; }
    }
}
