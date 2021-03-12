using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000717 RID: 1815
	internal sealed class CannotGetAggregatedAccountException : ServicePermanentException
	{
		// Token: 0x06003739 RID: 14137 RVA: 0x000C55AF File Offset: 0x000C37AF
		public CannotGetAggregatedAccountException(Enum messageId) : base(ResponseCodeType.ErrorCannotGetAggregatedAccount, messageId)
		{
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000C55BA File Offset: 0x000C37BA
		public CannotGetAggregatedAccountException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorCannotGetAggregatedAccount, messageId, innerException)
		{
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x0600373B RID: 14139 RVA: 0x000C55C6 File Offset: 0x000C37C6
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}
