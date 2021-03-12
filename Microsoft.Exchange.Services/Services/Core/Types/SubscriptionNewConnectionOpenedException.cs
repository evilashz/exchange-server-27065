using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200089A RID: 2202
	internal sealed class SubscriptionNewConnectionOpenedException : ServicePermanentException
	{
		// Token: 0x06003EE5 RID: 16101 RVA: 0x000D9D42 File Offset: 0x000D7F42
		public SubscriptionNewConnectionOpenedException() : base(ResponseCodeType.ErrorNewEventStreamConnectionOpened, (CoreResources.IDs)2943900075U)
		{
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06003EE6 RID: 16102 RVA: 0x000D9D59 File Offset: 0x000D7F59
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010SP1;
			}
		}
	}
}
