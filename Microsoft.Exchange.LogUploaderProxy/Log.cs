using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x02000015 RID: 21
	public class Log : ILogWriter
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00002B91 File Offset: 0x00000D91
		public Log(string fileNamePrefix, LogHeaderFormatter headerFormatter, string logComponent)
		{
			this.logImpl = new Log(fileNamePrefix, headerFormatter.LogHeaderFormatterImpl, logComponent);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002BAC File Offset: 0x00000DAC
		public Log(string fileNamePrefix, LogHeaderFormatter headerFormatter, string logComponent, bool handleKnownExceptions)
		{
			this.logImpl = new Log(fileNamePrefix, headerFormatter.LogHeaderFormatterImpl, logComponent, handleKnownExceptions);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002BC9 File Offset: 0x00000DC9
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00002BD6 File Offset: 0x00000DD6
		public bool TestHelper_ForceLogFileRollOver
		{
			get
			{
				return this.logImpl.TestHelper_ForceLogFileRollOver;
			}
			set
			{
				this.logImpl.TestHelper_ForceLogFileRollOver = value;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public static DirectoryInfo CreateLogDirectory(string path)
		{
			return Log.CreateLogDirectory(path);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002BEC File Offset: 0x00000DEC
		public void Flush()
		{
			this.logImpl.Flush();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002BF9 File Offset: 0x00000DF9
		public void Close()
		{
			this.logImpl.Close();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002C06 File Offset: 0x00000E06
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize)
		{
			this.logImpl.Configure(path, maxAge, maxDirectorySize, maxLogFileSize);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002C18 File Offset: 0x00000E18
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, int bufferSize, TimeSpan streamFlushInterval)
		{
			this.logImpl.Configure(path, maxAge, maxDirectorySize, maxLogFileSize, bufferSize, streamFlushInterval);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002C2E File Offset: 0x00000E2E
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, int bufferSize, TimeSpan streamFlushInterval, string note)
		{
			this.logImpl.Configure(path, maxAge, maxDirectorySize, maxLogFileSize, bufferSize, streamFlushInterval, note);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002C48 File Offset: 0x00000E48
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, int bufferSize, TimeSpan streamFlushInterval, string note, bool flushToDisk)
		{
			this.logImpl.Configure(path, maxAge, maxDirectorySize, maxLogFileSize, bufferSize, streamFlushInterval, note, flushToDisk);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00002C6D File Offset: 0x00000E6D
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, int bufferSize, TimeSpan streamFlushInterval, bool flushToDisk)
		{
			this.logImpl.Configure(path, maxAge, maxDirectorySize, maxLogFileSize, bufferSize, streamFlushInterval, flushToDisk);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002C85 File Offset: 0x00000E85
		public void Configure(string path, LogFileRollOver logFileRollOver)
		{
			this.logImpl.Configure(path, (LogFileRollOver)logFileRollOver);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002C94 File Offset: 0x00000E94
		public void Configure(string path, LogFileRollOver logFileRollOver, int bufferSize, TimeSpan streamFlushInterval)
		{
			this.logImpl.Configure(path, (LogFileRollOver)logFileRollOver, bufferSize, streamFlushInterval);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002CA6 File Offset: 0x00000EA6
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, bool enforceAccurateAge)
		{
			this.logImpl.Configure(path, maxAge, maxDirectorySize, maxLogFileSize, enforceAccurateAge);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002CBA File Offset: 0x00000EBA
		public void Configure(string path, LogFileRollOver logFileRollOver, TimeSpan maxAge)
		{
			this.logImpl.Configure(path, (LogFileRollOver)logFileRollOver, maxAge);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002CCA File Offset: 0x00000ECA
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, bool enforceAccurateAge, int bufferSize, TimeSpan streamFlushInterval)
		{
			this.logImpl.Configure(path, maxAge, maxDirectorySize, maxLogFileSize, enforceAccurateAge, bufferSize, streamFlushInterval);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, bool enforceAccurateAge, int bufferSize, TimeSpan streamFlushInterval, LogFileRollOver logFileRollOver)
		{
			this.logImpl.Configure(path, maxAge, maxDirectorySize, maxLogFileSize, enforceAccurateAge, bufferSize, streamFlushInterval, (LogFileRollOver)logFileRollOver);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002D0C File Offset: 0x00000F0C
		public void Append(IEnumerable<LogRowFormatter> rows, int timestampField)
		{
			ArgumentValidator.ThrowIfNull("rows", rows);
			List<LogRowFormatter> list = new List<LogRowFormatter>();
			foreach (LogRowFormatter logRowFormatter in rows)
			{
				list.Add(logRowFormatter.LogRowFormatterImpl);
			}
			this.logImpl.Append(list, timestampField);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002D78 File Offset: 0x00000F78
		public void Append(LogRowFormatter row, int timestampField)
		{
			this.logImpl.Append(row.LogRowFormatterImpl, timestampField, DateTime.MinValue);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002D91 File Offset: 0x00000F91
		public void Append(LogRowFormatter row, int timestampField, DateTime timeStamp)
		{
			this.logImpl.Append(row.LogRowFormatterImpl, timestampField, timeStamp);
		}

		// Token: 0x04000033 RID: 51
		private Log logImpl;
	}
}
