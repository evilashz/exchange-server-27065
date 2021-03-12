using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000855 RID: 2133
	internal class ProxyGroupSidLimitExceededException : ServicePermanentException
	{
		// Token: 0x06003D70 RID: 15728 RVA: 0x000D78AB File Offset: 0x000D5AAB
		public ProxyGroupSidLimitExceededException() : base(CoreResources.IDs.ErrorProxyGroupSidLimitExceeded)
		{
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06003D71 RID: 15729 RVA: 0x000D78BD File Offset: 0x000D5ABD
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
