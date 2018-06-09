namespace SharpyJson.Scripts.Core
{
    public enum RequestTypes
    {
        None = 0,
        
        // AUTH
        Login = 1,
        LogOut = 2,
        Register = 3,
        
        // Microservices (1001 - 1999)
        GetServiceStatus = 1001
    }
}