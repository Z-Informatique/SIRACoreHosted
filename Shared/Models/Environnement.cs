using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestCoreHosted.Shared.Models
{
    public partial class Environnement
    {
        public Environnement()
        {
            Databases = new HashSet<DataBase>();
            Serveurs = new HashSet<Serveur>();
        }

        public int EnvId { get; set; }
        [Required(ErrorMessage = "Ce champ est requis.")]
        [DataType(DataType.Text)]
        public string EnvType { get; set; }

        public virtual ICollection<DataBase> Databases { get; set; }
        public virtual ICollection<Serveur> Serveurs { get; set; }
    }
}
