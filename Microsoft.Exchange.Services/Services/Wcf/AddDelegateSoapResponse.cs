using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CE7 RID: 3303
	[MessageContract(IsWrapped = false)]
	public class AddDelegateSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003079 RID: 12409
		[MessageBodyMember(Name = "AddDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddDelegateResponseMessage Body;
	}
}
