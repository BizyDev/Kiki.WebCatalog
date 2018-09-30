using Kiki.WebApp.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Kiki.WebApp.ViewModels
{
    public class Utilisateur
    {
        public string Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères, dont au moins une miniscule, une majusucule, un chiffre et un caractère spécial.")]
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string Entreprise { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public ApplicationUser ToApplicationUser()
        {
            return new ApplicationUser
            {
                Id = Id,
                Email = Email,
                UserName = Email,
                Entreprise = Entreprise,
                Nom = Nom,
                Prenom = Prenom,
            };
        }
    }
}