using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200047C RID: 1148
	[XmlType("SetEncryptionConfigurationRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SetEncryptionConfigurationRequest : BaseRequest
	{
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x000A2B59 File Offset: 0x000A0D59
		// (set) Token: 0x060021FD RID: 8701 RVA: 0x000A2B61 File Offset: 0x000A0D61
		[XmlElement(ElementName = "ImageBase64", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string ImageBase64 { get; set; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x000A2B6A File Offset: 0x000A0D6A
		// (set) Token: 0x060021FF RID: 8703 RVA: 0x000A2B72 File Offset: 0x000A0D72
		[XmlElement(ElementName = "EmailText", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string EmailText { get; set; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x000A2B7B File Offset: 0x000A0D7B
		// (set) Token: 0x06002201 RID: 8705 RVA: 0x000A2B83 File Offset: 0x000A0D83
		[XmlElement(ElementName = "PortalText", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string PortalText { get; set; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x000A2B8C File Offset: 0x000A0D8C
		// (set) Token: 0x06002203 RID: 8707 RVA: 0x000A2B94 File Offset: 0x000A0D94
		[XmlElement(ElementName = "DisclaimerText", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string DisclaimerText { get; set; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x000A2B9D File Offset: 0x000A0D9D
		// (set) Token: 0x06002205 RID: 8709 RVA: 0x000A2BA5 File Offset: 0x000A0DA5
		[XmlElement(ElementName = "OTPEnabled", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public bool OTPEnabled { get; set; }

		// Token: 0x06002206 RID: 8710 RVA: 0x000A2BAE File Offset: 0x000A0DAE
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SetEncryptionConfiguration(callContext, this);
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000A2BB7 File Offset: 0x000A0DB7
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000A2BBA File Offset: 0x000A0DBA
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
