using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000151 RID: 337
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PSTImportSubscriptionSnapshot : SubscriptionSnapshot
	{
		// Token: 0x060010BE RID: 4286 RVA: 0x00046744 File Offset: 0x00044944
		public PSTImportSubscriptionSnapshot(ISubscriptionId id, SnapshotStatus status, bool isInitialSyncComplete, ExDateTime createTime, ExDateTime? lastUpdateTime, ExDateTime? lastSyncTime, LocalizedString? errorMessage, string batchName, bool primaryOnly, bool archiveOnly, string pstFilePath) : base(id, status, isInitialSyncComplete, createTime, lastUpdateTime, lastSyncTime, errorMessage, batchName)
		{
			this.PrimaryOnly = primaryOnly;
			this.ArchiveOnly = archiveOnly;
			this.PstFilePath = pstFilePath;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0004677C File Offset: 0x0004497C
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x00046784 File Offset: 0x00044984
		public bool PrimaryOnly { get; private set; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0004678D File Offset: 0x0004498D
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x00046795 File Offset: 0x00044995
		public bool ArchiveOnly { get; private set; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0004679E File Offset: 0x0004499E
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x000467A6 File Offset: 0x000449A6
		public string PstFilePath { get; set; }
	}
}
