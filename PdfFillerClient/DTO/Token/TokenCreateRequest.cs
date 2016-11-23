using System.Collections.Generic;

namespace PdfFillerClient.DTO.Token
{
    public class TokenCreateRequest
    {
        public List<object> data = new List<object>();

        public override string ToString()
        {
            return $"data={data.Count}";
        }
    }
}
