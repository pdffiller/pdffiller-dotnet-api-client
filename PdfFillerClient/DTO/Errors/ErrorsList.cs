using System.Collections.Generic;

namespace PdfFillerClient.DTO.Errors
{
    public class ErrorsList
    {
        public List<Error> errors { get; set; }

        public override string ToString()
        {
            string errorsList = "";
            foreach (Error error in errors)
            {
                errorsList += error.ToString() + ";";
            }
            return errorsList;
        }

        public string GetErrorsMsg()
        {
            string messages = "";
            foreach (Error error in errors)
            {
                messages += error.message + " ";
            }
            return messages;
        }
    }
}
