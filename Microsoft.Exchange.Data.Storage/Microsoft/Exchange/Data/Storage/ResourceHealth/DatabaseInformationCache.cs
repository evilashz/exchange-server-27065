using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B23 RID: 2851
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DatabaseInformationCache : LazyLookupExactTimeoutCache<Guid, DatabaseInformation>, IDatabaseInformationCache
	{
		// Token: 0x06006756 RID: 26454 RVA: 0x001B4ED6 File Offset: 0x001B30D6
		public DatabaseInformationCache() : base(10000, false, TimeSpan.FromHours(1.0), CacheFullBehavior.ExpireExisting)
		{
		}

		// Token: 0x06006757 RID: 26455 RVA: 0x001B4EF4 File Offset: 0x001B30F4
		protected override DatabaseInformation CreateOnCacheMiss(Guid key, ref bool shouldAdd)
		{
			shouldAdd = true;
			Dictionary<IADServer, CopyInfo> mailboxDatabaseCopyStatus;
			try
			{
				mailboxDatabaseCopyStatus = DatabaseInformationCache.dumpsterReplication.GetMailboxDatabaseCopyStatus(key);
			}
			catch (Exception ex)
			{
				if (ex is HaRpcServerTransientBaseException || ex is HaRpcServerBaseException || ex is DataBaseNotFoundException)
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceError<Guid, Exception>((long)this.GetHashCode(), "[DatabaseInformationCache.HandleHaExceptions] Copy status lookup failed for MDB '{0}' with exception: '{1}'.", key, ex);
					return null;
				}
				throw;
			}
			if (mailboxDatabaseCopyStatus != null && mailboxDatabaseCopyStatus.Count > 0)
			{
				foreach (KeyValuePair<IADServer, CopyInfo> keyValuePair in mailboxDatabaseCopyStatus)
				{
					if (keyValuePair.Value.Status != null && keyValuePair.Value.Status.IsActiveCopy() && !string.IsNullOrEmpty(keyValuePair.Value.Status.DBName) && !string.IsNullOrEmpty(keyValuePair.Value.Status.DatabaseVolumeName))
					{
						return new DatabaseInformation(keyValuePair.Value.Status.DBGuid, keyValuePair.Value.Status.DBName, keyValuePair.Value.Status.DatabaseVolumeName);
					}
				}
			}
			return null;
		}

		// Token: 0x17001C6C RID: 7276
		// (get) Token: 0x06006758 RID: 26456 RVA: 0x001B5040 File Offset: 0x001B3240
		public static DatabaseInformationCache Singleton
		{
			get
			{
				return DatabaseInformationCache.singleton;
			}
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x001B5047 File Offset: 0x001B3247
		IDatabaseInformation IDatabaseInformationCache.Get(Guid key)
		{
			return base.Get(key);
		}

		// Token: 0x04003A89 RID: 14985
		private static readonly DatabaseInformationCache singleton = new DatabaseInformationCache();

		// Token: 0x04003A8A RID: 14986
		private static readonly DumpsterReplicationStatus dumpsterReplication = new DumpsterReplicationStatus();
	}
}
