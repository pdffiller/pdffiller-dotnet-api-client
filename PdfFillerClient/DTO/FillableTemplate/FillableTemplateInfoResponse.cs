using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.FillableTemplate
{
    public class FillableTemplateFields
    {
        public string type { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public bool required { get; set; }

        public override string ToString()
        {
            return $"type={type}, name={name}, label={label}, required={required}";
        }
    }
}
