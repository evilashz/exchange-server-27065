using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200071A RID: 1818
	internal sealed class CannotSetAggregatedAccountException : ServicePermanentException
	{
		// Token: 0x06003742 RID: 14146 RVA: 0x000C560F File Offset: 0x000C380F
		public CannotSetAggregatedAccountException(Enum messageId) : base(ResponseCodeType.ErrorCannotSetAggregatedAccount, messageId)
		{
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000C561A File Offset: 0x000C381A
		public CannotSetAggregatedAccountException(ResponseCodeType responseCodeType, Enum messageId) : base(responseCodeType, messageId)
		{
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000C5624 File Offset: 0x000C3824
		public CannotSetAggregatedAccountException(ResponseCodeType responseCodeType, LocalizedString message) : base(responseCodeType, message)
		{
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000C562E File Offset: 0x000C382E
		public CannotSetAggregatedAccountException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorCannotSetAggregatedAccount, messageId, innerException)
		{
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06003746 RID: 14150 RVA: 0x000C563A File Offset: 0x000C383A
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}
