using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x02000008 RID: 8
	public static class Globals
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000022A3 File Offset: 0x000004A3
		public static IDirectory Directory
		{
			get
			{
				return Globals.directory;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000022AA File Offset: 0x000004AA
		public static bool IsInitialized
		{
			get
			{
				return Globals.directory != null;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000022B7 File Offset: 0x000004B7
		private static Globals.DirectoryCreatorDelegate DirectoryFactory
		{
			get
			{
				return Globals.directoryFactory.Value;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000022C3 File Offset: 0x000004C3
		public static void Initialize(ICachePerformanceCounters mailboxCacheCounters, ICachePerformanceCounters foreignMailboxCacheCounters, ICachePerformanceCounters addressCacheCounters, ICachePerformanceCounters foreignAddressCacheCounters, ICachePerformanceCounters databaseCacheCounters, ICachePerformanceCounters orgConatinerCacheCounters, ICachePerformanceCounters distributionListMembershipCacheCountes)
		{
			ADExecutionTracker.Initialize();
			Globals.directory = Globals.DirectoryFactory(mailboxCacheCounters, foreignMailboxCacheCounters, addressCacheCounters, foreignAddressCacheCounters, databaseCacheCounters, orgConatinerCacheCounters, distributionListMembershipCacheCountes);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000022E3 File Offset: 0x000004E3
		public static void Terminate()
		{
			Globals.directory = null;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000022EB File Offset: 0x000004EB
		public static uint GetCurrentServerCPUUsagePercentage()
		{
			return CpuUsage.GetCurrentUsagePercentage();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000022F2 File Offset: 0x000004F2
		internal static IDisposable SetTestHook(Globals.DirectoryCreatorDelegate testHook)
		{
			return Globals.directoryFactory.SetTestHook(testHook);
		}

		// Token: 0x04000003 RID: 3
		private const string ErrorDirectoryServicesLayerIsNotInitialized = "The DirectoryServices layer has not yet been initialized.";

		// Token: 0x04000004 RID: 4
		private static IDirectory directory;

		// Token: 0x04000005 RID: 5
		private static Hookable<Globals.DirectoryCreatorDelegate> directoryFactory = Hookable<Globals.DirectoryCreatorDelegate>.Create(true, new Globals.DirectoryCreatorDelegate(Microsoft.Exchange.Server.Storage.DirectoryServices.Directory.Create));

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x06000044 RID: 68
		internal delegate IDirectory DirectoryCreatorDelegate(ICachePerformanceCounters mailboxCacheCounters, ICachePerformanceCounters foreignMailboxCacheCounters, ICachePerformanceCounters addressCacheCounters, ICachePerformanceCounters foreignAddressCacheCounters, ICachePerformanceCounters databaseCacheCounters, ICachePerformanceCounters orgContainerCacheCounters, ICachePerformanceCounters distributionListMembershipCacheCountes);
	}
}
