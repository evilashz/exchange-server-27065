using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001E6 RID: 486
	internal class CopyStatusClientLookupTable : ReaderWriterLockedBase
	{
		// Token: 0x06001347 RID: 4935 RVA: 0x0004D724 File Offset: 0x0004B924
		public CopyStatusClientCachedEntry GetCopyStatusCachedEntry(Guid dbGuid, AmServerName server)
		{
			CopyStatusClientCachedEntry status = null;
			base.ReaderLockedOperation(delegate
			{
				status = this.GetCopyStatusCachedEntryNoLock(dbGuid, server);
			});
			return status;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0004D818 File Offset: 0x0004BA18
		public List<CopyStatusClientCachedEntry> GetCopyStatusCachedEntriesByDatabase(Guid dbGuid)
		{
			List<CopyStatusClientCachedEntry> statuses = null;
			Dictionary<AmServerName, CopyStatusClientCachedEntry> statusTable = null;
			base.ReaderLockedOperation(delegate
			{
				this.m_dbServerStatuses.TryGetValue(dbGuid, out statusTable);
				if (statusTable != null)
				{
					foreach (KeyValuePair<AmServerName, CopyStatusClientCachedEntry> keyValuePair in statusTable)
					{
						if (statuses == null)
						{
							statuses = new List<CopyStatusClientCachedEntry>(statusTable.Count);
						}
						statuses.Add(keyValuePair.Value);
					}
				}
			});
			return statuses;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0004D90C File Offset: 0x0004BB0C
		public List<CopyStatusClientCachedEntry> GetCopyStatusCachedEntriesByServer(AmServerName server)
		{
			List<CopyStatusClientCachedEntry> statuses = null;
			Dictionary<Guid, CopyStatusClientCachedEntry> statusTable = null;
			base.ReaderLockedOperation(delegate
			{
				this.m_serverDbStatuses.TryGetValue(server, out statusTable);
				if (statusTable != null)
				{
					foreach (KeyValuePair<Guid, CopyStatusClientCachedEntry> keyValuePair in statusTable)
					{
						if (statuses == null)
						{
							statuses = new List<CopyStatusClientCachedEntry>(statusTable.Count);
						}
						statuses.Add(keyValuePair.Value);
					}
				}
			});
			return statuses;
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0004D980 File Offset: 0x0004BB80
		public CopyStatusClientCachedEntry AddCopyStatusCachedEntry(Guid dbGuid, AmServerName server, CopyStatusClientCachedEntry status)
		{
			CopyStatusClientCachedEntry returnEntry = null;
			base.WriterLockedOperation(delegate
			{
				returnEntry = this.AddCopyStatusCachedEntryNoLock(dbGuid, server, status);
			});
			return returnEntry;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0004DA44 File Offset: 0x0004BC44
		public IEnumerable<CopyStatusClientCachedEntry> AddCopyStatusCachedEntriesForServer(AmServerName server, IEnumerable<CopyStatusClientCachedEntry> statusEntries)
		{
			int capacity = statusEntries.Count<CopyStatusClientCachedEntry>();
			List<CopyStatusClientCachedEntry> returnEntries = new List<CopyStatusClientCachedEntry>(capacity);
			base.WriterLockedOperation(delegate
			{
				foreach (CopyStatusClientCachedEntry copyStatusClientCachedEntry in statusEntries)
				{
					returnEntries.Add(this.AddCopyStatusCachedEntryNoLock(copyStatusClientCachedEntry.DbGuid, server, copyStatusClientCachedEntry));
				}
			});
			return returnEntries;
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0004DB64 File Offset: 0x0004BD64
		public void UpdateCopyStatusCachedEntries(Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>> dbServerStatuses)
		{
			base.WriterLockedOperation(delegate
			{
				foreach (Guid key in dbServerStatuses.Keys)
				{
					Dictionary<AmServerName, CopyStatusClientCachedEntry> dictionary = dbServerStatuses[key];
					if (dictionary != null)
					{
						foreach (KeyValuePair<AmServerName, CopyStatusClientCachedEntry> keyValuePair in dictionary)
						{
							this.AddCopyStatusCachedEntryNoLock(keyValuePair.Value.DbGuid, keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
			});
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0004DB98 File Offset: 0x0004BD98
		private CopyStatusClientCachedEntry GetCopyStatusCachedEntryNoLock(Guid dbGuid, AmServerName server)
		{
			CopyStatusClientCachedEntry result = null;
			if (this.m_dbServerStatuses.ContainsKey(dbGuid))
			{
				Dictionary<AmServerName, CopyStatusClientCachedEntry> dictionary = this.m_dbServerStatuses[dbGuid];
				dictionary.TryGetValue(server, out result);
			}
			return result;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0004DBD0 File Offset: 0x0004BDD0
		private CopyStatusClientCachedEntry AddCopyStatusCachedEntryNoLock(Guid dbGuid, AmServerName server, CopyStatusClientCachedEntry status)
		{
			CopyStatusClientCachedEntry copyStatusCachedEntryNoLock = this.GetCopyStatusCachedEntryNoLock(dbGuid, server);
			CopyStatusClientCachedEntry result = copyStatusCachedEntryNoLock;
			if (CopyStatusHelper.CheckCopyStatusNewer(status, copyStatusCachedEntryNoLock))
			{
				this.AddCopyStatusToDbTable(dbGuid, server, status);
				this.AddCopyStatusToServerTable(dbGuid, server, status);
				result = status;
			}
			return result;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0004DC08 File Offset: 0x0004BE08
		private void AddCopyStatusToServerTable(Guid dbGuid, AmServerName server, CopyStatusClientCachedEntry status)
		{
			Dictionary<Guid, CopyStatusClientCachedEntry> dictionary = null;
			if (!this.m_serverDbStatuses.TryGetValue(server, out dictionary))
			{
				dictionary = new Dictionary<Guid, CopyStatusClientCachedEntry>(48);
				this.m_serverDbStatuses[server] = dictionary;
			}
			dictionary[dbGuid] = status;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0004DC44 File Offset: 0x0004BE44
		private void AddCopyStatusToDbTable(Guid dbGuid, AmServerName server, CopyStatusClientCachedEntry status)
		{
			Dictionary<AmServerName, CopyStatusClientCachedEntry> dictionary = null;
			if (!this.m_dbServerStatuses.TryGetValue(dbGuid, out dictionary))
			{
				dictionary = new Dictionary<AmServerName, CopyStatusClientCachedEntry>(5);
				this.m_dbServerStatuses[dbGuid] = dictionary;
			}
			dictionary[server] = status;
		}

		// Token: 0x0400076C RID: 1900
		internal const int InitialActiveDatabasesPerServerCapacity = 20;

		// Token: 0x0400076D RID: 1901
		internal const int InitialDatabaseCopiesPerServerCapacity = 48;

		// Token: 0x0400076E RID: 1902
		internal const int InitialDatabaseCopiesPerDbCapacity = 5;

		// Token: 0x0400076F RID: 1903
		internal const int InitialServersCapacity = 16;

		// Token: 0x04000770 RID: 1904
		internal const int InitialDatabasesPerDagCapacity = 160;

		// Token: 0x04000771 RID: 1905
		private Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>> m_dbServerStatuses = new Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>>(160);

		// Token: 0x04000772 RID: 1906
		private Dictionary<AmServerName, Dictionary<Guid, CopyStatusClientCachedEntry>> m_serverDbStatuses = new Dictionary<AmServerName, Dictionary<Guid, CopyStatusClientCachedEntry>>(16);
	}
}
