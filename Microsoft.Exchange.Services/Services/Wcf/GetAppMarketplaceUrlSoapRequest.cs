using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D58 RID: 3416
	[MessageContract(IsWrapped = false)]
	public class GetAppMarketplaceUrlSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030EA RID: 12522
		[MessageBodyMember(Name = "GetAppMarketplaceUrl", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetAppMarketplaceUrlRequest Body;
	}
}
