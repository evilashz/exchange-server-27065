using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000767 RID: 1895
	internal sealed class ExchangeApplicationNotEnabledException : ServicePermanentException
	{
		// Token: 0x06003885 RID: 14469 RVA: 0x000C78E9 File Offset: 0x000C5AE9
		public ExchangeApplicationNotEnabledException(Enum messageId) : base(ResponseCodeType.ErrorExchangeApplicationNotEnabled, messageId)
		{
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000C78F4 File Offset: 0x000C5AF4
		public ExchangeApplicationNotEnabledException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorExchangeApplicationNotEnabled, messageId, innerException)
		{
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06003887 RID: 14471 RVA: 0x000C7900 File Offset: 0x000C5B00
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}
