using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Shared.Smtp
{
	// Token: 0x02000024 RID: 36
	internal interface ISmtpMailItemSenderNotifications
	{
		// Token: 0x060000F6 RID: 246
		void AckConnection(AckStatus status, SmtpResponse smtpResponse);

		// Token: 0x060000F7 RID: 247
		void AckMessage(AckStatus status, SmtpResponse smtpResponse);

		// Token: 0x060000F8 RID: 248
		void AckRecipient(AckStatus status, SmtpResponse smtpResponse, MailRecipient recipient);
	}
}
