using FluentResults;

namespace BuberDinner.Application.Common.Errors;

public class DuplicateEmailError(string message) : IError
{
    public List<IError> Reasons => throw new NotImplementedException();

    public Dictionary<string, object> Metadata => throw new NotImplementedException();

    public string Message => message;
}
