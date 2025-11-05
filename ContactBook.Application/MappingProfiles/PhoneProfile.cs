using AutoMapper;
using ContactBook.Application.DTOs.Phone;
using ContactBook.Domain.Entities;

namespace ContactBook.Application.MappingProfiles;

public class PhoneProfile : Profile
{
    public PhoneProfile()
    {
         // Phone -> PhoneDto
        CreateMap<Phone, PhoneDto>();

        // CreatePhoneDto -> Phone
        CreateMap<CreatePhoneDto, Phone>();

        // UpdatePhoneDto -> Phone
        CreateMap<UpdatePhoneDto, Phone>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}