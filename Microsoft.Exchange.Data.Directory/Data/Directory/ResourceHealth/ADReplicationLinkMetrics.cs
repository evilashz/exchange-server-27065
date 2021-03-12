using System;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009EB RID: 2539
	internal sealed class ADReplicationLinkMetrics
	{
		// Token: 0x060075D2 RID: 30162 RVA: 0x00183B81 File Offset: 0x00181D81
		public ADReplicationLinkMetrics(string partnerDnsHostName, long upToDatenessUsn)
		{
			this.NeighborDnsHostName = partnerDnsHostName;
			this.UpToDatenessUsn = upToDatenessUsn;
		}

		// Token: 0x17002A35 RID: 10805
		// (get) Token: 0x060075D3 RID: 30163 RVA: 0x00183B97 File Offset: 0x00181D97
		// (set) Token: 0x060075D4 RID: 30164 RVA: 0x00183B9F File Offset: 0x00181D9F
		public string NeighborDnsHostName { get; private set; }

		// Token: 0x17002A36 RID: 10806
		// (get) Token: 0x060075D5 RID: 30165 RVA: 0x00183BA8 File Offset: 0x00181DA8
		// (set) Token: 0x060075D6 RID: 30166 RVA: 0x00183BB0 File Offset: 0x00181DB0
		public long UpToDatenessUsn { get; private set; }

		// Token: 0x17002A37 RID: 10807
		// (get) Token: 0x060075D7 RID: 30167 RVA: 0x00183BB9 File Offset: 0x00181DB9
		// (set) Token: 0x060075D8 RID: 30168 RVA: 0x00183BC1 File Offset: 0x00181DC1
		public long Debt { get; set; }
	}
}
