using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestCoreHosted.Shared.Models
{
    public partial class Serveur
    {
        public Serveur()
        {
            Databases = new HashSet<DataBase>();
        }

        public int ServeurId { get; set; }
        public int OsId { get; set; }
        public int EnvId { get; set; }
        [Required(ErrorMessage = "Ce champ est requis.")]
        [DataType(DataType.Text)]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Ce champ est requis.")]
        [DataType(DataType.Text)]
        public string Categorie { get; set; }
        [Required(ErrorMessage = "Ce champ est requis.")]
        [DataType(DataType.Text)]
        public string TypeServeur { get; set; }
        public string Salle { get; set; }
        public string Rack { get; set; }
        public string Noyau { get; set; }
        public string Etat { get; set; }
        public string Observation { get; set; }
        public int VersionOsId { get; set; }
        public string CoutPrice { get; set; }
        public DateTime? MigDate { get; set; }

        public virtual Environnement Env { get; set; }
        public virtual TypeO Os { get; set; }
        public virtual OSystem VersionOs { get; set; }
        public virtual ICollection<DataBase> Databases { get; set; }
    }
}
