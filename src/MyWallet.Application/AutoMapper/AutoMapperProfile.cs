using System;
using AutoMapper;
using MyWallet.Application.Dto;
using MyWallet.Domain.Models;
using MyWallet.Infra.Data.DataModels;

namespace MyWallet.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<UserSaveOrUpdateDto, UserDataModel>().ReverseMap();
            CreateMap<UserLoginDto, UserDataModel>().ReverseMap();
            CreateMap<UserDto, UserDataModel>()
                .ReverseMap()
                .ForMember(u => u.Password, opt => opt.Ignore()); ;
            CreateMap<User, UserDataModel>().ReverseMap();

            CreateMap<User, UserDto>()
                .ForMember(u => u.Password, opt => opt.Ignore());
            CreateMap<UserSaveOrUpdateDto, User>()
                .ConstructUsing(src => new User(src.Name, src.Email, src.Password))
                .ForMember(u => u.Password, opt => opt.Ignore());
            CreateMap<UserLoginDto, User>()
                .ConstructUsing(src => new User(src.Email, src.Password))
                .ForMember(u => u.Password, opt => opt.Ignore());
            CreateMap<UserDto, User>();


            //wallet
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<WalletSaveOrUpdateDto, WalletDataModel>().ReverseMap();
            CreateMap<WalletDto, WalletDataModel>().ReverseMap();
            CreateMap<Wallet, WalletDataModel>();
            //Cards

            CreateMap<CardDataModel, CardSaveOrUpdateDto>().ReverseMap();
            CreateMap<CardDto, CardDataModel>().ReverseMap();
            CreateMap<CardDto, Card>()
                .ConstructUsing(src => new Card(Guid.NewGuid(),
                src.CardNumber, src.DueDate, src.ExpirationDate, src.Cvv, src.Limit, src.IsReleasedCreditAcepted))
                .ReverseMap();

        }
    }
}