using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Identification.Services;

public class PasswordManager : IPasswordManager
{
    public string Hash(string password)
    {
        // Generate a random salt
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Hash the password using PBKDF2 with 10000 iterations
        string hashedPassword = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
            )
        );

        // Combine the salt and hashed password for storage
        string combinedHash = $"{Convert.ToBase64String(salt)}:{hashedPassword}";

        return combinedHash;
    }

    public bool Verify(string combinedHash, string providedPassword)
    {
        // Extract the salt and hashed password from the combined hash
        var parts = combinedHash.Split(':');
        byte[] salt = Convert.FromBase64String(parts[0]);
        string storedHashedPassword = parts[1];

        // Hash the provided password using the stored salt
        string hashedPassword = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: providedPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32
            )
        );

        // Compare the stored hashed password with the computed hash
        return storedHashedPassword == hashedPassword;
    }

}