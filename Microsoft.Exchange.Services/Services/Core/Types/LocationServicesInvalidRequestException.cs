using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007E8 RID: 2024
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class LocationServicesInvalidRequestException : ServicePermanentException
	{
		// Token: 0x06003B65 RID: 15205 RVA: 0x000D00A0 File Offset: 0x000CE2A0
		public LocationServicesInvalidRequestException(Enum messageId) : base(ResponseCodeType.ErrorLocationServicesInvalidRequest, messageId)
		{
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06003B66 RID: 15206 RVA: 0x000D00AE File Offset: 0x000CE2AE
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
