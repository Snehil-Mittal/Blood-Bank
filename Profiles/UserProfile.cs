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
            CreateMap<UserDetails, ReadUserDto>();
            CreateMap<CreateUserDto, UserDetails>();
            CreateMap<UpdateUserDto, UserDetails>();
            CreateMap<UserDetails, UpdateUserDto>();

        }
    }
}
