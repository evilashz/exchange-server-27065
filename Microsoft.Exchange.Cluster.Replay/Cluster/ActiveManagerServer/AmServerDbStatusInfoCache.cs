using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000069 RID: 105
	internal class AmServerDbStatusInfoCache
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x00018184 File Offset: 0x00016384
		internal static AmDbStatusInfo2 GetServerForDatabase(Guid mdbGuid)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config.IsUnknown)
			{
				AmTrace.Error("GetSFD: Invalid configuration (db={0})", new object[]
				{
					mdbGuid
				});
				throw new AmInvalidConfiguration(config.LastError);
			}
			AmDbStateInfo stateInfo = config.DbState.Read(mdbGuid);
			return AmServerDbStatusInfoCache.ConvertToDbStatusInfo(stateInfo);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000181E0 File Offset: 0x000163E0
		internal AmDbStatusInfo2 GetEntry(Guid databaseGuid)
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config.IsUnknown)
			{
				AmTrace.Error("GetSFD: Invalid configuration (db={0})", new object[]
				{
					databaseGuid
				});
				throw new AmInvalidConfiguration(config.LastError);
			}
			string text = config.DbState.ReadStateString(databaseGuid);
			AmServerDbStatusInfoCache.StringStatusInfoPair stringStatusInfoPair = null;
			AmDbStatusInfo2 amDbStatusInfo = null;
			lock (this.m_locker)
			{
				this.m_cacheMap.TryGetValue(databaseGuid, out stringStatusInfoPair);
			}
			if (stringStatusInfoPair != null && text != null && string.Equals(text, stringStatusInfoPair.RawStateString))
			{
				amDbStatusInfo = stringStatusInfoPair.StatusInfo;
			}
			else
			{
				AmDbStateInfo stateInfo = AmDbStateInfo.Parse(databaseGuid, text);
				amDbStatusInfo = AmServerDbStatusInfoCache.ConvertToDbStatusInfo(stateInfo);
				stringStatusInfoPair = new AmServerDbStatusInfoCache.StringStatusInfoPair(text, amDbStatusInfo);
				lock (this.m_locker)
				{
					AmTrace.Debug("Updating cache for database {0}.", new object[]
					{
						databaseGuid
					});
					this.m_cacheMap[databaseGuid] = stringStatusInfoPair;
				}
			}
			return amDbStatusInfo;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001830C File Offset: 0x0001650C
		internal void Clear()
		{
			lock (this.m_locker)
			{
				this.m_cacheMap.Clear();
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00018354 File Offset: 0x00016554
		private static AmDbStatusInfo2 ConvertToDbStatusInfo(AmDbStateInfo stateInfo)
		{
			if (!stateInfo.IsMountSucceededAtleastOnce)
			{
				AmTrace.Error("Database does not appear to be ever attempted for mount {0}", new object[]
				{
					stateInfo.DatabaseGuid
				});
				throw new AmDatabaseNeverMountedException();
			}
			return new AmDbStatusInfo2(stateInfo.ActiveServer.Fqdn, 0, stateInfo.LastMountedServer.Fqdn, stateInfo.LastMountedTime);
		}

		// Token: 0x040001EC RID: 492
		private Dictionary<Guid, AmServerDbStatusInfoCache.StringStatusInfoPair> m_cacheMap = new Dictionary<Guid, AmServerDbStatusInfoCache.StringStatusInfoPair>(100);

		// Token: 0x040001ED RID: 493
		private object m_locker = new object();

		// Token: 0x0200006A RID: 106
		internal class StringStatusInfoPair
		{
			// Token: 0x06000488 RID: 1160 RVA: 0x000183D3 File Offset: 0x000165D3
			internal StringStatusInfoPair(string rawStateStr, AmDbStatusInfo2 statusInfo)
			{
				this.RawStateString = rawStateStr;
				this.StatusInfo = statusInfo;
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x06000489 RID: 1161 RVA: 0x000183E9 File Offset: 0x000165E9
			// (set) Token: 0x0600048A RID: 1162 RVA: 0x000183F1 File Offset: 0x000165F1
			internal string RawStateString { get; private set; }

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x0600048B RID: 1163 RVA: 0x000183FA File Offset: 0x000165FA
			// (set) Token: 0x0600048C RID: 1164 RVA: 0x00018402 File Offset: 0x00016602
			internal AmDbStatusInfo2 StatusInfo { get; private set; }
		}
	}
}
