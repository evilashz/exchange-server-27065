using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x02000703 RID: 1795
	[Table(Name = "dbo.SPOTeamSiteDeployedWeekly")]
	[DataServiceKey("Date")]
	[Serializable]
	public class SPOTeamSiteDeployedReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06003FAF RID: 16303 RVA: 0x0010590F File Offset: 0x00103B0F
		// (set) Token: 0x06003FB0 RID: 16304 RVA: 0x00105917 File Offset: 0x00103B17
		[Column(Name = "ID")]
		public long ID { get; set; }

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06003FB1 RID: 16305 RVA: 0x00105920 File Offset: 0x00103B20
		// (set) Token: 0x06003FB2 RID: 16306 RVA: 0x00105928 File Offset: 0x00103B28
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06003FB3 RID: 16307 RVA: 0x00105931 File Offset: 0x00103B31
		// (set) Token: 0x06003FB4 RID: 16308 RVA: 0x00105939 File Offset: 0x00103B39
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x06003FB5 RID: 16309 RVA: 0x00105942 File Offset: 0x00103B42
		// (set) Token: 0x06003FB6 RID: 16310 RVA: 0x0010594A File Offset: 0x00103B4A
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06003FB7 RID: 16311 RVA: 0x00105953 File Offset: 0x00103B53
		// (set) Token: 0x06003FB8 RID: 16312 RVA: 0x0010595B File Offset: 0x00103B5B
		[Column(Name = "Active")]
		public long? Active { get; set; }

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x00105964 File Offset: 0x00103B64
		// (set) Token: 0x06003FBA RID: 16314 RVA: 0x0010596C File Offset: 0x00103B6C
		[Column(Name = "Inactive")]
		public long? Inactive { get; set; }
	}
}
