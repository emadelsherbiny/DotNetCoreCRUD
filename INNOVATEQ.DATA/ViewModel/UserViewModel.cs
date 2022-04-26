using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INNOVATEQ.DATA.ViewModel
{
   public class UserViewModel
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        
        public string Designation { get; set; }
        public DateTime JoiningDate { get; set; }
        
        public string Street { get; set; }
       
        public string State { get; set; }
      
        public string Pincode { get; set; }
         
        public string Country { get; set; }
        public string Url { get; set; } = "";
        private string _ImagePath { get; set; }
        public string  ImagePath
        {
            get
            {
                return Url+"/StaticFiles/" + _ImagePath;
            }
            set
            {
                _ImagePath = value;
            }
        }

        private string _FullAddress;
        public string FullAddress
        {
            get
            {
                return Country + " , " + State +" ,"+ Street + " ," + Pincode;
            }
            set
            {
                _FullAddress = value;
            }
        }
       
    }
}
