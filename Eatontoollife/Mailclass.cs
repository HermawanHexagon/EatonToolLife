using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Threading;
using Eatontoollife.Properties;
using System.Windows.Forms;

namespace Eatontoollife
{
    class Mailclass
    {
        IniFile Myini = new IniFile(@".\AppConfig.ini");
        Sqloperation Mydb = new Sqloperation();

        public string messagedateissue = string.Empty;
        public string messagedatewarning = string.Empty;
        public string messagedatestop = string.Empty;
        public string usernameissue = string.Empty;
        public string usernamewarning = string.Empty;
        public string usernamestop = string.Empty;

        public void SendEmail(string statusmessage)
        {
            if (statusmessage == "warning")
            {
                Thread StartThread = new Thread(new ThreadStart(ReportMailItemwarning));
                StartThread.Start();
            }
            else if (statusmessage == "stop")
            {
                Thread StartThread = new Thread(new ThreadStart(ReportMailItemStop));
                StartThread.Start();
            }
            else if (statusmessage == "issue")
            {
                Thread StartThread = new Thread(new ThreadStart(ReportMailIteissue));
                StartThread.Start();
            }
        }

        private void ReportMailIteissue()
        {
            try
            {
                SmtpClient Client = new SmtpClient("mail.etn.com", 25);
                Client.EnableSsl = false;
                Client.DeliveryMethod = SmtpDeliveryMethod.Network;
                Client.UseDefaultCredentials = true;
                Client.Credentials = new NetworkCredential(Mydb.Getemailsender("ISSUE").Replace(";", ","), Mydb.Getpasswordsender("ISSUE"));
                Attachment Attach = new Attachment(Mydb.Getemailattachment("ISSUE") + @"\Issue - " + messagedateissue + ".txt");
                MailMessage message = new MailMessage();
                message.To.Add(Mydb.Getemaillist("ISSUE").Replace(";", ","));
                message.From = new MailAddress(Mydb.Getemailsender("ISSUE"));
                message.Subject = Mydb.Getemailsubject("ISSUE");
                message.Body = Mydb.Getemailmessage("ISSUE") + usernameissue;
                message.Priority = MailPriority.High;
                message.Attachments.Add(Attach);
                Client.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReportMailItemStop()
        {
            try
            {
                SmtpClient Client = new SmtpClient("mail.etn.com", 25);
                Client.EnableSsl = false;
                Client.DeliveryMethod = SmtpDeliveryMethod.Network;
                Client.UseDefaultCredentials = true;
                Client.Credentials = new NetworkCredential(Mydb.Getemailsender("STOP").Replace(";", ","), Mydb.Getpasswordsender("STOP"));
                Attachment Attach = new Attachment(Mydb.Getemailattachment("STOP") + @"\Stop - " + messagedateissue + ".txt");
                MailMessage message = new MailMessage();
                message.To.Add(Mydb.Getemaillist("STOP").Replace(";", ","));
                message.From = new MailAddress(Mydb.Getemailsender("STOP"));
                message.Subject = Mydb.Getemailsubject("STOP");
                message.Body = Mydb.Getemailmessage("STOP") + usernameissue;
                message.Priority = MailPriority.High;
                message.Attachments.Add(Attach);
                Client.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReportMailItemwarning()
        {
            try
            {
                SmtpClient Client = new SmtpClient("mail.etn.com", 25);
                Client.EnableSsl = false;
                Client.DeliveryMethod = SmtpDeliveryMethod.Network;
                Client.UseDefaultCredentials = true;
                Client.Credentials = new NetworkCredential(Mydb.Getemailsender("WARNING").Replace(";", ","), Mydb.Getpasswordsender("WARNING"));
                Attachment Attach = new Attachment(Mydb.Getemailattachment("WARNING") + @"\Warning - " + messagedateissue + ".txt");
                MailMessage message = new MailMessage();
                message.To.Add(Mydb.Getemaillist("WARNING").Replace(";", ","));
                message.From = new MailAddress(Mydb.Getemailsender("WARNING"));
                message.Subject = Mydb.Getemailsubject("WARNING");
                message.Body = Mydb.Getemailmessage("WARNING") + usernameissue;
                message.Priority = MailPriority.High;
                message.Attachments.Add(Attach);
                Client.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        /*
            try
            {
                Outlook.Application oApp = new Outlook.Application();
                Outlook.MailItem email = (Outlook.MailItem)(oApp.CreateItem(Outlook.OlItemType.olMailItem));
                email.Subject = Mydb.Getemailsubject("ISSUE");
                email.To = Mydb.Getemaillist("ISSUE");
                email.Body = Mydb.Getemailmessage("ISSUE") + usernameissue;
                email.Attachments.Add(Mydb.Getemailattachment("ISSUE") + @"\Issue - " + messagedateissue + ".txt");
                email.Importance = Outlook.OlImportance.olImportanceHigh;
                ((Outlook._MailItem)email).Send();
            }
            catch (Exception)
            {
                throw;
            } 
        */
    }
}
