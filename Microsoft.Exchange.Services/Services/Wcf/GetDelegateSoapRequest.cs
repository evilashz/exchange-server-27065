using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CE4 RID: 3300
	[MessageContract(IsWrapped = false)]
	public class GetDelegateSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003076 RID: 12406
		[MessageBodyMember(Name = "GetDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetDelegateRequest Body;
	}
}
