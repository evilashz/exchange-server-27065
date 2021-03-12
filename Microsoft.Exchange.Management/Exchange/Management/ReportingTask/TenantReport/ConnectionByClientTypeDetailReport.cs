using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006C0 RID: 1728
	[DataServiceKey("Date")]
	[Table(Name = "dbo.ConnectionByClientTypeDetail")]
	[Serializable]
	public class ConnectionByClientTypeDetailReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x00102EF3 File Offset: 0x001010F3
		// (set) Token: 0x06003D2B RID: 15659 RVA: 0x00102EFB File Offset: 0x001010FB
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06003D2C RID: 15660 RVA: 0x00102F04 File Offset: 0x00101104
		// (set) Token: 0x06003D2D RID: 15661 RVA: 0x00102F0C File Offset: 0x0010110C
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06003D2E RID: 15662 RVA: 0x00102F15 File Offset: 0x00101115
		// (set) Token: 0x06003D2F RID: 15663 RVA: 0x00102F1D File Offset: 0x0010111D
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x00102F26 File Offset: 0x00101126
		// (set) Token: 0x06003D31 RID: 15665 RVA: 0x00102F2E File Offset: 0x0010112E
		[Column(Name = "WindowsLiveID")]
		[SuppressPii(PiiDataType = PiiDataType.Smtp)]
		public string WindowsLiveID { get; set; }

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06003D32 RID: 15666 RVA: 0x00102F37 File Offset: 0x00101137
		// (set) Token: 0x06003D33 RID: 15667 RVA: 0x00102F3F File Offset: 0x0010113F
		[SuppressPii(PiiDataType = PiiDataType.String)]
		[Column(Name = "UserName")]
		public string UserName { get; set; }

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06003D34 RID: 15668 RVA: 0x00102F48 File Offset: 0x00101148
		// (set) Token: 0x06003D35 RID: 15669 RVA: 0x00102F50 File Offset: 0x00101150
		[Column(Name = "ClientType")]
		public string ClientType { get; set; }

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06003D36 RID: 15670 RVA: 0x00102F59 File Offset: 0x00101159
		// (set) Token: 0x06003D37 RID: 15671 RVA: 0x00102F61 File Offset: 0x00101161
		[Column(Name = "Count")]
		public long? Count { get; set; }
	}
}
