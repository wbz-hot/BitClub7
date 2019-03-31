﻿using System;
using MediatR;

namespace BC7.Business.Implementation.MultiAccounts.Commands.CreateMultiAccount
{
    public class CreateMultiAccountCommand : IRequest<Guid>
    {
        public Guid UserAccountId { get; set; }
        public string RefLink { get; set; }
    }
}
