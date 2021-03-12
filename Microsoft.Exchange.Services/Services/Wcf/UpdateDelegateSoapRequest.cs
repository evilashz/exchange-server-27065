using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CE8 RID: 3304
	[MessageContract(IsWrapped = false)]
	public class UpdateDelegateSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400307A RID: 12410
		[MessageBodyMember(Name = "UpdateDelegate", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateDelegateRequest Body;
	}
}
