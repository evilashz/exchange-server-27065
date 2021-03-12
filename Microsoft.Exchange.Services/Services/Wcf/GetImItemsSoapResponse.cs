using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D6B RID: 3435
	[MessageContract(IsWrapped = false)]
	public class GetImItemsSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030FD RID: 12541
		[MessageBodyMember(Name = "GetImItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetImItemsResponseMessage Body;
	}
}
