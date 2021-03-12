using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000028 RID: 40
	internal class WatermarkFile : IWatermarkFile, IDisposable
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00009F40 File Offset: 0x00008140
		public WatermarkFile(string logFileName, string watermarkFileName, string instanceName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("logFileName", logFileName);
			ArgumentValidator.ThrowIfNullOrEmpty("instance", instanceName);
			this.logFileFullName = logFileName;
			this.instance = instanceName;
			this.watermarkFullFileName = watermarkFileName;
			this.blocksNeedReprocessing = new List<LogFileRange>();
			this.blocksProcessed = new SortedList<long, LogFileRange>();
			this.blocksProcessedLock = new object();
			this.OpenFileAndReadWatermark();
			this.FindUnprocessedHoles();
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00009FB6 File Offset: 0x000081B6
		public List<LogFileRange> BlocksNeedProcessing
		{
			get
			{
				return this.blocksNeedReprocessing;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00009FBE File Offset: 0x000081BE
		public long ProcessedSize
		{
			get
			{
				return Interlocked.Read(ref this.processedBytes);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00009FCB File Offset: 0x000081CB
		public string WatermarkFileFullName
		{
			get
			{
				return this.watermarkFullFileName;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00009FD3 File Offset: 0x000081D3
		public SortedList<long, LogFileRange> BlocksProcessed
		{
			get
			{
				return this.blocksProcessed;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00009FDB File Offset: 0x000081DB
		public string LogFileFullName
		{
			get
			{
				return this.logFileFullName;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00009FE3 File Offset: 0x000081E3
		public bool IsDisposed
		{
			get
			{
				return this.disposed > 0;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009FEE File Offset: 0x000081EE
		public void UpdateLastReaderParsedEndOffset(long newEndOffset)
		{
			if (this.lastReaderProcessedEndOffset < newEndOffset)
			{
				this.lastReaderProcessedEndOffset = newEndOffset;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000A000 File Offset: 0x00008200
		public bool ReaderHasBytesToParse()
		{
			long logFileSize = this.GetLogFileSize();
			if (logFileSize == 0L)
			{
				return false;
			}
			if (this.lastReaderProcessedEndOffset < this.lastCheckedEndOffsertBeforeHoles)
			{
				ServiceLogger.LogCommon(ServiceLogger.LogLevel.Error, "lastReaderProcessedEndOffset", string.Format("this.lastReaderProcessedEndOffset {0}, lastCheckedEndOffsertBeforeHoles {1}", this.lastReaderProcessedEndOffset, this.lastCheckedEndOffsertBeforeHoles), ServiceLogger.Component.WatermarkFile, this.instance, this.logFileFullName);
				Tools.DebugAssert(false, string.Format("this.lastReaderProcessedEndOffset {0}, lastCheckedEndOffsertBeforeHoles {1}", this.lastReaderProcessedEndOffset, this.lastCheckedEndOffsertBeforeHoles));
			}
			if (this.blocksNeedReprocessing.Count == 0 && this.lastCheckedEndOffsertBeforeHoles < this.lastReaderProcessedEndOffset)
			{
				if (this.inMemoryBatchCount == 0)
				{
					this.FindUnprocessedHoles();
				}
				else
				{
					ServiceLogger.LogCommon(ServiceLogger.LogLevel.Debug, "MemoryBatchCountNotZero", this.inMemoryBatchCount.ToString(), ServiceLogger.Component.WatermarkFile, this.instance, this.logFileFullName);
				}
			}
			return this.blocksNeedReprocessing.Count > 0 || logFileSize > this.lastReaderProcessedEndOffset;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000A0F0 File Offset: 0x000082F0
		public void InMemoryCountIncrease()
		{
			Interlocked.Increment(ref this.inMemoryBatchCount);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000A0FE File Offset: 0x000082FE
		public void InMemoryCountDecrease()
		{
			Interlocked.Decrement(ref this.inMemoryBatchCount);
			Tools.DebugAssert(this.inMemoryBatchCount >= 0, "this.inMemoryBatchCount");
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A124 File Offset: 0x00008324
		public LogFileRange GetBlockToReprocess()
		{
			if (this.blocksNeedReprocessing.Count > 0)
			{
				LogFileRange result = this.blocksNeedReprocessing[0];
				this.blocksNeedReprocessing.RemoveAt(0);
				return result;
			}
			return null;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A15C File Offset: 0x0000835C
		public LogFileRange GetNewBlockToProcess()
		{
			long length = new FileInfo(this.logFileFullName).Length;
			if (length > this.lastReaderProcessedEndOffset)
			{
				this.TraceBlockNeedProcessing(this.lastReaderProcessedEndOffset, long.MaxValue);
				return new LogFileRange(this.lastReaderProcessedEndOffset, long.MaxValue, ProcessingStatus.NeedProcessing);
			}
			return null;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000A1B0 File Offset: 0x000083B0
		public void WriteWatermark(List<LogFileRange> ranges)
		{
			ArgumentValidator.ThrowIfNull("ranges", ranges);
			if (this.disposed > 0)
			{
				ServiceLogger.LogError(ServiceLogger.Component.WatermarkFile, (LogUploaderEventLogConstants.Message)3221231496U, string.Empty, this.instance, this.logFileFullName);
				return;
			}
			lock (this.watermarkFileLock)
			{
				if (this.disposed > 0)
				{
					ServiceLogger.LogError(ServiceLogger.Component.WatermarkFile, (LogUploaderEventLogConstants.Message)3221231496U, string.Empty, this.instance, this.logFileFullName);
					return;
				}
				if (this.streamWriter == null)
				{
					FileStream stream = File.Open(this.watermarkFullFileName, FileMode.Append, FileAccess.Write, FileShare.Read);
					this.streamWriter = new StreamWriter(stream);
				}
				string arg = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss");
				foreach (LogFileRange logFileRange in ranges)
				{
					this.streamWriter.WriteLine("{0},{1},{2}", logFileRange.StartOffset, logFileRange.EndOffset, arg);
					this.UpdateBlocksProcessed(logFileRange.StartOffset, logFileRange.EndOffset);
				}
				this.streamWriter.Flush();
			}
			if (ExTraceGlobals.WriterTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				DateTime utcNow = DateTime.UtcNow;
				string message = string.Format("Watermark update time is {0} for log {1}", utcNow, this.watermarkFullFileName);
				ExTraceGlobals.WriterTracer.TraceDebug((long)this.GetHashCode(), message);
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000A33C File Offset: 0x0000853C
		public void Dispose()
		{
			if (Interlocked.CompareExchange(ref this.disposed, 1, 0) > 0)
			{
				return;
			}
			if (this.streamWriter != null)
			{
				lock (this.watermarkFileLock)
				{
					if (this.streamWriter != null)
					{
						this.streamWriter.Close();
						this.streamWriter = null;
					}
				}
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000A3AC File Offset: 0x000085AC
		public bool IsLogCompleted()
		{
			bool flag = this.ReaderHasBytesToParse();
			ServiceLogger.LogCommon(ServiceLogger.LogLevel.Debug, "LogIsNotCompleted", string.Format("{0}, {1}, {2}, {3} ", new object[]
			{
				this.blocksNeedReprocessing.Count,
				this.lastCheckedEndOffsertBeforeHoles,
				this.lastReaderProcessedEndOffset,
				this.inMemoryBatchCount
			}), ServiceLogger.Component.WatermarkFile, this.instance, this.logFileFullName);
			return !flag && this.lastCheckedEndOffsertBeforeHoles >= this.GetLogFileSize();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000A440 File Offset: 0x00008640
		public void CreateDoneFile()
		{
			string path = Path.ChangeExtension(this.watermarkFullFileName, "done");
			try
			{
				File.Open(path, FileMode.OpenOrCreate).Close();
			}
			catch (IOException ex)
			{
				if (!ex.Message.Contains("There is not enough space on the disk."))
				{
					throw;
				}
				ServiceLogger.LogError(ServiceLogger.Component.WatermarkFile, (LogUploaderEventLogConstants.Message)3221231487U, ex.Message, this.instance, this.logFileFullName);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000A4B4 File Offset: 0x000086B4
		internal static string GetFileRangeFromWatermark(string line)
		{
			int num = line.LastIndexOf(",");
			int num2 = line.IndexOf(",");
			if (num == num2)
			{
				return line;
			}
			return line.Substring(0, num);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A4E8 File Offset: 0x000086E8
		internal LogFileRange ProcessOneWatermark(string line)
		{
			ArgumentValidator.ThrowIfNull("line", line);
			try
			{
				string fileRangeFromWatermark = WatermarkFile.GetFileRangeFromWatermark(line);
				LogFileRange logFileRange = LogFileRange.Parse(fileRangeFromWatermark);
				logFileRange.ProcessingStatus = ProcessingStatus.CompletedProcessing;
				lock (this.blocksProcessedLock)
				{
					if (!this.blocksProcessed.ContainsKey(logFileRange.StartOffset))
					{
						this.AddRangeToProcessed(logFileRange);
						return logFileRange;
					}
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_OverlappingLogRangeInWatermarkFile, this.WatermarkFileFullName, new object[]
					{
						this.WatermarkFileFullName,
						logFileRange.StartOffset,
						logFileRange.EndOffset,
						logFileRange.StartOffset,
						this.blocksProcessed[logFileRange.StartOffset].EndOffset
					});
					string text = string.Format("There are overlapping log ranges in watermark file {0}: ({1}, {2}), ({3}, {4}).", new object[]
					{
						this.WatermarkFileFullName,
						logFileRange.StartOffset,
						logFileRange.EndOffset,
						logFileRange.StartOffset,
						this.blocksProcessed[logFileRange.StartOffset].EndOffset
					});
					if (Interlocked.CompareExchange(ref WatermarkFile.overlappingWatermarksInWatermarkFile, 1, 0) == 0)
					{
						EventNotificationItem.Publish(ExchangeComponent.Name, "OverlappingWatermarkRecordsInFile", null, text, ResultSeverityLevel.Error, false);
					}
					ServiceLogger.LogError(ServiceLogger.Component.WatermarkFile, (LogUploaderEventLogConstants.Message)3221231476U, text, this.instance, this.WatermarkFileFullName);
				}
			}
			catch (MalformedLogRangeLineException ex)
			{
				string text2 = string.Format("Failed to parse watermark from {0}: {1}", this.watermarkFullFileName, ex.Message);
				ExTraceGlobals.ReaderTracer.TraceError((long)this.GetHashCode(), text2);
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_WatermarkFileParseException, this.watermarkFullFileName, new object[]
				{
					this.watermarkFullFileName,
					ex.Message
				});
				if (Interlocked.CompareExchange(ref WatermarkFile.watermarkParseError, 1, 0) == 0)
				{
					EventNotificationItem.Publish(ExchangeComponent.Name, "MalformedWatermarkRecordError", null, text2, ResultSeverityLevel.Warning, false);
				}
				ServiceLogger.LogError(ServiceLogger.Component.WatermarkFile, (LogUploaderEventLogConstants.Message)3221231475U, ex.Message, this.instance, this.watermarkFullFileName);
			}
			return null;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000A7A8 File Offset: 0x000089A8
		internal void FindUnprocessedHoles()
		{
			if (ServiceLogger.ServiceLogLevel == ServiceLogger.LogLevel.Debug)
			{
				string message = string.Format("FindblocksNeedProcessing found {0} blocks processed for {1}", this.blocksProcessed.Count, this.LogFileFullName);
				ExTraceGlobals.ReaderTracer.TraceDebug((long)this.GetHashCode(), message);
			}
			Tools.DebugAssert(this.blocksNeedReprocessing.Count == 0, "this.blocksNeedProcessing.Count == 0");
			bool foundHole = false;
			Action<long, long> action = delegate(long s, long e)
			{
				foundHole = true;
				LogFileRange logFileRange2 = new LogFileRange(s, e, ProcessingStatus.NeedProcessing);
				this.TraceBlockNeedProcessing(logFileRange2.StartOffset, logFileRange2.EndOffset);
				this.blocksNeedReprocessing.Add(logFileRange2);
			};
			long num = 0L;
			lock (this.blocksProcessedLock)
			{
				int count = this.blocksProcessed.Count;
				this.lastCheckedEndOffsertBeforeHoles = 0L;
				for (int i = 0; i < count; i++)
				{
					LogFileRange logFileRange = this.blocksProcessed.Values[i];
					if (logFileRange.EndOffset < logFileRange.StartOffset)
					{
						ServiceLogger.LogCommon(ServiceLogger.LogLevel.Error, "Invalid watermark range", string.Format("{0},{1}", logFileRange.StartOffset, logFileRange.EndOffset), ServiceLogger.Component.WatermarkFile, "", "");
						Tools.DebugAssert(false, string.Format("detected invalid range {0},{1}", logFileRange.StartOffset, logFileRange.EndOffset));
					}
					else
					{
						if (num < logFileRange.StartOffset)
						{
							action(num, logFileRange.StartOffset);
						}
						num = Math.Max(num, logFileRange.EndOffset);
						if (!foundHole)
						{
							this.lastCheckedEndOffsertBeforeHoles = num;
						}
					}
				}
			}
			if (this.lastReaderProcessedEndOffset > num)
			{
				action(num, this.lastReaderProcessedEndOffset);
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000A960 File Offset: 0x00008B60
		internal void UpdateBlocksProcessed(long startOffset, long endOffset)
		{
			LogFileRange logFileRange = new LogFileRange(startOffset, endOffset, ProcessingStatus.CompletedProcessing);
			lock (this.blocksProcessedLock)
			{
				if (this.blocksProcessed.ContainsKey(startOffset))
				{
					string text;
					if (this.blocksProcessed[startOffset].EndOffset == endOffset)
					{
						text = string.Format("Tried to add an existing block ({0}, {1}) when updating in-memory watermarks for log {2}.", startOffset, endOffset, this.logFileFullName);
						ExTraceGlobals.WriterTracer.TraceError((long)this.GetHashCode(), text);
						EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_WatermarkFileDuplicateBlock, this.logFileFullName, new object[]
						{
							startOffset.ToString(),
							endOffset.ToString(),
							this.logFileFullName
						});
						ServiceLogger.LogError(ServiceLogger.Component.WatermarkFile, (LogUploaderEventLogConstants.Message)3221231474U, string.Format("startOffset={0};endOffset={1}", startOffset, endOffset), this.instance, this.logFileFullName);
					}
					else
					{
						text = string.Format("Tried to add an block ({0}, {1}) that overlaps with an existing block ({2}, {3}) in the in-memory watermarks for log {4}.", new object[]
						{
							startOffset,
							endOffset,
							startOffset,
							this.blocksProcessed[startOffset].EndOffset,
							this.logFileFullName
						});
						ExTraceGlobals.WriterTracer.TraceError((long)this.GetHashCode(), text);
						EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_WatermarkFileOverlappingBlock, null, new object[]
						{
							startOffset,
							endOffset,
							startOffset,
							this.blocksProcessed[startOffset].EndOffset,
							this.logFileFullName
						});
						ServiceLogger.LogError(ServiceLogger.Component.WatermarkFile, (LogUploaderEventLogConstants.Message)3221231479U, string.Format("startOffset={0};endOffset={1}", startOffset, endOffset), this.instance, this.logFileFullName);
					}
					if (Interlocked.CompareExchange(ref WatermarkFile.overlappingWatermarksInMemory, 1, 0) == 0)
					{
						EventNotificationItem.Publish(ExchangeComponent.Name, "OverlappingWatermarkRecordsInMemory", null, text, ResultSeverityLevel.Error, false);
					}
				}
				else
				{
					this.AddRangeToProcessed(logFileRange);
				}
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000AB98 File Offset: 0x00008D98
		private void OpenFileAndReadWatermark()
		{
			if (!File.Exists(this.watermarkFullFileName))
			{
				return;
			}
			using (FileStream fileStream = File.Open(this.watermarkFullFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					string line;
					while ((line = streamReader.ReadLine()) != null)
					{
						LogFileRange logFileRange = this.ProcessOneWatermark(line);
						if (logFileRange != null)
						{
							this.lastReaderProcessedEndOffset = Math.Max(this.lastReaderProcessedEndOffset, logFileRange.EndOffset);
						}
					}
				}
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000AC2C File Offset: 0x00008E2C
		private void TraceBlockNeedProcessing(long startOffset, long endOffset)
		{
			if (ServiceLogger.ServiceLogLevel == ServiceLogger.LogLevel.Debug)
			{
				string message = string.Format("FindblocksNeedProcessing for {0} add ({1}, {2})", Path.GetFileName(this.logFileFullName), startOffset, endOffset);
				ExTraceGlobals.ReaderTracer.TraceDebug((long)this.GetHashCode(), message);
				ServiceLogger.LogCommon(ServiceLogger.LogLevel.Debug, "FindblocksNeedProcessing", string.Format("{0},{1}", startOffset, endOffset), ServiceLogger.Component.WatermarkFile, this.instance, this.LogFileFullName);
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000ACA4 File Offset: 0x00008EA4
		private void AddRangeToProcessed(LogFileRange logFileRange)
		{
			this.blocksProcessed.Add(logFileRange.StartOffset, logFileRange);
			Interlocked.Add(ref this.processedBytes, (long)logFileRange.Size);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000ACCC File Offset: 0x00008ECC
		private long GetLogFileSize()
		{
			long result;
			try
			{
				result = new FileInfo(this.LogFileFullName).Length;
			}
			catch (FileNotFoundException ex)
			{
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_FileDeletedWhenCheckingItsCompletion, this.LogFileFullName, new object[]
				{
					this.LogFileFullName
				});
				ServiceLogger.LogError(ServiceLogger.Component.WatermarkFile, (LogUploaderEventLogConstants.Message)2147486661U, ex.Message, this.instance, this.LogFileFullName);
				result = 0L;
			}
			return result;
		}

		// Token: 0x0400012A RID: 298
		public const string WatermarkFileExtension = "wmk";

		// Token: 0x0400012B RID: 299
		public const string DoneFileExtension = "done";

		// Token: 0x0400012C RID: 300
		public const string LogFileExtension = "log";

		// Token: 0x0400012D RID: 301
		private const string DateTimerFormatter = "yyyy-MM-ddTHH\\:mm\\:ss";

		// Token: 0x0400012E RID: 302
		private static int overlappingWatermarksInMemory;

		// Token: 0x0400012F RID: 303
		private static int overlappingWatermarksInWatermarkFile;

		// Token: 0x04000130 RID: 304
		private static int watermarkParseError;

		// Token: 0x04000131 RID: 305
		private readonly string instance;

		// Token: 0x04000132 RID: 306
		private readonly string watermarkFullFileName;

		// Token: 0x04000133 RID: 307
		private readonly string logFileFullName;

		// Token: 0x04000134 RID: 308
		private object blocksProcessedLock;

		// Token: 0x04000135 RID: 309
		private List<LogFileRange> blocksNeedReprocessing;

		// Token: 0x04000136 RID: 310
		private SortedList<long, LogFileRange> blocksProcessed;

		// Token: 0x04000137 RID: 311
		private int inMemoryBatchCount;

		// Token: 0x04000138 RID: 312
		private long processedBytes;

		// Token: 0x04000139 RID: 313
		private object watermarkFileLock = new object();

		// Token: 0x0400013A RID: 314
		private StreamWriter streamWriter;

		// Token: 0x0400013B RID: 315
		private long lastReaderProcessedEndOffset;

		// Token: 0x0400013C RID: 316
		private long lastCheckedEndOffsertBeforeHoles;

		// Token: 0x0400013D RID: 317
		private int disposed;
	}
}
