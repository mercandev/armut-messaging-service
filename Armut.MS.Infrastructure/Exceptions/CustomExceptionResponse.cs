using System;
using Newtonsoft.Json;
namespace Armut.MS.Infrastructure.Exceptions;

public class CustomExceptionResponse
{
    public string ErrorMessage { get; set; }

    public override string ToString() => JsonConvert.SerializeObject(this);
}