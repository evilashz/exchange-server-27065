using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x02000704 RID: 1796
	[Table(Name = "dbo.SPOTeamSiteStorageWeekly")]
	[DataServiceKey("Date")]
	[Serializable]
	public class SPOTeamSiteStorageReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06003FBC RID: 16316 RVA: 0x0010597D File Offset: 0x00103B7D
		// (set) Token: 0x06003FBD RID: 16317 RVA: 0x00105985 File Offset: 0x00103B85
		[Column(Name = "ID")]
		public long ID { get; set; }

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06003FBE RID: 16318 RVA: 0x0010598E File Offset: 0x00103B8E
		// (set) Token: 0x06003FBF RID: 16319 RVA: 0x00105996 File Offset: 0x00103B96
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06003FC0 RID: 16320 RVA: 0x0010599F File Offset: 0x00103B9F
		// (set) Token: 0x06003FC1 RID: 16321 RVA: 0x001059A7 File Offset: 0x00103BA7
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x001059B0 File Offset: 0x00103BB0
		// (set) Token: 0x06003FC3 RID: 16323 RVA: 0x001059B8 File Offset: 0x00103BB8
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06003FC4 RID: 16324 RVA: 0x001059C1 File Offset: 0x00103BC1
		// (set) Token: 0x06003FC5 RID: 16325 RVA: 0x001059C9 File Offset: 0x00103BC9
		[Column(Name = "Used")]
		public long? Used { get; set; }

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06003FC6 RID: 16326 RVA: 0x001059D2 File Offset: 0x00103BD2
		// (set) Token: 0x06003FC7 RID: 16327 RVA: 0x001059DA File Offset: 0x00103BDA
		[Column(Name = "Allocated")]
		public long? Allocated { get; set; }
	}
}
