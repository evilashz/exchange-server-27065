using System;
using System.ServiceModel;
using Microsoft.Exchange.InfoWorker.Availability;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B87 RID: 2951
	[MessageContract(IsWrapped = false)]
	public class GetUserAvailabilitySoapResponse : BaseSoapResponse
	{
		// Token: 0x04002EDC RID: 11996
		[MessageBodyMember(Name = "GetUserAvailabilityResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUserAvailabilityResponse Body;
	}
}
