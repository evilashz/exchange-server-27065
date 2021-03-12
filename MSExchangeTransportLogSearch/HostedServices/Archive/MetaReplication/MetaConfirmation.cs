using System;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x02000053 RID: 83
	public abstract class MetaConfirmation
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000BB1C File Offset: 0x00009D1C
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000BB24 File Offset: 0x00009D24
		public ReplicationStatus Status { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000BB2D File Offset: 0x00009D2D
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000BB35 File Offset: 0x00009D35
		public int CustomerId { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000BB3E File Offset: 0x00009D3E
		// (set) Token: 0x060001BF RID: 447 RVA: 0x0000BB46 File Offset: 0x00009D46
		public string DatacenterName { get; set; }

		// Token: 0x060001C0 RID: 448
		public abstract void Confirm(IReplicationService service);
	}
}
