using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.DTOs
{
    public class ImageDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string? Image { get; set; }
    }
}
