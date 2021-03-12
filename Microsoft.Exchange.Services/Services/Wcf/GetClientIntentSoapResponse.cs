using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D93 RID: 3475
	[MessageContract(IsWrapped = false)]
	public class GetClientIntentSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003125 RID: 12581
		[MessageBodyMember(Name = "GetClientIntentResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetClientIntentResponseMessage Body;
	}
}
