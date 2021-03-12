using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006BF RID: 1727
	[Table(Name = "dbo.ClientSoftwareOSSummary")]
	[DataServiceKey("Date")]
	[Serializable]
	public class ClientSoftwareOSSummaryReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06003D19 RID: 15641 RVA: 0x00102E63 File Offset: 0x00101063
		// (set) Token: 0x06003D1A RID: 15642 RVA: 0x00102E6B File Offset: 0x0010106B
		[Column(Name = "TENANTGUID")]
		public Guid TenantGuid { get; set; }

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06003D1B RID: 15643 RVA: 0x00102E74 File Offset: 0x00101074
		// (set) Token: 0x06003D1C RID: 15644 RVA: 0x00102E7C File Offset: 0x0010107C
		[Column(Name = "TENANTNAME")]
		public string TenantName { get; set; }

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06003D1D RID: 15645 RVA: 0x00102E85 File Offset: 0x00101085
		// (set) Token: 0x06003D1E RID: 15646 RVA: 0x00102E8D File Offset: 0x0010108D
		[Column(Name = "DATETIME")]
		public DateTime Date { get; set; }

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06003D1F RID: 15647 RVA: 0x00102E96 File Offset: 0x00101096
		// (set) Token: 0x06003D20 RID: 15648 RVA: 0x00102E9E File Offset: 0x0010109E
		[Column(Name = "NAME")]
		public string Name { get; set; }

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x00102EA7 File Offset: 0x001010A7
		// (set) Token: 0x06003D22 RID: 15650 RVA: 0x00102EAF File Offset: 0x001010AF
		[Column(Name = "VERSION")]
		public string Version { get; set; }

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x00102EB8 File Offset: 0x001010B8
		// (set) Token: 0x06003D24 RID: 15652 RVA: 0x00102EC0 File Offset: 0x001010C0
		[Column(Name = "COUNT")]
		public long Count { get; set; }

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06003D25 RID: 15653 RVA: 0x00102EC9 File Offset: 0x001010C9
		// (set) Token: 0x06003D26 RID: 15654 RVA: 0x00102ED1 File Offset: 0x001010D1
		[Column(Name = "CATEGORY")]
		public string Category { get; set; }

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06003D27 RID: 15655 RVA: 0x00102EDA File Offset: 0x001010DA
		// (set) Token: 0x06003D28 RID: 15656 RVA: 0x00102EE2 File Offset: 0x001010E2
		[Column(Name = "DISPLAYORDER")]
		public int DisplayOrder { get; set; }
	}
}
