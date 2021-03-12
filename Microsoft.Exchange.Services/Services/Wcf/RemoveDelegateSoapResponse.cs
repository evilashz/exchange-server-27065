using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CEB RID: 3307
	[MessageContract(IsWrapped = false)]
	public class RemoveDelegateSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400307D RID: 12413
		[MessageBodyMember(Name = "RemoveDelegateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public RemoveDelegateResponseMessage Body;
	}
}
