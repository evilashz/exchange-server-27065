using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000861 RID: 2145
	internal sealed class RequestStreamTooBigException : ServicePermanentException
	{
		// Token: 0x06003DA5 RID: 15781 RVA: 0x000D7B03 File Offset: 0x000D5D03
		public RequestStreamTooBigException() : base(ResponseCodeType.ErrorRequestStreamTooBig, CoreResources.IDs.ErrorInvalidRequestQuotaExceeded)
		{
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x000D7B1A File Offset: 0x000D5D1A
		public RequestStreamTooBigException(Exception innerException) : base(ResponseCodeType.ErrorRequestStreamTooBig, CoreResources.IDs.ErrorInvalidRequestQuotaExceeded, innerException)
		{
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x000D7B32 File Offset: 0x000D5D32
		public RequestStreamTooBigException(Enum messageId) : base(ResponseCodeType.ErrorRequestStreamTooBig, messageId)
		{
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x000D7B40 File Offset: 0x000D5D40
		public RequestStreamTooBigException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorRequestStreamTooBig, messageId, innerException)
		{
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x000D7B4F File Offset: 0x000D5D4F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
