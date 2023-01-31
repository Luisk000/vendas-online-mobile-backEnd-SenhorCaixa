using AutoMapper;
using SC.AplicativoSenhorCaixa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SC.AplicativoSenhorCaixa
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<Application_User, UserDto>().ReverseMap();

        }

    }
}
