using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D2F RID: 3375
	[MessageContract(IsWrapped = false)]
	public class FindMailboxStatisticsByKeywordsSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030C1 RID: 12481
		[MessageBodyMember(Name = "FindMailboxStatisticsByKeywordsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public FindMailboxStatisticsByKeywordsResponse Body;
	}
}
