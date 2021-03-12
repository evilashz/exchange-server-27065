using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CD8 RID: 3288
	[MessageContract(IsWrapped = false)]
	public class SubscribeSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003068 RID: 12392
		[MessageBodyMember(Name = "Subscribe", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SubscribeRequest Body;
	}
}
