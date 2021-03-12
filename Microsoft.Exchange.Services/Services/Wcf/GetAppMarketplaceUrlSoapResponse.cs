using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D59 RID: 3417
	[MessageContract(IsWrapped = false)]
	public class GetAppMarketplaceUrlSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030EB RID: 12523
		[MessageBodyMember(Name = "GetAppMarketplaceUrlResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetAppMarketplaceUrlResponse Body;
	}
}
