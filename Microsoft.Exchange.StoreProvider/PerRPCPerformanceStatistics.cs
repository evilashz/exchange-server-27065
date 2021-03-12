using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000202 RID: 514
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct PerRPCPerformanceStatistics
	{
		// Token: 0x06000862 RID: 2146 RVA: 0x0002B548 File Offset: 0x00029748
		public static PerRPCPerformanceStatistics CreateFromNative(uint validVersion, PerRpcStats nativeStats)
		{
			PerRPCPerformanceStatistics result = default(PerRPCPerformanceStatistics);
			result.validVersion = 0U;
			if (validVersion >= 1U)
			{
				result.timeInServer = new TimeSpan(0, 0, 0, 0, (int)nativeStats.cmsecInServer);
				result.timeInCPU = new TimeSpan(0, 0, 0, 0, (int)nativeStats.cmsecInCPU);
				result.pagesRead = nativeStats.ulPageRead;
				result.pagesPreread = nativeStats.ulPagePreread;
				result.logRecords = nativeStats.ulLogRecord;
				result.logBytes = nativeStats.ulcbLogRecord;
				result.ldapReads = nativeStats.ulLdapReads;
				result.ldapSearches = nativeStats.ulLdapSearches;
				result.avgDbLatency = nativeStats.avgDbLatency;
				result.avgServerLatency = nativeStats.avgServerLatency;
				result.currentThreads = nativeStats.currentThreads;
				result.validVersion = 1U;
			}
			if (validVersion >= 2U)
			{
				result.totalDbOperations = nativeStats.totalDbOperations;
				result.validVersion = 2U;
			}
			if (validVersion >= 3U)
			{
				result.currentDbThreads = nativeStats.currentDbThreads;
				result.currentSCTThreads = nativeStats.currentSCTThreads;
				result.currentSCTSessions = nativeStats.currentSCTSessions;
				result.processID = nativeStats.processID;
				result.validVersion = 3U;
			}
			if (validVersion >= 4U)
			{
				result.dataProtectionHealth = nativeStats.dataProtectionHealth;
				result.dataAvailabilityHealth = nativeStats.dataAvailabilityHealth;
				result.validVersion = 4U;
			}
			if (validVersion >= 5U)
			{
				result.currentCpuUsage = nativeStats.currentCpuUsage;
				result.validVersion = 5U;
			}
			return result;
		}

		// Token: 0x040009E6 RID: 2534
		public uint validVersion;

		// Token: 0x040009E7 RID: 2535
		public TimeSpan timeInServer;

		// Token: 0x040009E8 RID: 2536
		public TimeSpan timeInCPU;

		// Token: 0x040009E9 RID: 2537
		public uint pagesRead;

		// Token: 0x040009EA RID: 2538
		public uint pagesPreread;

		// Token: 0x040009EB RID: 2539
		public uint logRecords;

		// Token: 0x040009EC RID: 2540
		public uint logBytes;

		// Token: 0x040009ED RID: 2541
		public ulong ldapReads;

		// Token: 0x040009EE RID: 2542
		public ulong ldapSearches;

		// Token: 0x040009EF RID: 2543
		public uint avgDbLatency;

		// Token: 0x040009F0 RID: 2544
		public uint avgServerLatency;

		// Token: 0x040009F1 RID: 2545
		public uint currentThreads;

		// Token: 0x040009F2 RID: 2546
		public uint totalDbOperations;

		// Token: 0x040009F3 RID: 2547
		public uint currentDbThreads;

		// Token: 0x040009F4 RID: 2548
		public uint currentSCTThreads;

		// Token: 0x040009F5 RID: 2549
		public uint currentSCTSessions;

		// Token: 0x040009F6 RID: 2550
		public uint processID;

		// Token: 0x040009F7 RID: 2551
		public uint dataProtectionHealth;

		// Token: 0x040009F8 RID: 2552
		public uint dataAvailabilityHealth;

		// Token: 0x040009F9 RID: 2553
		public uint currentCpuUsage;
	}
}
