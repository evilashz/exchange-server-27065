using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200089D RID: 2205
	internal sealed class SubscriptionUnsubscribedException : ServicePermanentException
	{
		// Token: 0x06003EE9 RID: 16105 RVA: 0x000D9D79 File Offset: 0x000D7F79
		public SubscriptionUnsubscribedException() : base(CoreResources.IDs.ErrorSubscriptionUnsubscribed)
		{
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06003EEA RID: 16106 RVA: 0x000D9D8B File Offset: 0x000D7F8B
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010SP1;
			}
		}
	}
}
