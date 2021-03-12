using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CCE RID: 3278
	[MessageContract(IsWrapped = false)]
	public class GetClientAccessTokenSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400305E RID: 12382
		[MessageBodyMember(Name = "GetClientAccessToken", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetClientAccessTokenRequest Body;
	}
}
