using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D43 RID: 3395
	[MessageContract(IsWrapped = false)]
	public class GetPasswordExpirationDateSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030D5 RID: 12501
		[MessageBodyMember(Name = "GetPasswordExpirationDateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetPasswordExpirationDateResponse Body;
	}
}
