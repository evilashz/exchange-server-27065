using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000003 RID: 3
	// (Invoke) Token: 0x06000006 RID: 6
	internal delegate void SubmissionRecipientHandler(int? recipientType, Recipient recipient, TransportMailItem mailItem, MailRecipient mailRecipient);
}
