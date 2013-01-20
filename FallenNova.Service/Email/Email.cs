using FallenNova.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;

namespace FallenNova.Service
{
    internal class Email : IEmail
    {
        private const char ConstStringSeparator = ',';

        public string FromEmailAddress { get; set; }
        public string FromRecipientName { get; set; }
        public string ToEmailAddress { get; set; }
        public string ToRecipientName { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }

        public IEnumerable<string> ContactUsEmailAddresses
        {
            get
            {
                return Constants.ContactUsEmailAddresses.Split(ConstStringSeparator);
            }
        }

        public bool Dispatch()
        {
            try
            {
                Debug.Print("Email dispatched...");
            }
            catch (SmtpException)
            {
                Debug.Print("Email dispatch failed. SMTP reasons.");

                return false;
            }
            catch (Exception)
            {
                Debug.Print("Email dispatch failed. Other reasons.");

                return false;
            }

            Debug.Print("Email dispatch succeeded.");

            return true;
        }
    }
}
