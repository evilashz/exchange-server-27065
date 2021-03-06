using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200001E RID: 30
	internal class LogReader<T> where T : LogDataBatch
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x000080F4 File Offset: 0x000062F4
		public LogReader(ThreadSafeQueue<T> batchQueue, int id, ILogManager logMonitor, ConfigInstance config, string prefix, ILogMonitorHelper<T> logMonitorHelper, string instanceName = null)
		{
			ArgumentValidator.ThrowIfNull("batchQueue", batchQueue);
			ArgumentValidator.ThrowIfNull("logMonitor", logMonitor);
			ArgumentValidator.ThrowIfNull("config", config);
			ArgumentValidator.ThrowIfNull("logMonitorHelper", logMonitorHelper);
			ArgumentValidator.ThrowIfNullOrEmpty("prefix", prefix);
			if (id < 0)
			{
				throw new ArgumentOutOfRangeException("id cannot be negative.");
			}
			this.batchQueue = batchQueue;
			this.id = id;
			this.logMonitor = logMonitor;
			this.stopped = false;
			this.logPrefix = prefix;
			this.config = config;
			this.instance = (string.IsNullOrEmpty(instanceName) ? prefix : instanceName);
			this.messageBatchFlushInterval = (int)this.config.BatchFlushInterval.TotalSeconds;
			this.logMonitorHelper = logMonitorHelper;
			this.lastTimeWhenQeueFull = DateTime.UtcNow.Ticks;
			this.perfCounterInstance = PerfCountersInstanceCache.GetInstance(this.instance);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000081D7 File Offset: 0x000063D7
		public bool Stopped
		{
			get
			{
				return this.stopped;
			}
		}

		// Token: 0x170000BB RID: 187
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x000081DF File Offset: 0x000063DF
		internal CancellationContext CancellationContext
		{
			set
			{
				this.cancellationContext = value;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000081E8 File Offset: 0x000063E8
		public void DoWork(object obj)
		{
			ArgumentValidator.ThrowIfNull("obj", obj);
			this.cancellationContext = (CancellationContext)obj;
			CancellationToken stopToken = this.cancellationContext.StopToken;
			if (string.IsNullOrEmpty(Thread.CurrentThread.Name))
			{
				Thread.CurrentThread.Name = string.Format("Reader {0} for log prefix {1}", this.id, this.instance);
			}
			string text = string.Format("Reader {0} for log prefix {1} started", this.id, this.instance);
			ExTraceGlobals.ReaderTracer.TraceInformation(0, (long)this.GetHashCode(), text);
			ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogBatchEnqueue, string.Format("DoWork: called : thread={0}, {1}  ?---LogReader", Thread.CurrentThread.ManagedThreadId, text), "", "");
			while (!this.CheckServiceStopRequest("DoWork"))
			{
				this.ReadLog();
			}
			string message = string.Format("Reader {0} for log prefix {1} stopped", this.id, this.instance);
			ExTraceGlobals.ReaderTracer.TraceInformation(0, (long)this.GetHashCode(), message);
			ServiceLogger.LogInfo(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogMonitorRequestedStop, Thread.CurrentThread.Name, this.instance, "");
			this.stopped = true;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000831C File Offset: 0x0000651C
		internal void ReadLog()
		{
			LogFileInfo logFileInfo = null;
			int num = 0;
			try
			{
				if (this.batchQueue.IsFull)
				{
					num = Tools.RandomizeTimeSpan(this.config.ReaderSleepTime, this.config.ReaderSleepTimeRandomRange);
					string text = string.Format("The queue for log prefix {0} is full. Reader will not parse it until the queue is no longer full.", this.instance);
					ExTraceGlobals.ReaderTracer.TraceWarning((long)this.GetHashCode(), text);
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogReaderQueueFull, this.instance, new object[]
					{
						this.instance
					});
					long num2 = DateTime.UtcNow.Ticks - this.lastTimeWhenQeueFull;
					if (num2 > 1800000000L)
					{
						this.lastTimeWhenQeueFull = DateTime.UtcNow.Ticks;
						ServiceLogger.LogWarning(ServiceLogger.Component.LogReader, (LogUploaderEventLogConstants.Message)2147487654U, text, this.instance, "");
					}
				}
				else
				{
					logFileInfo = this.logMonitor.GetLogForReaderToProcess();
					if (logFileInfo == null)
					{
						num = Tools.RandomizeTimeSpan(this.config.ReaderSleepTime, this.config.ReaderSleepTimeRandomRange);
					}
					else
					{
						this.ProcessLog(logFileInfo);
					}
				}
			}
			catch (FileNotFoundException ex)
			{
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogReaderLogMissing, ex.Message, new object[]
				{
					Thread.CurrentThread.Name,
					ex
				});
				ServiceLogger.LogError(ServiceLogger.Component.LogReader, (LogUploaderEventLogConstants.Message)2147487659U, ex.Message, this.instance, "");
				this.logMonitor.ReaderCompletedProcessingLog(logFileInfo);
				return;
			}
			catch (IOException ex2)
			{
				string text2 = (logFileInfo == null) ? "unknown log" : logFileInfo.FullFileName;
				string text3 = string.Format("Caught an IOException when reading log {0}. Exception: {1}", text2, ex2);
				ExTraceGlobals.LogMonitorTracer.TraceError((long)this.GetHashCode(), text3);
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_ReadLogCaughtIOException, logFileInfo.FullFileName, new object[]
				{
					logFileInfo.FullFileName,
					ex2
				});
				ServiceLogger.LogError(ServiceLogger.Component.LogReader, (LogUploaderEventLogConstants.Message)3221229485U, text3, this.instance, text2);
				if (!ex2.Message.Contains("Insufficient system resources exist to complete the requested service"))
				{
					throw;
				}
				this.logMonitor.ReaderCompletedProcessingLog(logFileInfo);
			}
			catch (NotSupportedException ex3)
			{
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_UnsupportedLogVersion, "UnSupportedLogVersion", new object[]
				{
					logFileInfo.FullFileName,
					ex3
				});
				ServiceLogger.LogError(ServiceLogger.Component.LogMonitor, (LogUploaderEventLogConstants.Message)3221229488U, ex3.Message, this.instance, logFileInfo.FullFileName);
				EventNotificationItem.Publish(ExchangeComponent.Name, "UnsupportedLogVersion", null, ex3.Message, ResultSeverityLevel.Error, false);
				throw ex3;
			}
			catch (Exception ex4)
			{
				if (!(ex4 is ThreadAbortException) || !this.cancellationContext.StopToken.IsCancellationRequested)
				{
					string text4 = (logFileInfo == null) ? "unknown log" : logFileInfo.FullFileName;
					string text5 = string.Format("{0} processing {1} catches an exception: {2}", Thread.CurrentThread.Name, text4, ex4);
					ExTraceGlobals.ReaderTracer.TraceError((long)this.GetHashCode(), text5);
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogReaderUnknownError, null, new object[]
					{
						Thread.CurrentThread.Name,
						text4,
						ex4
					});
					EventNotificationItem.Publish(ExchangeComponent.Name, "LogReaderUnknownError", null, text5, ResultSeverityLevel.Error, false);
					this.perfCounterInstance.TotalLogReaderUnknownErrors.Increment();
					ServiceLogger.LogError(ServiceLogger.Component.LogReader, (LogUploaderEventLogConstants.Message)3221229474U, text5, this.instance, text4);
					throw;
				}
				Thread.ResetAbort();
				ServiceLogger.LogInfo(ServiceLogger.Component.DatabaseWriter, LogUploaderEventLogConstants.Message.LogMonitorRequestedStop, Thread.CurrentThread.Name + " received thread abort event", this.instance, "");
			}
			if (num > 0 && this.cancellationContext.StopToken.WaitHandle.WaitOne(num))
			{
				this.cancellationContext.StopWaitHandle.Set();
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008738 File Offset: 0x00006938
		internal FileStream OpenLogWithRetry(string logFileName, out string logVersion)
		{
			FileStream fileStream = null;
			logVersion = string.Empty;
			for (int i = 0; i < 3; i++)
			{
				fileStream = CsvFieldCache.OpenLogFile(logFileName, out logVersion);
				if (fileStream != null)
				{
					ExTraceGlobals.ReaderTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Open Log file {0}, Version={1}", logFileName, logVersion);
					break;
				}
				if (this.CheckServiceStopRequest("OpenLogWithRetry()") || new FileInfo(logFileName).Length == 0L)
				{
					break;
				}
				Thread.Sleep((int)this.config.OpenFileRetryInterval.TotalMilliseconds);
			}
			return fileStream;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000087B8 File Offset: 0x000069B8
		internal void ProcessBlock(CsvFieldCache cursor, LogFileRange block, LogFileInfo log)
		{
			bool flag = false;
			if (cursor.Position < block.StartOffset)
			{
				cursor.Seek(block.StartOffset);
			}
			long num = cursor.Position;
			while (cursor.Position < block.EndOffset)
			{
				if (this.CheckServiceStopRequest("ProcessBlock()"))
				{
					return;
				}
				try
				{
					flag = cursor.MoveNext(true);
					if (!flag)
					{
						if (cursor.AtEnd && !log.IsActive)
						{
							this.inputBuffer.AddInvalidRowToSkip(num, cursor.Position);
							block.EndOffset = cursor.Position;
							num = cursor.Position;
						}
						break;
					}
					ReadOnlyRow readOnlyRow = new ReadOnlyRow(cursor, num);
					this.inputBuffer.LineReceived(readOnlyRow);
					num = readOnlyRow.EndPosition;
				}
				catch (Exception ex)
				{
					if (RetryHelper.IsSystemFatal(ex))
					{
						throw;
					}
					string text = string.Format("Log={0} blockRange=({1},{2}) cursorOffset={3} rowEnd={4} logSize={5} \nException:{6}", new object[]
					{
						log.FullFileName,
						block.StartOffset,
						block.EndOffset,
						cursor.Position,
						num,
						log.Size,
						ex
					});
					string periodicKey = string.Format("{0}_{1}_{2}", log.FileName, block.StartOffset, block.EndOffset);
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_CsvParserFailedToParseLogLine, periodicKey, new object[]
					{
						text
					});
					ServiceLogger.LogError(ServiceLogger.Component.LogReader, (LogUploaderEventLogConstants.Message)3221229487U, string.Format("Detail={0}", text), this.instance, log.FullFileName);
					flag = false;
					break;
				}
			}
			if (cursor.Position == block.EndOffset)
			{
				block.ProcessingStatus = ProcessingStatus.ReadyToWriteToDatabase;
			}
			if (!flag)
			{
				if (cursor.AtEnd)
				{
					if (block.EndOffset == 9223372036854775807L)
					{
						block.EndOffset = cursor.Position;
					}
				}
				else if (this.logHeaderEndOffset != block.EndOffset)
				{
					string text2 = string.Format("Failed to read line from file {0} at position {1}", log.FullFileName, num);
					ExTraceGlobals.ReaderTracer.TraceError((long)this.GetHashCode(), text2);
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogReaderReadFailed, log.FullFileName + "_" + num.ToString(), new object[]
					{
						log.FullFileName,
						num
					});
					ServiceLogger.LogError(ServiceLogger.Component.LogReader, (LogUploaderEventLogConstants.Message)3221229476U, text2, this.instance, log.FullFileName);
				}
			}
			long incrementValue = num - block.StartOffset;
			if (Tools.IsRawProcessingType<T>())
			{
				this.perfCounterInstance.RawReaderParsedBytes.IncrementBy(incrementValue);
			}
			else
			{
				this.perfCounterInstance.ReaderParsedBytes.IncrementBy(incrementValue);
			}
			log.WatermarkFileObj.UpdateLastReaderParsedEndOffset(num);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008A94 File Offset: 0x00006C94
		internal void EnqueueLastBatchParsed()
		{
			if (this.inputBuffer != null)
			{
				this.inputBuffer.FinishBatch();
				this.inputBuffer.FlushMessageBatchBufferToWriter(true);
				this.inputBuffer.Dispose();
				this.inputBuffer = null;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008AC8 File Offset: 0x00006CC8
		internal void ProcessLog(LogFileInfo log)
		{
			FileStream fileStream = null;
			ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogReaderStartedParsingLog, string.Format("reprocessing or begin to process, isActive {0} +++++++++", log.IsActive), this.instance, log.FullFileName);
			if (log.Status == ProcessingStatus.NeedProcessing)
			{
				log.Status = ProcessingStatus.InProcessing;
				PerfCountersInstanceCache.GetInstance(this.instance).TotalNewLogsBeginProcessing.Increment();
				ExTraceGlobals.ReaderTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} started parsing log {1}", Thread.CurrentThread.Name, log.FullFileName);
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogReaderStartedParsingLog, log.FileName, new object[]
				{
					this.id,
					log.FullFileName
				});
				ServiceLogger.LogInfo(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogReaderStartedParsingLog, null, this.instance, log.FullFileName);
			}
			try
			{
				string defaultLogVersion;
				fileStream = this.OpenLogWithRetry(log.FullFileName, out defaultLogVersion);
				if (fileStream == null)
				{
					if (!this.isStopRequested)
					{
						string text = string.Format("Failed to open log file {0}", log.FullFileName);
						ExTraceGlobals.ReaderTracer.TraceError((long)this.GetHashCode(), text);
						EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogReaderFileOpenFailed, log.FullFileName, new object[]
						{
							log.FullFileName
						});
						EventNotificationItem.Publish(ExchangeComponent.Name, "FailToOpenLogFile", null, text, ResultSeverityLevel.Error, false);
						ServiceLogger.LogError(ServiceLogger.Component.LogReader, (LogUploaderEventLogConstants.Message)3221229475U, text, this.instance, log.FullFileName);
					}
				}
				else
				{
					if (string.IsNullOrWhiteSpace(defaultLogVersion))
					{
						defaultLogVersion = this.logMonitorHelper.GetDefaultLogVersion();
						ExTraceGlobals.ReaderTracer.TraceWarning<string, string>((long)this.GetHashCode(), "Failed to figure out version of log file {0}. Use default version {1} instead.", log.FullFileName, defaultLogVersion);
						EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_FailedToGetVersionFromLogHeader, log.FullFileName, new object[]
						{
							log.FullFileName,
							defaultLogVersion
						});
						ServiceLogger.LogError(ServiceLogger.Component.LogReader, (LogUploaderEventLogConstants.Message)2147487660U, null, this.instance, log.FullFileName);
					}
					CsvTable logSchema = this.logMonitorHelper.GetLogSchema(new Version(defaultLogVersion));
					CsvFieldCache csvFieldCache = new CsvFieldCache(logSchema, fileStream, 32768);
					int num = 0;
					this.logHeaderEndOffset = csvFieldCache.Position;
					ExTraceGlobals.ReaderTracer.TraceDebug<string, long>((long)this.GetHashCode(), "The end offset of the header of {0} is {1}.", log.FullFileName, this.logHeaderEndOffset);
					bool flag = false;
					while (!flag)
					{
						if (this.CheckServiceStopRequest("ProcessLog()"))
						{
							return;
						}
						LogFileRange logFileRange = log.WatermarkFileObj.GetBlockToReprocess();
						if (logFileRange == null)
						{
							logFileRange = log.WatermarkFileObj.GetNewBlockToProcess();
							flag = true;
							if (logFileRange == null)
							{
								break;
							}
						}
						if (num == 0)
						{
							long startOffset = logFileRange.StartOffset;
							this.inputBuffer = new InputBuffer<T>(this.config.BatchSizeInBytes, startOffset, log, this.batchQueue, this.logPrefix, this.logMonitorHelper, this.messageBatchFlushInterval, this.cancellationContext, this.config.InputBufferMaximumBatchCount, this.instance);
						}
						bool isFirstBlock = num == 0;
						this.inputBuffer.BeforeDataBlockIsProcessed(logFileRange, isFirstBlock);
						this.ProcessBlock(csvFieldCache, logFileRange, log);
						this.inputBuffer.AfterDataBlockIsProcessed();
						if (this.isStopRequested)
						{
							return;
						}
						num++;
					}
					this.EnqueueLastBatchParsed();
					log.LastProcessedTime = DateTime.UtcNow;
					if (!log.IsActive)
					{
						log.Status = ProcessingStatus.ReadyToWriteToDatabase;
						ExTraceGlobals.ReaderTracer.TraceInformation<string>(0, (long)this.GetHashCode(), "Finished parsing log {0}", log.FullFileName);
						EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogReaderFinishedParsingLog, log.FullFileName, new object[]
						{
							log.FullFileName
						});
						ServiceLogger.LogInfo(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogReaderFinishedParsingLog, null, this.instance, log.FullFileName);
					}
				}
			}
			finally
			{
				ServiceLogger.LogDebug(ServiceLogger.Component.LogReader, LogUploaderEventLogConstants.Message.LogReaderFinishedParsingLog, string.Format("Finished parsing for this round, isActive {0}  +++++++++", log.IsActive), this.instance, log.FullFileName);
				this.logMonitor.ReaderCompletedProcessingLog(log);
				this.Cleanup(fileStream);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008ECC File Offset: 0x000070CC
		private void Cleanup(FileStream fileStream)
		{
			if (fileStream != null)
			{
				fileStream.Flush();
				fileStream.Dispose();
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008EE0 File Offset: 0x000070E0
		private bool CheckServiceStopRequest(string caller)
		{
			if (this.cancellationContext.StopToken.IsCancellationRequested)
			{
				ExTraceGlobals.ReaderTracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "At {0}, reader received stop request in {1}", DateTime.UtcNow, caller);
				if (this.inputBuffer != null)
				{
					this.inputBuffer.Dispose();
				}
				this.cancellationContext.StopWaitHandle.Set();
				this.isStopRequested = true;
				return true;
			}
			return false;
		}

		// Token: 0x040000D3 RID: 211
		private const int OpenFileRetryCount = 3;

		// Token: 0x040000D4 RID: 212
		private const long MinimumIntervalToLogQueueFullWarningInTicks = 1800000000L;

		// Token: 0x040000D5 RID: 213
		private readonly int id;

		// Token: 0x040000D6 RID: 214
		private readonly string instance;

		// Token: 0x040000D7 RID: 215
		private readonly string logPrefix;

		// Token: 0x040000D8 RID: 216
		private readonly ConfigInstance config;

		// Token: 0x040000D9 RID: 217
		private readonly int messageBatchFlushInterval;

		// Token: 0x040000DA RID: 218
		private ThreadSafeQueue<T> batchQueue;

		// Token: 0x040000DB RID: 219
		private ILogManager logMonitor;

		// Token: 0x040000DC RID: 220
		private InputBuffer<T> inputBuffer;

		// Token: 0x040000DD RID: 221
		private bool stopped;

		// Token: 0x040000DE RID: 222
		private ILogMonitorHelper<T> logMonitorHelper;

		// Token: 0x040000DF RID: 223
		private long logHeaderEndOffset;

		// Token: 0x040000E0 RID: 224
		private CancellationContext cancellationContext;

		// Token: 0x040000E1 RID: 225
		private bool isStopRequested;

		// Token: 0x040000E2 RID: 226
		private long lastTimeWhenQeueFull;

		// Token: 0x040000E3 RID: 227
		private ILogUploaderPerformanceCounters perfCounterInstance;
	}
}
