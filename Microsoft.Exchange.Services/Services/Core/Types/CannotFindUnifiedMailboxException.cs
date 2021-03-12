using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000715 RID: 1813
	internal sealed class CannotFindUnifiedMailboxException : ServicePermanentException
	{
		// Token: 0x06003732 RID: 14130 RVA: 0x000C5569 File Offset: 0x000C3769
		public CannotFindUnifiedMailboxException(Enum messageId) : base(ResponseCodeType.ErrorCannotFindUnifiedMailbox, messageId)
		{
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x000C5574 File Offset: 0x000C3774
		public CannotFindUnifiedMailboxException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorCannotFindUnifiedMailbox, messageId, innerException)
		{
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06003734 RID: 14132 RVA: 0x000C5580 File Offset: 0x000C3780
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}
