using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200024D RID: 589
	public interface IMailboxLocationInfo
	{
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001CEE RID: 7406
		Guid MailboxGuid { get; }

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001CEF RID: 7407
		ADObjectId DatabaseLocation { get; }

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001CF0 RID: 7408
		MailboxLocationType MailboxLocationType { get; }
	}
}
