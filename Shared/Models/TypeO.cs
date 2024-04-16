using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestCoreHosted.Shared.Models
{
    public partial class TypeO
    {
        public TypeO()
        {
            OSystems = new HashSet<OSystem>();
            Serveurs = new HashSet<Serveur>();
        }

        public int TypeOsId { get; set; }
        [Required(ErrorMessage = "Ce champ est requis.")]
        [DataType(DataType.Text)]
        public string TitreOs { get; set; }

        public virtual ICollection<OSystem> OSystems { get; set; }
        public virtual ICollection<Serveur> Serveurs { get; set; }
    }
}
