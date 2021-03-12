using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D3E RID: 3390
	[MessageContract(IsWrapped = false)]
	public class GetNonIndexableItemStatisticsSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030D0 RID: 12496
		[MessageBodyMember(Name = "GetNonIndexableItemStatistics", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetNonIndexableItemStatisticsRequest Body;
	}
}
