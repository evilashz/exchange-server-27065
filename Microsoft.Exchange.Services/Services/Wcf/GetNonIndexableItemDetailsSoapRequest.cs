using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D40 RID: 3392
	[MessageContract(IsWrapped = false)]
	public class GetNonIndexableItemDetailsSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030D2 RID: 12498
		[MessageBodyMember(Name = "GetNonIndexableItemDetails", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetNonIndexableItemDetailsRequest Body;
	}
}
