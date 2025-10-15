namespace EnglishWordsNoteBook.DTO.User
{
    public class TokensDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationTime { get; set; }
    }
}
