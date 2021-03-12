using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D36 RID: 3382
	[MessageContract(IsWrapped = false)]
	public class SearchMailboxesSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030C8 RID: 12488
		[MessageBodyMember(Name = "SearchMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SearchMailboxesRequest Body;
	}
}
