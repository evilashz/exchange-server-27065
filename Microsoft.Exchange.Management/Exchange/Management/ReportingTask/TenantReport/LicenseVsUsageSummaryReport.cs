using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006F4 RID: 1780
	[DataServiceKey(new string[]
	{
		"TenantGuid",
		"Workload"
	})]
	[Table(Name = "dbo.LicenseVsUsageSummary")]
	[Serializable]
	public class LicenseVsUsageSummaryReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06003E96 RID: 16022 RVA: 0x00104FC2 File Offset: 0x001031C2
		// (set) Token: 0x06003E97 RID: 16023 RVA: 0x00104FCA File Offset: 0x001031CA
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06003E98 RID: 16024 RVA: 0x00104FD3 File Offset: 0x001031D3
		// (set) Token: 0x06003E99 RID: 16025 RVA: 0x00104FDB File Offset: 0x001031DB
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06003E9A RID: 16026 RVA: 0x00104FE4 File Offset: 0x001031E4
		// (set) Token: 0x06003E9B RID: 16027 RVA: 0x00104FEC File Offset: 0x001031EC
		[Column(Name = "Workload")]
		public string Workload { get; set; }

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06003E9C RID: 16028 RVA: 0x00104FF5 File Offset: 0x001031F5
		// (set) Token: 0x06003E9D RID: 16029 RVA: 0x00104FFD File Offset: 0x001031FD
		[Column(Name = "NonTrialEntitlements")]
		public long NonTrialEntitlements { get; set; }

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06003E9E RID: 16030 RVA: 0x00105006 File Offset: 0x00103206
		// (set) Token: 0x06003E9F RID: 16031 RVA: 0x0010500E File Offset: 0x0010320E
		[Column(Name = "TrialEntitlements")]
		public long TrialEntitlements { get; set; }

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06003EA0 RID: 16032 RVA: 0x00105017 File Offset: 0x00103217
		// (set) Token: 0x06003EA1 RID: 16033 RVA: 0x0010501F File Offset: 0x0010321F
		[Column(Name = "ActiveUsers")]
		public long ActiveUsers { get; set; }
	}
}
