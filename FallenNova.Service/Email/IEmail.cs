using System.Collections.Generic;

namespace FallenNova.Service
{
    internal interface IEmail
    {
        string FromEmailAddress { get; set; }
        string FromRecipientName { get; set; }
        string ToEmailAddress { get; set; }
        string ToRecipientName { get; set; }
        string Subject { get; set; }
        string EmailBody { get; set; }
        
        IEnumerable<string> ContactUsEmailAddresses { get; }

        bool Dispatch();
    }
}
