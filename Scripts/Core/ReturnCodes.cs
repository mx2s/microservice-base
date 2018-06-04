namespace SharpyJson.Scripts.Core
{
    public enum ReturnCodes
    {
        Success = 1,
        
        // SERVER PROBLEMS ( 2 - 99 )
        FailedServerError = 2,
        FailedEmptyResponse = 3,
        FailedNotFound = 4,
        
        // INPUT DATA PROBLEMS ( 100 - 199 )
        FailedWrongInputData = 100,
        FailedWrongRequestType = 101,
        FailedUserNotExist = 102,
        
        // AUTH ( 200 - 299 )
        FailedInvalidLoginData = 200,
        LoginFailed = 201,
    }
}