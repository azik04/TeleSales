﻿namespace TeleSales.Mail;

public class SmptSettings
{
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string SenderName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}