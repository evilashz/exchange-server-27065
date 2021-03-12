using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000017 RID: 23
	internal class AsyncLogWrapper : IAsyncLogWrapper
	{
		// Token: 0x06000122 RID: 290 RVA: 0x000060B4 File Offset: 0x000042B4
		public AsyncLogWrapper(string logFileName, LogHeaderFormatter logHeaderFormatter, string logComponentName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("logFileName", logFileName);
			ArgumentValidator.ThrowIfNull("logHeaderFormatter", logHeaderFormatter);
			ArgumentValidator.ThrowIfNullOrEmpty("logComponentName", logComponentName);
			this.asyncLog = new AsyncLog(logFileName, logHeaderFormatter, logComponentName);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000060EC File Offset: 0x000042EC
		public void Append(LogRowFormatter row, int timestampField)
		{
			ArgumentValidator.ThrowIfNull("row", row);
			ArgumentValidator.ThrowIfNegative("timestampField", timestampField);
			try
			{
				this.asyncLog.Append(row, timestampField);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006134 File Offset: 0x00004334
		public void Configure(string logDirectory, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, int bufferSize, TimeSpan streamFlushInterval, TimeSpan backgroundWriteInterval)
		{
			this.asyncLog.Configure(logDirectory, maxAge, maxDirectorySize, maxLogFileSize, bufferSize, streamFlushInterval, backgroundWriteInterval);
		}

		// Token: 0x0400007D RID: 125
		private readonly AsyncLog asyncLog;
	}
}
