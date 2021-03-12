using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000453 RID: 1107
	[XmlType("GetUMCallDataRecordsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUMCallDataRecordsRequest : BaseRequest
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x000A1CA1 File Offset: 0x0009FEA1
		// (set) Token: 0x0600208A RID: 8330 RVA: 0x000A1CA9 File Offset: 0x0009FEA9
		[DataMember(Name = "StartDateTime")]
		[XmlElement("StartDateTime")]
		public DateTime StartDateTime { get; set; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x000A1CB2 File Offset: 0x0009FEB2
		// (set) Token: 0x0600208C RID: 8332 RVA: 0x000A1CBA File Offset: 0x0009FEBA
		[XmlElement("EndDateTime")]
		[DataMember(Name = "EndDateTime")]
		public DateTime EndDateTime { get; set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x000A1CC3 File Offset: 0x0009FEC3
		// (set) Token: 0x0600208E RID: 8334 RVA: 0x000A1CCB File Offset: 0x0009FECB
		[DataMember(Name = "Offset")]
		[XmlElement("Offset")]
		public int Offset { get; set; }

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x000A1CD4 File Offset: 0x0009FED4
		// (set) Token: 0x06002090 RID: 8336 RVA: 0x000A1CDC File Offset: 0x0009FEDC
		[DataMember(Name = "NumberOfRecords")]
		[XmlElement("NumberOfRecords")]
		public int NumberOfRecords { get; set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x000A1CE5 File Offset: 0x0009FEE5
		// (set) Token: 0x06002092 RID: 8338 RVA: 0x000A1CED File Offset: 0x0009FEED
		[DataMember(Name = "UserLegacyExchangeDN")]
		[XmlElement("UserLegacyExchangeDN")]
		public string UserLegacyExchangeDN { get; set; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x000A1CF6 File Offset: 0x0009FEF6
		// (set) Token: 0x06002094 RID: 8340 RVA: 0x000A1CFE File Offset: 0x0009FEFE
		[XmlElement("FilterBy")]
		[DataMember(Name = "FilterBy")]
		public UMCDRFilterByType FilterBy { get; set; }

		// Token: 0x06002095 RID: 8341 RVA: 0x000A1D07 File Offset: 0x0009FF07
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetUMCallDataRecords(callContext, this);
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000A1D10 File Offset: 0x0009FF10
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x000A1D13 File Offset: 0x0009FF13
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
