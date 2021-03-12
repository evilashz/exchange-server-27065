using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000427 RID: 1063
	[XmlType("GetAppMarketplaceUrlRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetAppMarketplaceUrlRequest : BaseRequest
	{
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x000A095E File Offset: 0x0009EB5E
		// (set) Token: 0x06001F20 RID: 7968 RVA: 0x000A0966 File Offset: 0x0009EB66
		[XmlElement(ElementName = "ApiVersionSupported")]
		public string ApiVersionSupported { get; set; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x000A096F File Offset: 0x0009EB6F
		// (set) Token: 0x06001F22 RID: 7970 RVA: 0x000A0977 File Offset: 0x0009EB77
		[XmlElement(ElementName = "SchemaVersionSupported")]
		public string SchemaVersionSupported { get; set; }

		// Token: 0x06001F23 RID: 7971 RVA: 0x000A0980 File Offset: 0x0009EB80
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetAppMarketplaceUrl(callContext, this);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x000A0989 File Offset: 0x0009EB89
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x000A098C File Offset: 0x0009EB8C
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
