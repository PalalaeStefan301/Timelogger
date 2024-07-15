using System;
using System.Collections.Generic;
using System.Text;

namespace Timelogger.Entities
{
    public partial class UserDetails
    {
        public UserDetails()
        {
            Projects = new HashSet<Project>();
            Tempos = new HashSet<Tempo>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Tempo> Tempos { get; set; }
    }
}
