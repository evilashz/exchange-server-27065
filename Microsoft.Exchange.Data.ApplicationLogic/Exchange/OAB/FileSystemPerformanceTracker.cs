using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000149 RID: 329
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FileSystemPerformanceTracker : IDisposable
	{
		// Token: 0x06000D60 RID: 3424 RVA: 0x000385AA File Offset: 0x000367AA
		public FileSystemPerformanceTracker(string marker, IOCostStream ioCostStream, IPerformanceDataLogger logger)
		{
			if (string.IsNullOrEmpty(marker))
			{
				throw new ArgumentNullException("marker");
			}
			if (ioCostStream == null)
			{
				throw new ArgumentNullException("ioCostStream");
			}
			this.marker = marker;
			this.ioCostStream = ioCostStream;
			this.logger = logger;
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x000385E8 File Offset: 0x000367E8
		public void Dispose()
		{
			if (this.logger != null)
			{
				this.logger.Log(this.marker, "FS.BytesRead", (uint)this.ioCostStream.BytesRead);
				this.logger.Log(this.marker, "FS.BytesWritten", (uint)this.ioCostStream.BytesWritten);
				this.logger.Log(this.marker, "FS.Reading.ElapsedTime", this.ioCostStream.Reading);
				this.logger.Log(this.marker, "FS.Writing.ElapsedTime", this.ioCostStream.Writing);
			}
		}

		// Token: 0x04000718 RID: 1816
		private const string FSRead = "FS.BytesRead";

		// Token: 0x04000719 RID: 1817
		private const string FSWritten = "FS.BytesWritten";

		// Token: 0x0400071A RID: 1818
		private const string FSReadingTime = "FS.Reading.ElapsedTime";

		// Token: 0x0400071B RID: 1819
		private const string FSWritingTime = "FS.Writing.ElapsedTime";

		// Token: 0x0400071C RID: 1820
		private readonly string marker;

		// Token: 0x0400071D RID: 1821
		private readonly IPerformanceDataLogger logger;

		// Token: 0x0400071E RID: 1822
		private readonly IOCostStream ioCostStream;
	}
}
