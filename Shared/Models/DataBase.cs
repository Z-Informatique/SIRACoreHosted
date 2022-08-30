using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestCoreHosted.Shared.Models
{
    public partial class DataBase
    {
        public int DataId { get; set; }
        public int VersionDbId { get; set; }
        public int EnvId { get; set; }
        public int ServeurId { get; set; }
        public string Etat { get; set; }
        [Required(ErrorMessage = "Ce champ est requis.")]
        [DataType(DataType.Text)]
        public string DTitre { get; set; }
        public int AppId { get; set; }
        public string CoutPrice { get; set; }
        public DateTime? MigDate { get; set; }

        public virtual Environnement Env { get; set; }
        public virtual Serveur Serveur { get; set; }
        public virtual VersionDb VersionDb { get; set; }
        public virtual Application Application { get; set; }
    }
}
