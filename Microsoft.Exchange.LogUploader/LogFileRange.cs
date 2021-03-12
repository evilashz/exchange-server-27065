using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200001C RID: 28
	internal class LogFileRange
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00005DD5 File Offset: 0x00003FD5
		public LogFileRange(long startOffset)
		{
			if (startOffset < 0L)
			{
				throw new ArgumentOutOfRangeException("startOffset", "The start offset must be non-negative.");
			}
			this.startOffset = startOffset;
			this.endOffset = 2147483647L;
			this.processingStatus = ProcessingStatus.InProcessing;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005E0C File Offset: 0x0000400C
		public LogFileRange(long startOffset, long endOffset, ProcessingStatus processingStatus) : this(startOffset)
		{
			if (endOffset < startOffset)
			{
				throw new ArgumentException(string.Format("The end offset must be at least as large as the start offset. startOffset={0}, endOffset={1}", startOffset, endOffset));
			}
			this.endOffset = endOffset;
			this.processingStatus = processingStatus;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005E43 File Offset: 0x00004043
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00005E4B File Offset: 0x0000404B
		public long StartOffset
		{
			get
			{
				return this.startOffset;
			}
			internal set
			{
				if (value < 0L)
				{
					throw new InvalidLogFileRangeException(Strings.LogFileRangeNegativeStartOffset);
				}
				if (this.endOffset < value)
				{
					throw new InvalidLogFileRangeException(Strings.LogFileRangeWrongOffsets(value, this.endOffset));
				}
				this.startOffset = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005E7F File Offset: 0x0000407F
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00005E87 File Offset: 0x00004087
		public long EndOffset
		{
			get
			{
				return this.endOffset;
			}
			set
			{
				if (value < 0L)
				{
					throw new InvalidLogFileRangeException(Strings.LogFileRangeNegativeEndOffset);
				}
				if (this.startOffset > value)
				{
					throw new InvalidLogFileRangeException(Strings.LogFileRangeWrongOffsets(this.startOffset, value));
				}
				this.endOffset = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005EBB File Offset: 0x000040BB
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00005EC3 File Offset: 0x000040C3
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005ECC File Offset: 0x000040CC
		public int Size
		{
			get
			{
				return (int)(this.EndOffset - this.startOffset);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005EDC File Offset: 0x000040DC
		public static LogFileRange Parse(string line)
		{
			if (line == null)
			{
				throw new ArgumentNullException("line");
			}
			string[] array = line.Split(new char[]
			{
				','
			});
			if (array.Length != 2)
			{
				throw new MalformedLogRangeLineException(Strings.MalformedLogRangeLine(line));
			}
			LogFileRange result;
			try
			{
				long num = long.Parse(array[0]);
				long num2 = long.Parse(array[1]);
				if (num < 0L || num2 < 0L || num > num2)
				{
					throw new MalformedLogRangeLineException(Strings.MalformedLogRangeLine(line));
				}
				result = new LogFileRange(num, num2, ProcessingStatus.CompletedProcessing);
			}
			catch (FormatException)
			{
				throw new MalformedLogRangeLineException(Strings.MalformedLogRangeLine(line));
			}
			catch (OverflowException)
			{
				throw new MalformedLogRangeLineException(Strings.MalformedLogRangeLine(line));
			}
			return result;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005F90 File Offset: 0x00004190
		public override string ToString()
		{
			return string.Format("{0},{1}", this.startOffset, this.EndOffset);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005FB2 File Offset: 0x000041B2
		public bool IsAdjacentTo(LogFileRange other)
		{
			return this.EndOffset == other.startOffset || other.EndOffset == this.startOffset;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005FD2 File Offset: 0x000041D2
		public bool Overlaps(LogFileRange other)
		{
			return (this.startOffset <= other.StartOffset && other.StartOffset < this.EndOffset) || (other.StartOffset <= this.startOffset && this.startOffset < other.EndOffset);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006010 File Offset: 0x00004210
		public bool Overlaps(IEnumerable<LogFileRange> others)
		{
			foreach (LogFileRange other in others)
			{
				if (this.Overlaps(other))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006064 File Offset: 0x00004264
		public void Merge(LogFileRange other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other", "The other log file range must not be null.");
			}
			if (this.ProcessingStatus != other.ProcessingStatus)
			{
				throw new ArgumentException("The two log file ranges must have the same processing status.", "other");
			}
			string message = Strings.MergeLogRangesFailed(other.StartOffset, other.EndOffset, this.startOffset, this.EndOffset);
			if (this.Overlaps(other))
			{
				throw new IllegalRangeMergeException(message);
			}
			if (!this.IsAdjacentTo(other))
			{
				throw new IllegalRangeMergeException(message);
			}
			if (this.EndOffset == other.StartOffset)
			{
				this.EndOffset = other.EndOffset;
				return;
			}
			this.startOffset = other.StartOffset;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006108 File Offset: 0x00004308
		public LogFileRange Split(long splitOffset)
		{
			if (this.startOffset >= splitOffset || splitOffset >= this.EndOffset)
			{
				string paramName = string.Format(CultureInfo.InvariantCulture, "Argument ({0}) must be greater than startOffset ({1}) and less than endOffset ({2})", new object[]
				{
					splitOffset,
					this.startOffset,
					this.EndOffset
				});
				throw new ArgumentOutOfRangeException(paramName, "other");
			}
			LogFileRange result = new LogFileRange(splitOffset, this.EndOffset, ProcessingStatus.NeedProcessing);
			this.EndOffset = splitOffset;
			return result;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006186 File Offset: 0x00004386
		public bool Equals(LogFileRange other)
		{
			return this.startOffset == other.startOffset && this.endOffset == other.endOffset && this.processingStatus == other.processingStatus;
		}

		// Token: 0x040000AA RID: 170
		private long startOffset;

		// Token: 0x040000AB RID: 171
		private long endOffset;

		// Token: 0x040000AC RID: 172
		private ProcessingStatus processingStatus;
	}
}
