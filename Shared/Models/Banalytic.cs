using System;
using System.Collections.Generic;

namespace TestCoreHosted.Shared.Models
{
    public partial class Banalytic
    {
        public Banalytic()
        {
            Applications = new HashSet<Application>();
        }

        public int Id { get; set; }
        public string? Nom { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
    }
}
