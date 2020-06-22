namespace IMS.JWTAuth.Interfaces
{
    public interface IJWTAuthManager
    {
        string Authenticate(string name, string password );
    }
}
