using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EpochtaSms
{
    public class EpochtaSmsClient
    {
        private const string BaseUrl = "http://api.myatompark.com/members/sms/xml.php";

        public string Username { get; }
        public string Password { get; }
        public string Sender { get; }

        public EpochtaSmsClient(string username, string password, string sender)
        {
            Username = username;
            Password = password;
            Sender = sender;
        }

        public async Task SendAsync(Message message)
        {
            var xml =
$@"XML=<?xml version=""1.0"" encoding=""UTF-8""?>
<SMS>
    <operations>
        <operation>SEND</operation>
    </operations>
    <authentification>
        <username>{Username}</username>
        <password>{Password}</password>
    </authentification>
    <message>
        <sender>{Sender}</sender>
        <text>{message.Text}</text>
    </message>
    <numbers>
        <number messageID=""{Guid.NewGuid()}"">{message.Phone}</number>
    </numbers>
</SMS>";
            await Task.Run(() =>
            {
                var request = WebRequest.Create(BaseUrl) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                var data = Encoding.UTF8.GetBytes(xml);
                request.ContentLength = data.Length;
                var dataStream = request.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new WebException($"{response.StatusCode}: {response.StatusDescription}");
                    var reader = new StreamReader(response.GetResponseStream());
                    var result = reader.ReadToEnd();
                    var xdoc = XDocument.Parse(result);
                    var status = int.Parse(xdoc.Descendants("status").FirstOrDefault()?.Value ?? "0");
                    if (status <= 0)
                        throw new EpochtaException(status);
                }
            });
        }
    }
}
