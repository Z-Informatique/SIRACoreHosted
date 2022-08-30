using System;
using System.Collections.Generic;

#nullable disable

namespace TestCoreHosted.Shared.Models
{
    public partial class Documentation
    {
        public int Id { get; set; }
        public int? AppId { get; set; }
        public string Titre { get; set; }
        public string NameFile { get; set; }
        public DateTime ChargerLe { get; set; }

        public virtual Application App { get; set; }
    }
}
