namespace deepdivemailing.Entities.DataTransferObjects
{
    /// <summary>
    /// Represents an email attachment, bypassing the usual binary attachment constraints to prevent JSON read critical errors.
    /// </summary>
    public class EmailAttachmentBinaryBypass
    {
        public string FileName { get; set; }
        public string MediaType { get; set; }
        public byte[] Content { get; set; }

        /// <summary>
        /// Initializes a new instance of the EmailAttachmentBinaryBypass class.
        /// </summary>
        /// <param name="fileName">The file name of the attachment.</param>
        /// <param name="mediaType">The MIME type of the attachment.</param>
        /// <param name="content">The binary content of the attachment.</param>
        public EmailAttachmentBinaryBypass(string fileName, string mediaType, byte[] content)
        {
            FileName = fileName;
            MediaType = mediaType;
            Content = content;
        }
    }
}
