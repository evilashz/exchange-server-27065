using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200036D RID: 877
	internal sealed class PrimaryServerInfoMap : IPrimaryServerInfoMap
	{
		// Token: 0x0600261F RID: 9759 RVA: 0x00094169 File Offset: 0x00092369
		public PrimaryServerInfoMap(ShadowRedundancyEventLogger shadowRedundancyEventLogger, DataTable serverInfoTable, bool shouldCommit = true)
		{
			this.shadowRedundancyEventLogger = shadowRedundancyEventLogger;
			this.serverInfoTable = serverInfoTable;
			this.shouldCommit = shouldCommit;
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06002620 RID: 9760 RVA: 0x00094194 File Offset: 0x00092394
		// (remove) Token: 0x06002621 RID: 9761 RVA: 0x000941CC File Offset: 0x000923CC
		public event Action<PrimaryServerInfo> NotifyPrimaryServerStateChanged;

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x00094201 File Offset: 0x00092401
		public int Count
		{
			get
			{
				return this.primaryServerInfos.Count;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x0009420E File Offset: 0x0009240E
		public TimeSpan MaxDumpsterTime
		{
			get
			{
				return Components.Configuration.TransportSettings.TransportSettings.MaxDumpsterTime;
			}
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x0009422C File Offset: 0x0009242C
		public void Add(PrimaryServerInfo primaryServerInfo)
		{
			lock (this)
			{
				this.primaryServerInfos.Add(primaryServerInfo);
			}
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x00094270 File Offset: 0x00092470
		public IEnumerable<PrimaryServerInfo> GetAll()
		{
			return this.primaryServerInfos.ToArray();
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000942A4 File Offset: 0x000924A4
		public PrimaryServerInfo GetActive(string serverFqdn)
		{
			return (from primaryServerInfo in this.primaryServerInfos
			where serverFqdn.Equals(primaryServerInfo.ServerFqdn, StringComparison.InvariantCultureIgnoreCase) && primaryServerInfo.IsActive
			select primaryServerInfo).FirstOrDefault<PrimaryServerInfo>();
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000942DC File Offset: 0x000924DC
		public PrimaryServerInfo UpdateServerState(string serverFqdn, string state, ShadowRedundancyCompatibilityVersion version)
		{
			PrimaryServerInfoMap.StateChangeType stateChangeType = PrimaryServerInfoMap.StateChangeType.Add;
			PrimaryServerInfo primaryServerInfo;
			lock (this)
			{
				DateTime utcNow = DateTime.UtcNow;
				primaryServerInfo = this.GetActive(serverFqdn);
				stateChangeType = PrimaryServerInfoMap.GetStateChangeType(serverFqdn, state, this.shadowRedundancyEventLogger, primaryServerInfo);
				switch (stateChangeType)
				{
				case PrimaryServerInfoMap.StateChangeType.Add:
					primaryServerInfo = new PrimaryServerInfo(this.serverInfoTable)
					{
						ServerFqdn = serverFqdn,
						DatabaseState = state,
						StartTime = utcNow,
						Version = version
					};
					this.Commit(primaryServerInfo);
					this.Add(primaryServerInfo);
					break;
				case PrimaryServerInfoMap.StateChangeType.Change:
				{
					PrimaryServerInfo primaryServerInfo2 = new PrimaryServerInfo(primaryServerInfo, this.serverInfoTable)
					{
						DatabaseState = state,
						StartTime = utcNow
					};
					this.Commit(primaryServerInfo2);
					this.Add(primaryServerInfo2);
					primaryServerInfo.EndTime = utcNow;
					this.Commit(primaryServerInfo);
					break;
				}
				case PrimaryServerInfoMap.StateChangeType.Delete:
					primaryServerInfo.EndTime = utcNow;
					this.Commit(primaryServerInfo);
					break;
				}
			}
			if (stateChangeType != PrimaryServerInfoMap.StateChangeType.None && this.NotifyPrimaryServerStateChanged != null)
			{
				this.NotifyPrimaryServerStateChanged(primaryServerInfo);
			}
			return primaryServerInfo;
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x000943F8 File Offset: 0x000925F8
		public bool Remove(PrimaryServerInfo primaryServerInfo)
		{
			bool result;
			lock (this)
			{
				result = this.primaryServerInfos.Remove(primaryServerInfo);
			}
			return result;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x0009443C File Offset: 0x0009263C
		public IEnumerable<PrimaryServerInfo> RemoveExpiredServers(DateTime now)
		{
			Lazy<List<PrimaryServerInfo>> lazy = new Lazy<List<PrimaryServerInfo>>();
			Lazy<List<PrimaryServerInfo>> lazy2 = new Lazy<List<PrimaryServerInfo>>();
			lock (this)
			{
				foreach (PrimaryServerInfo primaryServerInfo in this.primaryServerInfos)
				{
					if (primaryServerInfo.IsActive || primaryServerInfo.EndTime + this.MaxDumpsterTime > now)
					{
						lazy.Value.Add(primaryServerInfo);
					}
					else
					{
						lazy2.Value.Add(primaryServerInfo);
					}
				}
				if (lazy.IsValueCreated)
				{
					this.primaryServerInfos = lazy.Value;
				}
			}
			if (!lazy2.IsValueCreated)
			{
				return null;
			}
			return lazy2.Value;
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x0009451C File Offset: 0x0009271C
		private static PrimaryServerInfoMap.StateChangeType GetStateChangeType(string serverFqdn, string newState, ShadowRedundancyEventLogger shadowRedundancyEventLogger, PrimaryServerInfo primaryServerInfo)
		{
			if (primaryServerInfo != null)
			{
				string databaseState = primaryServerInfo.DatabaseState;
				PrimaryServerInfoMap.StateChangeType stateChangeType;
				if (string.Equals(newState, databaseState, StringComparison.OrdinalIgnoreCase))
				{
					stateChangeType = PrimaryServerInfoMap.StateChangeType.None;
				}
				else if (newState == "0de1e7ed-0de1-0de1-0de1-de1e7edele7e")
				{
					stateChangeType = PrimaryServerInfoMap.StateChangeType.Delete;
				}
				else
				{
					stateChangeType = PrimaryServerInfoMap.StateChangeType.Change;
					if (shadowRedundancyEventLogger != null)
					{
						shadowRedundancyEventLogger.LogPrimaryServerDatabaseStateChanged(serverFqdn, databaseState, newState);
					}
				}
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "NotifyPrimaryServerState(): Server '{0}' Change Type {1} Old State {2} New State {3}", new object[]
				{
					serverFqdn,
					stateChangeType,
					databaseState,
					newState
				});
				return stateChangeType;
			}
			if (newState == "0de1e7ed-0de1-0de1-0de1-de1e7edele7e")
			{
				throw new InvalidOperationException(string.Format("Server {0} cannot be deleted as it is not present in PrimaryServerInfomap", serverFqdn));
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug(0L, "NotifyPrimaryServerState(): Server '{0}' Change Type {1} Old State {2} New State {3}", new object[]
			{
				serverFqdn,
				PrimaryServerInfoMap.StateChangeType.Add,
				"-NA-",
				newState
			});
			return PrimaryServerInfoMap.StateChangeType.Add;
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000945E1 File Offset: 0x000927E1
		private void Commit(PrimaryServerInfo primaryServerInfo)
		{
			if (this.shouldCommit)
			{
				primaryServerInfo.Commit(TransactionCommitMode.MediumLatencyLazy);
			}
		}

		// Token: 0x04001379 RID: 4985
		private readonly bool shouldCommit;

		// Token: 0x0400137A RID: 4986
		private List<PrimaryServerInfo> primaryServerInfos = new List<PrimaryServerInfo>();

		// Token: 0x0400137B RID: 4987
		private ShadowRedundancyEventLogger shadowRedundancyEventLogger;

		// Token: 0x0400137C RID: 4988
		private DataTable serverInfoTable;

		// Token: 0x0200036E RID: 878
		private enum StateChangeType
		{
			// Token: 0x0400137F RID: 4991
			None,
			// Token: 0x04001380 RID: 4992
			Add,
			// Token: 0x04001381 RID: 4993
			Change,
			// Token: 0x04001382 RID: 4994
			Delete
		}
	}
}
