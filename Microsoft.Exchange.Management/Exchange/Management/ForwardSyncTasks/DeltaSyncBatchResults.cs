using System;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200035D RID: 861
	public class DeltaSyncBatchResults : DirectoryChanges, ISyncBatchResults
	{
		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x000826DF File Offset: 0x000808DF
		// (set) Token: 0x06001DC6 RID: 7622 RVA: 0x000826E7 File Offset: 0x000808E7
		public byte[] LastCookie { get; set; }

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x000826F0 File Offset: 0x000808F0
		// (set) Token: 0x06001DC8 RID: 7624 RVA: 0x000826F8 File Offset: 0x000808F8
		public string RawResponse { get; set; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x00082701 File Offset: 0x00080901
		// (set) Token: 0x06001DCA RID: 7626 RVA: 0x00082709 File Offset: 0x00080909
		public SyncBatchStatisticsBase Stats { get; set; }

		// Token: 0x06001DCB RID: 7627 RVA: 0x00082712 File Offset: 0x00080912
		public DeltaSyncBatchResults()
		{
			this.Stats = new DeltaSyncBatchStatistics();
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00082728 File Offset: 0x00080928
		public DeltaSyncBatchResults(DirectoryChanges Changes) : this()
		{
			base.NextCookie = Changes.NextCookie;
			base.Objects = Changes.Objects;
			base.Links = Changes.Links;
			base.Contexts = Changes.Contexts;
			base.More = Changes.More;
			base.OperationResultCode = Changes.OperationResultCode;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00082783 File Offset: 0x00080983
		public void CalculateStats()
		{
			(this.Stats as DeltaSyncBatchStatistics).Calculate(this);
		}
	}
}
