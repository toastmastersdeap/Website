using Microsoft.AspNet.Identity;
using System.Diagnostics;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Toast.Utilities
{
   public class SmsService : IIdentityMessageService
   {
      public Task SendAsync(IdentityMessage message)
      {
            // Twilio Begin
            TwilioClient.Init(
                System.Configuration.ConfigurationManager.AppSettings["SMSAccountIdentification"],
                System.Configuration.ConfigurationManager.AppSettings["SMSAccountPassword"]);

            var result = MessageResource.Create(
                to: message.Destination,
                from: System.Configuration.ConfigurationManager.AppSettings["SMSAccountFrom"],
                body: message.Body);

            //TODO: To test 
            //Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
            Trace.TraceInformation(result.Status.ToString());

            // Twilio doesn't currently have an async API, so return success.
            return Task.FromResult(0);
            // Twilio End
        }
    }
}