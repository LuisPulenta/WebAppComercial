using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.Entities
{
    public class Supplier
    {
        public int Id { get; set; }

        [Display(Name = "Razón Social")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Tipo de Documento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DocumentType DocumentType { get; set; } = null!;

        [Display(Name = "Tipo de Documento")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int DocumentTypeId { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; } = null!;

        [Display(Name = "Nombre Contacto")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string ContactName { get; set; } = null!;

        [Display(Name = "Dirección")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Address { get; set; } = null!;

        [Display(Name = "Teléfono Fijo")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string LandPhone { get; set; } = null!;

        [Display(Name = "Celular")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string CellPhone { get; set; } = null!;

        [Display(Name = "Email")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Email { get; set; } = null!;

        [Display(Name = "Notas")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Remarks { get; set; } = null!;

        public ICollection<Buy>? Buys { get; set; }

        //[Display(Name = "Aniversario")]
        //[MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        //public DateOnly Anniversary { get; set; }
    }
}
