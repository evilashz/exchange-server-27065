using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004E6 RID: 1254
	[XmlType(TypeName = "GetAppManifestsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetAppManifestsResponse : ResponseMessage
	{
		// Token: 0x06002498 RID: 9368 RVA: 0x000A5020 File Offset: 0x000A3220
		public GetAppManifestsResponse()
		{
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000A5028 File Offset: 0x000A3228
		internal GetAppManifestsResponse(ServiceResultCode code, ServiceError error, XmlElement manifests) : base(code, error)
		{
			this.Manifests = manifests;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x000A5039 File Offset: 0x000A3239
		// (set) Token: 0x0600249B RID: 9371 RVA: 0x000A5041 File Offset: 0x000A3241
		[XmlAnyElement]
		public XmlNode Manifests { get; set; }

		// Token: 0x0600249C RID: 9372 RVA: 0x000A504A File Offset: 0x000A324A
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetAppManifestsResponseMessage;
		}
	}
}
