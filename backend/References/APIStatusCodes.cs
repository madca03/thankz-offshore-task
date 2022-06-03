namespace backend.References
{
    public class APIStatusCodes
    {
        public const int Success = 0;

        public const int UsernameExists = -101;
        public const int UserNotFound = -102;
        
        public const int GenericFailure = -500;
        public const int ExceptionOccured = -501;
        public const int InvalidModelState = -502;
    }
}