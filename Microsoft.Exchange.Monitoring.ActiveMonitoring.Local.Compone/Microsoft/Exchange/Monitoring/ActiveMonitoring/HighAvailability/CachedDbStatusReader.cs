using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability
{
	// Token: 0x02000189 RID: 393
	internal class CachedDbStatusReader
	{
		// Token: 0x06000B76 RID: 2934 RVA: 0x000496D0 File Offset: 0x000478D0
		private CachedDbStatusReader()
		{
			int value = HighAvailabilityUtility.NonCachedRegReader.GetValue<int>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\Parameters", "RpcCacheExpirationInSeconds", 120);
			this.DefaultTimeout = TimeSpan.FromSeconds((double)value);
			this.localCopyStatusCachedList = new CachedList<CopyStatusClientCachedEntry, Guid>(delegate(Guid[] guids)
			{
				List<KeyValuePair<Guid, CopyStatusClientCachedEntry>> list = new List<KeyValuePair<Guid, CopyStatusClientCachedEntry>>();
				Exception ex = null;
				CopyStatusClientCachedEntry[] copyStatus = CopyStatusHelper.GetCopyStatus(AmServerName.LocalComputerName, RpcGetDatabaseCopyStatusFlags2.None, guids, 5000, null, out ex);
				if (ex != null)
				{
					throw new HighAvailabilityMAProbeException(string.Format("exception caught GetCopyStatus - {0}", ex.ToString()));
				}
				if (copyStatus != null && copyStatus.Length > 0)
				{
					foreach (CopyStatusClientCachedEntry copyStatusClientCachedEntry in copyStatus)
					{
						list.Add(new KeyValuePair<Guid, CopyStatusClientCachedEntry>(copyStatusClientCachedEntry.DbGuid, copyStatusClientCachedEntry));
					}
				}
				return list.ToArray();
			}, this.DefaultTimeout);
			this.allCopiesStatusForDatabaseCachedList = new CachedList<List<CopyStatusClientCachedEntry>, Guid>(delegate(Guid guid)
			{
				IADDatabaseAvailabilityGroup localDAG = CachedAdReader.Instance.LocalDAG;
				List<AmServerName> list = new List<AmServerName>();
				if (localDAG == null || localDAG.Servers == null || localDAG.Servers.Count < 1)
				{
					list.Add(AmServerName.LocalComputerName);
				}
				else
				{
					MailboxDatabase mailboxDatabaseFromGuid = DirectoryAccessor.Instance.GetMailboxDatabaseFromGuid(guid);
					if (mailboxDatabaseFromGuid == null)
					{
						throw new InvalidOperationException(string.Format("Database with GUID '{0}' is not found.", guid));
					}
					DatabaseCopy[] databaseCopies = mailboxDatabaseFromGuid.GetDatabaseCopies();
					foreach (DatabaseCopy databaseCopy in databaseCopies)
					{
						AmServerName item = new AmServerName(databaseCopy.HostServer);
						list.Add(item);
					}
				}
				Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>> copyStatusForDatabaseInternal = CachedDbStatusReader.GetCopyStatusForDatabaseInternal(new Guid[]
				{
					guid
				}, list);
				List<CopyStatusClientCachedEntry> list2 = new List<CopyStatusClientCachedEntry>();
				if (copyStatusForDatabaseInternal == null || copyStatusForDatabaseInternal.Count < 1)
				{
					return list2;
				}
				Dictionary<AmServerName, CopyStatusClientCachedEntry> dictionary = null;
				if (!copyStatusForDatabaseInternal.TryGetValue(guid, out dictionary))
				{
					return list2;
				}
				foreach (KeyValuePair<AmServerName, CopyStatusClientCachedEntry> keyValuePair in dictionary)
				{
					list2.Add(keyValuePair.Value);
				}
				return list2;
			}, this.DefaultTimeout);
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x00049768 File Offset: 0x00047968
		public static CachedDbStatusReader Instance
		{
			get
			{
				if (CachedDbStatusReader.cachedDbStatusReaderInstance == null)
				{
					lock (CachedDbStatusReader.instanceCreationLock)
					{
						CachedDbStatusReader.cachedDbStatusReaderInstance = new CachedDbStatusReader();
					}
				}
				return CachedDbStatusReader.cachedDbStatusReaderInstance;
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000497B8 File Offset: 0x000479B8
		public CopyStatusClientCachedEntry GetDbCopyStatusOnLocalServer(Guid mdbGuid)
		{
			return this.localCopyStatusCachedList.GetValue(mdbGuid);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000497C6 File Offset: 0x000479C6
		public KeyValuePair<Guid, CopyStatusClientCachedEntry>[] GetDbsCopyStatusOnLocalServer(params Guid[] mdbGuids)
		{
			return this.localCopyStatusCachedList.GetValues(mdbGuids);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x000497D4 File Offset: 0x000479D4
		public List<CopyStatusClientCachedEntry> GetAllCopyStatusesForDatabase(Guid mdbGuid)
		{
			return this.allCopiesStatusForDatabaseCachedList.GetValue(mdbGuid);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x000497E4 File Offset: 0x000479E4
		private static Dictionary<Guid, Dictionary<AmServerName, CopyStatusClientCachedEntry>> GetCopyStatusForDatabaseInternal(Guid[] listOfMdbGuids, List<AmServerName> listOfTargetServers)
		{
			AmMultiNodeCopyStatusFetcher amMultiNodeCopyStatusFetcher = new AmMultiNodeCopyStatusFetcher(listOfTargetServers, listOfMdbGuids, null, RpcGetDatabaseCopyStatusFlags2.None, null, true);
			return amMultiNodeCopyStatusFetcher.GetStatus();
		}

		// Token: 0x040008B6 RID: 2230
		public readonly TimeSpan DefaultTimeout;

		// Token: 0x040008B7 RID: 2231
		private static CachedDbStatusReader cachedDbStatusReaderInstance = null;

		// Token: 0x040008B8 RID: 2232
		private static object instanceCreationLock = new object();

		// Token: 0x040008B9 RID: 2233
		private CachedList<CopyStatusClientCachedEntry, Guid> localCopyStatusCachedList;

		// Token: 0x040008BA RID: 2234
		private CachedList<List<CopyStatusClientCachedEntry>, Guid> allCopiesStatusForDatabaseCachedList;
	}
}
