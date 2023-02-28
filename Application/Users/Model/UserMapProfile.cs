using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class UserMapProfile : Profile
{
    public UserMapProfile()
    {
        CreateMap<UserDTO, UserEntity>()
            .ForMember(dst => dst.emailAddress, opt => opt.MapFrom(src => src.email))
            .ForMember(dst => dst.fullName, opt => opt.MapFrom(src => src.name));
    }
}
