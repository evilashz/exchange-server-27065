using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000897 RID: 2199
	internal sealed class SubscriptionAccessDeniedException : ServicePermanentException
	{
		// Token: 0x06003ED6 RID: 16086 RVA: 0x000D9996 File Offset: 0x000D7B96
		public SubscriptionAccessDeniedException() : base((CoreResources.IDs)2662672540U)
		{
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06003ED7 RID: 16087 RVA: 0x000D99A8 File Offset: 0x000D7BA8
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
