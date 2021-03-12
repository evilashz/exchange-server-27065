using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D31 RID: 3377
	[MessageContract(IsWrapped = false)]
	public class GetSearchableMailboxesSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030C3 RID: 12483
		[MessageBodyMember(Name = "GetSearchableMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetSearchableMailboxesResponse Body;
	}
}
