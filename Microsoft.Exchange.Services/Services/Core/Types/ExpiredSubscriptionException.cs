using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000772 RID: 1906
	internal sealed class ExpiredSubscriptionException : ServicePermanentException
	{
		// Token: 0x060038E2 RID: 14562 RVA: 0x000C928A File Offset: 0x000C748A
		public ExpiredSubscriptionException() : base((CoreResources.IDs)3329761676U)
		{
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x060038E3 RID: 14563 RVA: 0x000C929C File Offset: 0x000C749C
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
