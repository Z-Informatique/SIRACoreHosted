#nullable disable

namespace TestCoreHosted.Shared.Models
{
    public partial class AppCout
    {
        public int Id { get; set; }
        public int IdApp { get; set; }
        public decimal? Cout { get; set; }
        public int? NbreLicence { get; set; }
        public string? Licence { get; set; }

        public virtual Application Application { get; set; }
    }
}
