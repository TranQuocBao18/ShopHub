using ShopHub.SharedKernel.Domain.Exceptions;

namespace ShopHub.Modules.Identity.Domain.Exceptions;

public class UserAlreadyExistsException : DomainException
{
    public UserAlreadyExistsException(string email)
        : base("Identity.UserAlreadyExists", $"User with email '{email}' already exists.") { }
}