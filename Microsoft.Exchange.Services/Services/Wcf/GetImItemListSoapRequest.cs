using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D68 RID: 3432
	[MessageContract(IsWrapped = false)]
	public class GetImItemListSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030FA RID: 12538
		[MessageBodyMember(Name = "GetImItemList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetImItemListRequest Body;
	}
}
