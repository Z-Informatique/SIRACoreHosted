using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestCoreHosted.Shared.Models
{
    public partial class OSystem
    {
        public OSystem()
        {
            Serveurs = new HashSet<Serveur>();
        }

        public int OId { get; set; }
        public int Title { get; set; }
        [Required(ErrorMessage = "Ce champ est requis.")]
        [DataType(DataType.Text)]
        public string VersionO { get; set; }

        public virtual TypeO TypeO { get; set; }
        public virtual ICollection<Serveur> Serveurs { get; set; }
    }
}
