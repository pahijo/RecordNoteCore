using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecordNoteCore.Data.Entities
{
    public class ReportNote
    {
        public int Id { get; set; }

        public Student Student { get; set; }

        public Test Test { get; set; }

        [Required]
        [Range(0,5)]
        public decimal Note { get; set; }
    }
}
