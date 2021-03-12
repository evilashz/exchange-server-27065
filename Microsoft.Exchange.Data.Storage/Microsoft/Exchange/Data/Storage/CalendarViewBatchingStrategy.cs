using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003B8 RID: 952
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CalendarViewBatchingStrategy
	{
		// Token: 0x06002B74 RID: 11124 RVA: 0x000AD4FD File Offset: 0x000AB6FD
		private CalendarViewBatchingStrategy(int? idealMaxCount, CalendarViewQueryResumptionPoint queryResumptionPoint)
		{
			this.idealMaxCount = idealMaxCount;
			this.queryResumptionPoint = queryResumptionPoint;
			this.ResetCount();
			this.instanceKeyIndex = -1;
			this.sortKeyIndex = -1;
			this.keyIndicesAreSet = false;
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000AD530 File Offset: 0x000AB730
		public static CalendarViewBatchingStrategy CreateNoneBatchingInstance()
		{
			return new CalendarViewBatchingStrategy(null, null);
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000AD54C File Offset: 0x000AB74C
		public static CalendarViewBatchingStrategy CreateNewBatchingInstance(int idealMaxCount)
		{
			return new CalendarViewBatchingStrategy(new int?(idealMaxCount), CalendarViewBatchingStrategy.CreateResumptionPointWithoutInstanceKey(false));
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000AD55F File Offset: 0x000AB75F
		public static CalendarViewBatchingStrategy CreateResumingInstance(int idealMaxCount, CalendarViewQueryResumptionPoint resumptionPoint)
		{
			Util.ThrowOnNullArgument(resumptionPoint, "resumptionPoint");
			return new CalendarViewBatchingStrategy(new int?(idealMaxCount), resumptionPoint);
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06002B78 RID: 11128 RVA: 0x000AD578 File Offset: 0x000AB778
		public bool ShouldBatch
		{
			get
			{
				return this.idealMaxCount != null;
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06002B79 RID: 11129 RVA: 0x000AD593 File Offset: 0x000AB793
		public bool ShouldQuerySingleInstanceMeetings
		{
			get
			{
				return this.queryResumptionPoint == null || this.queryResumptionPoint.ResumeToSingleInstanceMeetings;
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x000AD5AC File Offset: 0x000AB7AC
		public bool ReachedBatchSizeLimit
		{
			get
			{
				return this.idealMaxCount != null && this.idealMaxCount.Value <= this.addedItemsCount;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x000AD5E4 File Offset: 0x000AB7E4
		public CalendarViewQueryResumptionPoint ResumptionPoint
		{
			get
			{
				return this.queryResumptionPoint;
			}
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000AD5EC File Offset: 0x000AB7EC
		public void ResetCount()
		{
			this.addedItemsCount = 0;
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000AD5F8 File Offset: 0x000AB7F8
		public bool TryGetNextBatch(QueryResult result, int storageLimit, int defaultBatchSize, bool recurring, bool isFirstFetch, out object[][] nextBatch, out long getRowsTime)
		{
			getRowsTime = 0L;
			if (this.ShouldBatch && !this.keyIndicesAreSet)
			{
				throw new InvalidOperationException("When batching is required, the critical columns' indices should be set prior to calling this method.");
			}
			int currentBatchSize = this.GetCurrentBatchSize(storageLimit, defaultBatchSize);
			bool flag;
			if (currentBatchSize == 0)
			{
				object[][] rows = result.GetRows(1);
				if (rows.Length > 0)
				{
					if (this.ShouldBatch)
					{
						this.ResetQueryResumptionPoint(rows[0], recurring);
					}
					ExTraceGlobals.StorageTracer.TraceDebug<string, int, bool>((long)this.GetHashCode(), "{0}. Batch size is equal to zero; terminating the process... (items in view: {1}; RecurringPhase: {2}).", "CalendarViewBatchingStrategy::TryGetNextBatch", this.addedItemsCount, recurring);
				}
				else
				{
					if (this.ShouldBatch)
					{
						this.PhaseComplete();
					}
					ExTraceGlobals.StorageTracer.TraceDebug<string, int, bool>((long)this.GetHashCode(), "{0}. Batch size is equal to zero, and there are no more rows to process (items in view: {1}; RecurringPhase: {2}).", "CalendarViewBatchingStrategy::TryGetNextBatch", this.addedItemsCount, recurring);
				}
				nextBatch = null;
				flag = false;
			}
			else if (this.ShouldBatch && isFirstFetch)
			{
				flag = this.TryResumeQueryOrStartFromBeginning(result, currentBatchSize, recurring, out nextBatch);
			}
			else
			{
				ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "{0}. Getting a batch with the size of {1} (items in view: {2}; RecurringPhase: {3}).", new object[]
				{
					"CalendarViewBatchingStrategy::TryGetNextBatch",
					currentBatchSize,
					this.addedItemsCount,
					recurring
				});
				Stopwatch stopwatch = Stopwatch.StartNew();
				try
				{
					nextBatch = result.GetRows(currentBatchSize, out flag);
				}
				finally
				{
					stopwatch.Stop();
					getRowsTime = stopwatch.ElapsedMilliseconds;
				}
			}
			if (this.ShouldBatch && currentBatchSize != 0 && !flag)
			{
				this.PhaseComplete();
			}
			return flag;
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000AD768 File Offset: 0x000AB968
		public void SetColumnIndices(int instanceKeyIndex, int sortKeyIndex)
		{
			this.instanceKeyIndex = instanceKeyIndex;
			this.sortKeyIndex = sortKeyIndex;
			this.keyIndicesAreSet = true;
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000AD780 File Offset: 0x000AB980
		public bool ShouldAddNewRow(object[] newRow, bool recurring)
		{
			bool result = true;
			if (this.ShouldBatch && this.addedItemsCount >= this.idealMaxCount)
			{
				result = false;
				ExTraceGlobals.StorageTracer.TraceDebug<string, int, bool>((long)this.GetHashCode(), "{0}. Hit the max count. Terminating the batch and resetting the resumption point (items in view: {1}; RecurringPhase: {2}).", "CalendarViewBatchingStrategy::ShouldAddNewRow", this.addedItemsCount, recurring);
				this.ResetQueryResumptionPoint(newRow, recurring);
			}
			return result;
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000AD7E8 File Offset: 0x000AB9E8
		public void AddNewRow(IList<object[]> results, object[] newRow)
		{
			this.addedItemsCount++;
			results.Add(newRow);
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000AD800 File Offset: 0x000ABA00
		private static CalendarViewQueryResumptionPoint CreateResumptionPointWithoutInstanceKey(bool recurring)
		{
			return CalendarViewQueryResumptionPoint.CreateInstance(recurring, null, null);
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000AD81D File Offset: 0x000ABA1D
		private void PhaseComplete()
		{
			this.queryResumptionPoint = CalendarViewBatchingStrategy.CreateResumptionPointWithoutInstanceKey(this.queryResumptionPoint.ResumeToSingleInstanceMeetings);
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000AD835 File Offset: 0x000ABA35
		private void ResetQueryResumptionPoint(object[] row, bool recurring)
		{
			this.queryResumptionPoint = CalendarViewQueryResumptionPoint.CreateInstance(recurring, row[this.instanceKeyIndex] as byte[], row[this.sortKeyIndex] as ExDateTime?);
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000AD864 File Offset: 0x000ABA64
		private int GetCurrentBatchSize(int storageLimit, int defaultBatchSize)
		{
			int result;
			if (storageLimit > this.addedItemsCount)
			{
				int val = storageLimit - this.addedItemsCount;
				if (this.ShouldBatch)
				{
					if (this.idealMaxCount.Value > this.addedItemsCount)
					{
						int val2 = this.idealMaxCount.Value - this.addedItemsCount;
						result = Math.Min(defaultBatchSize, Math.Min(val2, val));
					}
					else
					{
						result = 0;
					}
				}
				else
				{
					result = Math.Min(defaultBatchSize, val);
				}
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000AD8DC File Offset: 0x000ABADC
		private bool TryResumeQueryOrStartFromBeginning(QueryResult result, int currentBatchSize, bool recurring, out object[][] nextBatch)
		{
			bool flag = this.queryResumptionPoint.TryResume(result, this.sortKeyIndex, SeekReference.OriginBeginning, currentBatchSize, out nextBatch);
			ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "{0}. Resumption to the previous point was {1} (Batch Size: {2}; Items Already in View: {3}; RecurringPhase: {4}).", new object[]
			{
				"CalendarViewBatchingStrategy::TryGetNextBatch",
				flag ? "Successful" : "Unsuccessful",
				currentBatchSize,
				this.addedItemsCount,
				recurring
			});
			if (!flag)
			{
				ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "{0}. Starting from scratch and getting a batch with the size of {1} (items in view: {2}; RecurringPhase: {3}).", new object[]
				{
					"CalendarViewBatchingStrategy::TryGetNextBatch",
					currentBatchSize,
					this.addedItemsCount,
					recurring
				});
				result.SeekToOffset(SeekReference.OriginBeginning, 0);
				nextBatch = result.GetRows(currentBatchSize, out flag);
			}
			return flag;
		}

		// Token: 0x0400184F RID: 6223
		private readonly int? idealMaxCount;

		// Token: 0x04001850 RID: 6224
		private int addedItemsCount;

		// Token: 0x04001851 RID: 6225
		private CalendarViewQueryResumptionPoint queryResumptionPoint;

		// Token: 0x04001852 RID: 6226
		private int instanceKeyIndex;

		// Token: 0x04001853 RID: 6227
		private int sortKeyIndex;

		// Token: 0x04001854 RID: 6228
		private bool keyIndicesAreSet;
	}
}
