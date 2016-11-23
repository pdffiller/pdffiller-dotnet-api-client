using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.Document
{
    public class DocumentCreateResponse
    {
        public long id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public long created { get; set; }
        public Folder folder { get; set; }

        public override string ToString()
        {
            return $"id={id}, name={name}, type={type}, created={created}";
        }
    }

    public class Folder
    {
        public long id { get; set; }
        public string folder { get; set; }

        public override string ToString()
        {
            return $"id={id}, folder={folder}";
        }
    }
}
