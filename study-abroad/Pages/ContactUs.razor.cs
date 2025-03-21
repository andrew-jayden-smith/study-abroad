using PostmarkDotNet;
using System.Threading.Tasks;
using PostmarkDotNet.Model;

public class ContactUsService
{
    
    private readonly string _postmarkServerToken;

    public ContactUsService(string apitoken)
    {
        _postmarkServerToken = apitoken;
    }

    public async Task<bool> SendEmailAsync(string to, string from, string subject, string textBody, string htmlBody)
    {
        var message = new PostmarkMessage()
        {
            To = to,
            From = from,
            TrackOpens = true,
            Subject = subject,
            TextBody = textBody,
            HtmlBody = htmlBody,
            Tag = "Email Notification",
            Headers = new HeaderCollection
            {
                { new MailHeader("Header content") }
            }
        };

        var client = new PostmarkClient(_postmarkServerToken);
        var sendResult = await client.SendMessageAsync(message);

        return sendResult.Status == PostmarkStatus.Success;
    }
}
