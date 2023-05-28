using SendGrid;
using SendGrid.Helpers.Mail;

namespace MeerPflege.Application.SendGrid
{
  public class EmailSender
  {
    private readonly string _sendGridKey = "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";
    private readonly string _templateId = "d-14da577b24054e869b5c5d74b16bdbfa";

    public EmailSender()
    {
    }

    public async Task SendEmailWithTemplateAsync(string email, object dynamicTemplateData)
    {
      var client = new SendGridClient(_sendGridKey);
      var msg = new SendGridMessage()
      {
        From = new EmailAddress("andrei.ciornei@outlook.com", "Old People's Home"),
        TemplateId = _templateId
      };

      msg.SetTemplateData(dynamicTemplateData);
      msg.AddTo(new EmailAddress(email));

      await client.SendEmailAsync(msg);
    }
  }
}