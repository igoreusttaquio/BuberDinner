using System.Net;

namespace BuberDinner.Application.Common.Errors;

public class DuplicateEmailException(string message) : Exception(message), IServiceException
{
    private readonly string _message = message;
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => _message;
}
