using ShopHub.SharedKernel.Domain.Exceptions;

namespace ShopHub.Modules.Identity.Domain.Exceptions;

public class InvalidCredentialsException : DomainException
{
    public InvalidCredentialsException()
        : base("Identity.InvalidCredentials", "Invalid email or password.") { }
}