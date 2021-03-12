using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006FE RID: 1790
	[DataServiceKey("Date")]
	[Table(Name = "dbo.SPOActiveUserDaily")]
	[Serializable]
	public class SPOActiveUserReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06003F46 RID: 16198 RVA: 0x00105595 File Offset: 0x00103795
		// (set) Token: 0x06003F47 RID: 16199 RVA: 0x0010559D File Offset: 0x0010379D
		[Column(Name = "ID")]
		public long ID { get; set; }

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06003F48 RID: 16200 RVA: 0x001055A6 File Offset: 0x001037A6
		// (set) Token: 0x06003F49 RID: 16201 RVA: 0x001055AE File Offset: 0x001037AE
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06003F4A RID: 16202 RVA: 0x001055B7 File Offset: 0x001037B7
		// (set) Token: 0x06003F4B RID: 16203 RVA: 0x001055BF File Offset: 0x001037BF
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06003F4C RID: 16204 RVA: 0x001055C8 File Offset: 0x001037C8
		// (set) Token: 0x06003F4D RID: 16205 RVA: 0x001055D0 File Offset: 0x001037D0
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06003F4E RID: 16206 RVA: 0x001055D9 File Offset: 0x001037D9
		// (set) Token: 0x06003F4F RID: 16207 RVA: 0x001055E1 File Offset: 0x001037E1
		[Column(Name = "UniqueUsers")]
		public long? UniqueUsers { get; set; }

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x001055EA File Offset: 0x001037EA
		// (set) Token: 0x06003F51 RID: 16209 RVA: 0x001055F2 File Offset: 0x001037F2
		[Column(Name = "LicensesAssigned")]
		public long? LicensesAssigned { get; set; }

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06003F52 RID: 16210 RVA: 0x001055FB File Offset: 0x001037FB
		// (set) Token: 0x06003F53 RID: 16211 RVA: 0x00105603 File Offset: 0x00103803
		[Column(Name = "LicensesAcquired")]
		public long? LicensesAcquired { get; set; }

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06003F54 RID: 16212 RVA: 0x0010560C File Offset: 0x0010380C
		// (set) Token: 0x06003F55 RID: 16213 RVA: 0x00105614 File Offset: 0x00103814
		[Column(Name = "TotalUsers")]
		public long? TotalUsers { get; set; }
	}
}
