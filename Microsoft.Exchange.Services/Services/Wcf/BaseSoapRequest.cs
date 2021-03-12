using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B6D RID: 2925
	[MessageContract(IsWrapped = false)]
	[Serializable]
	public abstract class BaseSoapRequest
	{
		// Token: 0x04002E17 RID: 11799
		[MessageHeader(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public MessageHeader<ExchangeImpersonationType> ExchangeImpersonation;
	}
}
