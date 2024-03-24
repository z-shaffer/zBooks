using Microsoft.AspNetCore.Identity.UI.Services;

namespace zBooks.Utility;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // TODO: Send email
        return Task.CompletedTask;
    }
}