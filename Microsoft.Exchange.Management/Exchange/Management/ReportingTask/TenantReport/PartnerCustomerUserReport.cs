using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006F9 RID: 1785
	[Table(Name = "dbo.PartnerCustomerUser")]
	[DataServiceKey("Date")]
	[Serializable]
	public class PartnerCustomerUserReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06003EF3 RID: 16115 RVA: 0x001052D6 File Offset: 0x001034D6
		// (set) Token: 0x06003EF4 RID: 16116 RVA: 0x001052DE File Offset: 0x001034DE
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06003EF5 RID: 16117 RVA: 0x001052E7 File Offset: 0x001034E7
		// (set) Token: 0x06003EF6 RID: 16118 RVA: 0x001052EF File Offset: 0x001034EF
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06003EF7 RID: 16119 RVA: 0x001052F8 File Offset: 0x001034F8
		// (set) Token: 0x06003EF8 RID: 16120 RVA: 0x00105300 File Offset: 0x00103500
		[Column(Name = "CustomerTenantID")]
		public Guid CustomerTenantID { get; set; }

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06003EF9 RID: 16121 RVA: 0x00105309 File Offset: 0x00103509
		// (set) Token: 0x06003EFA RID: 16122 RVA: 0x00105311 File Offset: 0x00103511
		[Column(Name = "CompanyName")]
		public string CompanyName { get; set; }

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06003EFB RID: 16123 RVA: 0x0010531A File Offset: 0x0010351A
		// (set) Token: 0x06003EFC RID: 16124 RVA: 0x00105322 File Offset: 0x00103522
		[Column(Name = "UserPrincipalName")]
		public string UserPrincipalName { get; set; }

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06003EFD RID: 16125 RVA: 0x0010532B File Offset: 0x0010352B
		// (set) Token: 0x06003EFE RID: 16126 RVA: 0x00105333 File Offset: 0x00103533
		[Column(Name = "DisplayName")]
		public string DisplayName { get; set; }

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06003EFF RID: 16127 RVA: 0x0010533C File Offset: 0x0010353C
		// (set) Token: 0x06003F00 RID: 16128 RVA: 0x00105344 File Offset: 0x00103544
		[Column(Name = "CreateDate")]
		public DateTime CreateDate { get; set; }

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06003F01 RID: 16129 RVA: 0x0010534D File Offset: 0x0010354D
		// (set) Token: 0x06003F02 RID: 16130 RVA: 0x00105355 File Offset: 0x00103555
		[Column(Name = "PasswordLastResetDate")]
		public DateTime PasswordLastResetDate { get; set; }

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06003F03 RID: 16131 RVA: 0x0010535E File Offset: 0x0010355E
		// (set) Token: 0x06003F04 RID: 16132 RVA: 0x00105366 File Offset: 0x00103566
		[Column(Name = "LastAccessDate")]
		public DateTime LastAccessDate { get; set; }

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06003F05 RID: 16133 RVA: 0x0010536F File Offset: 0x0010356F
		// (set) Token: 0x06003F06 RID: 16134 RVA: 0x00105377 File Offset: 0x00103577
		[Column(Name = "UsageLocation")]
		public string UsageLocation { get; set; }

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06003F07 RID: 16135 RVA: 0x00105380 File Offset: 0x00103580
		// (set) Token: 0x06003F08 RID: 16136 RVA: 0x00105388 File Offset: 0x00103588
		[Column(Name = "AssignedLicense")]
		public string AssignedLicense { get; set; }
	}
}
