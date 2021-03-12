using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007D6 RID: 2006
	internal sealed class InvalidSubscriptionRequestException : ServicePermanentException
	{
		// Token: 0x06003B17 RID: 15127 RVA: 0x000CFBE3 File Offset: 0x000CDDE3
		public InvalidSubscriptionRequestException() : base((CoreResources.IDs)3647226175U)
		{
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x000CFBF5 File Offset: 0x000CDDF5
		public InvalidSubscriptionRequestException(Enum messageId) : base(ResponseCodeType.ErrorInvalidSubscriptionRequest, messageId)
		{
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06003B19 RID: 15129 RVA: 0x000CFC03 File Offset: 0x000CDE03
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
