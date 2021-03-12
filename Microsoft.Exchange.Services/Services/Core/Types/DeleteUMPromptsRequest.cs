using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000412 RID: 1042
	[XmlType("DeleteUMPromptsRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class DeleteUMPromptsRequest : BaseRequest
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x0009F94C File Offset: 0x0009DB4C
		// (set) Token: 0x06001DD7 RID: 7639 RVA: 0x0009F954 File Offset: 0x0009DB54
		[XmlElement("ConfigurationObject")]
		[DataMember(Name = "ConfigurationObject", IsRequired = true, Order = 1)]
		public Guid ConfigurationObject { get; set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x0009F95D File Offset: 0x0009DB5D
		// (set) Token: 0x06001DD9 RID: 7641 RVA: 0x0009F965 File Offset: 0x0009DB65
		[DataMember(Name = "PromptNames", IsRequired = false, Order = 2)]
		[XmlArray(ElementName = "PromptNames", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem(ElementName = "String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		public string[] PromptNames { get; set; }

		// Token: 0x06001DDA RID: 7642 RVA: 0x0009F96E File Offset: 0x0009DB6E
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new DeleteUMPrompts(callContext, this);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x0009F977 File Offset: 0x0009DB77
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x0009F97A File Offset: 0x0009DB7A
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
