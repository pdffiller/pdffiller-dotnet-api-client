using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.CustomLogo
{
    public class CustomLogoCreateRequest
    {
        /// <summary>
        /// The uploaded file can be an URL, or base64 string.
        /// Image to use as the custom logo. Can be .jpeg, .jpg, .gif or .png extension image.
        /// </summary>
        public string file { get; set; }

        public override string ToString()
        {
            return $"file={file}";
        }
    }
}