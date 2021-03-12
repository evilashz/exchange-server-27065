using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007D5 RID: 2005
	internal sealed class InvalidSubscriptionException : ServicePermanentException
	{
		// Token: 0x06003B14 RID: 15124 RVA: 0x000CFBB7 File Offset: 0x000CDDB7
		public InvalidSubscriptionException() : base(CoreResources.IDs.ErrorInvalidSubscription)
		{
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x000CFBC9 File Offset: 0x000CDDC9
		public InvalidSubscriptionException(Exception innerException) : base(CoreResources.IDs.ErrorInvalidSubscription, innerException)
		{
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06003B16 RID: 15126 RVA: 0x000CFBDC File Offset: 0x000CDDDC
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
