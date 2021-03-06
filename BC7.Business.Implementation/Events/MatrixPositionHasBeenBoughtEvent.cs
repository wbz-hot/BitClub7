﻿using System;
using MediatR;

namespace BC7.Business.Implementation.Events
{
    public class MatrixPositionHasBeenBoughtEvent : INotification
    {
        public Guid MatrixPositionId { get; private set; }

        public MatrixPositionHasBeenBoughtEvent(Guid matrixPositionId)
        {
            MatrixPositionId = matrixPositionId;
        }
    }
}
