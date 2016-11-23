using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.FillRequest
{
    public class FillRequestFilledFormsListResponse
    {
        public List<FillRequestFilledForm> items { get; set; }
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

    public class FillRequestFilledForm
    {
        public long id { get; set; }
        public long document_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public long date { get; set; }
        public string ip { get; set; }
        public string[] token { get; set; }
        public List<AdditionalDocuments> additional_documents { get; set; }
        public long reusable_document_id { get; set; }
    }

    public class AdditionalDocuments
    {
        public long id { get; set; }
        public string filename { get; set; }
        public string ip { get; set; }
        public long date_created { get; set; }
    }
}
