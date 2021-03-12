using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D42 RID: 3394
	[MessageContract(IsWrapped = false)]
	public class GetPasswordExpirationDateSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030D4 RID: 12500
		[MessageBodyMember(Name = "GetPasswordExpirationDate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetPasswordExpirationDateRequest Body;
	}
}
