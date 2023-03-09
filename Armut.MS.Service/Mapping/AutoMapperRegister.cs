using System;
using Armut.MS.Domain.Model;
using Armut.MS.SharedObjects.Message;
using Armut.MS.SharedObjects.User;
using AutoMapper;

namespace Armut.MS.Service.Mapping;

public class AutoMapperRegister : Profile
{
    public AutoMapperRegister()
    {
        //Users
        CreateMap<UserCreateViewModel, Users>().ReverseMap();

        //Chats
        CreateMap<Chats, ChatRoomListViewModel>()
               .ForMember(dest => dest.ChatId, from => from.MapFrom(s => s.Id.ToString()));

        CreateMap<Messages, MessageHistoryListViewModel>();
               
    }
}
