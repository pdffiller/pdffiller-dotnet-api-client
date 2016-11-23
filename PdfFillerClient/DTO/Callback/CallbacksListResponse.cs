using System.Collections.Generic;

namespace PdfFillerClient.DTO.Callback
{
    public class CallbacksListResponse
    {
        public List<CallbackCreateResponse> items { get; set; }
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
}
