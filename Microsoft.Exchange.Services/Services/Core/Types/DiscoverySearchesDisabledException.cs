using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000751 RID: 1873
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DiscoverySearchesDisabledException : ServicePermanentException
	{
		// Token: 0x06003833 RID: 14387 RVA: 0x000C6FFC File Offset: 0x000C51FC
		public DiscoverySearchesDisabledException() : base(ResponseCodeType.ErrorDiscoverySearchesDisabled, CoreResources.IDs.ErrorDiscoverySearchesDisabled)
		{
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06003834 RID: 14388 RVA: 0x000C7013 File Offset: 0x000C5213
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
