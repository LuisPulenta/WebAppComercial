using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.Entities
{
    public class DocumentType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo Documento")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;
        public ICollection<Customer>? Customers { get; set; }
        public ICollection<Supplier>? Suppliers { get; set; }
    }
}
