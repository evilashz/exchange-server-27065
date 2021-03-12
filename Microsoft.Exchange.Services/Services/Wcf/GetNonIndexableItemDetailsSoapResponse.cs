using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D41 RID: 3393
	[MessageContract(IsWrapped = false)]
	public class GetNonIndexableItemDetailsSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030D3 RID: 12499
		[MessageBodyMember(Name = "GetNonIndexableItemDetailsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetNonIndexableItemDetailsResponse Body;
	}
}
