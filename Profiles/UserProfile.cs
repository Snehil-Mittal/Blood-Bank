using AutoMapper;
using BloodBankManagementSystem.Dtos;
using BloodBankManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserDetails, ReadUserDto>()
                .ForMember(dest => dest.Availability, src => src.MapFrom(s => s.Account.Availability))
                .ForMember(dest => dest.IsApproved, src => src.MapFrom(s => s.Account.IsApproved))
                .ForMember(dest => dest.Badge, src => src.MapFrom(s => s.Account.Badge)); 
            CreateMap<CreateUserDto, UserDetails>();
            CreateMap<UpdateUserDto, UserDetails>();
            CreateMap<UserDetails, UpdateUserDto>();

        }
    }
}
