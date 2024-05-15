using deepdivemailing.Entities.DataTransferObjects;
using deepdivemailing.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace deepdivemailing.Controllers
{
    /// <summary>
    /// Controller to handle email sending operations.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        /// <summary>
        /// Constructs an EmailController with a dependency on IEmailService.
        /// </summary>
        /// <param name="emailService">The service to send emails.</param>

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;       
        }

        /// <summary>
        /// Endpoint to send an email based on the provided email model.
        /// </summary>
        /// <param name="input">The email model containing all necessary information.</param>
        /// <returns>An IActionResult that indicates the result of the send operation.</returns>
        [HttpPost]
        [Route("Send")]
        public async Task<IActionResult> SendMail(EmailModelDto input)
        {
            try
            {
                if (_emailService.SendMail(input))
                {
                    Log.Information("Email sent successfully.");
                    return Ok();
                }
                else
                {
                    Log.Error("Data Model for email is invalid");
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, "Model for email is invalid");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while sending email. Message: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
