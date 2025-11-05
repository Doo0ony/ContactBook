using AutoMapper;
using ContactBook.Shared.DTOs.User;
using ContactBook.Domain.Entities;

namespace ContactBook.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // User -> UserDto
        CreateMap<User, UserDto>();

        // CreateUserDto -> User
        CreateMap<CreateUserDto, User>();

        // UpdateUserDto -> User
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 
    }
}