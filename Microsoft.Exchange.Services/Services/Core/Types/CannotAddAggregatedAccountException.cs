using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200070F RID: 1807
	internal sealed class CannotAddAggregatedAccountException : ServicePermanentException
	{
		// Token: 0x06003721 RID: 14113 RVA: 0x000C54B0 File Offset: 0x000C36B0
		public CannotAddAggregatedAccountException(Enum messageId) : base(ResponseCodeType.ErrorCannotAddAggregatedAccount, messageId)
		{
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000C54BB File Offset: 0x000C36BB
		public CannotAddAggregatedAccountException(ResponseCodeType responseCodeType, Enum messageId) : base(responseCodeType, messageId)
		{
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000C54C5 File Offset: 0x000C36C5
		public CannotAddAggregatedAccountException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorCannotAddAggregatedAccount, messageId, innerException)
		{
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000C54D1 File Offset: 0x000C36D1
		public CannotAddAggregatedAccountException(ResponseCodeType responseCodeType, LocalizedString message) : base(responseCodeType, message)
		{
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06003725 RID: 14117 RVA: 0x000C54DB File Offset: 0x000C36DB
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}
