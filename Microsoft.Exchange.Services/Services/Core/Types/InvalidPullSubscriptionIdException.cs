using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007C9 RID: 1993
	internal sealed class InvalidPullSubscriptionIdException : ServicePermanentException
	{
		// Token: 0x06003AF2 RID: 15090 RVA: 0x000CFA01 File Offset: 0x000CDC01
		public InvalidPullSubscriptionIdException() : base(CoreResources.IDs.ErrorInvalidPullSubscriptionId)
		{
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x000CFA13 File Offset: 0x000CDC13
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
