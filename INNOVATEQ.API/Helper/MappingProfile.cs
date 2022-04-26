using AutoMapper;
using INNOVATEQ.DATA.DTO;
using INNOVATEQ.DATA.Models;
using INNOVATEQ.DATA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INNOVATEQ.API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();


        }
    }
}
