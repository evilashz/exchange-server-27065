using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000898 RID: 2200
	internal sealed class SubscriptionDelegateAccessNotSupportedException : ServicePermanentException
	{
		// Token: 0x06003ED8 RID: 16088 RVA: 0x000D99AF File Offset: 0x000D7BAF
		public SubscriptionDelegateAccessNotSupportedException() : base((CoreResources.IDs)3640136739U)
		{
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06003ED9 RID: 16089 RVA: 0x000D99C1 File Offset: 0x000D7BC1
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
