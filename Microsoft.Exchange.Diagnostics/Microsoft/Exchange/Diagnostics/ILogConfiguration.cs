using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000129 RID: 297
	internal interface ILogConfiguration
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600088C RID: 2188
		bool IsLoggingEnabled { get; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600088D RID: 2189
		bool IsActivityEventHandler { get; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600088E RID: 2190
		string LogPath { get; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600088F RID: 2191
		string LogPrefix { get; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000890 RID: 2192
		string LogComponent { get; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000891 RID: 2193
		string LogType { get; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000892 RID: 2194
		long MaxLogDirectorySizeInBytes { get; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000893 RID: 2195
		long MaxLogFileSizeInBytes { get; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000894 RID: 2196
		TimeSpan MaxLogAge { get; }
	}
}
