using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000426 RID: 1062
	[XmlType("GetAppManifestsRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetAppManifestsRequest : BaseRequest
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x000A0925 File Offset: 0x0009EB25
		// (set) Token: 0x06001F18 RID: 7960 RVA: 0x000A092D File Offset: 0x0009EB2D
		[XmlElement(ElementName = "ApiVersionSupported")]
		public string ApiVersionSupported { get; set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x000A0936 File Offset: 0x0009EB36
		// (set) Token: 0x06001F1A RID: 7962 RVA: 0x000A093E File Offset: 0x0009EB3E
		[XmlElement(ElementName = "SchemaVersionSupported")]
		public string SchemaVersionSupported { get; set; }

		// Token: 0x06001F1B RID: 7963 RVA: 0x000A0947 File Offset: 0x0009EB47
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetAppManifests(callContext, this);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x000A0950 File Offset: 0x0009EB50
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x000A0953 File Offset: 0x0009EB53
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
