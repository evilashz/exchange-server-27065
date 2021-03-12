using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000454 RID: 1108
	[XmlType("GetUMCallSummaryType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUMCallSummaryRequest : BaseRequest
	{
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x000A1D1E File Offset: 0x0009FF1E
		// (set) Token: 0x0600209A RID: 8346 RVA: 0x000A1D26 File Offset: 0x0009FF26
		[XmlElement("DailPlanGuid")]
		[DataMember(Name = "DailPlanGuid", IsRequired = true)]
		public Guid DailPlanGuid { get; set; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x000A1D2F File Offset: 0x0009FF2F
		// (set) Token: 0x0600209C RID: 8348 RVA: 0x000A1D37 File Offset: 0x0009FF37
		[XmlElement("GatewayGuid")]
		[DataMember(Name = "GatewayGuid", IsRequired = true)]
		public Guid GatewayGuid { get; set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x000A1D40 File Offset: 0x0009FF40
		// (set) Token: 0x0600209E RID: 8350 RVA: 0x000A1D48 File Offset: 0x0009FF48
		[XmlElement("GroupRecordsBy")]
		[DataMember(Name = "GroupRecordsBy", IsRequired = true)]
		public UMCDRGroupByType GroupRecordsBy { get; set; }

		// Token: 0x0600209F RID: 8351 RVA: 0x000A1D51 File Offset: 0x0009FF51
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetUMCallSummary(callContext, this);
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000A1D5A File Offset: 0x0009FF5A
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000A1D5D File Offset: 0x0009FF5D
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
