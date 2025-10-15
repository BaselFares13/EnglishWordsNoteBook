namespace EnglishWordsNoteBook.Exceptions
{
    public class HttpStatusCodeException : Exception
    {
        public int StatusCode { get; private set; }

        public HttpStatusCodeException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
