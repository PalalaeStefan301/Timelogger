using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Timelogger.Utils;

namespace Timelogger.Entities
{
    [ModelMetadataType(typeof(TempoMetadata))]
    public partial class Tempo
    {
    }
    public class TempoMetadata
    {
        [Required, Range(1, int.MaxValue)]
        public int ProjectId { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [RequiredIf("Hours", null, ErrorMessage = "This field is required when Hours is 0.")]
        public DateTime? EndDate { get; set; }
        [RequiredIf("EndDate", 0, ErrorMessage = "This field is required when EndDate is 0.")]
        [Range(0, int.MaxValue)]
        public float Hours { get; set; }

    }
}
