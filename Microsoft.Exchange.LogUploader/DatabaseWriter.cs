using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200000C RID: 12
	internal abstract class DatabaseWriter<T> where T : LogDataBatch
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00003ED3 File Offset: 0x000020D3
		public DatabaseWriter(ThreadSafeQueue<T> queue, int id, ConfigInstance config, string instanceName)
		{
			this.InitializeDatabaseWriter(queue, id, config, instanceName);
			this.perfCounterInstance = PerfCountersInstanceCache.GetInstance(this.instance);
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003EF7 File Offset: 0x000020F7
		public bool Stopped
		{
			get
			{
				return this.stopped;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003EFF File Offset: 0x000020FF
		public ThreadSafeQueue<T> Queue
		{
			get
			{
				return this.queue;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003F07 File Offset: 0x00002107
		public string Instance
		{
			get
			{
				return this.instance;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003F0F File Offset: 0x0000210F
		public TimeSpan SleepTimeForTransientDBError
		{
			get
			{
				return this.config.SleepTimeForTransientDBError;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003F1C File Offset: 0x0000211C
		public int RetryCount
		{
			get
			{
				return this.config.RetryCount;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003F29 File Offset: 0x00002129
		public int RetriesBeforeAlert
		{
			get
			{
				return this.config.RetriesBeforeAlert;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003F36 File Offset: 0x00002136
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003F3E File Offset: 0x0000213E
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003F46 File Offset: 0x00002146
		public CancellationContext CancellationContext
		{
			get
			{
				return this.cancellationContext;
			}
			internal set
			{
				this.cancellationContext = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00003F4F File Offset: 0x0000214F
		protected bool IsStopRequested
		{
			set
			{
				this.isStopRequested = value;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003F58 File Offset: 0x00002158
		public void SetLogMonitorInterface(ILogManager logManager)
		{
			this.logManager = logManager;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003F64 File Offset: 0x00002164
		public void DoWork(object obj)
		{
			ArgumentValidator.ThrowIfNull("obj", obj);
			if (string.IsNullOrEmpty(Thread.CurrentThread.Name))
			{
				Thread.CurrentThread.Name = string.Format("Writer {0} for log type {1}", this.id, this.instance);
			}
			ServiceLogger.LogDebug(ServiceLogger.Component.DatabaseWriter, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("DoWork: called : thread={0}, log={1}  ?---LogWriter", Thread.CurrentThread.ManagedThreadId, this.instance), "", "");
			this.cancellationContext = (CancellationContext)obj;
			while (!this.CheckServiceStopRequest("DoWork()"))
			{
				this.DequeueAndWriteOneItem();
			}
			string message = string.Format("Writer {0} for log type {1} stopped.", this.id, this.instance);
			ExTraceGlobals.WriterTracer.TraceDebug((long)this.GetHashCode(), message);
			ServiceLogger.LogInfo(ServiceLogger.Component.DatabaseWriter, LogUploaderEventLogConstants.Message.LogMonitorRequestedStop, Thread.CurrentThread.Name, this.instance, "");
			this.stopped = true;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000405C File Offset: 0x0000225C
		internal void DequeueAndWriteOneItem()
		{
			string text = string.Empty;
			IWatermarkFile watermarkFile = null;
			try
			{
				T t = this.queue.Dequeue(this.cancellationContext);
				if (t != null)
				{
					this.perfCounterInstance.BatchQueueLength.Decrement();
					text = t.FullLogName;
					watermarkFile = this.logManager.FindWatermarkFileObject(text);
					if (ExTraceGlobals.WriterTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (LogFileRange logFileRange in t.LogRanges)
						{
							string value = string.Format("({0}, {1}) ", logFileRange.StartOffset, logFileRange.EndOffset);
							stringBuilder.Append(value);
						}
						string message = string.Format("For log {0}, writer {1} get batch that has {2} ranges: {3}", new object[]
						{
							text,
							this.id,
							t.LogRanges.Count,
							stringBuilder.ToString()
						});
						ExTraceGlobals.WriterTracer.TraceDebug((long)this.GetHashCode(), message);
					}
					this.WriteBatch(t);
				}
				else if (!this.CheckServiceStopRequest("DequeueAndWriteOneItem()"))
				{
					Tools.DebugAssert(this.cancellationContext.StopToken.IsCancellationRequested, "If Enqueue is not cancelled, it should always get a batch back");
				}
			}
			catch (FaultException ex)
			{
				ExTraceGlobals.WriterTracer.TraceError<string, string>((long)this.GetHashCode(), "Writer failed to write data through web service DAL when processing log {0}. Check the event log on the FFO web service role. The exception is: {1}.", text, ex.Message);
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_WebServiceWriteException, ex.Message, new object[]
				{
					text,
					ex.ToString()
				});
				ServiceLogger.LogError(ServiceLogger.Component.DatabaseWriter, (LogUploaderEventLogConstants.Message)3221489634U, ex.Message, this.instance, text);
			}
			catch (CommunicationException ex2)
			{
				ExTraceGlobals.WriterTracer.TraceError<string, string>((long)this.GetHashCode(), "Writer failed to write data through web service DAL when processing log {0}. The exception is: {1}.", text, ex2.Message);
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_WebServiceWriteException, ex2.Message, new object[]
				{
					text,
					ex2.ToString()
				});
				ServiceLogger.LogError(ServiceLogger.Component.DatabaseWriter, (LogUploaderEventLogConstants.Message)3221489633U, ex2.Message, this.instance, text);
			}
			catch (ADTopologyEndpointNotFoundException ex3)
			{
				ExTraceGlobals.WriterTracer.TraceError<string, string>((long)this.GetHashCode(), "Writer caught an ADTopologyEndpointNotFoundException when processing log {0}. Details is: {1}", text, ex3.Message);
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_ADTopologyEndpointNotFound, ex3.Message, new object[]
				{
					text,
					ex3.Message
				});
				ServiceLogger.LogError(ServiceLogger.Component.DatabaseWriter, (LogUploaderEventLogConstants.Message)3221489629U, ex3.Message, this.instance, text);
			}
			catch (Exception ex4)
			{
				if (!(ex4 is ThreadAbortException) || !this.cancellationContext.StopToken.IsCancellationRequested)
				{
					string text2 = string.Format("Writer caught an exception when processing log {0}: {1}", text, ex4);
					ExTraceGlobals.WriterTracer.TraceError((long)this.GetHashCode(), text2);
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_DatabaseWriterUnknownException, ex4.Message, new object[]
					{
						text,
						ex4
					});
					EventNotificationItem.Publish(ExchangeComponent.Name, "UnexpectedWriterError", null, text2, ResultSeverityLevel.Error, false);
					this.perfCounterInstance.TotalUnexpectedWriterErrors.Increment();
					ServiceLogger.LogError(ServiceLogger.Component.DatabaseWriter, (LogUploaderEventLogConstants.Message)3221489617U, text2, this.instance, text);
					throw;
				}
				Thread.ResetAbort();
				ServiceLogger.LogInfo(ServiceLogger.Component.DatabaseWriter, LogUploaderEventLogConstants.Message.LogMonitorRequestedStop, Thread.CurrentThread.Name + " received thread abort event", this.instance, "");
			}
			finally
			{
				if (watermarkFile != null)
				{
					watermarkFile.InMemoryCountDecrease();
				}
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000449C File Offset: 0x0000269C
		internal void WriteBatch(T batch)
		{
			bool flag = false;
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				ExTraceGlobals.WriterTracer.TraceDebug<string>((long)this.GetHashCode(), "Start writing batch to db for log {0}", this.instance);
				flag = (batch.NumberOfLinesInBatch == 0 || this.WriteBatchDataToDataStore(batch));
				if (flag && !this.isStopRequested)
				{
					this.UpdateWatermark(batch);
				}
			}
			finally
			{
				stopwatch.Stop();
				this.perfCounterInstance.TotalDBWrite.Increment();
				if (batch.NumberOfLinesInBatch != 0)
				{
					this.perfCounterInstance.AverageDbWriteLatencyBase.Increment();
					this.perfCounterInstance.AverageDbWriteLatency.IncrementBy(stopwatch.ElapsedTicks);
				}
				if (flag)
				{
					if (Tools.IsRawProcessingType<T>())
					{
						this.perfCounterInstance.RawWrittenBytes.IncrementBy(batch.Size);
					}
					else
					{
						this.perfCounterInstance.TotalLogBytesProcessed.IncrementBy(batch.Size);
					}
				}
			}
		}

		// Token: 0x060000D1 RID: 209
		protected abstract bool WriteBatchDataToDataStore(T batch);

		// Token: 0x060000D2 RID: 210 RVA: 0x000045A8 File Offset: 0x000027A8
		protected void InitializeDatabaseWriter(ThreadSafeQueue<T> queue, int id, ConfigInstance config, string instanceName)
		{
			ArgumentValidator.ThrowIfNull("queue", queue);
			ArgumentValidator.ThrowIfNull("config", config);
			ArgumentValidator.ThrowIfNullOrEmpty("instanceName", instanceName);
			if (id < 0)
			{
				throw new ArgumentOutOfRangeException("id should be equal or greater than 0.");
			}
			this.queue = queue;
			this.config = config;
			this.id = id;
			this.stopped = false;
			this.instance = instanceName;
			this.isStopRequested = false;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004614 File Offset: 0x00002814
		protected bool CheckServiceStopRequest(string caller)
		{
			if (this.CancellationContext.StopToken.IsCancellationRequested)
			{
				ExTraceGlobals.WriterTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "At {0}, received stop request in {1}", DateTime.UtcNow, caller);
				this.CancellationContext.StopWaitHandle.Set();
				this.IsStopRequested = true;
				return true;
			}
			return false;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004670 File Offset: 0x00002870
		private void UpdateWatermark(T batch)
		{
			string fullLogName = batch.FullLogName;
			IWatermarkFile watermarkFile = this.logManager.FindWatermarkFileObject(fullLogName);
			if (watermarkFile != null)
			{
				watermarkFile.WriteWatermark(batch.LogRanges);
				return;
			}
			string message = string.Format("DatabaseWriter failed to get a watermark instance for {0}", fullLogName);
			ExTraceGlobals.WriterTracer.TraceError((long)this.GetHashCode(), message);
			EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_MissingWatermark, fullLogName, new object[]
			{
				fullLogName
			});
			Tools.DebugAssert(false, "WatermarkFile.GetInstanceBasedOnLogName " + fullLogName);
		}

		// Token: 0x04000060 RID: 96
		private int id;

		// Token: 0x04000061 RID: 97
		private ThreadSafeQueue<T> queue;

		// Token: 0x04000062 RID: 98
		private bool stopped;

		// Token: 0x04000063 RID: 99
		private string instance;

		// Token: 0x04000064 RID: 100
		private CancellationContext cancellationContext;

		// Token: 0x04000065 RID: 101
		private bool isStopRequested;

		// Token: 0x04000066 RID: 102
		private ConfigInstance config;

		// Token: 0x04000067 RID: 103
		private ILogUploaderPerformanceCounters perfCounterInstance;

		// Token: 0x04000068 RID: 104
		private ILogManager logManager;
	}
}
