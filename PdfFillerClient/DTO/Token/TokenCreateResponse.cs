using System.Collections.Generic;

namespace PdfFillerClient.DTO.Token
{
    public class TokenCreateResponse
    {
        public long id { get; set; }
        public string hash { get; set; }
        public List<object> data { get; set; }

        public override string ToString()
        {
            return $"id={id}, hash={hash}, data={data.Count}";
        }
    }
}
