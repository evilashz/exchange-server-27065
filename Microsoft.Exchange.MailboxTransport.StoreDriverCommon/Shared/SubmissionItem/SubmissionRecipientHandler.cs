using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Shared.SubmissionItem
{
	// Token: 0x02000030 RID: 48
	// (Invoke) Token: 0x0600017C RID: 380
	internal delegate void SubmissionRecipientHandler(int? recipientType, Recipient recipient, TransportMailItem mailItem, MailRecipient mailRecipient);
}
