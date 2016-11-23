using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.CustomLogo
{
    public class CustomLogoCreateResponse
    {
        public long id { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public long filesize { get; set; }
        public string logo_url { get; set; }
    }
}
