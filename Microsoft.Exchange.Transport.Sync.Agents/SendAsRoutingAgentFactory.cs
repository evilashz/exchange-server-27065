using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.SendAs
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SendAsRoutingAgentFactory : RoutingAgentFactory
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002329 File Offset: 0x00000529
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new SendAsRoutingAgent(this.sendAsManager);
		}

		// Token: 0x04000004 RID: 4
		private SendAsManager sendAsManager = new SendAsManager();
	}
}
