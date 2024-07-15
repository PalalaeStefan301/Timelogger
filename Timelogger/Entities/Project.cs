using System;
using System.Collections.Generic;

namespace Timelogger.Entities
{
	public partial class Project
	{
		public Project()
        {
            Tempos = new HashSet<Tempo>();
        }

        public int Id { get; set; }
		public string Name { get; set; }
		public DateTime? Deadline { get; set; }
        public int CreatedBy { get; set; }
        public virtual ICollection<Tempo> Tempos { get; set; }

    }
}
