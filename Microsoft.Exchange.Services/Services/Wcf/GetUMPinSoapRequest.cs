using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D8E RID: 3470
	[MessageContract(IsWrapped = false)]
	public class GetUMPinSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003120 RID: 12576
		[MessageBodyMember(Name = "GetUMPin", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUMPinRequest Body;
	}
}
