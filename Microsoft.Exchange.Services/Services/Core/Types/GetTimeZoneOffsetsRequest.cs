using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000452 RID: 1106
	[XmlType(TypeName = "GetTimeZoneOffsetsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetTimeZoneOffsetsRequest : BaseRequest
	{
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x000A1C57 File Offset: 0x0009FE57
		// (set) Token: 0x06002080 RID: 8320 RVA: 0x000A1C5F File Offset: 0x0009FE5F
		[XmlElement("StartTime", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(IsRequired = true, Order = 1)]
		public string StartTime { get; set; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x000A1C68 File Offset: 0x0009FE68
		// (set) Token: 0x06002082 RID: 8322 RVA: 0x000A1C70 File Offset: 0x0009FE70
		[DataMember(IsRequired = true, Order = 2)]
		[XmlElement("EndTime", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string EndTime { get; set; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x000A1C79 File Offset: 0x0009FE79
		// (set) Token: 0x06002084 RID: 8324 RVA: 0x000A1C81 File Offset: 0x0009FE81
		[XmlElement("TimeZoneId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public string TimeZoneId { get; set; }

		// Token: 0x06002085 RID: 8325 RVA: 0x000A1C8A File Offset: 0x0009FE8A
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetTimeZoneOffsets(callContext, this);
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000A1C93 File Offset: 0x0009FE93
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000A1C96 File Offset: 0x0009FE96
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
