using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200089B RID: 2203
	internal sealed class SubscriptionNotFoundException : ServicePermanentException
	{
		// Token: 0x06003EE7 RID: 16103 RVA: 0x000D9D60 File Offset: 0x000D7F60
		public SubscriptionNotFoundException() : base((CoreResources.IDs)2884324330U)
		{
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06003EE8 RID: 16104 RVA: 0x000D9D72 File Offset: 0x000D7F72
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
