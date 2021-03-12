using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006FB RID: 1787
	[DataServiceKey("Date")]
	[Table(Name = "dbo.ScorecardClientOS")]
	[Serializable]
	public class ScorecardClientOSReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06003F1B RID: 16155 RVA: 0x00105429 File Offset: 0x00103629
		// (set) Token: 0x06003F1C RID: 16156 RVA: 0x00105431 File Offset: 0x00103631
		[Column(Name = "DATETIME")]
		public DateTime Date { get; set; }

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06003F1D RID: 16157 RVA: 0x0010543A File Offset: 0x0010363A
		// (set) Token: 0x06003F1E RID: 16158 RVA: 0x00105442 File Offset: 0x00103642
		[Column(Name = "TENANTGUID")]
		public Guid TenantGuid { get; set; }

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06003F1F RID: 16159 RVA: 0x0010544B File Offset: 0x0010364B
		// (set) Token: 0x06003F20 RID: 16160 RVA: 0x00105453 File Offset: 0x00103653
		[Column(Name = "NAME")]
		public string Name { get; set; }

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06003F21 RID: 16161 RVA: 0x0010545C File Offset: 0x0010365C
		// (set) Token: 0x06003F22 RID: 16162 RVA: 0x00105464 File Offset: 0x00103664
		[Column(Name = "USERCOUNT")]
		public int UserCount { get; set; }

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06003F23 RID: 16163 RVA: 0x0010546D File Offset: 0x0010366D
		// (set) Token: 0x06003F24 RID: 16164 RVA: 0x00105475 File Offset: 0x00103675
		[Column(Name = "CATEGORY")]
		public string Category { get; set; }

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06003F25 RID: 16165 RVA: 0x0010547E File Offset: 0x0010367E
		// (set) Token: 0x06003F26 RID: 16166 RVA: 0x00105486 File Offset: 0x00103686
		[Column(Name = "DISPLAYORDER")]
		public int DisplayOrder { get; set; }
	}
}
