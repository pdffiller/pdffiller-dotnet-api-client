using System;

namespace PdfFillerClient.Exceptions
{
    public class PdfFillerAppException : Exception
    {
        public PdfFillerAppException()
        {

        }

        public PdfFillerAppException(string message) : base(message)
        {

        }

        public PdfFillerAppException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
