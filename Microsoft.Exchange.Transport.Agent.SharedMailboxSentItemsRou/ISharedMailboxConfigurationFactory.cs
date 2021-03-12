using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Agent.SharedMailboxSentItemsRoutingAgent
{
	// Token: 0x02000005 RID: 5
	internal interface ISharedMailboxConfigurationFactory
	{
		// Token: 0x06000005 RID: 5
		SharedMailboxConfiguration GetSharedMailboxConfiguration(MailItem transportMailItem, string sender);
	}
}
