using Azure.Communication.Email;

namespace deepdivemailing.Entities.DataTransferObjects
{
    /// <summary>
    /// Represents the data needed to send an email.
    /// </summary>
    public class EmailModelDto
    {
        public string Subject { get; set; }

        public string PlainTextContent { get; set; }
        public string HtmlContent { get; set; }

        public List<string> toRecipients { get; set; }
        public List<string> ccRecipients { get; set; }
        public List<string> bccRecipients { get; set; }

        public List<EmailAttachmentBinaryBypass> Attachments { get; set; }
    }
}
