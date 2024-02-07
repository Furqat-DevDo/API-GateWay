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
            s.Summary = "short summary goes here";
            s.Description = "long description goes here";
            s.ExampleRequest = new CreateUserRequest("fullname","email@.com","requiredPhone : 998 90 099 00 00","Password :Asd123",
                [1,2]);
            s.ResponseExamples[200] = new UserResponse();
            s.Responses[200] = "ok response description goes here";
            s.Responses[403] = "forbidden response description goes here";
        });
    }

    public override async Task HandleAsync(CreateUserRequest r, CancellationToken c)
    {

        await SendAsync(new UserResponse(1, "", "", "", "", DateTime.Now), cancellation: c);
    }
}