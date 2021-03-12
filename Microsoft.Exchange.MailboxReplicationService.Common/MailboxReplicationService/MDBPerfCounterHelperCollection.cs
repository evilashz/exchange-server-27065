using System;
using Microsoft.Exchange.Collections.TimeoutCache;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200015A RID: 346
	internal static class MDBPerfCounterHelperCollection
	{
		// Token: 0x06000C25 RID: 3109 RVA: 0x0001CDC4 File Offset: 0x0001AFC4
		public static MDBPerfCounterHelper GetMDBHelper(Guid mdbGuid, bool createIfNotPresent)
		{
			MDBPerfCounterHelper mdbperfCounterHelper = null;
			MDBPerfCounterHelper result;
			lock (MDBPerfCounterHelperCollection.locker)
			{
				if (!MDBPerfCounterHelperCollection.data.TryGetValue(mdbGuid, out mdbperfCounterHelper) && createIfNotPresent)
				{
					DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(mdbGuid, null, null, FindServerFlags.AllowMissing);
					mdbperfCounterHelper = new MDBPerfCounterHelper(databaseInformation.DatabaseName ?? MrsStrings.MissingDatabaseName2(mdbGuid, databaseInformation.ForestFqdn));
					MDBPerfCounterHelperCollection.data.TryInsertSliding(mdbGuid, mdbperfCounterHelper, MDBPerfCounterHelperCollection.RefreshInterval);
				}
				result = mdbperfCounterHelper;
			}
			return result;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0001CE98 File Offset: 0x0001B098
		private static bool ShouldRemovePerfCounter(Guid mdbGuid, MDBPerfCounterHelper perfCounter)
		{
			bool result = false;
			CommonUtils.CatchKnownExceptions(delegate
			{
				DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(mdbGuid, null, null, FindServerFlags.AllowMissing);
				result = (databaseInformation.IsMissing || !databaseInformation.IsOnThisServer);
			}, null);
			return result;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0001CED1 File Offset: 0x0001B0D1
		private static void RemovePerfCounter(Guid mdbGuid, MDBPerfCounterHelper perfCounter, RemoveReason reason)
		{
			perfCounter.RemovePerfCounter();
		}

		// Token: 0x040006EF RID: 1775
		private static readonly TimeSpan RefreshInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x040006F0 RID: 1776
		private static readonly ExactTimeoutCache<Guid, MDBPerfCounterHelper> data = new ExactTimeoutCache<Guid, MDBPerfCounterHelper>(new RemoveItemDelegate<Guid, MDBPerfCounterHelper>(MDBPerfCounterHelperCollection.RemovePerfCounter), new ShouldRemoveDelegate<Guid, MDBPerfCounterHelper>(MDBPerfCounterHelperCollection.ShouldRemovePerfCounter), null, 10000, false);

		// Token: 0x040006F1 RID: 1777
		private static readonly object locker = new object();
	}
}
