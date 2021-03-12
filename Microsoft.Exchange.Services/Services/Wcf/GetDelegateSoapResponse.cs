using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CE5 RID: 3301
	[MessageContract(IsWrapped = false)]
	public class GetDelegateSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003077 RID: 12407
		[MessageBodyMember(Name = "GetDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetDelegateResponseMessage Body;
	}
}
