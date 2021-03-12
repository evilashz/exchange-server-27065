using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006FA RID: 1786
	[DataServiceKey("Date")]
	[Table(Name = "dbo.ScorecardClientDevice")]
	[Serializable]
	public class ScorecardClientDeviceReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06003F0A RID: 16138 RVA: 0x00105399 File Offset: 0x00103599
		// (set) Token: 0x06003F0B RID: 16139 RVA: 0x001053A1 File Offset: 0x001035A1
		[Column(Name = "DATETIME")]
		public DateTime Date { get; set; }

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06003F0C RID: 16140 RVA: 0x001053AA File Offset: 0x001035AA
		// (set) Token: 0x06003F0D RID: 16141 RVA: 0x001053B2 File Offset: 0x001035B2
		[Column(Name = "TENANTGUID")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06003F0E RID: 16142 RVA: 0x001053BB File Offset: 0x001035BB
		// (set) Token: 0x06003F0F RID: 16143 RVA: 0x001053C3 File Offset: 0x001035C3
		[Column(Name = "NAME")]
		public string Name { get; set; }

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06003F10 RID: 16144 RVA: 0x001053CC File Offset: 0x001035CC
		// (set) Token: 0x06003F11 RID: 16145 RVA: 0x001053D4 File Offset: 0x001035D4
		[Column(Name = "MAJORVERSION")]
		public string MajorVersion { get; set; }

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06003F12 RID: 16146 RVA: 0x001053DD File Offset: 0x001035DD
		// (set) Token: 0x06003F13 RID: 16147 RVA: 0x001053E5 File Offset: 0x001035E5
		[Column(Name = "MINORVERSION")]
		public string MinorVersion { get; set; }

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06003F14 RID: 16148 RVA: 0x001053EE File Offset: 0x001035EE
		// (set) Token: 0x06003F15 RID: 16149 RVA: 0x001053F6 File Offset: 0x001035F6
		[Column(Name = "CATEGORY")]
		public string Category { get; set; }

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06003F16 RID: 16150 RVA: 0x001053FF File Offset: 0x001035FF
		// (set) Token: 0x06003F17 RID: 16151 RVA: 0x00105407 File Offset: 0x00103607
		[Column(Name = "USERCOUNT")]
		public int UserCount { get; set; }

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x00105410 File Offset: 0x00103610
		// (set) Token: 0x06003F19 RID: 16153 RVA: 0x00105418 File Offset: 0x00103618
		[Column(Name = "DISPLAYORDER")]
		public int DisplayOrder { get; set; }
	}
}
