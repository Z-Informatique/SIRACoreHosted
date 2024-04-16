using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestCoreHosted.Shared.Models
{
    public partial class VersionDb
    {
        public VersionDb()
        {
            Databases = new HashSet<DataBase>();
        }

        public int VdbId { get; set; }
        [Required(ErrorMessage = "Ce champ est requis.")]
        [DataType(DataType.Text)]
        public string Titre { get; set; }
        [Required(ErrorMessage = "Ce champ est requis.")]
        [DataType(DataType.Text)]
        public string Noyau { get; set; }
        public int DbId { get; set; }

        public virtual Db Db { get; set; }
        public virtual ICollection<DataBase> Databases { get; set; }
    }
}
