namespace TestCoreHosted.Shared.Models
{
    public class Analytics
    {
        public string? Etat { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }

        public string Statut
        {
            get
            {
                if (Etat == "1")
                {
                    return "Démobilisé";
                }
                else if (Etat == "2")
                {
                    return "Migré";
                }
                else if (Etat == "0")
                {
                    return "En maintenance";
                }
                else
                {
                    return "En ligne";
                }
            }
        }
    }
}
