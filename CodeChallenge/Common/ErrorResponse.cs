namespace CodeChallenge.Common
{
    public class Error {

        private readonly string Message;

        public Error(string message)
        {
            Message = message;
        }

        public string getErrorMessage() => Message;
    }
   
}
