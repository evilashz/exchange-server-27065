using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct CumulativeRPCPerformanceStatistics
	{
		// Token: 0x040000C1 RID: 193
		public uint validVersion;

		// Token: 0x040000C2 RID: 194
		public TimeSpan timeInServer;

		// Token: 0x040000C3 RID: 195
		public TimeSpan timeInCPU;

		// Token: 0x040000C4 RID: 196
		public uint pagesRead;

		// Token: 0x040000C5 RID: 197
		public uint pagesPreread;

		// Token: 0x040000C6 RID: 198
		public uint logRecords;

		// Token: 0x040000C7 RID: 199
		public uint logBytes;

		// Token: 0x040000C8 RID: 200
		public ulong ldapReads;

		// Token: 0x040000C9 RID: 201
		public ulong ldapSearches;

		// Token: 0x040000CA RID: 202
		public uint avgDbLatency;

		// Token: 0x040000CB RID: 203
		public uint avgServerLatency;

		// Token: 0x040000CC RID: 204
		public uint currentThreads;

		// Token: 0x040000CD RID: 205
		public uint totalDbOperations;

		// Token: 0x040000CE RID: 206
		public uint currentDbThreads;

		// Token: 0x040000CF RID: 207
		public uint currentSCTThreads;

		// Token: 0x040000D0 RID: 208
		public uint currentSCTSessions;

		// Token: 0x040000D1 RID: 209
		public uint processID;

		// Token: 0x040000D2 RID: 210
		public uint dataProtectionHealth;

		// Token: 0x040000D3 RID: 211
		public uint dataAvailabilityHealth;

		// Token: 0x040000D4 RID: 212
		public uint currentCpuUsage;
	}
}
