using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestCoreHosted.Shared.Models
{
    public partial class Db
    {
        public Db()
        {
            VersionDbs = new HashSet<VersionDb>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Ce champ est requis.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public virtual ICollection<VersionDb> VersionDbs { get; set; }
    }
}
