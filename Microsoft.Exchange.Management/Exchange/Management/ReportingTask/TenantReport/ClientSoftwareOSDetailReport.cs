using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006BE RID: 1726
	[Table(Name = "dbo.ClientSoftwareOSDetail")]
	[DataServiceKey("Date")]
	[Serializable]
	public class ClientSoftwareOSDetailReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06003D04 RID: 15620 RVA: 0x00102DB1 File Offset: 0x00100FB1
		// (set) Token: 0x06003D05 RID: 15621 RVA: 0x00102DB9 File Offset: 0x00100FB9
		[Column(Name = "TENANTGUID")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06003D06 RID: 15622 RVA: 0x00102DC2 File Offset: 0x00100FC2
		// (set) Token: 0x06003D07 RID: 15623 RVA: 0x00102DCA File Offset: 0x00100FCA
		[Column(Name = "TENANTNAME")]
		public string TenantName { get; set; }

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06003D08 RID: 15624 RVA: 0x00102DD3 File Offset: 0x00100FD3
		// (set) Token: 0x06003D09 RID: 15625 RVA: 0x00102DDB File Offset: 0x00100FDB
		[Column(Name = "DATETIME")]
		public DateTime Date { get; set; }

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x00102DE4 File Offset: 0x00100FE4
		// (set) Token: 0x06003D0B RID: 15627 RVA: 0x00102DEC File Offset: 0x00100FEC
		[Column(Name = "NAME")]
		public string Name { get; set; }

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x00102DF5 File Offset: 0x00100FF5
		// (set) Token: 0x06003D0D RID: 15629 RVA: 0x00102DFD File Offset: 0x00100FFD
		[Column(Name = "VERSION")]
		public string Version { get; set; }

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06003D0E RID: 15630 RVA: 0x00102E06 File Offset: 0x00101006
		// (set) Token: 0x06003D0F RID: 15631 RVA: 0x00102E0E File Offset: 0x0010100E
		[Column(Name = "COUNT")]
		public long Count { get; set; }

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06003D10 RID: 15632 RVA: 0x00102E17 File Offset: 0x00101017
		// (set) Token: 0x06003D11 RID: 15633 RVA: 0x00102E1F File Offset: 0x0010101F
		[Column(Name = "LASTACCESSTIME")]
		public DateTime LastAccessTime { get; set; }

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06003D12 RID: 15634 RVA: 0x00102E28 File Offset: 0x00101028
		// (set) Token: 0x06003D13 RID: 15635 RVA: 0x00102E30 File Offset: 0x00101030
		[Column(Name = "OBJECTID")]
		public Guid ObjectId { get; set; }

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06003D14 RID: 15636 RVA: 0x00102E39 File Offset: 0x00101039
		// (set) Token: 0x06003D15 RID: 15637 RVA: 0x00102E41 File Offset: 0x00101041
		[SuppressPii(PiiDataType = PiiDataType.Smtp)]
		[Column(Name = "UPN")]
		public string UPN { get; set; }

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06003D16 RID: 15638 RVA: 0x00102E4A File Offset: 0x0010104A
		// (set) Token: 0x06003D17 RID: 15639 RVA: 0x00102E52 File Offset: 0x00101052
		[SuppressPii(PiiDataType = PiiDataType.String)]
		[Column(Name = "DISPLAYNAME")]
		public string DisplayName { get; set; }
	}
}
