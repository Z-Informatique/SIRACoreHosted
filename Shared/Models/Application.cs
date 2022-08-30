using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#nullable disable

namespace TestCoreHosted.Shared.Models
{
    public partial class Application
    {
        public Application()
        {
            AppCouts = new HashSet<AppCout>();
            Documentations = new HashSet<Documentation>();
        }

        public int AppId { get; set; }
        public int DomaineId { get; set; }
        public int MetierId { get; set; }
        public string ServeurId { get; set; }
        [Required(ErrorMessage = "Ce champ est requis.")]
        [DataType(DataType.Text)]
        public string Titre { get; set; }
        public string Description { get; set; }
        public string SiteApp { get; set; }
        public string VersionApp { get; set; }
        public string Architecture { get; set; }
        public string Cout { get; set; }
        public string Conformite { get; set; }
        public string ContactUser { get; set; }
        public int? NbreUser { get; set; }
        public string Impact { get; set; }
        public string Commentaire { get; set; }
        public string Documentation { get; set; }
        public string Etat { get; set; }
        public string Ba { get; set; }
        public string Editeur { get; set; }
        public DateTime? MigDate { get; set; }
        public string DemoDate { get; set; }
        public string OnlineDate { get; set; }
        public string CoutPrice { get; set; }
        public string Dependences { get; set; }
        public string Perimetre { get; set; }
        public string Bm { get; set; }
        public string Escalade { get; set; }

        public virtual Domaine Domaine { get; set; }
        public virtual Metier Metier { get; set; }
        public virtual ICollection<AppCout> AppCouts { get; set; }
        public virtual ICollection<Documentation> Documentations { get; set; }
        public virtual ICollection<DataBase> DataBases { get; set; }
    }
}
