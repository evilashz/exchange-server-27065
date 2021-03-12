using System;
using System.ServiceModel;
using Microsoft.Exchange.InfoWorker.Availability;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B86 RID: 2950
	[MessageContract(IsWrapped = false)]
	public class GetUserAvailabilitySoapRequest : BaseSoapRequest
	{
		// Token: 0x04002EDB RID: 11995
		[MessageBodyMember(Name = "GetUserAvailabilityRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUserAvailabilityRequest Body;
	}
}
