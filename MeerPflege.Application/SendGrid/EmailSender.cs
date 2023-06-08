using SendGrid;
using SendGrid.Helpers.Mail;

namespace MeerPflege.Application.SendGrid
{
  public class EmailSender
  {
    private readonly string _sendGridKey = "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";
    private readonly string _templateId = "d-14da577b24054e869b5c5d74b16bdbfa";
    private readonly string _resetPasswordTemplateId = "d-55483e515fec409ea674d9b8de07e5df";

    public EmailSender()
    {
    }

    public async Task SendEmailWithTemplateAsync(string email, object dynamicTemplateData)
    {
      var client = new SendGridClient(_sendGridKey);
      var msg = new SendGridMessage()
      {
        From = new EmailAddress("info@oldpeoplehome.com", "Old People's Home"),
        TemplateId = _templateId
      };

      msg.SetTemplateData(dynamicTemplateData);
      msg.AddTo(new EmailAddress(email));

      await client.SendEmailAsync(msg);
    }

    public async Task SendResetPasswordEmailWithTemplateAsync(string email, object dynamicTemplateData)
    {
      var client = new SendGridClient(_sendGridKey);
      var msg = new SendGridMessage()
      {
        From = new EmailAddress("info@oldpeoplehome.com", "Old People's Home"),
        TemplateId = _resetPasswordTemplateId
      };

      msg.SetTemplateData(dynamicTemplateData);
      msg.AddTo(new EmailAddress(email));

      await client.SendEmailAsync(msg);

    }
  }
}