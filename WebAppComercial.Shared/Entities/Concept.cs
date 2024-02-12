using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.Entities
{
    public class Concept
    {
        public int Id { get; set; }

        [Display(Name = "Concepto")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;
    }
}
