using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestCoreHosted.Shared.Models
{
    public partial class Domaine
    {
        public Domaine()
        {
            Applications = new HashSet<Application>();
        }

        public int DomaineId { get; set; }
        [Required(ErrorMessage = "Ce champ est requis.")]
        [DataType(DataType.Text)]
        public string DTitle { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
    }
}
