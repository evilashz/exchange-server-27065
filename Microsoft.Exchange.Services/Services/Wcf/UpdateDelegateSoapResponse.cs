using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CE9 RID: 3305
	[MessageContract(IsWrapped = false)]
	public class UpdateDelegateSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400307B RID: 12411
		[MessageBodyMember(Name = "UpdateDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateDelegateResponseMessage Body;
	}
}
