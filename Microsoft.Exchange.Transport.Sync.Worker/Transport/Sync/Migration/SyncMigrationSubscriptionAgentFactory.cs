using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Worker.Agents;

namespace Microsoft.Exchange.Transport.Sync.Migration
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SyncMigrationSubscriptionAgentFactory : SubscriptionAgentFactory
	{
		// Token: 0x06000207 RID: 519 RVA: 0x00009784 File Offset: 0x00007984
		public override SubscriptionAgent CreateAgent()
		{
			return new SyncMigrationSubscriptionAgent();
		}
	}
}
