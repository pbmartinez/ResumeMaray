using AzureFunctionApp.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;


[assembly: FunctionsStartup(typeof(AzureFunctionApp.Startup))]
namespace AzureFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var sendGridApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var emailTo = Environment.GetEnvironmentVariable("EMAILTO");

            builder.Services.AddSingleton<IEmailService>(new EmailService(sendGridApiKey, emailTo));
            builder.Services.AddScoped<IValidatorService, ValidatorService>();
        }
    }
}
