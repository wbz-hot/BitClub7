﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC7.Database;
using BC7.Domain;
using Microsoft.EntityFrameworkCore;

namespace BC7.Repository.Implementation
{
    public class MatrixPositionRepository : IMatrixPositionRepository
    {
        private readonly IBitClub7Context _context;

        public MatrixPositionRepository(IBitClub7Context context)
        {
            _context = context;
        }

        public Task<MatrixPosition> GetAsync(Guid id)
        {
            return _context.Set<MatrixPosition>().SingleAsync(x => x.Id == id);
        }

        public Task<MatrixPosition> GetPositionForAccountAtLevelAsync(Guid userMultiAccountId, int matrixLevel = 0)
        {
            return _context.Set<MatrixPosition>()
                .Where(x => x.UserMultiAccountId == userMultiAccountId)
                .Where(x => x.MatrixLevel == matrixLevel)
                .SingleOrDefaultAsync(); // Cycles available later
        }

        public async Task<IEnumerable<MatrixPosition>> GetMatrixAsync(MatrixPosition matrixPosition, int matrixLevel = 0)
        {
           return await _context.Set<MatrixPosition>()
                .Where(x => x.Left >= matrixPosition.Left)
                .Where(x => x.Right <= matrixPosition.Right)
                .Where(x => x.DepthLevel >= matrixPosition.DepthLevel)
                .Where(x => x.DepthLevel <= matrixPosition.DepthLevel + 2) // Each matrix has 2 depth level
                .Where(x => x.MatrixLevel == matrixLevel)
                .ToListAsync();
        }

        public async Task<IEnumerable<MatrixPosition>> GetMatrixForGivenMultiAccountAsync(Guid userMultiAccountId, int matrixLevel = 0)
        {
            var userMatrixPosition = await GetPositionForAccountAtLevelAsync(userMultiAccountId, matrixLevel);
            if (userMatrixPosition == null)
            {
                return null;
            }

            var matrixAccounts = await _context.Set<MatrixPosition>()
                .Where(x => x.Left >= userMatrixPosition.Left)
                .Where(x => x.Right <= userMatrixPosition.Right)
                .Where(x => x.DepthLevel >= userMatrixPosition.DepthLevel)
                .Where(x => x.DepthLevel <= userMatrixPosition.DepthLevel + 2) // Each matrix has 2 depth level
                .Where(x => x.MatrixLevel == matrixLevel)
                .ToListAsync();

            return matrixAccounts;
        }

        public Task UpdateAsync(MatrixPosition matrixPosition)
        {
            _context.Set<MatrixPosition>().Attach(matrixPosition);
            return _context.SaveChangesAsync();
        }
    }
}
