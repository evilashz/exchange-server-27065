using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Worker.Agents
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SubscriptionAgentFactory
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00008825 File Offset: 0x00006A25
		public virtual void Close()
		{
		}

		// Token: 0x060001C6 RID: 454
		public abstract SubscriptionAgent CreateAgent();
	}
}
