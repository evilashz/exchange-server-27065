using System;
using Microsoft.Exchange.Data.Transport.Delivery;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000046 RID: 70
	public class TextMessagingDeliveryAgentManager : DeliveryAgentManager
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000A1FE File Offset: 0x000083FE
		public override string SupportedDeliveryProtocol
		{
			get
			{
				return "MOBILE";
			}
		}
	}
}
