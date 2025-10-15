namespace EnglishWordsNoteBook.DTO.User
{
    public class ReturnAuthDTO
    {
        public bool Success {  get; set; }
        public string Message { get; set; }

        public TokensDTO? Tokens { get; set; }
    }
}
