using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CDA RID: 3290
	[MessageContract(IsWrapped = false)]
	public class UnsubscribeSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400306A RID: 12394
		[MessageBodyMember(Name = "Unsubscribe", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UnsubscribeRequest Body;
	}
}
