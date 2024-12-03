namespace TeleSales.Mail;

internal interface IEmailService
{ 
    Task SendEmailAsync(string to, string subject, string content);
}
