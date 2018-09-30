namespace Kiki.WebApp.Data.Models
{
    using Kiki.WebApp.ViewModels;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public string Entreprise { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public Utilisateur ToUtilisateur()
        {
            return new Utilisateur
            {
                Id = Id,
                Email = Email,
                UserName = UserName,
                Entreprise = Entreprise,
                Nom = Nom,
                Prenom = Prenom,
            };
        }
    }
}