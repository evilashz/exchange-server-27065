using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CDC RID: 3292
	[MessageContract(IsWrapped = false)]
	public class GetEventsSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400306C RID: 12396
		[MessageBodyMember(Name = "GetEvents", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetEventsRequest Body;
	}
}
