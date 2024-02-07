using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identification.Entities;

public class Role
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ushort Id { get; }
    public string Name { get; private set; } = string.Empty;

    private Role(){ }

    public static Role Create(string name)
    {
        if(string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        var role = new Role
        {
            Name = name
        };

        return role;
    }
    
}