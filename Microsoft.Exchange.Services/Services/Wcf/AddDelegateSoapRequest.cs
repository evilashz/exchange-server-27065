using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CE6 RID: 3302
	[MessageContract(IsWrapped = false)]
	public class AddDelegateSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003078 RID: 12408
		[MessageBodyMember(Name = "AddDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddDelegateRequest Body;
	}
}
