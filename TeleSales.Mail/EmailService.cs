using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using TeleSales.Mail;

namespace TRAK.EmailSender;

public class EmailService : IEmailService
{
    private readonly SmptSettings _smptSettings;

    public EmailService(SmptSettings smptSettings)
    {
        _smptSettings = smptSettings;
    }

    public async Task SendEmailAsync(string to, string subject, string content)
    {
        var message = new MimeMessage();

        // Указываем отправителя
        message.From.Add(new MailboxAddress(_smptSettings.SenderName, _smptSettings.Login));

        // Указываем получателя
        message.To.Add(new MailboxAddress(to, to));

        // Тема письма
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = content
        };
        message.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            try
            {
                // Подключаемся к SMTP-серверу
                await client.ConnectAsync(_smptSettings.SmtpServer, _smptSettings.Port, SecureSocketOptions.StartTls);

                // Авторизуемся
                await client.AuthenticateAsync(_smptSettings.Login, _smptSettings.Password);

                // Отправляем сообщение
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                // Логирование ошибки или повторная попытка
                throw new InvalidOperationException($"An error occurred while sending email: {ex.Message}");
            }
            finally
            {
                // Отключаемся от сервера
                await client.DisconnectAsync(true);
            }
        }
    }
}