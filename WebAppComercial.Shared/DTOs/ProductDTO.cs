﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppComercial.Shared.Entities;

namespace WebAppComercial.Shared.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CategoryId { get; set; }
        public string? Category { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal? Price { get; set; } = null!;

        [Display(Name = "Descripción")]
        [MaxLength(255, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Remarks { get; set; } = null!;
        public string? Image { get; set; }

        [Display(Name = "Unidad")]
        [MaxLength(2, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Unit { get; set; } = string.Empty;

        [Display(Name = "Unidad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int MeasureId { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Medida")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal? Quantity { get; set; } = null!;
        public List<string>? ProductImages { get; set; }

        [Display(Name = "Imágenes")]
        public int ImagesNumber => ProductImages == null ? 0 : ProductImages.Count;

        public string ImageFullPath => string.IsNullOrEmpty(Image)
      ? $"https://localhost:7020/images/products/noimage.png"
      : $"https://localhost:7020{Image[1..]}";
    }
}
