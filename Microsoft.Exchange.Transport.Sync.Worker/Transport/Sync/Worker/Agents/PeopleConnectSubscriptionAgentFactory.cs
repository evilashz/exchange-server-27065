using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Worker.Agents
{
	// Token: 0x02000023 RID: 35
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PeopleConnectSubscriptionAgentFactory : SubscriptionAgentFactory
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x000091A9 File Offset: 0x000073A9
		public override SubscriptionAgent CreateAgent()
		{
			return new PeopleConnectSubscriptionAgent();
		}
	}
}
