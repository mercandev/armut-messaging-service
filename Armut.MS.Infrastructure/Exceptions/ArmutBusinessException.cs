using System;
using System.Runtime.Serialization;

namespace Armut.MS.Infrastructure.Exceptions;

public class ArmutBusinessException : Exception
{
    public ArmutBusinessException() : base()
    {

    }

    public ArmutBusinessException(string message) : base(message)
    {
    }
}
