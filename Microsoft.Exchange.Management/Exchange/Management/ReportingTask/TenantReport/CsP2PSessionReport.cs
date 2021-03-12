using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.Query;

namespace Microsoft.Exchange.Management.ReportingTask.TenantReport
{
	// Token: 0x020006C7 RID: 1735
	[DataServiceKey("Date")]
	[Table(Name = "dbo.CsP2PSessionDaily")]
	[Serializable]
	public class CsP2PSessionReport : ReportObject, IDateColumn, ITenantColumn
	{
		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06003D8F RID: 15759 RVA: 0x0010324A File Offset: 0x0010144A
		// (set) Token: 0x06003D90 RID: 15760 RVA: 0x00103252 File Offset: 0x00101452
		[Column(Name = "ID")]
		public long ID { get; set; }

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06003D91 RID: 15761 RVA: 0x0010325B File Offset: 0x0010145B
		// (set) Token: 0x06003D92 RID: 15762 RVA: 0x00103263 File Offset: 0x00101463
		[Column(Name = "DateTime")]
		public DateTime Date { get; set; }

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06003D93 RID: 15763 RVA: 0x0010326C File Offset: 0x0010146C
		// (set) Token: 0x06003D94 RID: 15764 RVA: 0x00103274 File Offset: 0x00101474
		[Column(Name = "TenantGuid")]
		public Guid TenantGuid { get; set; }

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06003D95 RID: 15765 RVA: 0x0010327D File Offset: 0x0010147D
		// (set) Token: 0x06003D96 RID: 15766 RVA: 0x00103285 File Offset: 0x00101485
		[Column(Name = "TenantName")]
		public string TenantName { get; set; }

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06003D97 RID: 15767 RVA: 0x0010328E File Offset: 0x0010148E
		// (set) Token: 0x06003D98 RID: 15768 RVA: 0x00103296 File Offset: 0x00101496
		[Column(Name = "TotalP2PSessions")]
		public long? TotalP2PSessions { get; set; }

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06003D99 RID: 15769 RVA: 0x0010329F File Offset: 0x0010149F
		// (set) Token: 0x06003D9A RID: 15770 RVA: 0x001032A7 File Offset: 0x001014A7
		[Column(Name = "P2PIMSessions")]
		public long? P2PIMSessions { get; set; }

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06003D9B RID: 15771 RVA: 0x001032B0 File Offset: 0x001014B0
		// (set) Token: 0x06003D9C RID: 15772 RVA: 0x001032B8 File Offset: 0x001014B8
		[Column(Name = "P2PAudioSessions")]
		public long? P2PAudioSessions { get; set; }

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06003D9D RID: 15773 RVA: 0x001032C1 File Offset: 0x001014C1
		// (set) Token: 0x06003D9E RID: 15774 RVA: 0x001032C9 File Offset: 0x001014C9
		[Column(Name = "P2PVideoSessions")]
		public long? P2PVideoSessions { get; set; }

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06003D9F RID: 15775 RVA: 0x001032D2 File Offset: 0x001014D2
		// (set) Token: 0x06003DA0 RID: 15776 RVA: 0x001032DA File Offset: 0x001014DA
		[Column(Name = "P2PApplicationSharingSessions")]
		public long? P2PApplicationSharingSessions { get; set; }

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06003DA1 RID: 15777 RVA: 0x001032E3 File Offset: 0x001014E3
		// (set) Token: 0x06003DA2 RID: 15778 RVA: 0x001032EB File Offset: 0x001014EB
		[Column(Name = "P2PFileTransferSessions")]
		public long? P2PFileTransferSessions { get; set; }
	}
}
