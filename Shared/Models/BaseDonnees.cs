namespace TestCoreHosted.Shared.Models
{
    public class BaseDonnees
    {
        public int DataId { get; set; }
        public string? DTitre { get; set; }
        public string? Noyau { get; set; }
        public string? VersionDb { get; set; }
        public string? Application { get; set; }
        public string? Serveur { get; set; }
        public string? Environeement { get; set; }
        public string? Etat { get; set; }
        public DateTime? MigDate { get; set; }
    }
}
