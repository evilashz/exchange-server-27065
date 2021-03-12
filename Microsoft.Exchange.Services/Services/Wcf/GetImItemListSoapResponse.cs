using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D69 RID: 3433
	[MessageContract(IsWrapped = false)]
	public class GetImItemListSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030FB RID: 12539
		[MessageBodyMember(Name = "GetImItemListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetImItemListResponseMessage Body;
	}
}
