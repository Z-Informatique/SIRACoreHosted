using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestCoreHosted.Shared.Models
{
    public partial class Metier
    {
        public Metier()
        {
            Applications = new HashSet<Application>();
        }

        public int MetierId { get; set; }
        [Required(ErrorMessage = "Ce champ est requis.")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
    }
}
