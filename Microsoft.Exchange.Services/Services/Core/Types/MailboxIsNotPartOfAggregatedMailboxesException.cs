using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007F2 RID: 2034
	internal sealed class MailboxIsNotPartOfAggregatedMailboxesException : ServicePermanentException
	{
		// Token: 0x06003B84 RID: 15236 RVA: 0x000D020F File Offset: 0x000CE40F
		public MailboxIsNotPartOfAggregatedMailboxesException(Enum messageId) : base(ResponseCodeType.ErrorMailboxIsNotPartOfAggregatedMailboxes, messageId)
		{
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x000D021D File Offset: 0x000CE41D
		public MailboxIsNotPartOfAggregatedMailboxesException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorMailboxIsNotPartOfAggregatedMailboxes, messageId, innerException)
		{
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x000D022C File Offset: 0x000CE42C
		public MailboxIsNotPartOfAggregatedMailboxesException(ResponseCodeType responseCodeType, LocalizedString message) : base(responseCodeType, message)
		{
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06003B87 RID: 15239 RVA: 0x000D0236 File Offset: 0x000CE436
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}
