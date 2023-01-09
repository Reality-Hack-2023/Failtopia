using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using UnityEngine;
using System.Net.Mail;
using System.Text;
using FoundryNetwork.Email;

namespace FoundryNetwork.Email
{
    public class EmailHandler
    {
        public static void SendEmail(string name, string fromAdress, string toAdress, string body, string attachments)
        {
            EmailBody emailBody = EmailBody.EmailBodyFromJson(body);

            #region EmailCode

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials =
                new System.Net.NetworkCredential("spatialapetesting", "svsgpodfrexhyccu") as ICredentialsByHost;
            client.EnableSsl = true;

            MailAddress from = new MailAddress(fromAdress, name);
            MailAddress to = new MailAddress(toAdress);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Spatial Ape";
            message.Body =
                $"Website : {emailBody.website}, Discord : {emailBody.discord}, Email : {emailBody.email}, Github : {emailBody.github}";
            message.BodyEncoding = Encoding.UTF8;
            client.SendCompleted += SendCompletedCallback;
            string userstate = "test message";
            client.SendAsync(message, userstate);

            #endregion
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            string token = (string)e.UserState;

            if (e.Cancelled)
            {
                Debug.Log("Send canceled " + token);
            }

            if (e.Error != null)
            {
                Debug.Log("[ " + token + " ] " + " " + e.Error.ToString());
            }
            else
            {
                Debug.Log("Message sent.");
            }
        }

        [System.Serializable]
        public class EmailBody
        {
            public string website;
            public string discord;
            public string email;
            public string github;

            public static EmailBody EmailBodyFromJson(string rawBody)
            {
                return JsonUtility.FromJson<EmailBody>(rawBody);
            }
        }
    }
}
