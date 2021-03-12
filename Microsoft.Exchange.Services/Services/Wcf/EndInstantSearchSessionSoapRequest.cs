using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D34 RID: 3380
	[MessageContract(IsWrapped = false)]
	public class EndInstantSearchSessionSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030C6 RID: 12486
		[MessageBodyMember(Name = "EndInstantSearchSession", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public EndInstantSearchSessionRequest Body;
	}
}
