using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000049 RID: 73
	public sealed class TextMessagingRoutingAgentFactory : RoutingAgentFactory
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000A2FB File Offset: 0x000084FB
		public TextMessagingRoutingAgentFactory()
		{
			this.session = new MobileSession();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000A30E File Offset: 0x0000850E
		public override RoutingAgent CreateAgent(SmtpServer server)
		{
			return new TextMessagingRoutingAgent(this.session);
		}

		// Token: 0x04000104 RID: 260
		private MobileSession session;
	}
}
