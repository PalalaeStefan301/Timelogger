using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Timelogger.Entities
{
    [ModelMetadataType(typeof(ProjectMetadata))]
    public partial class Project
    {
    }
    public class ProjectMetadata
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime? Deadline { get; set; }


    }
}
