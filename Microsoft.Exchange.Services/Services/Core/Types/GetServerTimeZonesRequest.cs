using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200044D RID: 1101
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetServerTimeZonesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetServerTimeZonesRequest : BaseRequest
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x000A1A12 File Offset: 0x0009FC12
		// (set) Token: 0x0600204D RID: 8269 RVA: 0x000A1A1A File Offset: 0x0009FC1A
		[XmlAttribute("ReturnFullTimeZoneData")]
		[DataMember(Name = "ReturnFullTimeZoneData", IsRequired = false, Order = 1)]
		public bool ReturnFullTimeZoneData
		{
			get
			{
				return this.returnFullTimeZoneData;
			}
			set
			{
				this.returnFullTimeZoneData = value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x000A1A23 File Offset: 0x0009FC23
		// (set) Token: 0x0600204F RID: 8271 RVA: 0x000A1A2B File Offset: 0x0009FC2B
		[XmlArray(ElementName = "Ids", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "Ids", IsRequired = false, Order = 2)]
		[XmlArrayItem("Id", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		public string[] Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000A1A34 File Offset: 0x0009FC34
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetServerTimeZones(callContext, this);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000A1A3D File Offset: 0x0009FC3D
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x000A1A40 File Offset: 0x0009FC40
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x0400143C RID: 5180
		private bool returnFullTimeZoneData = true;

		// Token: 0x0400143D RID: 5181
		private string[] id;
	}
}
