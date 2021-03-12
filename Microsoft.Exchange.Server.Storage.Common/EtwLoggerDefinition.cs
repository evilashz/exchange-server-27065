using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200005C RID: 92
	public struct EtwLoggerDefinition
	{
		// Token: 0x0400055E RID: 1374
		public LoggerType LoggerType;

		// Token: 0x0400055F RID: 1375
		public string LogFilePrefixName;

		// Token: 0x04000560 RID: 1376
		public Guid ProviderGuid;

		// Token: 0x04000561 RID: 1377
		public uint LogFileSizeMB;

		// Token: 0x04000562 RID: 1378
		public uint MemoryBufferSizeKB;

		// Token: 0x04000563 RID: 1379
		public uint MinimumNumberOfMemoryBuffers;

		// Token: 0x04000564 RID: 1380
		public uint NumberOfMemoryBuffers;

		// Token: 0x04000565 RID: 1381
		public uint MaximumTotalFilesSizeMB;

		// Token: 0x04000566 RID: 1382
		public bool FileModeCreateNew;

		// Token: 0x04000567 RID: 1383
		public TimeSpan FlushTimer;

		// Token: 0x04000568 RID: 1384
		public TimeSpan RetentionLimit;
	}
}
