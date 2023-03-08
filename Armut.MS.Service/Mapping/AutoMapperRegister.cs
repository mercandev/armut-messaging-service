using System;
using Armut.MS.Domain.Model;
using Armut.MS.SharedObjects.User;
using AutoMapper;

namespace Armut.MS.Service.Mapping;

public class AutoMapperRegister : Profile
{
    public AutoMapperRegister()
    {
        CreateMap<UserCreateViewModel, Users>().ReverseMap();
    }
}
