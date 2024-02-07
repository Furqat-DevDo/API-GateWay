using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Identification.Services;

namespace Identification.Entities;

public class User
{
    private static readonly IPasswordManager _passwordManager = new PasswordManager();

    private static readonly List<UserRole> _roles = new ();
    private User(){ }

    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; private set; }
    public string? Fullname { get; private set; }
    public string? Email { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; } = null;
    public  string Phone { get; private set; } = string.Empty;
    public  string Password { get; private set; } = string.Empty;
    public IReadOnlyCollection<UserRole> Roles => _roles;

    public static User CreateAsync(string? fullname,string? email,string phone,string password, ushort[] roles)
    {
        var user = new User
        {
            Email = email,
            Phone = phone,
            Fullname = fullname,
            CreatedAt = DateTime.UtcNow,
            Password = _passwordManager.Hash(password)
        };

        if (roles.Length <= 0) return user;

        foreach (var roleId in roles)
        {
            _roles.Add(UserRole.Create(roleId,user.Id)); 
        }

        return user;
    }
}