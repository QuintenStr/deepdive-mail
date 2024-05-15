using deepdivemailing.Service.Interface;
using Azure;
using Azure.Communication.Email;
using deepdivemailing.Entities.DataTransferObjects;
using Serilog;

namespace deepdivemailing.Service
{
    /// <summary>
    /// Provides functionality to send emails using Azure Communication Services.
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the EmailService with necessary configuration.
        /// </summary>
        /// <param name="configuration">Application configuration holder</param>
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("EmailAzureConnection");
        }

        /// <summary>
        /// Sends an email using the provided email model.
        /// </summary>
        /// <param name="input">Data transfer object containing email details.</param>
        /// <returns>Boolean indicating success or failure of the email sending operation.</returns>
        public bool SendMail(EmailModelDto input)
        {
            bool isValidInput = !string.IsNullOrEmpty(input.Subject) &&
                                (!string.IsNullOrEmpty(input.PlainTextContent) || !string.IsNullOrEmpty(input.HtmlContent)) &&
                                (input.toRecipients?.Any() == true || input.ccRecipients?.Any() == true || input.bccRecipients?.Any() == true);

            if (!isValidInput)
            {
                Log.Information("Data model is not valid");
                return false;
            }

            // Initialize EmailClient with connection string
            EmailClient emailClient = new EmailClient(connectionString);

            // Setup email content
            var emailContent = new EmailContent(input.Subject)
            {
                PlainText = input.PlainTextContent,
                Html = input.HtmlContent
            };

            // Prepare recipient lists
            var toRecipients = input.toRecipients.Select(item => new EmailAddress(item)).ToList();
            var ccRecipients = input.ccRecipients.Select(item => new EmailAddress(item)).ToList();
            var bccRecipients = input.bccRecipients.Select(item => new EmailAddress(item)).ToList();

            EmailRecipients emailRecipients = new EmailRecipients(toRecipients, ccRecipients, bccRecipients);

            // Create email message
            var emailMessage = new EmailMessage(senderAddress: _configuration.GetSection("MailAddress")["Sender"], emailRecipients, emailContent);

            // Add attachments if any
            if (input.Attachments != null && input.Attachments.Any())
            {
                foreach (var item in input.Attachments)
                {
                    var newAttachement = new EmailAttachment(item.FileName, item.MediaType, new BinaryData(item.Content));
                    emailMessage.Attachments.Add(newAttachement);
                }
            }
            try
            {
                // Send email and log result
                EmailSendOperation emailSendOperation = emailClient.Send(WaitUntil.Started, emailMessage);
                Log.Information($"Email Sent. Status = {emailSendOperation.Value.Status}");

                string operationId = emailSendOperation.Id;
                Log.Information($"Email operation id = {operationId}");

            }
            catch (RequestFailedException ex)
            {
                Log.Information($"Email send operation failed with error code: {ex.ErrorCode}, message: {ex.Message}");
                return false;
            }
            return true;
        }
    }
}
