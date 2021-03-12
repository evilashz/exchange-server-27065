using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D30 RID: 3376
	[MessageContract(IsWrapped = false)]
	public class GetSearchableMailboxesSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030C2 RID: 12482
		[MessageBodyMember(Name = "GetSearchableMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetSearchableMailboxesRequest Body;
	}
}
