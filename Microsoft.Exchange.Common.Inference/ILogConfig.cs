using System;

namespace Microsoft.Exchange.Inference.Common.Diagnostics
{
	// Token: 0x02000005 RID: 5
	public interface ILogConfig
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001E RID: 30
		bool IsLoggingEnabled { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001F RID: 31
		string SoftwareName { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000020 RID: 32
		string SoftwareVersion { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000021 RID: 33
		string ComponentName { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34
		string LogType { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35
		string LogPrefix { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36
		string LogPath { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000025 RID: 37
		ulong MaxLogDirectorySize { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000026 RID: 38
		ulong MaxLogFileSize { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000027 RID: 39
		TimeSpan MaxLogAge { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000028 RID: 40
		int LogSessionLineCount { get; }
	}
}
