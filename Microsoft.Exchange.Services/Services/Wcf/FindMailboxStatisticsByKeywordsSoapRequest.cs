using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D2E RID: 3374
	[MessageContract(IsWrapped = false)]
	public class FindMailboxStatisticsByKeywordsSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030C0 RID: 12480
		[MessageBodyMember(Name = "FindMailboxStatisticsByKeywords", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public FindMailboxStatisticsByKeywordsRequest Body;
	}
}
