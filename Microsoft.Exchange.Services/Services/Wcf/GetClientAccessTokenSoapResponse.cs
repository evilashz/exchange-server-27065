using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CCF RID: 3279
	[MessageContract(IsWrapped = false)]
	public class GetClientAccessTokenSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400305F RID: 12383
		[MessageBodyMember(Name = "GetClientAccessTokenResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetClientAccessTokenResponse Body;
	}
}
