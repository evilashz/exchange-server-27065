using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004E7 RID: 1255
	[XmlType("GetAppMarketplaceUrlResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetAppMarketplaceUrlResponse : ResponseMessage
	{
		// Token: 0x0600249D RID: 9373 RVA: 0x000A504E File Offset: 0x000A324E
		public GetAppMarketplaceUrlResponse()
		{
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000A5056 File Offset: 0x000A3256
		internal GetAppMarketplaceUrlResponse(ServiceResultCode code, ServiceError error, string appMarketplaceUrl) : base(code, error)
		{
			this.AppMarketplaceUrl = appMarketplaceUrl;
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600249F RID: 9375 RVA: 0x000A5067 File Offset: 0x000A3267
		// (set) Token: 0x060024A0 RID: 9376 RVA: 0x000A506F File Offset: 0x000A326F
		[IgnoreDataMember]
		public string AppMarketplaceUrl { get; set; }

		// Token: 0x060024A1 RID: 9377 RVA: 0x000A5078 File Offset: 0x000A3278
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetAppMarketplaceUrlResponseMessage;
		}
	}
}
