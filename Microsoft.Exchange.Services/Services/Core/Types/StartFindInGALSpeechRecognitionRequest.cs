using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000481 RID: 1153
	[XmlType("StartFindInGALSpeechRecognitionRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class StartFindInGALSpeechRecognitionRequest : BaseRequest
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x000A2D38 File Offset: 0x000A0F38
		// (set) Token: 0x06002235 RID: 8757 RVA: 0x000A2D40 File Offset: 0x000A0F40
		[XmlElement("Culture")]
		[DataMember(Name = "Culture", IsRequired = true)]
		public string Culture { get; set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x000A2D49 File Offset: 0x000A0F49
		// (set) Token: 0x06002237 RID: 8759 RVA: 0x000A2D51 File Offset: 0x000A0F51
		[XmlElement("TimeZone")]
		[DataMember(Name = "TimeZone", IsRequired = true)]
		public string TimeZone { get; set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x000A2D5A File Offset: 0x000A0F5A
		// (set) Token: 0x06002239 RID: 8761 RVA: 0x000A2D62 File Offset: 0x000A0F62
		[DataMember(Name = "UserObjectGuid", IsRequired = true)]
		[XmlElement("UserObjectGuid")]
		public Guid UserObjectGuid { get; set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x000A2D6B File Offset: 0x000A0F6B
		// (set) Token: 0x0600223B RID: 8763 RVA: 0x000A2D73 File Offset: 0x000A0F73
		[XmlElement("TenantGuid")]
		[DataMember(Name = "TenantGuid", IsRequired = true)]
		public Guid TenantGuid { get; set; }

		// Token: 0x0600223C RID: 8764 RVA: 0x000A2D7C File Offset: 0x000A0F7C
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new StartFindInGALSpeechRecognitionCommand(callContext, this);
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000A2D85 File Offset: 0x000A0F85
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000A2D88 File Offset: 0x000A0F88
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
