using Api.Domain.DTOs.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DTOToModelProfile : Profile
    {
        public DTOToModelProfile()
        {
            CreateMap<UserModel, UserDTO>()
                .ReverseMap();
        }
    }
}