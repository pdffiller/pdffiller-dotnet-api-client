using System.Collections.Generic;

namespace PdfFillerClient.DTO.Application
{
    public class ApplicationCreateResponse
    {
        public string id { get; set; }
        public string secret { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string domain { get; set; }
        public string redirect_uri { get; set; }

        public override string ToString()
        {
            return $"id={id}, secret={secret}, name={name}, description={description}, domain={domain}, redirect_uri={redirect_uri}";
        }
    }
}
