using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Models
{
    public class MSMQModel
    {
        MessageQueue MessageQ = new MessageQueue();
        public void sendData2Queue(string token)
        {
            MessageQ.Path = @".\private$\Token";
            if (!MessageQueue.Exists(MessageQ.Path))

            {
                MessageQueue.Create(MessageQ.Path);
                //Exists
            }

            MessageQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            MessageQ.ReceiveCompleted += MessageQ_ReceiveCompleted;
            MessageQ.Send(token);
            MessageQ.BeginReceive();
            MessageQ.Close();
        }

        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = MessageQ.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                string subject = "BookStore App Reset Link";
                string body = token;
                var SMTP = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("vetribrlabz@gmail.com", "trpvpgzsjcvnxglj"),
                    EnableSsl = true,
                };
                SMTP.Send("vetribrlabz@gmail.com", " vetribrlabz@gmail.com", subject, body);
                MessageQ.BeginReceive();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
