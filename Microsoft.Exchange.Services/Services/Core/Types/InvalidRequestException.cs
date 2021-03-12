using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007CC RID: 1996
	internal sealed class InvalidRequestException : ServicePermanentException
	{
		// Token: 0x06003AF9 RID: 15097 RVA: 0x000CFA5F File Offset: 0x000CDC5F
		public InvalidRequestException() : base((CoreResources.IDs)3784063568U)
		{
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x000CFA71 File Offset: 0x000CDC71
		public InvalidRequestException(Exception innerException) : base((CoreResources.IDs)3784063568U, innerException)
		{
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x000CFA84 File Offset: 0x000CDC84
		public InvalidRequestException(Enum messageId) : base(ResponseCodeType.ErrorInvalidRequest, messageId)
		{
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x000CFA92 File Offset: 0x000CDC92
		public InvalidRequestException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorInvalidRequest, messageId, innerException)
		{
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x000CFAA1 File Offset: 0x000CDCA1
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
