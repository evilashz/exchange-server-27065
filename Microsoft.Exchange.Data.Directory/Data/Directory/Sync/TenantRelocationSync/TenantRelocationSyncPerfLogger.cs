using System;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x0200080B RID: 2059
	internal class TenantRelocationSyncPerfLogger
	{
		// Token: 0x060065A6 RID: 26022 RVA: 0x00164561 File Offset: 0x00162761
		internal TenantRelocationSyncPerfLogger(TenantRelocationSyncData syncData)
		{
			this.syncData = syncData;
			this.timestampOfLastCheckpoint = DateTime.UtcNow;
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x0016457B File Offset: 0x0016277B
		internal void IncrementPlaceHolderCount()
		{
			this.placeholderCount++;
			this.IncrementProcessedObjectCount();
		}

		// Token: 0x060065A8 RID: 26024 RVA: 0x00164591 File Offset: 0x00162791
		internal void IncrementUpdateCount()
		{
			this.updateCount++;
			this.IncrementProcessedObjectCount();
		}

		// Token: 0x060065A9 RID: 26025 RVA: 0x001645A7 File Offset: 0x001627A7
		internal void IncrementDeleteCount()
		{
			this.deleteCount++;
			this.IncrementProcessedObjectCount();
		}

		// Token: 0x060065AA RID: 26026 RVA: 0x001645BD File Offset: 0x001627BD
		internal void IncrementRenameCount()
		{
			this.renameCount++;
			this.IncrementProcessedObjectCount();
		}

		// Token: 0x060065AB RID: 26027 RVA: 0x001645D3 File Offset: 0x001627D3
		internal void AddLinkCount(int delta)
		{
			this.linkCount += delta;
		}

		// Token: 0x060065AC RID: 26028 RVA: 0x001645E3 File Offset: 0x001627E3
		internal void IncrementPageCount()
		{
			this.pageCount++;
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x001645F4 File Offset: 0x001627F4
		internal void Flush()
		{
			DateTime utcNow = DateTime.UtcNow;
			TimeSpan timeSpan = utcNow - this.timestampOfLastCheckpoint;
			int num = this.processedObjectCount % TenantRelocationSyncPerfLogger.CheckpointObjectNumber;
			num = ((num == 0) ? TenantRelocationSyncPerfLogger.CheckpointObjectNumber : num);
			double num2 = (timeSpan.TotalSeconds == 0.0) ? -1.0 : ((double)num / timeSpan.TotalSeconds);
			TenantRelocationSyncLogger.Instance.Log(this.syncData, "Info", null, string.Format("Page No. {0}: {1} objects processed, {2} placeholders, {3} updates, {4} deletions, {5} renames, {6} links, rate: {7} objects/s", new object[]
			{
				this.pageCount,
				this.processedObjectCount,
				this.placeholderCount,
				this.updateCount,
				this.deleteCount,
				this.renameCount,
				this.linkCount,
				num2
			}), null);
		}

		// Token: 0x060065AE RID: 26030 RVA: 0x001646F3 File Offset: 0x001628F3
		private void IncrementProcessedObjectCount()
		{
			this.processedObjectCount++;
			if (this.processedObjectCount % TenantRelocationSyncPerfLogger.CheckpointObjectNumber == 0)
			{
				this.Flush();
				this.timestampOfLastCheckpoint = DateTime.UtcNow;
			}
		}

		// Token: 0x04004361 RID: 17249
		private static readonly int CheckpointObjectNumber = 100;

		// Token: 0x04004362 RID: 17250
		private TenantRelocationSyncData syncData;

		// Token: 0x04004363 RID: 17251
		private int pageCount;

		// Token: 0x04004364 RID: 17252
		private int processedObjectCount;

		// Token: 0x04004365 RID: 17253
		private int placeholderCount;

		// Token: 0x04004366 RID: 17254
		private int deleteCount;

		// Token: 0x04004367 RID: 17255
		private int renameCount;

		// Token: 0x04004368 RID: 17256
		private int linkCount;

		// Token: 0x04004369 RID: 17257
		private int updateCount;

		// Token: 0x0400436A RID: 17258
		private DateTime timestampOfLastCheckpoint;
	}
}
