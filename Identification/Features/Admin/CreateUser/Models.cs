using FluentValidation;

namespace Identification.Features.Admin.CreateUser;

public record CreateUserRequest(string? Fullname, string? Email, string Phone, string Password);

public sealed class Validator : Validator<CreateUserRequest>
{
    public Validator()
    {
        RuleFor(c => c.Phone)
            .Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");

        RuleFor(c => c.Password)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$");
    }
}

public record UserResponse(long Id, string? Fullname, string? Email, string Password, string Phone, DateTime CreatedAt);