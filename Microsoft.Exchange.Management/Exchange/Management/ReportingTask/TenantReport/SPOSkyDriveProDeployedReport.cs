using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x02000701 RID: 1793
	[Table(Name = "dbo.SPOSkyDriveProDeployedWeekly")]
	[DataServiceKey("Date")]
	[Serializable]
	public class SPOSkyDriveProDeployedReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06003F95 RID: 16277 RVA: 0x00105833 File Offset: 0x00103A33
		// (set) Token: 0x06003F96 RID: 16278 RVA: 0x0010583B File Offset: 0x00103A3B
		[Column(Name = "ID")]
		public long ID { get; set; }

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06003F97 RID: 16279 RVA: 0x00105844 File Offset: 0x00103A44
		// (set) Token: 0x06003F98 RID: 16280 RVA: 0x0010584C File Offset: 0x00103A4C
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06003F99 RID: 16281 RVA: 0x00105855 File Offset: 0x00103A55
		// (set) Token: 0x06003F9A RID: 16282 RVA: 0x0010585D File Offset: 0x00103A5D
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06003F9B RID: 16283 RVA: 0x00105866 File Offset: 0x00103A66
		// (set) Token: 0x06003F9C RID: 16284 RVA: 0x0010586E File Offset: 0x00103A6E
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06003F9D RID: 16285 RVA: 0x00105877 File Offset: 0x00103A77
		// (set) Token: 0x06003F9E RID: 16286 RVA: 0x0010587F File Offset: 0x00103A7F
		[Column(Name = "Active")]
		public long? Active { get; set; }

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06003F9F RID: 16287 RVA: 0x00105888 File Offset: 0x00103A88
		// (set) Token: 0x06003FA0 RID: 16288 RVA: 0x00105890 File Offset: 0x00103A90
		[Column(Name = "Inactive")]
		public long? Inactive { get; set; }
	}
}
