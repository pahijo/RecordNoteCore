using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecordNoteCore.Data.Entities
{
    public class Test
    {
        
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(1, 25, ErrorMessage = "El valor debe ser menor a 25")]
        public decimal Percentage { get; set; }

        [Display(Name ="Habilitado")]
        public bool IsAvailable { get; set; }

        
    }
}
