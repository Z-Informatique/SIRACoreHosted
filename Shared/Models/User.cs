using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCoreHosted.Shared.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Ce champ est requis")]
        [DataType(DataType.Text)]
        public string? Nom { get; set; }
        [Required(ErrorMessage = "Ce champ est requis")]
        [DataType(DataType.Text)]
        public string? Prenom { get; set; }
        [Required(ErrorMessage = "Ce champ est requis")]
        [DataType(DataType.PhoneNumber, ErrorMessage ="Vous devez saisir un numéro valide")]
        public string? Telephone { get; set; }
        [Required(ErrorMessage = "Ce champ est requis")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Vous devez saisir une adresse mail valide")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Ce champ est requis")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public int Role { get; set; }
        public int Statut { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Token { get; set; }
        public string? ExpireToken { get; set; }
    }
}
