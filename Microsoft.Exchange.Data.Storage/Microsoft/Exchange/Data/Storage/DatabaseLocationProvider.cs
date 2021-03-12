using System;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001CD RID: 461
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DatabaseLocationProvider : IDatabaseLocationProvider
	{
		// Token: 0x06001897 RID: 6295 RVA: 0x00078030 File Offset: 0x00076230
		public DatabaseLocationProvider() : this(ActiveManager.GetCachingActiveManagerInstance(), NullPerformanceDataLogger.Instance)
		{
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00078042 File Offset: 0x00076242
		public DatabaseLocationProvider(ActiveManager activeManager, IPerformanceDataLogger performanceDataLogger)
		{
			this.activeManager = activeManager;
			this.performanceDataLogger = performanceDataLogger;
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00078058 File Offset: 0x00076258
		public DatabaseLocationInfo GetLocationInfo(Guid mdbGuid, bool bypassCache, bool ignoreSiteBoundary)
		{
			GetServerForDatabaseFlags getServerForDatabaseFlags = GetServerForDatabaseFlags.None;
			if (ignoreSiteBoundary)
			{
				getServerForDatabaseFlags |= GetServerForDatabaseFlags.IgnoreAdSiteBoundary;
			}
			if (bypassCache)
			{
				getServerForDatabaseFlags |= GetServerForDatabaseFlags.ReadThrough;
			}
			DatabaseLocationInfo serverForDatabase;
			try
			{
				serverForDatabase = this.activeManager.GetServerForDatabase(mdbGuid, getServerForDatabaseFlags, this.performanceDataLogger);
			}
			catch (DatabaseNotFoundException innerException)
			{
				throw new MailboxInfoStaleException(string.Format("Mailbox database guid: {0}", mdbGuid), innerException);
			}
			if (serverForDatabase == null || serverForDatabase.RequestResult == DatabaseLocationInfoResult.Unknown)
			{
				throw new DatabaseLocationUnavailableException(ServerStrings.DatabaseLocationNotAvailable(mdbGuid));
			}
			ExTraceGlobals.SessionTracer.TraceDebug<GetServerForDatabaseFlags, DatabaseLocationInfoResult, string>((long)this.GetHashCode(), "DatabaseLocationProvider::GetLocationInfo. flags {0}, result {1}, {2}", getServerForDatabaseFlags, serverForDatabase.RequestResult, serverForDatabase.ServerFqdn);
			return serverForDatabase;
		}

		// Token: 0x04000CE2 RID: 3298
		private readonly ActiveManager activeManager;

		// Token: 0x04000CE3 RID: 3299
		private readonly IPerformanceDataLogger performanceDataLogger;
	}
}
