using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000456 RID: 1110
	[XmlType("GetUMPromptNamesRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUMPromptNamesRequest : BaseRequest
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x000A1D7F File Offset: 0x0009FF7F
		// (set) Token: 0x060020A8 RID: 8360 RVA: 0x000A1D87 File Offset: 0x0009FF87
		[XmlElement("ConfigurationObject")]
		[DataMember(Name = "ConfigurationObject", IsRequired = true, Order = 1)]
		public Guid ConfigurationObject { get; set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x000A1D90 File Offset: 0x0009FF90
		// (set) Token: 0x060020AA RID: 8362 RVA: 0x000A1D98 File Offset: 0x0009FF98
		[DataMember(Name = "HoursElapsedSinceLastModified", IsRequired = true, Order = 2)]
		[XmlElement("HoursElapsedSinceLastModified")]
		public int HoursElapsedSinceLastModified { get; set; }

		// Token: 0x060020AB RID: 8363 RVA: 0x000A1DA1 File Offset: 0x0009FFA1
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetUMPromptNames(callContext, this);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x000A1DAA File Offset: 0x0009FFAA
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x000A1DAD File Offset: 0x0009FFAD
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
