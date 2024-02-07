namespace Identification.Features.Admin.CreateUser;

public sealed class Endpoint : Endpoint<CreateUserRequest, UserResponse, Mapper>
{
    public override void Configure()
    {
        Post("api/users");
        AllowAnonymous();
        Description(d => d
                .Accepts<CreateUserRequest>("application/json+custom")
                .Produces<UserResponse>(200, "application/json+custom")
                .ProducesProblemFE(400)
                .ProducesProblemFE<InternalErrorResponse>(500),
            clearDefaults:true);

        Summary(s => {
            s.Summary = "will create new user in system.";
            s.Responses[200] = "ok response description goes here";
            s.Responses[403] = "forbidden response description goes here";
        });
    }

    public override async Task HandleAsync(CreateUserRequest r, CancellationToken c)
    {
        var createdUser = await Data.CreateUser(await Map.ToEntityAsync(r,c));
        var response = await Map.FromEntityAsync(createdUser, c);

        await SendAsync(response, cancellation: c);
    }
}