using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006C2 RID: 1730
	[DataServiceKey("Date")]
	[Table(Name = "dbo.CsActiveUserDaily")]
	[Serializable]
	public class CsActiveUserReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06003D44 RID: 15684 RVA: 0x00102FCF File Offset: 0x001011CF
		// (set) Token: 0x06003D45 RID: 15685 RVA: 0x00102FD7 File Offset: 0x001011D7
		[Column(Name = "ID")]
		public long ID { get; set; }

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06003D46 RID: 15686 RVA: 0x00102FE0 File Offset: 0x001011E0
		// (set) Token: 0x06003D47 RID: 15687 RVA: 0x00102FE8 File Offset: 0x001011E8
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06003D48 RID: 15688 RVA: 0x00102FF1 File Offset: 0x001011F1
		// (set) Token: 0x06003D49 RID: 15689 RVA: 0x00102FF9 File Offset: 0x001011F9
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06003D4A RID: 15690 RVA: 0x00103002 File Offset: 0x00101202
		// (set) Token: 0x06003D4B RID: 15691 RVA: 0x0010300A File Offset: 0x0010120A
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06003D4C RID: 15692 RVA: 0x00103013 File Offset: 0x00101213
		// (set) Token: 0x06003D4D RID: 15693 RVA: 0x0010301B File Offset: 0x0010121B
		[Column(Name = "ActiveUsersCount")]
		public long? ActiveUsers { get; set; }
	}
}
