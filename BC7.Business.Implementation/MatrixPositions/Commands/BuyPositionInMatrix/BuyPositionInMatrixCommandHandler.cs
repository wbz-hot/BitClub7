﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BC7.Business.Helpers;
using BC7.Business.Implementation.Events;
using BC7.Database;
using BC7.Entity;
using BC7.Infrastructure.CustomExceptions;
using BC7.Repository;
using MediatR;

namespace BC7.Business.Implementation.MatrixPositions.Commands.BuyPositionInMatrix
{
    public class BuyPositionInMatrixCommandHandler : IRequestHandler<BuyPositionInMatrixCommand, Guid>
    {
        private readonly IBitClub7Context _context;
        private readonly IUserMultiAccountRepository _userMultiAccountRepository;
        private readonly IMatrixPositionHelper _matrixPositionHelper;
        private readonly IReflinkHelper _reflinkHelper;
        private readonly IMediator _mediator;

        public BuyPositionInMatrixCommandHandler(
            IBitClub7Context context,
            IUserMultiAccountRepository userMultiAccountRepository,
            IMatrixPositionHelper matrixPositionHelper,
            IReflinkHelper reflinkHelper,
            IMediator mediator)
        {
            _context = context;
            _userMultiAccountRepository = userMultiAccountRepository;
            _matrixPositionHelper = matrixPositionHelper;
            _reflinkHelper = reflinkHelper;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(BuyPositionInMatrixCommand request, CancellationToken cancellationToken)
        {
            var userMultiAccount = await _userMultiAccountRepository.GetAsync(request.UserMultiAccountId);

            if (userMultiAccount.MatrixPositions.Any())
            {
                throw new ValidationException("This account already exists in a matrix");
            }

            // 1. Check if user paid for the matrix position on this level (available in etap 1)

            // ReSharper disable once PossibleInvalidOperationException
            var invitingUserMatrix = await _matrixPositionHelper.GetMatrix(userMultiAccount.UserMultiAccountInvitingId.Value, request.MatrixLevel);

            if (invitingUserMatrix == null)
            {
                throw new ValidationException("The inviting user from reflink does not have matrix on this level");
            }

            if (!CheckIfMatrixHasEmptySpace(invitingUserMatrix))
            {
                // Maybe we should find another sponsor instead of throwing an error here?
                throw new ValidationException("Matrix is full for the user from reflink");
            }

            var matrixPositionId = await BuyPositionInMatrix(request.UserMultiAccountId, invitingUserMatrix);

            await PublishMatrixPositionBoughtNotification(matrixPositionId);

            // TODO: Generate reflink in event maybe?
            userMultiAccount.RefLink = _reflinkHelper.GenerateReflink();
            await _context.SaveChangesAsync();

            return matrixPositionId;
        }

        private async Task<Guid> BuyPositionInMatrix(Guid multiAccountId, IEnumerable<MatrixPosition> invitingUserMatrix)
        {
            var matrixPosition = invitingUserMatrix.First(x => x.UserMultiAccountId == null);

            matrixPosition.UserMultiAccountId = multiAccountId;

            _context.Set<MatrixPosition>().Attach(matrixPosition);
            await _context.SaveChangesAsync();

            return matrixPosition.Id;
        }

        private bool CheckIfMatrixHasEmptySpace(IEnumerable<MatrixPosition> invitingUserMatrix)
        {
            return _matrixPositionHelper.CheckIfMatrixHasEmptySpace(invitingUserMatrix);
        }

        private async Task PublishMatrixPositionBoughtNotification(Guid matrixPositionId)
        {
            var @event = new MatrixPositionBoughtEvent { MatrixPositionId = matrixPositionId };
            await _mediator.Publish(@event);
        }
    }
}
