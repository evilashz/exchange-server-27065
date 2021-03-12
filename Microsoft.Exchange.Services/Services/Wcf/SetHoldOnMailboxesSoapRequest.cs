using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D3C RID: 3388
	[MessageContract(IsWrapped = false)]
	public class SetHoldOnMailboxesSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030CE RID: 12494
		[MessageBodyMember(Name = "SetHoldOnMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetHoldOnMailboxesRequest Body;
	}
}
