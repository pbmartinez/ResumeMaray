using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Threading.Tasks;
using AzureFunctionApp.Services;
using Common.Models;

namespace FunctionApp.Controllers
{
    public class ContactInfoController
    {
        private readonly ILogger<ContactInfoController> _logger;
        private readonly IValidatorService _validatorService;
        private readonly IEmailService _emailService;

        public ContactInfoController(ILogger<ContactInfoController> logger, IValidatorService validatorService, IEmailService emailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validatorService = validatorService ?? throw new ArgumentNullException(nameof(validatorService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        /// <summary>
        /// Health endpoint to check if the function is running
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("health")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage = "This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        /// <summary>
        /// ContactInfo endpoint to send email from interested contact
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [FunctionName("contactinfo")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("HTTP trigger function ContactInfo.");
            var cadena = "Entro a la funcion \n";
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            cadena += "\n " + requestBody;
            var contactInfo = JsonConvert.DeserializeObject<ContactInfo>(requestBody);
            var validationResults = _validatorService.TryValidateObject(contactInfo);
            if (!validationResults.IsValid)
            {
                return new BadRequestObjectResult(validationResults.Results);
            }
            var result = await _emailService.SendEmailAsync(contactInfo.Email, contactInfo.Subject, contactInfo.Message);
            cadena += "\n el resultado del correo: " + result.StatusCode;
            _logger.LogInformation("Email response: " + result.StatusCode);
            return result.IsSuccessStatusCode ? new OkObjectResult(result) : new ObjectResult(cadena);
        }
    }
}
