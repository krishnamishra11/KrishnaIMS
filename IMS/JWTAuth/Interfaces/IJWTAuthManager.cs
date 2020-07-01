namespace IMS.JWTAuth.Interfaces
{
    public interface IJwtAuthManager
    {
        string Authenticate(string name, string password );
    }
}
