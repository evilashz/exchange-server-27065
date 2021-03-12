using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006C1 RID: 1729
	[Table(Name = "dbo.ConnectionByClientType")]
	[DataServiceKey("Date")]
	[Serializable]
	public class ConnectionByClientTypeReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06003D39 RID: 15673 RVA: 0x00102F72 File Offset: 0x00101172
		// (set) Token: 0x06003D3A RID: 15674 RVA: 0x00102F7A File Offset: 0x0010117A
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06003D3B RID: 15675 RVA: 0x00102F83 File Offset: 0x00101183
		// (set) Token: 0x06003D3C RID: 15676 RVA: 0x00102F8B File Offset: 0x0010118B
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06003D3D RID: 15677 RVA: 0x00102F94 File Offset: 0x00101194
		// (set) Token: 0x06003D3E RID: 15678 RVA: 0x00102F9C File Offset: 0x0010119C
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x00102FA5 File Offset: 0x001011A5
		// (set) Token: 0x06003D40 RID: 15680 RVA: 0x00102FAD File Offset: 0x001011AD
		[Column(Name = "ClientType")]
		public string ClientType { get; set; }

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06003D41 RID: 15681 RVA: 0x00102FB6 File Offset: 0x001011B6
		// (set) Token: 0x06003D42 RID: 15682 RVA: 0x00102FBE File Offset: 0x001011BE
		[Column(Name = "Count")]
		public long? Count { get; set; }
	}
}
