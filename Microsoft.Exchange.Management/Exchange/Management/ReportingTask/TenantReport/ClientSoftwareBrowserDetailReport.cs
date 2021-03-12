using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006BC RID: 1724
	[Table(Name = "dbo.ClientSoftwareBrowserDetail")]
	[DataServiceKey("Date")]
	[Serializable]
	public class ClientSoftwareBrowserDetailReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06003CDE RID: 15582 RVA: 0x00102C6F File Offset: 0x00100E6F
		// (set) Token: 0x06003CDF RID: 15583 RVA: 0x00102C77 File Offset: 0x00100E77
		[Column(Name = "TENANTGUID")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06003CE0 RID: 15584 RVA: 0x00102C80 File Offset: 0x00100E80
		// (set) Token: 0x06003CE1 RID: 15585 RVA: 0x00102C88 File Offset: 0x00100E88
		[Column(Name = "TENANTNAME")]
		public string TenantName { get; set; }

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06003CE2 RID: 15586 RVA: 0x00102C91 File Offset: 0x00100E91
		// (set) Token: 0x06003CE3 RID: 15587 RVA: 0x00102C99 File Offset: 0x00100E99
		[Column(Name = "DATETIME")]
		public DateTime Date { get; set; }

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x00102CA2 File Offset: 0x00100EA2
		// (set) Token: 0x06003CE5 RID: 15589 RVA: 0x00102CAA File Offset: 0x00100EAA
		[Column(Name = "NAME")]
		public string Name { get; set; }

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x00102CB3 File Offset: 0x00100EB3
		// (set) Token: 0x06003CE7 RID: 15591 RVA: 0x00102CBB File Offset: 0x00100EBB
		[Column(Name = "VERSION")]
		public string Version { get; set; }

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06003CE8 RID: 15592 RVA: 0x00102CC4 File Offset: 0x00100EC4
		// (set) Token: 0x06003CE9 RID: 15593 RVA: 0x00102CCC File Offset: 0x00100ECC
		[Column(Name = "COUNT")]
		public long Count { get; set; }

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06003CEA RID: 15594 RVA: 0x00102CD5 File Offset: 0x00100ED5
		// (set) Token: 0x06003CEB RID: 15595 RVA: 0x00102CDD File Offset: 0x00100EDD
		[Column(Name = "LASTACCESSTIME")]
		public DateTime LastAccessTime { get; set; }

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06003CEC RID: 15596 RVA: 0x00102CE6 File Offset: 0x00100EE6
		// (set) Token: 0x06003CED RID: 15597 RVA: 0x00102CEE File Offset: 0x00100EEE
		[Column(Name = "OBJECTID")]
		public Guid ObjectId { get; set; }

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06003CEE RID: 15598 RVA: 0x00102CF7 File Offset: 0x00100EF7
		// (set) Token: 0x06003CEF RID: 15599 RVA: 0x00102CFF File Offset: 0x00100EFF
		[SuppressPii(PiiDataType = PiiDataType.Smtp)]
		[Column(Name = "UPN")]
		public string UPN { get; set; }

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x00102D08 File Offset: 0x00100F08
		// (set) Token: 0x06003CF1 RID: 15601 RVA: 0x00102D10 File Offset: 0x00100F10
		[Column(Name = "DISPLAYNAME")]
		[SuppressPii(PiiDataType = PiiDataType.String)]
		public string DisplayName { get; set; }
	}
}
