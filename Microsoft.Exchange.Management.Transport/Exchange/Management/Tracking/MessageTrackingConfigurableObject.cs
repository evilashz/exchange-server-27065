using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x020000A0 RID: 160
	[Serializable]
	public abstract class MessageTrackingConfigurableObject : ConfigurableObject
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00016389 File Offset: 0x00014589
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00016390 File Offset: 0x00014590
		public MessageTrackingConfigurableObject() : base(new SimpleProviderPropertyBag())
		{
		}
	}
}
