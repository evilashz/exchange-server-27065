using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200001D RID: 29
	internal class LogMonitor<T> : ILogManager where T : LogDataBatch
	{
		// Token: 0x06000161 RID: 353 RVA: 0x000061B8 File Offset: 0x000043B8
		public LogMonitor(ConfigInstance config, string logDir, string logPrefixFilter, ILogMonitorHelper<T> logMonitorHelper, string instanceName = null, string wmkFileDiretory = null)
		{
			ArgumentValidator.ThrowIfNull("config", config);
			ArgumentValidator.ThrowIfNull("logDir", logDir);
			ArgumentValidator.ThrowIfNull("logMonitorHelper", logMonitorHelper);
			ArgumentValidator.ThrowIfNullOrEmpty("logPrefix", logPrefixFilter);
			this.logPrefixToBeMonitored = logPrefixFilter;
			this.config = config;
			this.logDirectory = logDir;
			this.logMonitorHelper = logMonitorHelper;
			this.logsJustFinishedParsing = new ConcurrentDictionary<string, LogFileInfo>(StringComparer.InvariantCultureIgnoreCase);
			this.watermarkFileHelper = new WatermarkFileHelper(this.logDirectory, wmkFileDiretory);
			this.instance = (string.IsNullOrWhiteSpace(instanceName) ? this.logPrefixToBeMonitored : instanceName);
			this.batchQueue = new ThreadSafeQueue<T>(this.config.QueueCapacity);
			this.knownLogNameToLogFileMap = new ConcurrentDictionary<string, ILogFileInfo>(StringComparer.InvariantCultureIgnoreCase);
			this.logsNeedProcessing = new List<LogFileInfo>();
			this.previousLogDirectories = new HashSet<string>();
			this.reprocessingActiveFileWaitTime = Tools.RandomizeTimeSpan(this.config.WaitTimeToReprocessActiveFile, this.config.WaitTimeToReprocessActiveFileRandomRange);
			this.instanceInstantiateTime = DateTime.UtcNow;
			this.staleLogs = new List<ILogFileInfo>();
			this.veryStaleLogs = new List<ILogFileInfo>();
			this.workerThreads = new List<Thread>();
			this.maxNumberOfWriterThreads = config.MaxNumOfWriters;
			this.maxNumberOfReaderThreads = config.MaxNumOfReaders;
			this.perfCounterInstance = PerfCountersInstanceCache.GetInstance(this.instance);
			this.perfCounterInstance.TotalIncompleteLogs.RawValue = 0L;
			this.perfCounterInstance.BatchQueueLength.RawValue = 0L;
			this.perfCounterInstance.InputBufferBatchCounts.RawValue = 0L;
			this.perfCounterInstance.InputBufferBackfilledLines.RawValue = 0L;
			this.perfCounterInstance.TotalLogLinesProcessed.RawValue = 0L;
			if (Tools.IsRawProcessingType<T>())
			{
				this.perfCounterInstance.RawIncompleteBytes.RawValue = 0L;
				this.perfCounterInstance.RawTotalLogBytes.RawValue = 0L;
				this.perfCounterInstance.RawWrittenBytes.RawValue = 0L;
				this.perfCounterInstance.RawReaderParsedBytes.RawValue = 0L;
				return;
			}
			this.perfCounterInstance.IncompleteBytes.RawValue = 0L;
			this.perfCounterInstance.TotalLogBytes.RawValue = 0L;
			this.perfCounterInstance.TotalLogBytesProcessed.RawValue = 0L;
			this.perfCounterInstance.ReaderParsedBytes.RawValue = 0L;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00006407 File Offset: 0x00004607
		// (set) Token: 0x06000163 RID: 355 RVA: 0x0000640F File Offset: 0x0000460F
		public int MaxNumberOfReaders
		{
			get
			{
				return this.maxNumberOfReaderThreads;
			}
			protected set
			{
				this.maxNumberOfReaderThreads = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006418 File Offset: 0x00004618
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00006420 File Offset: 0x00004620
		public int MaxNumberOfWriters
		{
			get
			{
				return this.maxNumberOfWriterThreads;
			}
			protected set
			{
				this.maxNumberOfWriterThreads = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00006429 File Offset: 0x00004629
		public TimeSpan LogDirCheckInterval
		{
			get
			{
				return this.config.LogDirCheckInterval;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006436 File Offset: 0x00004636
		public ThreadSafeQueue<T> BatchQueue
		{
			get
			{
				return this.batchQueue;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000643E File Offset: 0x0000463E
		public int NumberOfWorkerThreads
		{
			get
			{
				return this.workerThreads.Count;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000644B File Offset: 0x0000464B
		public string Instance
		{
			get
			{
				return this.instance;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00006453 File Offset: 0x00004653
		public string LogPrefixToBeMonitored
		{
			get
			{
				return this.logPrefixToBeMonitored;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000645B File Offset: 0x0000465B
		public string LogDirectory
		{
			get
			{
				return this.logDirectory;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006463 File Offset: 0x00004663
		public int StaleLogCount
		{
			get
			{
				return this.staleLogs.Count;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006470 File Offset: 0x00004670
		public int VeryStaleLogCount
		{
			get
			{
				return this.veryStaleLogs.Count;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000647D File Offset: 0x0000467D
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00006485 File Offset: 0x00004685
		public DatabaseWriter<T>[] Writers
		{
			get
			{
				return this.writers;
			}
			internal set
			{
				this.writers = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000648E File Offset: 0x0000468E
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00006496 File Offset: 0x00004696
		public ConfigInstance Config
		{
			get
			{
				return this.config;
			}
			internal set
			{
				this.config = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000649F File Offset: 0x0000469F
		internal IEnumerable<LogFileInfo> LogsNeedProcessing
		{
			get
			{
				return this.logsNeedProcessing;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000064A7 File Offset: 0x000046A7
		internal ConcurrentDictionary<string, LogFileInfo> UnitTest_Get_logsJustFinishedParsing
		{
			get
			{
				return this.logsJustFinishedParsing;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000064AF File Offset: 0x000046AF
		internal int CheckDirectoryCount
		{
			get
			{
				return this.checkDirectoryCount;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000064B7 File Offset: 0x000046B7
		internal List<ILogFileInfo> StaleLogs
		{
			get
			{
				return this.staleLogs;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000064BF File Offset: 0x000046BF
		internal List<ILogFileInfo> VeryStaleLogs
		{
			get
			{
				return this.veryStaleLogs;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000064C7 File Offset: 0x000046C7
		internal IWatermarkFileHelper WatermarkFileHelper
		{
			get
			{
				return this.watermarkFileHelper;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000064CF File Offset: 0x000046CF
		internal string UnitTestCatchedReaderWriterException
		{
			get
			{
				return this.unitTestCatchedExceptionMessage;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000064D7 File Offset: 0x000046D7
		protected ConcurrentDictionary<string, ILogFileInfo> KnownLogNameToLogFileMap
		{
			get
			{
				return this.knownLogNameToLogFileMap;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000064E0 File Offset: 0x000046E0
		public virtual void Start()
		{
			int num = this.maxNumberOfReaderThreads + this.maxNumberOfWriterThreads;
			this.stopWaitHandles = new ManualResetEvent[num];
			this.stopTokenSource = new CancellationTokenSource();
			CancellationToken token = this.stopTokenSource.Token;
			this.logMonitorHelper.Initialize(token);
			this.needToRetryStart = !this.RetryableStart();
			this.checkDirectoryTimer = new Timer(new TimerCallback(this.CheckDirectory), null, new TimeSpan(0L), this.config.LogDirCheckInterval);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000656C File Offset: 0x0000476C
		public virtual void Stop()
		{
			if (this.checkDirectoryTimer != null)
			{
				this.checkDirectoryTimer.Dispose();
				this.checkDirectoryTimer = null;
			}
			if (this.stopTokenSource != null)
			{
				ExTraceGlobals.LogMonitorTracer.TraceInformation<string>(0, (long)this.GetHashCode(), "LogMonitor of type {0} sent stop requests to readers and writers.", this.instance);
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogMonitorRequestedStop, this.instance, new object[]
				{
					this.instance
				});
				ServiceLogger.LogInfo(ServiceLogger.Component.LogMonitor, LogUploaderEventLogConstants.Message.LogMonitorRequestedStop, "", this.instance, "");
				this.stopTokenSource.Cancel();
				if (this.stopWaitHandles != null)
				{
					ManualResetEvent[] array = (from h in this.stopWaitHandles
					where h != null
					select h).ToArray<ManualResetEvent>();
					if (array.Length == 0 || WaitHandle.WaitAll(array, this.config.ServiceStopWaitTime))
					{
						ExTraceGlobals.LogMonitorTracer.TraceInformation<string>(0, (long)this.GetHashCode(), "No worker threads of log type {0} is running.", this.instance);
						EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogMonitorAllStopped, this.instance, new object[]
						{
							this.instance
						});
						ServiceLogger.LogInfo(ServiceLogger.Component.LogMonitor, LogUploaderEventLogConstants.Message.LogMonitorAllStopped, "", this.instance, "");
					}
					else
					{
						ExTraceGlobals.LogMonitorTracer.TraceInformation<string>(0, (long)this.GetHashCode(), "Timeout waiting for all readers and writers of log type {0} to stop. Call thread's abort anyway.", this.instance);
						EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogMonitorStopTimedOut, this.instance, new object[]
						{
							this.instance
						});
						ServiceLogger.LogInfo(ServiceLogger.Component.LogMonitor, LogUploaderEventLogConstants.Message.LogMonitorStopTimedOut, "", this.instance, "");
						foreach (Thread thread in this.workerThreads)
						{
							if (thread.IsAlive)
							{
								thread.Abort();
							}
						}
						Thread.Sleep(1000);
					}
				}
			}
			else
			{
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogMonitorAllStopped, null, new object[]
				{
					this.instance
				});
				ServiceLogger.LogInfo(ServiceLogger.Component.LogMonitor, LogUploaderEventLogConstants.Message.LogMonitorAllStopped, "", this.instance, "");
			}
			this.workerThreads.Clear();
			if (this.batchQueue != null)
			{
				this.batchQueue.Close();
			}
			this.DisposeAllWatermarkFileObjects();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000067DC File Offset: 0x000049DC
		public virtual LogFileInfo GetLogForReaderToProcess()
		{
			if (this.checkDirectoryCount == 0)
			{
				return null;
			}
			LogFileInfo logFileInfo = null;
			bool flag = false;
			do
			{
				logFileInfo = null;
				flag = false;
				lock (this.logsNeedProcessingSyncObject)
				{
					if (this.logsNeedProcessing.Count > 0)
					{
						logFileInfo = this.logsNeedProcessing[0];
						this.logsNeedProcessing.RemoveAt(0);
					}
				}
				if (logFileInfo != null)
				{
					try
					{
						if (logFileInfo.Size == 0L)
						{
							flag = true;
						}
					}
					catch (IOException)
					{
						flag = true;
					}
					if (flag)
					{
						this.logsJustFinishedParsing.TryAdd(logFileInfo.FileName, logFileInfo);
					}
				}
			}
			while (flag);
			return logFileInfo;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000688C File Offset: 0x00004A8C
		public IWatermarkFile FindWatermarkFileObject(string logFileName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("logFileName", logFileName);
			string fileName = Path.GetFileName(logFileName);
			ILogFileInfo logFileInfo;
			this.knownLogNameToLogFileMap.TryGetValue(fileName, out logFileInfo);
			if (logFileInfo != null)
			{
				return logFileInfo.WatermarkFileObj;
			}
			return null;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000068C5 File Offset: 0x00004AC5
		public virtual void ReaderCompletedProcessingLog(LogFileInfo log)
		{
			if (log == null)
			{
				return;
			}
			this.logsJustFinishedParsing.TryAdd(log.FileName, log);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000068F4 File Offset: 0x00004AF4
		internal virtual void AddLogToNeedProcessing(IEnumerable<LogFileInfo> logFilesList)
		{
			lock (this.logsNeedProcessingSyncObject)
			{
				foreach (LogFileInfo item in logFilesList)
				{
					this.logsNeedProcessing.Add(item);
				}
				this.logsNeedProcessing.Sort((LogFileInfo log1, LogFileInfo log2) => DateTime.Compare(log2.CreationTimeUtc, log1.CreationTimeUtc));
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000069D0 File Offset: 0x00004BD0
		internal void CheckDirectory(object state)
		{
			if (Interlocked.CompareExchange(ref this.checkDirectoryDone, 0, 1) == 1)
			{
				try
				{
					if (this.logDirectory == null)
					{
						return;
					}
					if (this.needToRetryStart && (this.needToRetryStart = !this.RetryableStart()))
					{
						return;
					}
					if (!Directory.Exists(this.logDirectory))
					{
						EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_NonexistentLogDirectory, this.logDirectory, new object[]
						{
							this.logDirectory
						});
						return;
					}
					IEnumerable<FileInfo> source = new DirectoryInfo(this.logDirectory).EnumerateFiles(this.logPrefixToBeMonitored + "*.log");
					IEnumerable<FileInfo> enumerable = from f in source
					where f.Length < 2147483647L && this.logMonitorHelper.ShouldProcessLogFile(this.logPrefixToBeMonitored, f.Name) && !this.HasDoneFile(f.Name)
					select f;
					this.DumpPendingProcessLogFilesInfo(enumerable);
					this.AddNewLogsToProcess(enumerable);
					this.CheckBackLogs();
					this.CheckLogsAndMarkComplete();
					this.CheckLogsDueToReprocess();
					this.DeleteResidueFilesForRetiredLogs();
				}
				catch (Exception ex)
				{
					string message = string.Format("Caught an Exception when checking directory {0}. Exception: {1}", this.logDirectory, ex);
					ExTraceGlobals.LogMonitorTracer.TraceError((long)this.GetHashCode(), message);
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_CheckDirectoryCaughtException, this.logDirectory, new object[]
					{
						this.logDirectory,
						ex
					});
					ServiceLogger.LogError(ServiceLogger.Component.LogMonitor, (LogUploaderEventLogConstants.Message)3221228483U, ex.Message, this.instance, this.logDirectory);
					if (!ex.Message.Contains("Insufficient system resources exist to complete the requested service"))
					{
						throw;
					}
				}
				finally
				{
					this.checkDirectoryCount++;
					this.checkDirectoryDone = 1;
				}
				this.UpdateIncompleteLogsPerfCounter();
				if (Tools.IsRawProcessingType<T>())
				{
					this.perfCounterInstance.RawIncompleteBytes.RawValue = this.unprocessedBytes;
					this.perfCounterInstance.RawTotalLogBytes.RawValue = this.totalLogBytes;
				}
				else
				{
					this.perfCounterInstance.IncompleteBytes.RawValue = this.unprocessedBytes;
					this.perfCounterInstance.TotalLogBytes.RawValue = this.totalLogBytes;
				}
				this.perfCounterInstance.ThreadSafeQueueConsumerSemaphoreCount.RawValue = (long)this.batchQueue.ConsumerSemaphoreCount;
				this.perfCounterInstance.ThreadSafeQueueProducerSemaphoreCount.RawValue = (long)this.batchQueue.ProducerSemaphoreCount;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006C40 File Offset: 0x00004E40
		internal virtual void AddNewLogsToProcess(IEnumerable<FileInfo> fileInfoList)
		{
			this.ResetStaleLogCollection();
			int num = 0;
			int num2 = 0;
			IOrderedEnumerable<FileInfo> orderedEnumerable = from f in fileInfoList
			orderby f.CreationTimeUtc descending
			select f;
			bool flag = true;
			bool flag2 = false;
			List<LogFileInfo> list = new List<LogFileInfo>();
			DateTime t = DateTime.UtcNow - this.Config.ActiveLogFileIdleTimeout;
			foreach (FileInfo fileInfo in orderedEnumerable)
			{
				try
				{
					bool isActive = flag || (this.Config.EnableMultipleWriters && fileInfo.LastWriteTimeUtc > t);
					ILogFileInfo logFileInfo;
					LogFileInfo logFileInfo2;
					if (!this.knownLogNameToLogFileMap.TryGetValue(fileInfo.Name, out logFileInfo))
					{
						logFileInfo2 = new LogFileInfo(fileInfo.Name, isActive, this.instance, this.watermarkFileHelper);
						this.knownLogNameToLogFileMap.TryAdd(fileInfo.Name, logFileInfo2);
						list.Add(logFileInfo2);
						num++;
					}
					else
					{
						logFileInfo2 = (LogFileInfo)logFileInfo;
						logFileInfo2.IsActive = isActive;
					}
					if (!flag2)
					{
						this.CalculateIncompleteBytes(logFileInfo2, out flag2);
					}
					this.UpdateStaleLogs(logFileInfo2);
					if (logFileInfo2.Status == ProcessingStatus.NeedProcessing)
					{
						num2++;
					}
				}
				catch (FailedToInstantiateLogFileInfoException ex)
				{
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_FailedToInstantiateLogFileInfo, fileInfo.Name, new object[]
					{
						ex.Message
					});
					ServiceLogger.LogError(ServiceLogger.Component.LogMonitor, (LogUploaderEventLogConstants.Message)2147486660U, ex.Message, this.instance, fileInfo.Name);
				}
				flag = false;
			}
			if (list.Count > 0)
			{
				this.AddLogToNeedProcessing(list);
			}
			this.perfCounterInstance.TotalIncomingLogs.IncrementBy((long)num);
			this.perfCounterInstance.NumberOfIncomingLogs.RawValue = (long)num;
			this.perfCounterInstance.LogsNeverProcessedBefore.RawValue = (long)num2;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006E50 File Offset: 0x00005050
		internal virtual void CheckLogsAndMarkComplete()
		{
			List<LogFileInfo> list = new List<LogFileInfo>();
			List<LogFileInfo> list2 = new List<LogFileInfo>();
			foreach (LogFileInfo logFileInfo in this.logsJustFinishedParsing.Values)
			{
				if (!logFileInfo.FileExists)
				{
					list2.Add(logFileInfo);
				}
				else if (!logFileInfo.IsActive && logFileInfo.WatermarkFileObj.IsLogCompleted())
				{
					list.Add(logFileInfo);
				}
			}
			this.RemoveLogFileInfoObjects(list2);
			this.ReportMissingLogs(list2);
			this.ReportCompleteLogs(list);
			this.RemoveLogFileInfoObjects(list);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006EF0 File Offset: 0x000050F0
		internal virtual bool ShouldSuppressBackLogAlert()
		{
			return DateTime.UtcNow.Subtract(this.instanceInstantiateTime) < AppConfigReader.MaxWaitTimeBeforeAlertOnBackLog;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006F1C File Offset: 0x0000511C
		internal bool IsFileProcessedBefore(ILogFileInfo logFileInfo)
		{
			string watermarkFileFullName = logFileInfo.WatermarkFileObj.WatermarkFileFullName;
			return File.Exists(watermarkFileFullName) || (logFileInfo.Status != ProcessingStatus.NeedProcessing && logFileInfo.Status != ProcessingStatus.Unknown);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006F58 File Offset: 0x00005158
		internal void DeleteResidueFilesForRetiredLogs()
		{
			string filter = string.Format("{0}*.{1}", this.logPrefixToBeMonitored, "wmk");
			string filter2 = string.Format("{0}*.{1}", this.logPrefixToBeMonitored, "done");
			this.ClearnupWmkDoneFiles(this.watermarkFileHelper.WatermarkFileDirectory, filter);
			this.ClearnupWmkDoneFiles(this.watermarkFileHelper.WatermarkFileDirectory, filter2);
			if (this.previousLogDirectories.Count > 0)
			{
				foreach (string dir in this.previousLogDirectories)
				{
					this.ClearnupWmkDoneFiles(dir, filter);
					this.ClearnupWmkDoneFiles(dir, filter2);
				}
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007014 File Offset: 0x00005214
		internal void ClearnupWmkDoneFiles(string dir, string filter)
		{
			foreach (string text in Directory.EnumerateFiles(dir, filter))
			{
				try
				{
					string path = this.watermarkFileHelper.DeduceLogFullFileNameFromDoneOrWatermarkFileName(text);
					if (!File.Exists(path))
					{
						if (this.knownLogNameToLogFileMap.ContainsKey(Path.GetFileName(path)))
						{
							ServiceLogger.LogInfo(ServiceLogger.Component.LogMonitor, LogUploaderEventLogConstants.Message.DeletingFile, "Skipped for it's still processed by this logMonitor", this.instance, text);
						}
						else
						{
							ServiceLogger.LogInfo(ServiceLogger.Component.LogMonitor, LogUploaderEventLogConstants.Message.DeletingFile, "Delete Done or watermark file", this.instance, text);
							File.Delete(text);
						}
					}
				}
				catch (IOException ex)
				{
					string text2 = string.Format("Failed to clean up {0}. Exception: {1}.", text, ex.Message);
					ExTraceGlobals.LogMonitorTracer.TraceError((long)this.GetHashCode(), text2);
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogMonitorWatermarkCleanupFailed, text, new object[]
					{
						text,
						ex.Message
					});
					ServiceLogger.LogError(ServiceLogger.Component.LogMonitor, (LogUploaderEventLogConstants.Message)3221228476U, text2, "", text);
				}
				catch (UnauthorizedAccessException ex2)
				{
					string text3 = string.Format("Failed to clean up {0}. Exception: {1}.", text, ex2);
					ExTraceGlobals.LogMonitorTracer.TraceError((long)this.GetHashCode(), text3);
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogMonitorWatermarkCleanupFailed, text, new object[]
					{
						text,
						ex2
					});
					ServiceLogger.LogError(ServiceLogger.Component.LogMonitor, (LogUploaderEventLogConstants.Message)3221228476U, text3, "", text);
				}
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000071C8 File Offset: 0x000053C8
		internal void StartReaderThreads(CancellationToken stopToken)
		{
			ExTraceGlobals.LogMonitorTracer.TraceDebug<int, string>((long)this.GetHashCode(), "Starting {0} reader threads for logPrefix {1}", this.config.MaxNumOfReaders, this.instance);
			this.readers = new LogReader<T>[this.config.MaxNumOfReaders];
			for (int i = 0; i < this.maxNumberOfReaderThreads; i++)
			{
				this.readers[i] = new LogReader<T>(this.batchQueue, i, this, this.config, this.logPrefixToBeMonitored, this.logMonitorHelper, this.instance);
				this.stopWaitHandles[i] = new ManualResetEvent(false);
				Thread thread = new Thread(new ParameterizedThreadStart(this.DoWorkAction(new Action<object>(this.readers[i], ldftn(DoWork))).Invoke));
				thread.Priority = this.config.ReaderPrioritySetting;
				thread.Start(new CancellationContext(stopToken, this.stopWaitHandles[i]));
				this.workerThreads.Add(thread);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000072C0 File Offset: 0x000054C0
		internal void StartWriterThreads(CancellationToken stopToken)
		{
			ExTraceGlobals.LogMonitorTracer.TraceDebug<int, string>((long)this.GetHashCode(), "Starting {0} writer threads for logPrefix {1}", this.maxNumberOfWriterThreads, this.instance);
			for (int i = 0; i < this.maxNumberOfWriterThreads; i++)
			{
				this.stopWaitHandles[i + this.maxNumberOfReaderThreads] = new ManualResetEvent(false);
				Thread thread = new Thread(new ParameterizedThreadStart(this.DoWorkAction(new Action<object>(this.writers[i], ldftn(DoWork))).Invoke));
				thread.Priority = this.config.WriterPrioritySetting;
				thread.Start(new CancellationContext(stopToken, this.stopWaitHandles[i + this.maxNumberOfReaderThreads]));
				this.workerThreads.Add(thread);
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000737C File Offset: 0x0000557C
		internal virtual void CreateWriters()
		{
			if (this.writers == null)
			{
				this.writers = new DatabaseWriter<T>[this.maxNumberOfWriterThreads];
			}
			for (int i = 0; i < this.maxNumberOfWriterThreads; i++)
			{
				if (this.writers[i] == null)
				{
					this.writers[i] = this.logMonitorHelper.CreateDBWriter(this.batchQueue, i, this.config, this.instance);
					this.writers[i].SetLogMonitorInterface(this);
				}
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000073F4 File Offset: 0x000055F4
		internal long UpdateIncompleteLogsPerfCounter()
		{
			long num = 0L;
			this.unprocessedBytes = 0L;
			foreach (string key in this.knownLogNameToLogFileMap.Keys)
			{
				ILogFileInfo logFileInfo;
				if (this.knownLogNameToLogFileMap.TryGetValue(key, out logFileInfo))
				{
					if (logFileInfo.IsActive)
					{
						if (!logFileInfo.WatermarkFileObj.IsLogCompleted())
						{
							this.unprocessedBytes += Math.Max(0L, logFileInfo.Size - logFileInfo.WatermarkFileObj.ProcessedSize);
							num += 1L;
						}
					}
					else if (logFileInfo.Status != ProcessingStatus.CompletedProcessing)
					{
						this.unprocessedBytes += Math.Max(0L, logFileInfo.Size - logFileInfo.WatermarkFileObj.ProcessedSize);
						num += 1L;
					}
				}
			}
			long incrementValue = num - this.perfCounterInstance.TotalIncompleteLogs.RawValue;
			this.perfCounterInstance.TotalIncompleteLogs.IncrementBy(incrementValue);
			return num;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007504 File Offset: 0x00005704
		internal void CheckLogsDueToReprocess()
		{
			List<LogFileInfo> list = new List<LogFileInfo>();
			foreach (LogFileInfo logFileInfo in this.logsJustFinishedParsing.Values)
			{
				if (this.DueForReprocess(logFileInfo))
				{
					list.Add(logFileInfo);
				}
			}
			foreach (LogFileInfo logFileInfo2 in list)
			{
				LogFileInfo logFileInfo3;
				this.logsJustFinishedParsing.TryRemove(logFileInfo2.FileName, out logFileInfo3);
			}
			this.AddLogToNeedProcessing(list);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000075BC File Offset: 0x000057BC
		internal bool DueForReprocess(LogFileInfo log)
		{
			bool flag = log.WatermarkFileObj.ReaderHasBytesToParse();
			if (flag)
			{
				bool flag2 = DateTime.UtcNow.Subtract(log.LastProcessedTime).TotalMilliseconds > (double)this.reprocessingActiveFileWaitTime;
				if (flag2)
				{
					this.reprocessingActiveFileWaitTime = Tools.RandomizeTimeSpan(this.config.WaitTimeToReprocessActiveFile, this.config.WaitTimeToReprocessActiveFileRandomRange);
				}
				return flag2;
			}
			return false;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007624 File Offset: 0x00005824
		internal void UnitTestSetCatchReaderWriterExceptionFlag()
		{
			this.catchReaderWriterExceptionsForUnitTest = true;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000762D File Offset: 0x0000582D
		protected void ResetStaleLogCollection()
		{
			this.staleLogs = new List<ILogFileInfo>();
			this.veryStaleLogs = new List<ILogFileInfo>();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007648 File Offset: 0x00005848
		protected void CalculateIncompleteBytes(LogFileInfo logFile, out bool skipRestOfFilesInTheList)
		{
			skipRestOfFilesInTheList = false;
			if (this.checkDirectoryCount == 0)
			{
				long num = logFile.AddedLogSize();
				this.totalLogBytes += Math.Max(0L, num - logFile.WatermarkFileObj.ProcessedSize);
				return;
			}
			long num2 = logFile.AddedLogSize();
			this.totalLogBytes += num2;
			skipRestOfFilesInTheList = (!logFile.IsActive && num2 == 0L);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000076B0 File Offset: 0x000058B0
		protected void UpdateStaleLogs(ILogFileInfo logFileInfo)
		{
			double totalHours = DateTime.UtcNow.Subtract(logFileInfo.LastWriteTimeUtc).TotalHours;
			if (!this.IsFileProcessedBefore(logFileInfo) && !LogMonitor<T>.IsEmptyLog(logFileInfo) && totalHours >= this.config.BacklogAlertNonUrgentThreshold.TotalHours)
			{
				this.staleLogs.Add(logFileInfo);
				if (totalHours >= this.config.BacklogAlertUrgentThreshold.TotalHours)
				{
					this.veryStaleLogs.Add(logFileInfo);
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007730 File Offset: 0x00005930
		protected void CheckBackLogs()
		{
			if (this.veryStaleLogs.Count == 0)
			{
				Interlocked.CompareExchange(ref this.veryStaleLogReportedBefore, 0, 1);
			}
			if (this.staleLogs.Count == 0)
			{
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogMonitorDetectNoStaleLog, null, new object[]
				{
					this.instance
				});
				Interlocked.CompareExchange(ref this.staleLogReportedBefore, 0, 1);
				return;
			}
			this.HandleBackLogAlert();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000779B File Offset: 0x0000599B
		protected bool HasDoneFile(string logFileName)
		{
			return File.Exists(this.watermarkFileHelper.DeduceDoneFileFullNameFromLogFileName(logFileName));
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000077AE File Offset: 0x000059AE
		private static void RaiseAlertIfHealthStateChange(ref int state, string monitorName, string error)
		{
			if (Interlocked.CompareExchange(ref state, 1, 0) == 0)
			{
				EventNotificationItem.Publish(ExchangeComponent.Name, monitorName, null, error, ResultSeverityLevel.Error, false);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000077C9 File Offset: 0x000059C9
		private static bool IsEmptyLog(ILogFileInfo logFileInfo)
		{
			return logFileInfo.Size == 0L;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000077D8 File Offset: 0x000059D8
		private void ReportMissingLogs(List<LogFileInfo> missingLogs)
		{
			if (missingLogs.Count == 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("The following logs are not found while being processed:");
			foreach (LogFileInfo logFileInfo in missingLogs)
			{
				ServiceLogger.LogError(ServiceLogger.Component.LogMonitor, (LogUploaderEventLogConstants.Message)3221231489U, "CheckLogsAndMarkComplete", this.instance, logFileInfo.FullFileName);
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_FileDeleted, logFileInfo.FullFileName, new object[]
				{
					logFileInfo.FullFileName
				});
				stringBuilder.AppendLine(logFileInfo.FullFileName);
			}
			EventNotificationItem.Publish(ExchangeComponent.Name, "LogFileNotFoundError", null, stringBuilder.ToString(), ResultSeverityLevel.Error, false);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000078A4 File Offset: 0x00005AA4
		private void ReportCompleteLogs(List<LogFileInfo> completeLogs)
		{
			foreach (LogFileInfo logFileInfo in completeLogs)
			{
				this.perfCounterInstance.TotalCompletedLogs.Increment();
				string message = string.Format("The processing of log {0} is completed.", logFileInfo.FullFileName);
				ExTraceGlobals.LogMonitorTracer.TraceDebug((long)this.GetHashCode(), message);
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogMonitorLogCompleted, logFileInfo.FullFileName, new object[]
				{
					logFileInfo.FullFileName
				});
				ServiceLogger.LogInfo(ServiceLogger.Component.LogMonitor, LogUploaderEventLogConstants.Message.LogMonitorLogCompleted, "", "", logFileInfo.FullFileName);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007964 File Offset: 0x00005B64
		private void RemoveLogFileInfoObjects(IEnumerable<LogFileInfo> toBeRemovedList)
		{
			foreach (LogFileInfo logFileInfo in toBeRemovedList)
			{
				LogFileInfo logFileInfo2;
				this.logsJustFinishedParsing.TryRemove(logFileInfo.FileName, out logFileInfo2);
				logFileInfo.Status = ProcessingStatus.CompletedProcessing;
				logFileInfo.WatermarkFileObj.Dispose();
				if (logFileInfo.FileExists)
				{
					logFileInfo.WatermarkFileObj.CreateDoneFile();
				}
				ILogFileInfo logFileInfo3;
				if (this.knownLogNameToLogFileMap.TryRemove(logFileInfo.FileName, out logFileInfo3))
				{
					string text = string.Format("log file {0} is removed from KnownLogNameToLogFileMap.", logFileInfo.FileName);
					ExTraceGlobals.LogMonitorTracer.TraceDebug((long)this.GetHashCode(), text);
					ServiceLogger.LogInfo(ServiceLogger.Component.LogMonitor, LogUploaderEventLogConstants.Message.LogFileDeletedFromKnownLogNameToLogFileMap, text, "", logFileInfo.FileName);
				}
				else
				{
					string.Format("The log {0} disappeared from KnownLogNameToLogFileMap. This indicates a bug somewhere", logFileInfo.FullFileName);
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogDisappearedFromKnownLogNameToLogFileMap, logFileInfo.FullFileName, new object[]
					{
						logFileInfo.FullFileName
					});
					ServiceLogger.LogError(ServiceLogger.Component.LogMonitor, (LogUploaderEventLogConstants.Message)2147748808U, "", "", logFileInfo.FullFileName);
				}
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007A90 File Offset: 0x00005C90
		private void ReportBacklogCondition(int staleLogCount, int veryStaleLogCount, string logDir, List<ILogFileInfo> logList, bool isUrgent)
		{
			int num = Math.Min(logList.Count, 10);
			IEnumerable<ILogFileInfo> enumerable = logList.Take(num);
			StringBuilder stringBuilder = new StringBuilder();
			string value = (num > 1) ? string.Format("The first {0} logs are:", num) : "Here are the detailed info:";
			stringBuilder.AppendLine(value);
			foreach (ILogFileInfo logFileInfo in enumerable)
			{
				string text = this.BuildWatermarkFileInfo(logFileInfo);
				string text2 = "unknown";
				try
				{
					FileInfo fileInfo = new FileInfo(logFileInfo.FullFileName);
					if (fileInfo.Exists)
					{
						text2 = string.Format("{0} bytes", fileInfo.Length);
					}
				}
				catch (Exception ex)
				{
					if (RetryHelper.IsSystemFatal(ex))
					{
						throw;
					}
				}
				stringBuilder.AppendLine(string.Format("The log file {0} is {1}, its size is {2}, created on {3}, last modified on {4}, {5}", new object[]
				{
					logFileInfo.FullFileName,
					logFileInfo.IsActive ? "Active" : "Inactive",
					text2,
					logFileInfo.CreationTimeUtc,
					logFileInfo.LastWriteTimeUtc,
					text
				}));
			}
			string text3 = string.Format("There are {0} logs in directory {1} that haven't been processed for {2} hours. {3} of them are over {4} hours.\n{5}", new object[]
			{
				staleLogCount,
				logDir,
				this.config.BacklogAlertNonUrgentThreshold.TotalHours,
				veryStaleLogCount,
				this.config.BacklogAlertUrgentThreshold.TotalHours,
				stringBuilder.ToString()
			});
			if (isUrgent)
			{
				LogMonitor<T>.RaiseAlertIfHealthStateChange(ref this.veryStaleLogReportedBefore, "SeriousBacklogBuiltUp", text3);
			}
			else
			{
				LogMonitor<T>.RaiseAlertIfHealthStateChange(ref this.staleLogReportedBefore, "BacklogBuiltUp", text3);
			}
			EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_LogMonitorDetectLogProcessingFallsBehind, this.instance, new object[]
			{
				staleLogCount,
				logDir,
				this.config.BacklogAlertNonUrgentThreshold.TotalHours,
				veryStaleLogCount,
				this.config.BacklogAlertUrgentThreshold.TotalHours,
				stringBuilder.ToString()
			});
			ServiceLogger.LogError(ServiceLogger.Component.LogMonitor, (LogUploaderEventLogConstants.Message)3221228478U, text3, this.instance, this.logDirectory);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007D1C File Offset: 0x00005F1C
		private void HandleBackLogAlert()
		{
			if (this.ShouldSuppressBackLogAlert())
			{
				return;
			}
			if (this.veryStaleLogs.Count > 0)
			{
				this.ReportBacklogCondition(this.staleLogs.Count, this.veryStaleLogs.Count, this.logDirectory, this.veryStaleLogs, true);
				return;
			}
			if (this.staleLogs.Count > 0)
			{
				this.ReportBacklogCondition(this.staleLogs.Count, this.veryStaleLogs.Count, this.logDirectory, this.staleLogs, false);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007DA4 File Offset: 0x00005FA4
		private void DumpPendingProcessLogFilesInfo(IEnumerable<FileInfo> pendingProcessLogFiles)
		{
			if (ServiceLogger.ServiceLogLevel > ServiceLogger.LogLevel.Debug)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (FileInfo fileInfo in pendingProcessLogFiles)
			{
				stringBuilder.AppendLine(string.Format("file={0},created={1},modified={2}", fileInfo.FullName, fileInfo.CreationTimeUtc, fileInfo.LastWriteTimeUtc));
			}
			if (stringBuilder.Length > 0)
			{
				ServiceLogger.LogDebug(ServiceLogger.Component.LogMonitor, LogUploaderEventLogConstants.Message.PendingProcessLogFilesInfo, stringBuilder.ToString(), this.instance, this.logDirectory);
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007E48 File Offset: 0x00006048
		private string BuildWatermarkFileInfo(ILogFileInfo logFileInfo)
		{
			string watermarkFileFullName = logFileInfo.WatermarkFileObj.WatermarkFileFullName;
			string result = string.Empty;
			try
			{
				FileInfo fileInfo = new FileInfo(watermarkFileFullName);
				if (fileInfo.Exists)
				{
					result = string.Format("the watermark file={0},created={1},modified={2}", watermarkFileFullName, fileInfo.CreationTimeUtc, fileInfo.LastWriteTimeUtc);
				}
				else
				{
					result = string.Format("the watermark file {0} doesn't exist", watermarkFileFullName);
				}
			}
			catch (Exception ex)
			{
				if (RetryHelper.IsSystemFatal(ex))
				{
					throw;
				}
				result = string.Format("the watermark file {0} exists but can't retrive its info because of exception: {1}", watermarkFileFullName, ex.Message);
			}
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007EDC File Offset: 0x000060DC
		private bool RetryableStart()
		{
			try
			{
				this.CreateWriters();
			}
			catch (ConfigurationErrorsException ex)
			{
				if (ex.Message.Contains("Log Path is not set in Registry"))
				{
					int num = Math.Max(1, 300 / (int)this.config.LogDirCheckInterval.TotalSeconds);
					if (this.checkDirectoryCount / num < 1)
					{
						EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_FailedToGetLogPath, this.instance, new object[]
						{
							this.instance,
							ex.Message
						});
						ServiceLogger.LogError(ServiceLogger.Component.LogMonitor, (LogUploaderEventLogConstants.Message)3221488641U, ex.Message, this.instance, this.logDirectory);
						return false;
					}
				}
				throw;
			}
			this.StartWriterThreads(this.stopTokenSource.Token);
			this.StartReaderThreads(this.stopTokenSource.Token);
			return true;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007FC4 File Offset: 0x000061C4
		private void DisposeAllWatermarkFileObjects()
		{
			foreach (ILogFileInfo logFileInfo in this.knownLogNameToLogFileMap.Values)
			{
				LogFileInfo logFileInfo2 = (LogFileInfo)logFileInfo;
				logFileInfo2.WatermarkFileObj.Dispose();
			}
			this.knownLogNameToLogFileMap.Clear();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000080B4 File Offset: 0x000062B4
		private Action<object> DoWorkAction(Action<object> realDoWorkMethod)
		{
			if (!this.catchReaderWriterExceptionsForUnitTest)
			{
				return realDoWorkMethod;
			}
			return delegate(object stateObj)
			{
				try
				{
					realDoWorkMethod(stateObj);
				}
				catch (Exception ex)
				{
					ServiceLogger.LogError(ServiceLogger.Component.LogMonitor, (LogUploaderEventLogConstants.Message)3221226485U, ex.ToString(), "", "");
					EventNotificationItem.Publish(ExchangeComponent.Name, "ServiceStartUnknownException", null, ex.ToString(), ResultSeverityLevel.Error, false);
					LogMonitor<T> <>4__this = this;
					<>4__this.unitTestCatchedExceptionMessage += ex.ToString();
				}
			};
		}

		// Token: 0x040000AD RID: 173
		private const int MaxNumberOfStaleLogsToReport = 10;

		// Token: 0x040000AE RID: 174
		private readonly string instance;

		// Token: 0x040000AF RID: 175
		private readonly string logPrefixToBeMonitored;

		// Token: 0x040000B0 RID: 176
		private readonly string logDirectory;

		// Token: 0x040000B1 RID: 177
		private readonly ThreadSafeQueue<T> batchQueue;

		// Token: 0x040000B2 RID: 178
		private readonly DateTime instanceInstantiateTime;

		// Token: 0x040000B3 RID: 179
		private HashSet<string> previousLogDirectories;

		// Token: 0x040000B4 RID: 180
		private Timer checkDirectoryTimer;

		// Token: 0x040000B5 RID: 181
		private ConcurrentDictionary<string, ILogFileInfo> knownLogNameToLogFileMap;

		// Token: 0x040000B6 RID: 182
		private List<LogFileInfo> logsNeedProcessing;

		// Token: 0x040000B7 RID: 183
		private object logsNeedProcessingSyncObject = new object();

		// Token: 0x040000B8 RID: 184
		private ConcurrentDictionary<string, LogFileInfo> logsJustFinishedParsing;

		// Token: 0x040000B9 RID: 185
		private DatabaseWriter<T>[] writers;

		// Token: 0x040000BA RID: 186
		private LogReader<T>[] readers;

		// Token: 0x040000BB RID: 187
		private int checkDirectoryDone = 1;

		// Token: 0x040000BC RID: 188
		private CancellationTokenSource stopTokenSource;

		// Token: 0x040000BD RID: 189
		private ManualResetEvent[] stopWaitHandles;

		// Token: 0x040000BE RID: 190
		private List<Thread> workerThreads;

		// Token: 0x040000BF RID: 191
		private int staleLogReportedBefore;

		// Token: 0x040000C0 RID: 192
		private int veryStaleLogReportedBefore;

		// Token: 0x040000C1 RID: 193
		private ConfigInstance config;

		// Token: 0x040000C2 RID: 194
		private int checkDirectoryCount;

		// Token: 0x040000C3 RID: 195
		private ILogMonitorHelper<T> logMonitorHelper;

		// Token: 0x040000C4 RID: 196
		private int reprocessingActiveFileWaitTime;

		// Token: 0x040000C5 RID: 197
		private List<ILogFileInfo> staleLogs;

		// Token: 0x040000C6 RID: 198
		private List<ILogFileInfo> veryStaleLogs;

		// Token: 0x040000C7 RID: 199
		private long totalLogBytes;

		// Token: 0x040000C8 RID: 200
		private long unprocessedBytes;

		// Token: 0x040000C9 RID: 201
		private ILogUploaderPerformanceCounters perfCounterInstance;

		// Token: 0x040000CA RID: 202
		private bool needToRetryStart;

		// Token: 0x040000CB RID: 203
		private IWatermarkFileHelper watermarkFileHelper;

		// Token: 0x040000CC RID: 204
		private int maxNumberOfReaderThreads;

		// Token: 0x040000CD RID: 205
		private int maxNumberOfWriterThreads;

		// Token: 0x040000CE RID: 206
		private bool catchReaderWriterExceptionsForUnitTest;

		// Token: 0x040000CF RID: 207
		private string unitTestCatchedExceptionMessage;
	}
}
