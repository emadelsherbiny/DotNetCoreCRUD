using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INNOVATEQ.DATA.Models
{
  
    public class User : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(500)]
        [Required]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Designation { get; set; }
        public DateTime JoiningDate { get; set; }
        [MaxLength(250)]
        public string Street { get; set; }
        [MaxLength(250)]
        public string State { get; set; }
        [MaxLength(50)]
        public string Pincode  { get; set; }
        [MaxLength(250)]
        [Required] 
        public string Country { get; set; }
        [MaxLength(500)]
        public string ImagePath { get; set; }

    }
}
