using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000856 RID: 2134
	internal class ProxyTokenExpiredException : ServicePermanentException
	{
		// Token: 0x06003D72 RID: 15730 RVA: 0x000D78C4 File Offset: 0x000D5AC4
		public ProxyTokenExpiredException() : base((CoreResources.IDs)3699987394U)
		{
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06003D73 RID: 15731 RVA: 0x000D78D6 File Offset: 0x000D5AD6
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
