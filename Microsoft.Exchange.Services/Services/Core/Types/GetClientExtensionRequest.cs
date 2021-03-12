using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200042B RID: 1067
	[XmlType("GetClientExtensionRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetClientExtensionRequest : BaseRequest
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x000A0ACD File Offset: 0x0009ECCD
		// (set) Token: 0x06001F44 RID: 8004 RVA: 0x000A0AD5 File Offset: 0x0009ECD5
		[XmlArrayItem("String", IsNullable = false, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public string[] RequestedExtensionIds { get; set; }

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x000A0ADE File Offset: 0x0009ECDE
		// (set) Token: 0x06001F46 RID: 8006 RVA: 0x000A0AE6 File Offset: 0x0009ECE6
		[XmlElement]
		public ClientExtensionUserParameters UserParameters { get; set; }

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x000A0AEF File Offset: 0x0009ECEF
		// (set) Token: 0x06001F48 RID: 8008 RVA: 0x000A0AF7 File Offset: 0x0009ECF7
		[XmlElement]
		public bool IsDebug { get; set; }

		// Token: 0x06001F49 RID: 8009 RVA: 0x000A0B00 File Offset: 0x0009ED00
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetClientExtension(callContext, this);
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x000A0B09 File Offset: 0x0009ED09
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x000A0B0C File Offset: 0x0009ED0C
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
