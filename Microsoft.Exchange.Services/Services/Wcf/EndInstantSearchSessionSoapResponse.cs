using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D35 RID: 3381
	[MessageContract(IsWrapped = false)]
	public class EndInstantSearchSessionSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030C7 RID: 12487
		[MessageBodyMember(Name = "EndInstantSearchSessionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public EndInstantSearchSessionResponse Body;
	}
}
