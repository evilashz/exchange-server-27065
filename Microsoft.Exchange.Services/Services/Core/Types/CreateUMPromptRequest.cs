using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200040C RID: 1036
	[XmlType("CreateUMPromptRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateUMPromptRequest : BaseRequest
	{
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x0009F4D2 File Offset: 0x0009D6D2
		// (set) Token: 0x06001D95 RID: 7573 RVA: 0x0009F4DA File Offset: 0x0009D6DA
		[XmlElement("ConfigurationObject")]
		[DataMember(Name = "ConfigurationObject", IsRequired = true, Order = 1)]
		public Guid ConfigurationObject { get; set; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x0009F4E3 File Offset: 0x0009D6E3
		// (set) Token: 0x06001D97 RID: 7575 RVA: 0x0009F4EB File Offset: 0x0009D6EB
		[DataMember(Name = "PromptName", IsRequired = true, Order = 2)]
		[XmlElement("PromptName")]
		public string PromptName { get; set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x0009F4F4 File Offset: 0x0009D6F4
		// (set) Token: 0x06001D99 RID: 7577 RVA: 0x0009F4FC File Offset: 0x0009D6FC
		[XmlElement(ElementName = "AudioData")]
		[DataMember(Name = "AudioData", IsRequired = true, Order = 3)]
		public string AudioData { get; set; }

		// Token: 0x06001D9A RID: 7578 RVA: 0x0009F505 File Offset: 0x0009D705
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new CreateUMPrompt(callContext, this);
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0009F50E File Offset: 0x0009D70E
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0009F511 File Offset: 0x0009D711
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
