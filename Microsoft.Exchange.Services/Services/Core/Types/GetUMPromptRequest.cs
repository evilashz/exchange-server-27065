using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000457 RID: 1111
	[XmlType("GetUMPromptRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUMPromptRequest : BaseRequest
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060020AF RID: 8367 RVA: 0x000A1DB8 File Offset: 0x0009FFB8
		// (set) Token: 0x060020B0 RID: 8368 RVA: 0x000A1DC0 File Offset: 0x0009FFC0
		[XmlElement("ConfigurationObject")]
		[DataMember(Name = "ConfigurationObject", IsRequired = true, Order = 1)]
		public Guid ConfigurationObject { get; set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060020B1 RID: 8369 RVA: 0x000A1DC9 File Offset: 0x0009FFC9
		// (set) Token: 0x060020B2 RID: 8370 RVA: 0x000A1DD1 File Offset: 0x0009FFD1
		[XmlElement("PromptName")]
		[DataMember(Name = "PromptName", IsRequired = true, Order = 2)]
		public string PromptName { get; set; }

		// Token: 0x060020B3 RID: 8371 RVA: 0x000A1DDA File Offset: 0x0009FFDA
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetUMPrompt(callContext, this);
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000A1DE3 File Offset: 0x0009FFE3
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000A1DE6 File Offset: 0x0009FFE6
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
