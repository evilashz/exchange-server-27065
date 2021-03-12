using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000016 RID: 22
	internal interface IAsyncLogWrapper
	{
		// Token: 0x06000120 RID: 288
		void Append(LogRowFormatter row, int timestampField);

		// Token: 0x06000121 RID: 289
		void Configure(string logDirectory, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, int bufferSize, TimeSpan streamFlushInterval, TimeSpan backgroundWriteInterval);
	}
}
