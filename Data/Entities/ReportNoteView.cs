using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecordNoteCore.Data.Entities
{
    public class ReportNoteView
    {
        public virtual  int Id { get; set; }

        public virtual int Student { get; set; }
        public virtual SelectList Students { get; set; }

        public virtual int Test { get; set; }
        public virtual SelectList Tests { get; set; }

        [Required]
        [Range(0, 5)]
        public virtual decimal Note { get; set; }
    }
}
