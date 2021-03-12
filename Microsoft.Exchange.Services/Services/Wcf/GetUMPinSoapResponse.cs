using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D8F RID: 3471
	[MessageContract(IsWrapped = false)]
	public class GetUMPinSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003121 RID: 12577
		[MessageBodyMember(Name = "GetUMPinResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUMPinResponseMessage Body;
	}
}
