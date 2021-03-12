using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000853 RID: 2131
	internal class ProxiedSubscriptionCallFailureException : ServicePermanentException
	{
		// Token: 0x06003D6C RID: 15724 RVA: 0x000D7879 File Offset: 0x000D5A79
		public ProxiedSubscriptionCallFailureException() : base(CoreResources.IDs.ErrorProxiedSubscriptionCallFailure)
		{
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06003D6D RID: 15725 RVA: 0x000D788B File Offset: 0x000D5A8B
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
