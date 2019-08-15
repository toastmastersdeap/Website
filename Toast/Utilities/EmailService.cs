using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Toast.Utilities
{
    public class EmailService : IIdentityMessageService
    {
        private static SendGridClient _sendGrid = new SendGridClient(ConfigurationManager.AppSettings["SendGridApiKey"]);
        private const string EmailFromName = "SpeakerAnalytics";
        private const string Company = "SpeakerAnalytics";

        private EmailAddress EmailFromAddress = new EmailAddress("no_reply@speakeranalytics.com", EmailFromName);

        public async Task SendAsync(IdentityMessage message)
        {
            var theMessage = new SendGridMessage()
            {
                From = EmailFromAddress,
                Subject = message.Subject,
                HtmlContent = message.Body
            };
            theMessage.AddTo(new EmailAddress(message.Destination));

            var response = await _sendGrid.SendEmailAsync(theMessage);
        }

        //public Task SendRequestCodeEmail(StringDictionary contactForm)
        //{
        //   const string subject        = "Request - Registration Code";
        //   const string toEmailAddress = "contact@speakeranalytics.com";

        //   var theMessage = new SendMessageTemplateRequest(new EmailMessage
        //   {
        //      FromEmail = EmailFromAddress,
        //      FromName  = EmailFromName,
        //      Subject   = subject,
        //      To        = new List<EmailAddress> { new EmailAddress(toEmailAddress) },
        //      Merge     = true
        //   }, "registration-code-request");
        //   theMessage.Async = true;

        //   foreach (DictionaryEntry item in contactForm)
        //   {
        //      theMessage.Message.AddGlobalVariable(item.Key.ToString(), item.Value.ToString());
        //   }

        //   theMessage.Message.AddGlobalVariable("current_year", DateTimeOffset.UtcNow.Year.ToString());
        //   theMessage.Message.AddGlobalVariable("company", Company);

        //   var task = _mandrill.SendMessageTemplate(theMessage);

        //   return task;
        //}

        //public Task SendRegistrationCodeToAdminEmail(StringDictionary contactForm, string userEmail)
        //{
        //   const string subject = "Registration Code";

        //   var theMessage = new SendMessageTemplateRequest(new EmailMessage
        //   {
        //      FromEmail = EmailFromAddress,
        //      FromName  = EmailFromName,
        //      Subject   = subject,
        //      To        = new List<EmailAddress> { new EmailAddress(userEmail) },
        //      Merge     = true
        //   }, "registration-code-admin");
        //   theMessage.Async = true;

        //   foreach (DictionaryEntry item in contactForm)
        //   {
        //      theMessage.Message.AddGlobalVariable(item.Key.ToString(), item.Value.ToString());
        //   }

        //   theMessage.Message.AddGlobalVariable("current_year", DateTimeOffset.UtcNow.Year.ToString());
        //   theMessage.Message.AddGlobalVariable("company", Company);

        //   var task = _mandrill.SendMessageTemplate(theMessage);

        //   return task;
        //}

        //public Task SendRegistrationCodeToMasterEmail(StringDictionary contactForm, string userEmail)
        //{
        //   const string subject = "Registration Code";

        //   var theMessage = new SendMessageTemplateRequest(new EmailMessage
        //   {
        //      FromEmail = EmailFromAddress,
        //      FromName  = EmailFromName,
        //      Subject   = subject,
        //      To        = new List<EmailAddress> { new EmailAddress(userEmail) },
        //      Merge     = true
        //   }, "registration-code-master");
        //   theMessage.Async = true;

        //   foreach (DictionaryEntry item in contactForm)
        //   {
        //      theMessage.Message.AddGlobalVariable(item.Key.ToString(), item.Value.ToString());
        //   }

        //   theMessage.Message.AddGlobalVariable("current_year", DateTimeOffset.UtcNow.Year.ToString());
        //   theMessage.Message.AddGlobalVariable("company", Company);

        //   var task = _mandrill.SendMessageTemplate(theMessage);

        //   return task;
        //}

        //public Task SendRegistrationCodeToUserEmail(StringDictionary contactForm, string userEmail)
        //{
        //    const string subject = "Registration Code";

        //    var theMessage = new SendMessageTemplateRequest(new EmailMessage
        //    {
        //        FromEmail = EmailFromAddress,
        //        FromName = EmailFromName,
        //        Subject = subject,
        //        To = new List<EmailAddress> { new EmailAddress(userEmail) },
        //        Merge = true
        //    }, "registration-code");
        //    theMessage.Async = true;

        //    foreach (DictionaryEntry item in contactForm)
        //    {
        //        theMessage.Message.AddGlobalVariable(item.Key.ToString(), item.Value.ToString());
        //    }

        //    theMessage.Message.AddGlobalVariable("current_year", DateTimeOffset.UtcNow.Year.ToString());
        //    theMessage.Message.AddGlobalVariable("company", Company);

        //    var task = _mandrill.SendMessageTemplate(theMessage);

        //    return task;
        //}

        //public Task SendRequestInfoEmail(StringDictionary contactForm)
        //{
        //   const string subject        = "Request - Customer Info";
        //   const string toEmailAddress = "contact@speakeranalytics.com";

        //   var theMessage = new SendMessageTemplateRequest(new EmailMessage
        //   {
        //      FromEmail = EmailFromAddress,
        //      FromName  = EmailFromName,
        //      Subject   = subject,
        //      To        = new List<EmailAddress> { new EmailAddress(toEmailAddress) },
        //      Merge     = true
        //   }, "contact-form-request");
        //   theMessage.Async = true;

        //   foreach (DictionaryEntry item in contactForm)
        //   {
        //      if (!string.IsNullOrEmpty(item.Value?.ToString()))
        //      {
        //         theMessage.Message.AddGlobalVariable(item.Key.ToString(), item.Value.ToString());
        //      }
        //   }

        //   theMessage.Message.AddGlobalVariable("current_year", DateTimeOffset.UtcNow.Year.ToString());
        //   theMessage.Message.AddGlobalVariable("company", Company);

        //   var task = _mandrill.SendMessageTemplate(theMessage);

        //   //task.Wait();

        //   return task;
        //}

        //public Task SendRequestSupportEmail(StringDictionary contactForm)
        //{
        //    const string subject = "Request - Customer Support";
        //    const string toEmailAddress = "contact@speakeranalytics.com";

        //    var theMessage = new SendMessageTemplateRequest(new EmailMessage
        //    {
        //        FromEmail = EmailFromAddress,
        //        FromName = EmailFromName,
        //        Subject = subject,
        //        To = new List<EmailAddress> { new EmailAddress(toEmailAddress) },
        //        Merge = true
        //    }, "contact-support-request");
        //    theMessage.Async = true;

        //    foreach (DictionaryEntry item in contactForm)
        //    {
        //        if (!string.IsNullOrEmpty(item.Value?.ToString()))
        //        {
        //            theMessage.Message.AddGlobalVariable(item.Key.ToString(), item.Value.ToString());
        //        }
        //    }

        //    theMessage.Message.AddGlobalVariable("current_year", DateTimeOffset.UtcNow.Year.ToString());
        //    theMessage.Message.AddGlobalVariable("company", Company);

        //    var task = _mandrill.SendMessageTemplate(theMessage);

        //    //task.Wait();

        //    return task;
        //}

        public Task SendWelcomeEmail(string firstName, string toEmailAddress, string confirmationUrl)
        {
            const string subject = "Welcome to SpeakerAnalytics!";

            var theMessage = new SendGridMessage()
            {
                From = EmailFromAddress,
                Subject = subject
            };
            theMessage.SetTemplateId("d-ca86c08eb0c84dbe87c5f2e53bf9e37a");
            theMessage.AddTo(new EmailAddress(toEmailAddress));

            var dynamicTemplateData = new
            {
                FirstName = firstName,
                ConfirmUrl = confirmationUrl,
                CurrentYear = DateTimeOffset.UtcNow.Year.ToString(),
                Company = Company
            };
            theMessage.SetTemplateData(dynamicTemplateData);

            var response = _sendGrid.SendEmailAsync(theMessage);

            return response;
        }

        public Task SendResetPasswordEmail(string firstName, string email, string ip, string country, string resetLink)
        {
            const string subject = "Recover your Account";

            var theMessage = new SendGridMessage()
            {
                From = EmailFromAddress,
                Subject = subject,
            };
            theMessage.SetTemplateId("d-0798f22893c943dabe60c67f85fb4318");
            theMessage.AddTo(new EmailAddress(email));

            if (string.IsNullOrEmpty(ip))
            {
                ip = "Not Found";
            }

            if (string.IsNullOrEmpty(country))
            {
                country = "- Not Found";
            }

            var dynamicTemplateData = new
            {
                FirstName = firstName,
                Email = email,
                ResetURL = resetLink,
                CurrentYear = DateTimeOffset.UtcNow.Year.ToString(),
                Company = Company
            };
            theMessage.SetTemplateData(dynamicTemplateData);

            var response = _sendGrid.SendEmailAsync(theMessage);

            return response;
        }

        //public Task SendWebError(string toEmailAddress, string user, string routeData, string exceptionMessage, string exceptionTrace)
        //{
        //    const string subject = "Error - Web Server";

        //    var theMessage = new SendMessageTemplateRequest(new EmailMessage
        //    {
        //        FromEmail = EmailFromAddress,
        //        FromName = EmailFromName,
        //        Subject = subject,
        //        To = new List<Mandrill.Models.EmailAddress> { new EmailAddress(toEmailAddress) },
        //        Merge = true
        //    }, "web-error");
        //    theMessage.Async = true;

        //    theMessage.Message.AddGlobalVariable("web_address", ConfigurationManager.AppSettings["AppWebSite"]);
        //    theMessage.Message.AddGlobalVariable("user_affected", user);
        //    theMessage.Message.AddGlobalVariable("route_data", routeData);
        //    theMessage.Message.AddGlobalVariable("datestamp", DateTimeOffset.UtcNow);
        //    theMessage.Message.AddGlobalVariable("exception_message", exceptionMessage);
        //    theMessage.Message.AddGlobalVariable("exception_trace", exceptionTrace);
        //    theMessage.Message.AddGlobalVariable("current_year", DateTimeOffset.UtcNow.Year.ToString());
        //    theMessage.Message.AddGlobalVariable("company", Company);

        //    var task = _mandrill.SendMessageTemplate(theMessage);

        //    return task;
        //}
    }
}