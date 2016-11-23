using PdfFillerClient.DTO.Errors;
using System;

namespace PdfFillerClient.Exceptions
{
    public class PdfFillerApiException : Exception
    {
        public PdfFillerApiException()
        {

        }

        public PdfFillerApiException(string message) : base(message)
        {

        }

        public PdfFillerApiException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public PdfFillerApiException(string message, ErrorsList responseErrors, Exception innerException) : base(message, innerException)
        {

        }
    }
}
