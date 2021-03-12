using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007E9 RID: 2025
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class LocationServicesRequestFailedException : ServicePermanentException
	{
		// Token: 0x06003B67 RID: 15207 RVA: 0x000D00B5 File Offset: 0x000CE2B5
		public LocationServicesRequestFailedException(Exception innerException) : base(ResponseCodeType.ErrorLocationServicesRequestFailed, (CoreResources.IDs)2653243941U, innerException)
		{
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06003B68 RID: 15208 RVA: 0x000D00CD File Offset: 0x000CE2CD
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
