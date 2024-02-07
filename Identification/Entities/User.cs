using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Identification.Services;

namespace Identification.Entities;

public class User
{
    private static readonly IPasswordManager _passwordManager = new PasswordManager();

    private User(){ }

    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; private set; }
    public string? Fullname { get; private set; }
    public string? Email { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; } = null;
    public  string Phone { get; private set; } = string.Empty;
    public  string Password { get; private set; } = string.Empty;

    public static User CreateAsync(string? fullname,string? email,string phone,string password,CancellationToken ct)
    {
        if (ct.IsCancellationRequested) return new User();

        var user = new User
        {
            Email = email,
            Phone = phone,
            Fullname = fullname,
            CreatedAt = DateTime.UtcNow,
            Password = _passwordManager.Hash(password)
        };

        return user;
    }
}