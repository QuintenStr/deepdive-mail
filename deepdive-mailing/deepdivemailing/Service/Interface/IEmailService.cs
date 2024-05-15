using deepdivemailing.Entities.DataTransferObjects;

namespace deepdivemailing.Service.Interface
{
    /// <summary>
    /// Defines a interface for email services that can send emails.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email based on the provided email model data.
        /// </summary>
        /// <param name="input">The email model data transfer object containing all necessary information to send an email.</param>
        /// <returns>True if the email was sent successfully, otherwise false.</returns>
        public bool SendMail(EmailModelDto input);
    }
}
