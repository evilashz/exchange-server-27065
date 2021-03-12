using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Agent.SharedMailboxSentItemsRoutingAgent
{
	// Token: 0x02000004 RID: 4
	internal interface ISentItemWrapperCreator
	{
		// Token: 0x06000004 RID: 4
		Exception CreateAndSubmit(MailItem item, int traceId);
	}
}
