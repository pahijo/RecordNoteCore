using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecordNoteCore.Data.Entities
{
    public class TestNoteView
    {

        public virtual int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(1, 25, ErrorMessage = "El valor debe ser menor a 25")]
        public virtual decimal Percentage { get; set; }

        [Display(Name = "Habilitado")]
        public virtual bool IsAvailable { get; set; }

        [Required]
        [Range(0, 5)]
        public decimal Note { get; set; }
    }
}
