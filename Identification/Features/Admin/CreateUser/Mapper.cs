using Identification.Entities;

namespace Identification.Features.Admin.CreateUser;

public sealed class Mapper : Mapper<CreateUserRequest, UserResponse, User>
{
    public override Task<User> ToEntityAsync(CreateUserRequest r, CancellationToken ct = default)
    {
        var user = User.CreateAsync(fullname: r.Fullname, email: r.Email, phone: r.Phone, password: r.Password,roles:r.Roles);

        return Task.FromResult(user);
    }


    public override Task<UserResponse> FromEntityAsync(User e, CancellationToken ct = default)
        => Task.FromResult<UserResponse>(new(e.Id, e.Fullname, e.Email, e.Password, e.Phone, e.CreatedAt));

}