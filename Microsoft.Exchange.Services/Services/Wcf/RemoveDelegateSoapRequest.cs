using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CEA RID: 3306
	[MessageContract(IsWrapped = false)]
	public class RemoveDelegateSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400307C RID: 12412
		[MessageBodyMember(Name = "RemoveDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public RemoveDelegateRequest Body;
	}
}
