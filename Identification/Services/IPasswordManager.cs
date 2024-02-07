namespace Identification.Services;

public interface IPasswordManager
{
    public string Hash(string password);
    public bool Verify(string hash, string providedPassword);
}