using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000088 RID: 136
	[DataContract]
	public class MessagingConfigurationBase : BaseRow
	{
		// Token: 0x06001B94 RID: 7060 RVA: 0x00057515 File Offset: 0x00055715
		public MessagingConfigurationBase(MailboxMessageConfiguration mailboxMessageConfiguration) : base(mailboxMessageConfiguration)
		{
			this.MailboxMessageConfiguration = mailboxMessageConfiguration;
		}

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x00057525 File Offset: 0x00055725
		// (set) Token: 0x06001B96 RID: 7062 RVA: 0x0005752D File Offset: 0x0005572D
		public MailboxMessageConfiguration MailboxMessageConfiguration { get; private set; }
	}
}
