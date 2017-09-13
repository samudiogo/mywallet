using System;
using AutoMapper;
using MyWallet.Application.Dto;
using MyWallet.Domain.Models;

namespace MyWallet.Application.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(u => u.Password, opt => opt.Ignore());
            CreateMap<UserRegistrationDto, User>()
                .ConstructUsing(src => new User(src.Name, src.Email, src.Password))
                .ForMember(u => u.Password, opt => opt.Ignore()); ;
            CreateMap<UserLoginDto, User>()
                .ConstructUsing(src => new User(src.Email, src.Password))
                .ForMember(u => u.Password, opt => opt.Ignore());
            CreateMap<UserDto, User>();


            //wallet
            CreateMap<Wallet, WalletDto>().ReverseMap();
            
        }
    }
}