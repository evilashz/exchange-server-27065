using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000018 RID: 24
	internal abstract class LogDataBatch
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000053B9 File Offset: 0x000035B9
		public LogDataBatch(int batchSizeInBytes, long beginOffSet, string fullLogName, string logPrefix)
		{
			this.InitializeBatch(batchSizeInBytes, beginOffSet, fullLogName, logPrefix);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000053CC File Offset: 0x000035CC
		public long Size
		{
			get
			{
				return this.numOfBytesRead;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000053D4 File Offset: 0x000035D4
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000053DC File Offset: 0x000035DC
		public ProcessingStatus ProcessingStatus
		{
			get
			{
				return this.processingStatus;
			}
			set
			{
				this.processingStatus = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000053E5 File Offset: 0x000035E5
		public int RangeCount
		{
			get
			{
				return this.logRanges.Count;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000053F2 File Offset: 0x000035F2
		public long EndOffset
		{
			get
			{
				if (this.RangeCount > 0)
				{
					return this.logRanges[this.RangeCount - 1].EndOffset;
				}
				return -1L;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005418 File Offset: 0x00003618
		public string LogName
		{
			get
			{
				return Path.GetFileName(this.fullLogName);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00005425 File Offset: 0x00003625
		public LogFileRange CurrentRange
		{
			get
			{
				return this.currentRange;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000542D File Offset: 0x0000362D
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00005435 File Offset: 0x00003635
		public int NumberOfLinesInBatch
		{
			get
			{
				return this.numberOfLinesInBatch;
			}
			internal set
			{
				this.numberOfLinesInBatch = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000543E File Offset: 0x0000363E
		public string FullLogName
		{
			get
			{
				return this.fullLogName;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005446 File Offset: 0x00003646
		public int BatchSizeInBytes
		{
			get
			{
				return this.batchSizeInBytes;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000544E File Offset: 0x0000364E
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00005456 File Offset: 0x00003656
		public List<LogFileRange> LogRanges
		{
			get
			{
				return this.logRanges;
			}
			protected set
			{
				this.logRanges = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000545F File Offset: 0x0000365F
		public long NumOfBytesRead
		{
			get
			{
				return this.numOfBytesRead;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005467 File Offset: 0x00003667
		public string LogPrefix
		{
			get
			{
				return this.logPrefix;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000546F File Offset: 0x0000366F
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00005490 File Offset: 0x00003690
		public string Instance
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.instance))
				{
					this.instance = this.logPrefix;
				}
				return this.instance;
			}
			set
			{
				this.instance = value;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005499 File Offset: 0x00003699
		public void CreateNewRange(long beginOffSet)
		{
			this.currentRange = new LogFileRange(beginOffSet);
			this.logRanges.Add(this.currentRange);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000054B8 File Offset: 0x000036B8
		public void RemoveLastRange()
		{
			if (this.RangeCount > 0)
			{
				this.LogRanges.RemoveAt(this.RangeCount - 1);
				if (this.RangeCount > 0)
				{
					this.currentRange = this.LogRanges[this.RangeCount - 1];
					return;
				}
				this.currentRange = null;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000550C File Offset: 0x0000370C
		public void RemoveLastOpenRange()
		{
			if (this.currentRange != null && this.currentRange.EndOffset == 2147483647L)
			{
				ExTraceGlobals.ReaderTracer.TraceDebug<long, long>(1000L, "RemoveLastOpenRangeFromActiveBatch remove ({0},{1}", this.currentRange.StartOffset, this.currentRange.EndOffset);
				this.RemoveLastRange();
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005568 File Offset: 0x00003768
		public bool LineReceived(ReadOnlyRow row)
		{
			ArgumentValidator.ThrowIfNull("row", row);
			try
			{
				ParsedReadOnlyRow parsedReadOnlyRow = new ParsedReadOnlyRow(row);
				if (this.ShouldProcessLogLine(parsedReadOnlyRow))
				{
					this.ProcessRowData(parsedReadOnlyRow);
					this.numOfBytesRead += row.EndPosition - row.Position;
					this.numberOfLinesInBatch++;
				}
			}
			catch (InvalidLogLineException exception)
			{
				this.LogErrorAndUpdatePerfCounter(row.Position, row.EndPosition, exception, LogUploaderEventLogConstants.Tuple_LogLineParseError, (LogUploaderEventLogConstants.Message)3221230473U, "InvalidLogLineInParse");
			}
			catch (InvalidPropertyValueException exception2)
			{
				this.LogErrorAndUpdatePerfCounter(row.Position, row.EndPosition, exception2, LogUploaderEventLogConstants.Tuple_InvalidPropertyValueInParse, (LogUploaderEventLogConstants.Message)3221230481U, "InvalidPropertyValueInParse");
			}
			catch (MissingPropertyException exception3)
			{
				this.LogErrorAndUpdatePerfCounter(row.Position, row.EndPosition, exception3, LogUploaderEventLogConstants.Tuple_MissingPropertyInParse, (LogUploaderEventLogConstants.Message)3221230480U, "MissingPropertyInParse");
			}
			catch (InvalidCastException exception4)
			{
				this.LogErrorAndUpdatePerfCounter(row.Position, row.EndPosition, exception4, LogUploaderEventLogConstants.Tuple_InvalidCastInParse, (LogUploaderEventLogConstants.Message)3221230482U, "InvalidCastInParse");
			}
			catch (FailedToRetrieveRegionTagException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000056A4 File Offset: 0x000038A4
		public virtual bool IsBatchFull(ReadOnlyRow row)
		{
			if (row == null)
			{
				throw new ArgumentNullException("row", "row must not be null.");
			}
			long num = row.EndPosition - row.Position;
			return this.numOfBytesRead + num > (long)this.batchSizeInBytes && this.numberOfLinesInBatch != 0;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000056F0 File Offset: 0x000038F0
		internal void LogErrorAndUpdatePerfCounter(long rowStartOffset, long rowEndOffset, Exception exception, ExEventLog.EventTuple eventTuple, LogUploaderEventLogConstants.Message message, string component)
		{
			string text = string.Format("Failed to parse log {0} at row ({1}, {2}): \nException: {3}", new object[]
			{
				this.FullLogName,
				rowStartOffset,
				rowEndOffset,
				exception
			});
			ExTraceGlobals.ParserTracer.TraceError((long)this.GetHashCode(), text);
			EventLogger.Logger.LogEvent(eventTuple, exception.Message, new object[]
			{
				text
			});
			PerfCountersInstanceCache.GetInstance(this.Instance).TotalInvalidLogLineParseErrors.Increment();
			EventNotificationItem.Publish(ExchangeComponent.Name, component, null, text, ResultSeverityLevel.Error, false);
			ServiceLogger.LogError(ServiceLogger.Component.LogDataBatch, message, text, this.Instance, this.FullLogName);
			PerfCountersInstanceCache.GetInstance(this.Instance).TotalParseErrors.Increment();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000057B0 File Offset: 0x000039B0
		internal void SetCurrentRangeEndOffset(long offset)
		{
			this.currentRange.EndOffset = offset;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000057BE File Offset: 0x000039BE
		internal void AddRangeToBufferedBatch(long start, long end)
		{
			if (this.CurrentRange != null && this.CurrentRange.EndOffset == start)
			{
				this.CurrentRange.EndOffset = end;
				return;
			}
			this.CreateNewRange(start, end, ProcessingStatus.ReadyToWriteToDatabase);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000057EC File Offset: 0x000039EC
		internal void StartNewRangeForActiveBatch(long startOffset)
		{
			if (this.currentRange.EndOffset == 2147483647L)
			{
				this.currentRange.StartOffset = startOffset;
				return;
			}
			this.CreateNewRange(startOffset);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005818 File Offset: 0x00003A18
		internal void UpdateCurrentRangeOffsets(long startOffset, long endOffset)
		{
			if (this.currentRange.EndOffset == startOffset)
			{
				this.currentRange.EndOffset = endOffset;
				return;
			}
			if (this.currentRange.EndOffset == 2147483647L)
			{
				if (this.currentRange.StartOffset != 0L)
				{
					this.currentRange.StartOffset = startOffset;
				}
				this.currentRange.EndOffset = endOffset;
				return;
			}
			this.CreateNewRange(startOffset, endOffset, ProcessingStatus.ReadyToWriteToDatabase);
		}

		// Token: 0x06000134 RID: 308
		protected abstract bool ShouldProcessLogLine(ParsedReadOnlyRow parsedRow);

		// Token: 0x06000135 RID: 309
		protected abstract void ProcessRowData(ParsedReadOnlyRow rowData);

		// Token: 0x06000136 RID: 310 RVA: 0x00005884 File Offset: 0x00003A84
		protected void CheckIfArgumentNegative(string name, long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException(name);
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005894 File Offset: 0x00003A94
		private void InitializeBatch(int batchSizeInBytes, long beginOffSet, string fullLogName, string logPrefix)
		{
			if (batchSizeInBytes <= 0)
			{
				throw new ArgumentOutOfRangeException("batchSizeInBytes", "The byte count limit must be positive.");
			}
			this.CheckIfArgumentNegative("beginOffSet", beginOffSet);
			if (string.IsNullOrEmpty(fullLogName))
			{
				throw new ArgumentException("fullLogName", "logName cannot be null or empty");
			}
			this.batchSizeInBytes = batchSizeInBytes;
			this.LogRanges = new List<LogFileRange>();
			this.numberOfLinesInBatch = 0;
			this.numOfBytesRead = 0L;
			this.fullLogName = fullLogName;
			this.ProcessingStatus = ProcessingStatus.InProcessing;
			this.logPrefix = logPrefix;
			this.CreateNewRange(beginOffSet);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005917 File Offset: 0x00003B17
		private void CreateNewRange(long beginOffSet, long endOffset, ProcessingStatus processingStatus)
		{
			this.currentRange = new LogFileRange(beginOffSet, endOffset, processingStatus);
			this.logRanges.Add(this.currentRange);
		}

		// Token: 0x04000091 RID: 145
		private int batchSizeInBytes;

		// Token: 0x04000092 RID: 146
		private string fullLogName;

		// Token: 0x04000093 RID: 147
		private List<LogFileRange> logRanges;

		// Token: 0x04000094 RID: 148
		private LogFileRange currentRange;

		// Token: 0x04000095 RID: 149
		private int numberOfLinesInBatch;

		// Token: 0x04000096 RID: 150
		private long numOfBytesRead;

		// Token: 0x04000097 RID: 151
		private ProcessingStatus processingStatus;

		// Token: 0x04000098 RID: 152
		private string logPrefix;

		// Token: 0x04000099 RID: 153
		private string instance;
	}
}
