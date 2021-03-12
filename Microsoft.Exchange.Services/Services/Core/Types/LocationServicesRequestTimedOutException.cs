using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007EA RID: 2026
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class LocationServicesRequestTimedOutException : ServicePermanentException
	{
		// Token: 0x06003B69 RID: 15209 RVA: 0x000D00D4 File Offset: 0x000CE2D4
		public LocationServicesRequestTimedOutException() : base(ResponseCodeType.ErrorLocationServicesRequestTimedOut, (CoreResources.IDs)4226485813U)
		{
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06003B6A RID: 15210 RVA: 0x000D00EB File Offset: 0x000CE2EB
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
