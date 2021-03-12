using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D82 RID: 3458
	[MessageContract(IsWrapped = false)]
	public class GetUMCallSummarySoapRequest : BaseSoapRequest
	{
		// Token: 0x04003114 RID: 12564
		[MessageBodyMember(Name = "GetUMCallSummary", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUMCallSummaryRequest Body;
	}
}
