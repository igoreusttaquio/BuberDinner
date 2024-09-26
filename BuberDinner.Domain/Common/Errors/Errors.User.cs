using ErrorOr;

namespace BuberDinner.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail = Error.Validation("User.DuplicateEmail",
            description: "Email already in use.");
    }
}
