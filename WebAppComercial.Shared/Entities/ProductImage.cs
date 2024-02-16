using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Product Product { get; set; } = null!;

        public int ProductId { get; set; }

        [Display(Name = "Imagen")]
        public string Image { get; set; } = null!;
        public string ImageFullPath => string.IsNullOrEmpty(Image)
       ? $"https://localhost:7020/images/products/noimage.png"
       : $"https://localhost:7020{Image[1..]}";
    }
}
