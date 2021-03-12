using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006FC RID: 1788
	[Table(Name = "dbo.ScorecardClientOutlook")]
	[DataServiceKey("Date")]
	[Serializable]
	public class ScorecardClientOutlookReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06003F28 RID: 16168 RVA: 0x00105497 File Offset: 0x00103697
		// (set) Token: 0x06003F29 RID: 16169 RVA: 0x0010549F File Offset: 0x0010369F
		[Column(Name = "DATETIME")]
		public DateTime Date { get; set; }

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06003F2A RID: 16170 RVA: 0x001054A8 File Offset: 0x001036A8
		// (set) Token: 0x06003F2B RID: 16171 RVA: 0x001054B0 File Offset: 0x001036B0
		[Column(Name = "TENANTGUID")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06003F2C RID: 16172 RVA: 0x001054B9 File Offset: 0x001036B9
		// (set) Token: 0x06003F2D RID: 16173 RVA: 0x001054C1 File Offset: 0x001036C1
		[Column(Name = "NAME")]
		public string Name { get; set; }

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06003F2E RID: 16174 RVA: 0x001054CA File Offset: 0x001036CA
		// (set) Token: 0x06003F2F RID: 16175 RVA: 0x001054D2 File Offset: 0x001036D2
		[Column(Name = "RELEASENAME")]
		public string ReleaseName { get; set; }

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06003F30 RID: 16176 RVA: 0x001054DB File Offset: 0x001036DB
		// (set) Token: 0x06003F31 RID: 16177 RVA: 0x001054E3 File Offset: 0x001036E3
		[Column(Name = "CATEGORY")]
		public string Category { get; set; }

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06003F32 RID: 16178 RVA: 0x001054EC File Offset: 0x001036EC
		// (set) Token: 0x06003F33 RID: 16179 RVA: 0x001054F4 File Offset: 0x001036F4
		[Column(Name = "USERCOUNT")]
		public int UserCount { get; set; }

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06003F34 RID: 16180 RVA: 0x001054FD File Offset: 0x001036FD
		// (set) Token: 0x06003F35 RID: 16181 RVA: 0x00105505 File Offset: 0x00103705
		[Column(Name = "DISPLAYORDER")]
		public int DisplayOrder { get; set; }
	}
}
