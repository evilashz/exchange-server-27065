using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D3F RID: 3391
	[MessageContract(IsWrapped = false)]
	public class GetNonIndexableItemStatisticsSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030D1 RID: 12497
		[MessageBodyMember(Name = "GetNonIndexableItemStatisticsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetNonIndexableItemStatisticsResponse Body;
	}
}
