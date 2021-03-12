using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D6A RID: 3434
	[MessageContract(IsWrapped = false)]
	public class GetImItemsSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030FC RID: 12540
		[MessageBodyMember(Name = "GetImItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetImItemsRequest Body;
	}
}
