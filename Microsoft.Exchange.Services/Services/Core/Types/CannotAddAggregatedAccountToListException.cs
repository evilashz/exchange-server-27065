using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000710 RID: 1808
	internal sealed class CannotAddAggregatedAccountToListException : ServicePermanentException
	{
		// Token: 0x06003726 RID: 14118 RVA: 0x000C54E2 File Offset: 0x000C36E2
		public CannotAddAggregatedAccountToListException(Enum messageId) : base(ResponseCodeType.ErrorCannotAddAggregatedAccountToList, messageId)
		{
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000C54ED File Offset: 0x000C36ED
		public CannotAddAggregatedAccountToListException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorCannotAddAggregatedAccountToList, messageId, innerException)
		{
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06003728 RID: 14120 RVA: 0x000C54F9 File Offset: 0x000C36F9
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}
