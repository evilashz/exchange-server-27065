using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006C4 RID: 1732
	[DataServiceKey("Date")]
	[Table(Name = "dbo.CsDeviceSummaryMonthly")]
	[Serializable]
	public class CsClientDeviceReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06003D5A RID: 15706 RVA: 0x00103089 File Offset: 0x00101289
		// (set) Token: 0x06003D5B RID: 15707 RVA: 0x00103091 File Offset: 0x00101291
		[Column(Name = "ID")]
		public long ID { get; set; }

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06003D5C RID: 15708 RVA: 0x0010309A File Offset: 0x0010129A
		// (set) Token: 0x06003D5D RID: 15709 RVA: 0x001030A2 File Offset: 0x001012A2
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06003D5E RID: 15710 RVA: 0x001030AB File Offset: 0x001012AB
		// (set) Token: 0x06003D5F RID: 15711 RVA: 0x001030B3 File Offset: 0x001012B3
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06003D60 RID: 15712 RVA: 0x001030BC File Offset: 0x001012BC
		// (set) Token: 0x06003D61 RID: 15713 RVA: 0x001030C4 File Offset: 0x001012C4
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06003D62 RID: 15714 RVA: 0x001030CD File Offset: 0x001012CD
		// (set) Token: 0x06003D63 RID: 15715 RVA: 0x001030D5 File Offset: 0x001012D5
		[Column(Name = "WindowsCount")]
		public long WindowsUsers { get; set; }

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06003D64 RID: 15716 RVA: 0x001030DE File Offset: 0x001012DE
		// (set) Token: 0x06003D65 RID: 15717 RVA: 0x001030E6 File Offset: 0x001012E6
		[Column(Name = "WindowsPhoneCount")]
		public long WindowsPhoneUsers { get; set; }

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06003D66 RID: 15718 RVA: 0x001030EF File Offset: 0x001012EF
		// (set) Token: 0x06003D67 RID: 15719 RVA: 0x001030F7 File Offset: 0x001012F7
		[Column(Name = "AndroidCount")]
		public long AndroidUsers { get; set; }

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06003D68 RID: 15720 RVA: 0x00103100 File Offset: 0x00101300
		// (set) Token: 0x06003D69 RID: 15721 RVA: 0x00103108 File Offset: 0x00101308
		[Column(Name = "iPhoneCount")]
		public long iPhoneUsers { get; set; }

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06003D6A RID: 15722 RVA: 0x00103111 File Offset: 0x00101311
		// (set) Token: 0x06003D6B RID: 15723 RVA: 0x00103119 File Offset: 0x00101319
		[Column(Name = "iPadCount")]
		public long iPadUsers { get; set; }
	}
}
