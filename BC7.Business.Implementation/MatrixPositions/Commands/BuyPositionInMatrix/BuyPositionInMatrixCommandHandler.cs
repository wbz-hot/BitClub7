﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BC7.Business.Helpers;
using BC7.Business.Implementation.Jobs;
using BC7.Business.Implementation.Withdrawals.Jobs;
using BC7.Business.Implementation.Withdrawals.Jobs.JobModels;
using BC7.Domain;
using BC7.Infrastructure.CustomExceptions;
using BC7.Repository;
using Hangfire;
using MediatR;
// ReSharper disable PossibleInvalidOperationException
// ReSharper disable PossibleMultipleEnumeration

namespace BC7.Business.Implementation.MatrixPositions.Commands.BuyPositionInMatrix
{
    public class BuyPositionInMatrixCommandHandler : IRequestHandler<BuyPositionInMatrixCommand, Guid>
    {
        private readonly IUserMultiAccountRepository _userMultiAccountRepository;
        private readonly IUserAccountDataRepository _userAccountDataRepository;
        private readonly IMatrixPositionRepository _matrixPositionRepository;
        private readonly IMatrixPositionHelper _matrixPositionHelper;
        private readonly IPaymentHistoryHelper _paymentHistoryHelper;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public BuyPositionInMatrixCommandHandler(
            IUserMultiAccountRepository userMultiAccountRepository,
            IUserAccountDataRepository userAccountDataRepository,
            IMatrixPositionRepository matrixPositionRepository,
            IMatrixPositionHelper matrixPositionHelper,
            IPaymentHistoryHelper paymentHistoryHelper,
            IBackgroundJobClient backgroundJobClient)
        {
            _userMultiAccountRepository = userMultiAccountRepository;
            _userAccountDataRepository = userAccountDataRepository;
            _matrixPositionRepository = matrixPositionRepository;
            _matrixPositionHelper = matrixPositionHelper;
            _paymentHistoryHelper = paymentHistoryHelper;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<Guid> Handle(BuyPositionInMatrixCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var userMultiAccount = await _userMultiAccountRepository.GetAsync(command.UserMultiAccountId);

            await ValidateUserMultiAccount(userMultiAccount, command.MatrixLevel);

            var sponsorAccountId = userMultiAccount.SponsorId.Value;

            var invitingUserMatrix = await _matrixPositionHelper.GetMatrixForGivenMultiAccountAsync(sponsorAccountId, command.MatrixLevel);
            if (invitingUserMatrix is null)
            {
                throw new ValidationException($"The inviting user from reflink does not have structure on level: {command.MatrixLevel}");
            }

            MatrixPosition matrixPosition;
            if (_matrixPositionHelper.CheckIfMatrixHasEmptySpace(invitingUserMatrix))
            {
                matrixPosition = invitingUserMatrix
                    .OrderBy(x => x.DepthLevel)
                    .First(x => x.UserMultiAccountId == null);
            }
            else
            {
                var userAccount = await _userAccountDataRepository.GetAsync(userMultiAccount.UserAccountDataId);
                var userMultiAccountIds = userAccount.UserMultiAccounts.Select(x => x.Id).ToList();

                matrixPosition = await _matrixPositionHelper.FindTheNearestEmptyPositionFromGivenAccountWhereInParentsMatrixThereIsNoAnyMultiAccountAsync(
                            sponsorAccountId, userMultiAccountIds, command.MatrixLevel);

                if (matrixPosition is null)
                {
                    throw new ValidationException("There is no empty space in the structure where account can be assigned");
                }

                await ChangeUserSponsor(userMultiAccount, matrixPosition);
            }

            matrixPosition.AssignMultiAccount(command.UserMultiAccountId);
            await _matrixPositionRepository.UpdateAsync(matrixPosition);

            _backgroundJobClient.Enqueue<MatrixPositionHasBeenBoughtJob>(
                job => job.Execute(matrixPosition.Id, null));

            _backgroundJobClient.Enqueue<UserBoughtMatrixPositionJob>(
                job => job.Execute(userMultiAccount.Id, null));

            _backgroundJobClient.Enqueue<InitWithdrawalJob>(
                job => job.Execute(new InitWithdrawalModel
                {
                    MatrixPositionId = matrixPosition.Id,
                    WithdrawalFor = WithdrawalForHelper.AssignmentInMatrix
                }, null));

            return matrixPosition.Id;
        }


        private async Task ValidateUserMultiAccount(UserMultiAccount userMultiAccount, int matrixLevel)
        {
            if (userMultiAccount is null) throw new ArgumentNullException(nameof(userMultiAccount));
            if (userMultiAccount.MatrixPositions.Any())
            {
                throw new ValidationException("This account already exists in a structure");
            }

            if (userMultiAccount.UserAccountData.IsMembershipFeePaid == false)
            {
                throw new ValidationException("The main account did not buy pay the membership's fee yet");
            }

            if (await _paymentHistoryHelper.DoesUserPaidForMatrixLevelAsync(matrixLevel, userMultiAccount.Id) == false)
            {
                throw new ValidationException($"User didn't pay for the structure at level {matrixLevel} yet");
            }

            if (!userMultiAccount.SponsorId.HasValue)
            {
                throw new ValidationException("This account does not have inviting multi account set");
            }
        }

        private async Task ChangeUserSponsor(UserMultiAccount userMultiAccount, MatrixPosition matrixPosition)
        {
            var parentPosition = await _matrixPositionRepository.GetAsync(matrixPosition.ParentId.Value);

            userMultiAccount.ChangeSponsor(parentPosition.UserMultiAccountId.Value);
            await _userMultiAccountRepository.UpdateAsync(userMultiAccount);
        }
    }
}
