using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000015 RID: 21
	internal class InputBuffer<T> : IDisposable where T : LogDataBatch
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00004814 File Offset: 0x00002A14
		public InputBuffer(int batchSizeInBytes, long beginOffset, ILogFileInfo logFileInfoObj, ThreadSafeQueue<T> logDataBatchQueue, string prefix, ILogMonitorHelper<T> logMonitorHelper, int messageBatchFlushInterval, CancellationContext cancelContext, int maxBatchCount, string instanceName = null)
		{
			if (batchSizeInBytes <= 0)
			{
				throw new ArgumentOutOfRangeException("batchSizeInByte", "The batch size should be greater than 0.");
			}
			if (beginOffset < 0L)
			{
				throw new ArgumentOutOfRangeException("beginOffset", "The beginOffset should be equal or greater than 0.");
			}
			if (logDataBatchQueue == null)
			{
				throw new ArgumentNullException("logDataBatchQueue cannot be null.");
			}
			if (messageBatchFlushInterval < 0)
			{
				throw new ArgumentOutOfRangeException("messageBatchFlushInterval", "The messageBatchFlushInterval must not be a negative number.");
			}
			ArgumentValidator.ThrowIfNull("logFileInfoObj", logFileInfoObj);
			ArgumentValidator.ThrowIfNull("watermarkFileRef", logFileInfoObj.WatermarkFileObj);
			if (string.IsNullOrEmpty(logFileInfoObj.FullFileName))
			{
				throw new ArgumentException("fullLogName cannot be null or emtpy.");
			}
			ArgumentValidator.ThrowIfNull("cancelContext", cancelContext);
			this.cancellationContext = cancelContext;
			this.messageBatchFlushInterval = messageBatchFlushInterval;
			this.batchSizeInBytes = batchSizeInBytes;
			this.logDataBatchQueue = logDataBatchQueue;
			this.watermarkFileRef = logFileInfoObj.WatermarkFileObj;
			this.maximumBatchCount = ((maxBatchCount <= 0) ? int.MaxValue : maxBatchCount);
			this.lastFluchCheckTime = DateTime.UtcNow;
			this.logMonitorHelper = logMonitorHelper;
			this.fullLogName = logFileInfoObj.FullFileName;
			this.instance = (string.IsNullOrEmpty(instanceName) ? prefix : instanceName);
			this.logPrefix = prefix;
			this.perfCounterInstance = PerfCountersInstanceCache.GetInstance(this.instance);
			this.shouldBufferBatches = this.ShouldBufferBatches();
			this.CreateNewBatch(beginOffset);
			if (this.shouldBufferBatches)
			{
				MessageBatchBase messageBatchBase = this.activeBatch as MessageBatchBase;
				if (messageBatchBase != null)
				{
					messageBatchBase.MessageBatchFlushInterval = this.messageBatchFlushInterval;
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004990 File Offset: 0x00002B90
		public IWatermarkFile UnitTestGetWatermarkFileObjectReference
		{
			get
			{
				return this.watermarkFileRef;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004998 File Offset: 0x00002B98
		public string FullLogName
		{
			get
			{
				return this.fullLogName;
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000049A0 File Offset: 0x00002BA0
		public void EnqueueBatch(T batch)
		{
			if (ExTraceGlobals.ReaderTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				for (int i = 0; i < batch.LogRanges.Count; i++)
				{
					string message = string.Format("InputBuffer: enqueueBatch range {0}: ({1}, {2}) for log {3}", new object[]
					{
						i,
						batch.LogRanges[i].StartOffset,
						batch.LogRanges[i].EndOffset,
						this.fullLogName
					});
					ExTraceGlobals.ReaderTracer.TraceDebug((long)this.GetHashCode(), message);
				}
			}
			long size = batch.Size;
			long num = (long)batch.NumberOfLinesInBatch;
			ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("before enqueue; batch size (in original log) {0}, lines {1} +++++++++ ", size, num), this.instance, this.fullLogName);
			this.watermarkFileRef.InMemoryCountIncrease();
			try
			{
				this.logDataBatchQueue.Enqueue(batch, this.cancellationContext);
			}
			catch
			{
				this.watermarkFileRef.InMemoryCountDecrease();
				throw;
			}
			if (this.cancellationContext.StopToken.IsCancellationRequested)
			{
				return;
			}
			ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("after enqueue; batch size (in original log) {0}, lines {1} +++++++++ ", size, num), this.instance, this.fullLogName);
			this.perfCounterInstance.InputBufferBatchCounts.Decrement();
			this.perfCounterInstance.BatchQueueLength.Increment();
			this.perfCounterInstance.InputBufferBackfilledLines.IncrementBy(this.newBackfilledLogLines);
			this.newBackfilledLogLines = 0L;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004B6C File Offset: 0x00002D6C
		public void AddInvalidRowToSkip(long startOffset, long endOffset)
		{
			this.activeBatch.UpdateCurrentRangeOffsets(startOffset, endOffset);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004B84 File Offset: 0x00002D84
		public void LineReceived(ReadOnlyRow row)
		{
			if (this.activeBatch.IsBatchFull(row))
			{
				this.FinishAndCreateNewBatch(row);
			}
			if (this.cancellationContext.StopToken.IsCancellationRequested)
			{
				return;
			}
			if (this.shouldBufferBatches)
			{
				this.MsgtrkLineReceivedHelper(row);
			}
			else
			{
				bool flag = this.activeBatch.LineReceived(row);
				if (flag)
				{
					this.activeBatch.UpdateCurrentRangeOffsets(row.Position, row.EndPosition);
				}
			}
			this.FlushMessageBatchBufferToWriter(false);
			Tools.DebugAssert(this.messageBatchBuffer.Count <= this.maximumBatchCount, "Verify the buffer size will never go above the maximumBatchCount");
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004C30 File Offset: 0x00002E30
		public void FinishAndCreateNewBatch(ReadOnlyRow row)
		{
			this.FinishBatch();
			this.CreateNewBatch(row.Position);
			MessageBatchBase messageBatchBase = this.activeBatch as MessageBatchBase;
			if (messageBatchBase != null)
			{
				messageBatchBase.MessageBatchFlushInterval = this.messageBatchFlushInterval;
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004C6F File Offset: 0x00002E6F
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004C86 File Offset: 0x00002E86
		public void BeforeDataBlockIsProcessed(LogFileRange block, bool isFirstBlock)
		{
			if (!isFirstBlock && this.activeBatch != null)
			{
				this.activeBatch.CreateNewRange(block.StartOffset);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004CAF File Offset: 0x00002EAF
		public void AfterDataBlockIsProcessed()
		{
			this.activeBatch.RemoveLastOpenRange();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004CC4 File Offset: 0x00002EC4
		internal T GetBatch()
		{
			if (this.logDataBatchQueue.Count == 0)
			{
				return default(T);
			}
			T t = this.logDataBatchQueue.Dequeue(null);
			if (t != null)
			{
				this.perfCounterInstance.BatchQueueLength.Decrement();
				return t;
			}
			return default(T);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004D1C File Offset: 0x00002F1C
		internal void FinishBatch()
		{
			if (this.activeBatch != null)
			{
				if (this.activeBatch.RangeCount > 0)
				{
					if (this.activeBatch.EndOffset == 2147483647L)
					{
						this.activeBatch.RemoveLastRange();
					}
					else
					{
						this.activeBatch.CurrentRange.ProcessingStatus = ProcessingStatus.ReadyToWriteToDatabase;
					}
					this.activeBatch.ProcessingStatus = ProcessingStatus.ReadyToWriteToDatabase;
					this.BufferOrEnqueueToWriter();
				}
				else
				{
					this.perfCounterInstance.InputBufferBatchCounts.Decrement();
				}
				this.activeBatch = default(T);
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004DF8 File Offset: 0x00002FF8
		internal void FlushMessageBatchBufferToWriter(bool forceFlush)
		{
			if (!this.shouldBufferBatches)
			{
				return;
			}
			if (!LogDataBatchReflectionCache<T>.IsMessageBatch)
			{
				return;
			}
			List<T> list = this.messageBatchBuffer;
			Tools.DebugAssert(list.Count <= this.maximumBatchCount, "Verify the buffer size will never goes above the maximumBatchCount");
			if (!forceFlush && (DateTime.UtcNow - this.lastFluchCheckTime).TotalMilliseconds < 1000.0 && list.Count < this.maximumBatchCount)
			{
				return;
			}
			this.lastFluchCheckTime = DateTime.UtcNow;
			foreach (T t in list)
			{
				MessageBatchBase messageBatchBase = t as MessageBatchBase;
				Tools.DebugAssert(messageBatchBase != null, "Failed cast to MessageBatchBase.");
				if (forceFlush || messageBatchBase.ReadyToFlush(this.newestLogLineTS))
				{
					ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("FlushMessageBatchBufferToWriter: called : thread={0}, forceFlush={1}, this.shouldBufferBatches={2}, bufferCount={3}, this.maximumBatchCount={4} ?---BufEnqueBatch ", new object[]
					{
						Thread.CurrentThread.ManagedThreadId,
						forceFlush,
						this.shouldBufferBatches,
						list.Count,
						this.maximumBatchCount
					}), "", "");
					this.EnqueueBatch(t);
					messageBatchBase.Flushed = true;
				}
			}
			list.RemoveAll(delegate(T batch)
			{
				MessageBatchBase messageBatchBase3 = batch as MessageBatchBase;
				Tools.DebugAssert(messageBatchBase3 != null, "Failed cast to MessageBatchBase.");
				return messageBatchBase3.Flushed;
			});
			while (list.Count >= this.maximumBatchCount)
			{
				ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("FlushMessageBatchBufferToWriter: called : thread={0}, forceFlush={1}, this.shouldBufferBatches={2}, bufferCount={3}, this.maximumBatchCount={4} ?---BufEnqueBatch2 ", new object[]
				{
					Thread.CurrentThread.ManagedThreadId,
					forceFlush,
					this.shouldBufferBatches,
					list.Count,
					this.maximumBatchCount
				}), "", "");
				this.EnqueueBatch(list[0]);
				MessageBatchBase messageBatchBase2 = list[0] as MessageBatchBase;
				Tools.DebugAssert(messageBatchBase2 != null, "Failed cast to MessageBatchBase.");
				messageBatchBase2.Flushed = true;
				list.RemoveAt(0);
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000505C File Offset: 0x0000325C
		private void BufferOrEnqueueToWriter()
		{
			if (!this.shouldBufferBatches)
			{
				ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("BufferOrEnqueueToWriter: called : thread={0} ?---BufEnque2 ", Thread.CurrentThread.ManagedThreadId), "", "");
				this.EnqueueBatch(this.activeBatch);
				return;
			}
			this.messageBatchBuffer.Add(this.activeBatch);
			Tools.DebugAssert(this.messageBatchBuffer.Count <= this.maximumBatchCount, "Verify the buffer size will never go above the maximumBatchCount");
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000050DE File Offset: 0x000032DE
		private bool ShouldBufferBatches()
		{
			return DatacenterRegistry.IsForefrontForOffice() && (this.logPrefix.StartsWith("MSGTRKSPLIT", StringComparison.OrdinalIgnoreCase) || this.logPrefix.StartsWith("MSGTRACECOMBOSPLIT", StringComparison.OrdinalIgnoreCase)) && this.messageBatchFlushInterval > 0;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005118 File Offset: 0x00003318
		private bool TryBackfillBufferedMessage(ParsedReadOnlyRow parsedRow)
		{
			if (!this.shouldBufferBatches)
			{
				return false;
			}
			ReadOnlyRow unParsedRow = parsedRow.UnParsedRow;
			foreach (T t in this.messageBatchBuffer)
			{
				MessageBatchBase messageBatchBase = t as MessageBatchBase;
				Tools.DebugAssert(messageBatchBase != null, "Failed cast to MessageBatchBase.");
				if (!messageBatchBase.ReachedDalOptimizationLimit() && messageBatchBase.ContainsMessage(parsedRow))
				{
					if (messageBatchBase.LineReceived(unParsedRow))
					{
						messageBatchBase.AddRangeToBufferedBatch(unParsedRow.Position, unParsedRow.EndPosition);
					}
					this.newBackfilledLogLines += 1L;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000051D8 File Offset: 0x000033D8
		private void MsgtrkLineReceivedHelper(ReadOnlyRow row)
		{
			bool flag = false;
			try
			{
				ParsedReadOnlyRow parsedReadOnlyRow = new ParsedReadOnlyRow(row);
				this.newestLogLineTS = parsedReadOnlyRow.GetField<DateTime>("date-time");
				if (this.TryBackfillBufferedMessage(parsedReadOnlyRow))
				{
					this.activeBatch.StartNewRangeForActiveBatch(row.EndPosition);
				}
				else
				{
					flag = this.activeBatch.LineReceived(row);
				}
			}
			catch (InvalidLogLineException exception)
			{
				this.activeBatch.LogErrorAndUpdatePerfCounter(row.Position, row.EndPosition, exception, LogUploaderEventLogConstants.Tuple_LogLineParseError, (LogUploaderEventLogConstants.Message)3221230473U, "InvalidLogLineInParse");
				flag = true;
			}
			catch (InvalidPropertyValueException exception2)
			{
				this.activeBatch.LogErrorAndUpdatePerfCounter(row.Position, row.EndPosition, exception2, LogUploaderEventLogConstants.Tuple_InvalidPropertyValueInParse, (LogUploaderEventLogConstants.Message)3221230481U, "InvalidPropertyValueInParse");
				flag = true;
			}
			catch (MissingPropertyException exception3)
			{
				this.activeBatch.LogErrorAndUpdatePerfCounter(row.Position, row.EndPosition, exception3, LogUploaderEventLogConstants.Tuple_MissingPropertyInParse, (LogUploaderEventLogConstants.Message)3221230480U, "MissingPropertyInParse");
				flag = true;
			}
			catch (InvalidCastException exception4)
			{
				this.activeBatch.LogErrorAndUpdatePerfCounter(row.Position, row.EndPosition, exception4, LogUploaderEventLogConstants.Tuple_InvalidCastInParse, (LogUploaderEventLogConstants.Message)3221230482U, "InvalidCastInParse");
				flag = true;
			}
			if (flag)
			{
				this.activeBatch.UpdateCurrentRangeOffsets(row.Position, row.EndPosition);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005360 File Offset: 0x00003560
		private void CreateNewBatch(long beginOffset)
		{
			this.activeBatch = this.logMonitorHelper.CreateBatch(this.batchSizeInBytes, beginOffset, this.fullLogName, this.logPrefix);
			this.activeBatch.Instance = this.instance;
			this.perfCounterInstance.InputBufferBatchCounts.Increment();
		}

		// Token: 0x0400007E RID: 126
		private readonly string fullLogName;

		// Token: 0x0400007F RID: 127
		private readonly int batchSizeInBytes;

		// Token: 0x04000080 RID: 128
		private readonly int messageBatchFlushInterval;

		// Token: 0x04000081 RID: 129
		private readonly string instance;

		// Token: 0x04000082 RID: 130
		private readonly string logPrefix;

		// Token: 0x04000083 RID: 131
		private readonly int maximumBatchCount;

		// Token: 0x04000084 RID: 132
		private readonly bool shouldBufferBatches;

		// Token: 0x04000085 RID: 133
		private T activeBatch;

		// Token: 0x04000086 RID: 134
		private List<T> messageBatchBuffer = new List<T>();

		// Token: 0x04000087 RID: 135
		private ThreadSafeQueue<T> logDataBatchQueue;

		// Token: 0x04000088 RID: 136
		private ILogMonitorHelper<T> logMonitorHelper;

		// Token: 0x04000089 RID: 137
		private DateTime newestLogLineTS = DateTime.MinValue;

		// Token: 0x0400008A RID: 138
		private bool disposed;

		// Token: 0x0400008B RID: 139
		private CancellationContext cancellationContext;

		// Token: 0x0400008C RID: 140
		private DateTime lastFluchCheckTime;

		// Token: 0x0400008D RID: 141
		private ILogUploaderPerformanceCounters perfCounterInstance;

		// Token: 0x0400008E RID: 142
		private long newBackfilledLogLines;

		// Token: 0x0400008F RID: 143
		private IWatermarkFile watermarkFileRef;
	}
}
