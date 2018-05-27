namespace SharpyJson.Scripts.Core
{
    public enum ReturnCodes
    {
        Success = 1,
        
        // SERVER PROBLEMS ( 2 - 99 )
        FailedServerError = 2,
        
        // INPUT DATA PROBLEMS ( 100 - 199 )
        FailedWrongInputData = 100,
        FailedWrongRequestType = 101
    }
}