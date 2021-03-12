using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000161 RID: 353
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MoveSubscriptionSnapshot : SubscriptionSnapshot
	{
		// Token: 0x06001131 RID: 4401 RVA: 0x00048364 File Offset: 0x00046564
		public MoveSubscriptionSnapshot(ISubscriptionId id, SnapshotStatus status, bool isInitialSyncComplete, ExDateTime createTime, ExDateTime? lastUpdateTime, ExDateTime? lastSyncTime, LocalizedString? errorMessage, string batchName, MigrationBatchDirection direction, bool primaryOnly, bool archiveOnly, string targetDatabase, string targetArchiveDatabase) : base(id, status, isInitialSyncComplete, createTime, lastUpdateTime, lastSyncTime, errorMessage, batchName)
		{
			this.Direction = direction;
			this.PrimaryOnly = primaryOnly;
			this.ArchiveOnly = archiveOnly;
			this.TargetDatabase = targetDatabase;
			this.TargetArchiveDatabase = targetArchiveDatabase;
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x000483AC File Offset: 0x000465AC
		// (set) Token: 0x06001133 RID: 4403 RVA: 0x000483B4 File Offset: 0x000465B4
		public MigrationBatchDirection Direction { get; private set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x000483BD File Offset: 0x000465BD
		// (set) Token: 0x06001135 RID: 4405 RVA: 0x000483C5 File Offset: 0x000465C5
		public bool PrimaryOnly { get; private set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x000483CE File Offset: 0x000465CE
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x000483D6 File Offset: 0x000465D6
		public bool ArchiveOnly { get; private set; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x000483DF File Offset: 0x000465DF
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x000483E7 File Offset: 0x000465E7
		public string TargetDatabase { get; private set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x000483F0 File Offset: 0x000465F0
		// (set) Token: 0x0600113B RID: 4411 RVA: 0x000483F8 File Offset: 0x000465F8
		public string TargetArchiveDatabase { get; private set; }
	}
}
