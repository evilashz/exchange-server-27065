using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007CA RID: 1994
	internal sealed class InvalidPushSubscriptionUrlException : ServicePermanentException
	{
		// Token: 0x06003AF4 RID: 15092 RVA: 0x000CFA1A File Offset: 0x000CDC1A
		public InvalidPushSubscriptionUrlException() : base(CoreResources.IDs.ErrorInvalidPushSubscriptionUrl)
		{
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x000CFA2C File Offset: 0x000CDC2C
		public InvalidPushSubscriptionUrlException(Exception innerException) : base(CoreResources.IDs.ErrorInvalidPushSubscriptionUrl, innerException)
		{
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x000CFA3F File Offset: 0x000CDC3F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
