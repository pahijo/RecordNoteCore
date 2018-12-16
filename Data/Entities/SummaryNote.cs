using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecordNoteCore.Data.Entities
{
    public class SummaryNote
    {
        public virtual Student Student {get; set;}

        public virtual List<TestNoteView> TestNoteViews { get; set; }
        
        public virtual decimal Final { get; set; }
    }
}
