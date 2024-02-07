namespace Identification.Entities;

public class UserRole
{
    public ushort RoleId { get; private set; }
    public virtual Role? Role { get; set; }
    public long UserId { get; private set; }
    public virtual User? User { get; set; }

    private UserRole() {}

    public static UserRole Create(ushort roleId,long userId)
    {
        return new UserRole { RoleId = roleId,UserId = userId};
    }
   
}