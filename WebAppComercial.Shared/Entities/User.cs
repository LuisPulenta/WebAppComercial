using Microsoft.AspNetCore.Identity;
using WebAppComercial.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebAppComercial.Shared.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Apellido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }
        
        [Display(Name = "Activo")]
        public bool Active { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";
        public string Rol => UserType==0 ? "Administrador" : "Ventas";
    }
}