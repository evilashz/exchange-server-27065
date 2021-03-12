using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200082A RID: 2090
	internal sealed class NotApplicableOutsideOfDatacenterException : ServicePermanentException
	{
		// Token: 0x06003C6C RID: 15468 RVA: 0x000D5CD6 File Offset: 0x000D3ED6
		public NotApplicableOutsideOfDatacenterException(Enum messageId) : base(ResponseCodeType.ErrorNotApplicableOutsideOfDatacenter, messageId)
		{
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x000D5CE4 File Offset: 0x000D3EE4
		public NotApplicableOutsideOfDatacenterException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorNotApplicableOutsideOfDatacenter, messageId, innerException)
		{
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06003C6E RID: 15470 RVA: 0x000D5CF3 File Offset: 0x000D3EF3
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}
