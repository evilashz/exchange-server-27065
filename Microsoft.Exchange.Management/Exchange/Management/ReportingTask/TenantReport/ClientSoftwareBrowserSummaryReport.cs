using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006BD RID: 1725
	[DataServiceKey("Date")]
	[Table(Name = "dbo.ClientSoftwareBrowserSummary")]
	[Serializable]
	public class ClientSoftwareBrowserSummaryReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06003CF3 RID: 15603 RVA: 0x00102D21 File Offset: 0x00100F21
		// (set) Token: 0x06003CF4 RID: 15604 RVA: 0x00102D29 File Offset: 0x00100F29
		[Column(Name = "TENANTGUID")]
		public Guid TenantGuid { get; set; }

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06003CF5 RID: 15605 RVA: 0x00102D32 File Offset: 0x00100F32
		// (set) Token: 0x06003CF6 RID: 15606 RVA: 0x00102D3A File Offset: 0x00100F3A
		[Column(Name = "TENANTNAME")]
		public string TenantName { get; set; }

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06003CF7 RID: 15607 RVA: 0x00102D43 File Offset: 0x00100F43
		// (set) Token: 0x06003CF8 RID: 15608 RVA: 0x00102D4B File Offset: 0x00100F4B
		[Column(Name = "DATETIME")]
		public DateTime Date { get; set; }

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06003CF9 RID: 15609 RVA: 0x00102D54 File Offset: 0x00100F54
		// (set) Token: 0x06003CFA RID: 15610 RVA: 0x00102D5C File Offset: 0x00100F5C
		[Column(Name = "NAME")]
		public string Name { get; set; }

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06003CFB RID: 15611 RVA: 0x00102D65 File Offset: 0x00100F65
		// (set) Token: 0x06003CFC RID: 15612 RVA: 0x00102D6D File Offset: 0x00100F6D
		[Column(Name = "VERSION")]
		public string Version { get; set; }

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06003CFD RID: 15613 RVA: 0x00102D76 File Offset: 0x00100F76
		// (set) Token: 0x06003CFE RID: 15614 RVA: 0x00102D7E File Offset: 0x00100F7E
		[Column(Name = "COUNT")]
		public long Count { get; set; }

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06003CFF RID: 15615 RVA: 0x00102D87 File Offset: 0x00100F87
		// (set) Token: 0x06003D00 RID: 15616 RVA: 0x00102D8F File Offset: 0x00100F8F
		[Column(Name = "CATEGORY")]
		public string Category { get; set; }

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x00102D98 File Offset: 0x00100F98
		// (set) Token: 0x06003D02 RID: 15618 RVA: 0x00102DA0 File Offset: 0x00100FA0
		[Column(Name = "DISPLAYORDER")]
		public int DisplayOrder { get; set; }
	}
}
