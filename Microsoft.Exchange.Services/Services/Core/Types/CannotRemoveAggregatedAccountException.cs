using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000719 RID: 1817
	internal sealed class CannotRemoveAggregatedAccountException : ServicePermanentException
	{
		// Token: 0x0600373E RID: 14142 RVA: 0x000C55E7 File Offset: 0x000C37E7
		public CannotRemoveAggregatedAccountException(Enum messageId) : base(ResponseCodeType.ErrorCannotRemoveAggregatedAccount, messageId)
		{
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000C55F2 File Offset: 0x000C37F2
		public CannotRemoveAggregatedAccountException(ResponseCodeType responseCode, Enum messageId) : base(responseCode, messageId)
		{
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000C55FC File Offset: 0x000C37FC
		public CannotRemoveAggregatedAccountException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorCannotRemoveAggregatedAccount, messageId, innerException)
		{
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x000C5608 File Offset: 0x000C3808
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}
