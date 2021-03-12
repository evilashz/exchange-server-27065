using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D37 RID: 3383
	[MessageContract(IsWrapped = false)]
	public class SearchMailboxesSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030C9 RID: 12489
		[MessageBodyMember(Name = "SearchMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SearchMailboxesResponse Body;
	}
}
