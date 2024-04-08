using System;
using System.Net;
using System.Net.Mail;

namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new SmtpClient("bulk.smtp.mailtrap.io", 587)
            {
                Credentials = new NetworkCredential("api", "e8761e829bb5029ab396344d43537310"),
                EnableSsl = true
            };
            client.Send("mailtrap@srrairlines.duckdns.org", "ivanavalcheva@gmail.com", "Hello world", "testbody");
        }
    }
}
