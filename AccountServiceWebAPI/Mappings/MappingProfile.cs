using AccountServiceWebAPI.Models;
using AutoMapper;
using Core.Domain.Entities;

namespace AccountServiceWebAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<Account, AccountDto>();
            CreateMap< CreateAccountDto, Account>();

        
        }

    }
}
