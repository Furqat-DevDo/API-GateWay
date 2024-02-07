namespace Identification.Features.Admin.CreateUser;

public sealed class Endpoint : Endpoint<CreateUserRequest, UserResponse, Mapper>
{
    public override void Configure()
    {
        Post("route-pattern");
    }

    public override async Task HandleAsync(CreateUserRequest r, CancellationToken c)
    {

        await SendAsync(new UserResponse(1, "", "", "", "", DateTime.Now), cancellation: c);
    }
}