using System;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200035E RID: 862
	public class TenantSyncBatchResults : DirectoryObjectsAndLinks, ISyncBatchResults
	{
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x00082796 File Offset: 0x00080996
		// (set) Token: 0x06001DCF RID: 7631 RVA: 0x0008279E File Offset: 0x0008099E
		public byte[] LastPageToken { get; set; }

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x000827A7 File Offset: 0x000809A7
		// (set) Token: 0x06001DD1 RID: 7633 RVA: 0x000827AF File Offset: 0x000809AF
		public string RawResponse { get; set; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x000827B8 File Offset: 0x000809B8
		// (set) Token: 0x06001DD3 RID: 7635 RVA: 0x000827C0 File Offset: 0x000809C0
		public SyncBatchStatisticsBase Stats { get; set; }

		// Token: 0x06001DD4 RID: 7636 RVA: 0x000827C9 File Offset: 0x000809C9
		public TenantSyncBatchResults()
		{
			this.Stats = new TenantSyncBatchStatistics();
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x000827DC File Offset: 0x000809DC
		public TenantSyncBatchResults(DirectoryObjectsAndLinks ObjectsAndLinks) : this()
		{
			base.Objects = ObjectsAndLinks.Objects;
			base.Links = ObjectsAndLinks.Links;
			base.More = ObjectsAndLinks.More;
			base.Errors = ObjectsAndLinks.Errors;
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00082814 File Offset: 0x00080A14
		public void CalculateStats()
		{
			(this.Stats as TenantSyncBatchStatistics).Calculate(this);
		}
	}
}
