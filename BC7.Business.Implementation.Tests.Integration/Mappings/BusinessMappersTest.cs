﻿using AutoMapper;
using BC7.Business.Implementation.Users.Commands.RegisterNewUserAccount;
using BC7.Business.Implementation.Users.Commands.UpdateUser;
using BC7.Business.Models;
using BC7.Domain;

namespace BC7.Business.Implementation.Tests.Integration.Mappings
{
    public class BusinessMappersTest : Profile
    {
        public BusinessMappersTest()
        {
            CreateMap<RegisterNewUserModel, RegisterNewUserAccountCommand>()
                .ForMember(x => x.SponsorRefLink, opt => opt.Ignore());

            CreateMap<UserMultiAccount, UserMultiAccountModel>()
                .ForMember(x => x.MatrixPositionModels, opt => opt.MapFrom(y => y.MatrixPositions));

            CreateMap<MatrixPosition, MatrixPositionModel>()
                .ForMember(x => x.MultiAccountName, opt => opt.Ignore());

            CreateMap<Article, ArticleModel>()
                .ForMember(x => x.Creator, opt => opt.MapFrom(y => $"{y.Creator.FirstName} {y.Creator.LastName}"));

            CreateMap<Ticket, TicketModel>()
                .ForMember(x => x.TicketNumber, opt => opt.MapFrom(y => y.FullTicketNumber))
                .ForMember(x => x.SenderEmail, opt => opt.MapFrom(y => y.Email));

            CreateMap<UpdateUserModel, UpdateUserCommand>()
                .ForMember(x => x.UserId, opt => opt.Ignore())
                .ForMember(x => x.RequestedUser, opt => opt.Ignore());

            CreateMap<UserAccountData, UserAccountDataModel>();

            CreateMap<PaymentHistory, PaymentHistoryModel>()
                .ForMember(x => x.AccountName, opt => opt.Ignore());
        }
    }
}
