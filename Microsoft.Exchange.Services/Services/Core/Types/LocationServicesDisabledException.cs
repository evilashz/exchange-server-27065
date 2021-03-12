using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007E7 RID: 2023
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class LocationServicesDisabledException : ServicePermanentException
	{
		// Token: 0x06003B63 RID: 15203 RVA: 0x000D0082 File Offset: 0x000CE282
		public LocationServicesDisabledException() : base(ResponseCodeType.ErrorLocationServicesDisabled, (CoreResources.IDs)2451443999U)
		{
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06003B64 RID: 15204 RVA: 0x000D0099 File Offset: 0x000CE299
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
