using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006C6 RID: 1734
	[Table(Name = "dbo.CsP2PAVTimeDaily")]
	[DataServiceKey("Date")]
	[Serializable]
	public class CsP2PAVTimeReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06003D82 RID: 15746 RVA: 0x001031DC File Offset: 0x001013DC
		// (set) Token: 0x06003D83 RID: 15747 RVA: 0x001031E4 File Offset: 0x001013E4
		[Column(Name = "ID")]
		public long ID { get; set; }

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06003D84 RID: 15748 RVA: 0x001031ED File Offset: 0x001013ED
		// (set) Token: 0x06003D85 RID: 15749 RVA: 0x001031F5 File Offset: 0x001013F5
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06003D86 RID: 15750 RVA: 0x001031FE File Offset: 0x001013FE
		// (set) Token: 0x06003D87 RID: 15751 RVA: 0x00103206 File Offset: 0x00101406
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06003D88 RID: 15752 RVA: 0x0010320F File Offset: 0x0010140F
		// (set) Token: 0x06003D89 RID: 15753 RVA: 0x00103217 File Offset: 0x00101417
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x00103220 File Offset: 0x00101420
		// (set) Token: 0x06003D8B RID: 15755 RVA: 0x00103228 File Offset: 0x00101428
		[Column(Name = "TotalAudioMinutes")]
		public long? TotalAudioMinutes { get; set; }

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x00103231 File Offset: 0x00101431
		// (set) Token: 0x06003D8D RID: 15757 RVA: 0x00103239 File Offset: 0x00101439
		[Column(Name = "TotalVideoMinutes")]
		public long? TotalVideoMinutes { get; set; }
	}
}
