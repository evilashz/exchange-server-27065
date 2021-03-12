using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006FD RID: 1789
	[Table(Name = "dbo.ScorecardMetrics")]
	[DataServiceKey("Date")]
	[Serializable]
	public class ScorecardMetricsReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06003F37 RID: 16183 RVA: 0x00105516 File Offset: 0x00103716
		// (set) Token: 0x06003F38 RID: 16184 RVA: 0x0010551E File Offset: 0x0010371E
		[Column(Name = "DATETIME")]
		public DateTime Date { get; set; }

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x00105527 File Offset: 0x00103727
		// (set) Token: 0x06003F3A RID: 16186 RVA: 0x0010552F File Offset: 0x0010372F
		[Column(Name = "TENANTGUID")]
		public Guid TenantGuid { get; set; }

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06003F3B RID: 16187 RVA: 0x00105538 File Offset: 0x00103738
		// (set) Token: 0x06003F3C RID: 16188 RVA: 0x00105540 File Offset: 0x00103740
		[Column(Name = "SERVICE")]
		public string Service { get; set; }

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06003F3D RID: 16189 RVA: 0x00105549 File Offset: 0x00103749
		// (set) Token: 0x06003F3E RID: 16190 RVA: 0x00105551 File Offset: 0x00103751
		[Column(Name = "TYPE")]
		public string Type { get; set; }

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06003F3F RID: 16191 RVA: 0x0010555A File Offset: 0x0010375A
		// (set) Token: 0x06003F40 RID: 16192 RVA: 0x00105562 File Offset: 0x00103762
		[Column(Name = "METRICNAME")]
		public string MetricName { get; set; }

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06003F41 RID: 16193 RVA: 0x0010556B File Offset: 0x0010376B
		// (set) Token: 0x06003F42 RID: 16194 RVA: 0x00105573 File Offset: 0x00103773
		[Column(Name = "THRESHOLD")]
		public double? Threshold { get; set; }

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06003F43 RID: 16195 RVA: 0x0010557C File Offset: 0x0010377C
		// (set) Token: 0x06003F44 RID: 16196 RVA: 0x00105584 File Offset: 0x00103784
		[Column(Name = "METRICVALUE")]
		public double MetricValue { get; set; }
	}
}
