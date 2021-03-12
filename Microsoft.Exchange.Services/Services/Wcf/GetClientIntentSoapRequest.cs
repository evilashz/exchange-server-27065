using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D92 RID: 3474
	[MessageContract(IsWrapped = false)]
	public class GetClientIntentSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003124 RID: 12580
		[MessageBodyMember(Name = "GetClientIntent", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetClientIntentRequest Body;
	}
}
