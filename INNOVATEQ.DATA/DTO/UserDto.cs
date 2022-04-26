using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INNOVATEQ.DATA.DTO
{
    public class UserDto
    { 
      
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
        public string Pincode { get; set; }
        [MaxLength(250)]
        [Required]
        public string Country { get; set; }
         
        public IFormFile Image { get; set; }
         
    }
}
