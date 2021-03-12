using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004E8 RID: 1256
	[XmlType("GetAppMarketplaceUrlResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetAppMarketplaceUrlResponseMessage : ResponseMessage
	{
		// Token: 0x060024A2 RID: 9378 RVA: 0x000A507C File Offset: 0x000A327C
		public GetAppMarketplaceUrlResponseMessage()
		{
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000A5084 File Offset: 0x000A3284
		internal GetAppMarketplaceUrlResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
