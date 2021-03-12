using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D3A RID: 3386
	[MessageContract(IsWrapped = false)]
	public class GetHoldOnMailboxesSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030CC RID: 12492
		[MessageBodyMember(Name = "GetHoldOnMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetHoldOnMailboxesRequest Body;
	}
}
