using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Delivery;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000047 RID: 71
	public class TextMessagingDeliveryAgentFactory : DeliveryAgentFactory<TextMessagingDeliveryAgentManager>
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x0000A20D File Offset: 0x0000840D
		public TextMessagingDeliveryAgentFactory()
		{
			this.session = new MobileSession();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000A220 File Offset: 0x00008420
		public override DeliveryAgent CreateAgent(SmtpServer server)
		{
			return new TextMessagingDeliveryAgent(this.session);
		}

		// Token: 0x04000102 RID: 258
		private MobileSession session;
	}
}
