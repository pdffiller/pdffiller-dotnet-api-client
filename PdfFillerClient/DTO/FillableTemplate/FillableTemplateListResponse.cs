using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.FillableTemplate
{
    public class FillableTemplateListResponse
    {
        public List<FillableTemplateListItem> items { get; set; }
        public int total { get; set; }
        public int current_page { get; set; }
        public int per_page { get; set; }
        public string prev_page_url { get; set; }
        public string next_page_url { get; set; }

        public override string ToString()
        {
            return $"total={total}, current_page={current_page}, per_page={per_page}, prev_page_url={prev_page_url}, next_page_url={next_page_url}, items={items.Count}";
        }
    }

    public class FillableTemplateListItem
    {
        public long id { get; set; }
        public long created { get; set; }

        public override string ToString()
        {
            return $"id={id}, created={created}";
        }
    }
}
