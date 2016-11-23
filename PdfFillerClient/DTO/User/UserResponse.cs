namespace PdfFillerClient.DTO.User
{
    public class UserResponse
    {
        public long id { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }

        public override string ToString()
        {
            return $"id={id}, email={email}, avatar={avatar}";
        }
    }
}
