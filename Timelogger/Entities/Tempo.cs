using System;
using System.Collections.Generic;
using System.Text;

namespace Timelogger.Entities
{
    public partial class Tempo
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public float Hours { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
    }
}
