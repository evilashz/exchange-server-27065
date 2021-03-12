using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common
{
	// Token: 0x02000002 RID: 2
	public class MonitorHealthCommon
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public MonitorHealthCommon(string identity, string healthSet, bool haImapctingOnly) : this(identity, healthSet, haImapctingOnly, 0, 70)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E0 File Offset: 0x000002E0
		public MonitorHealthCommon(string identity, string healthSet, bool haImapctingOnly, int groupSize, int minimumOnlinePercent)
		{
			this.identity = identity;
			this.healthSet = healthSet;
			this.isHaImapctingOnly = haImapctingOnly;
			this.groupSize = groupSize;
			this.minimumOnlinePercent = minimumOnlinePercent;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002141 File Offset: 0x00000341
		internal Dictionary<string, Dictionary<string, List<MonitorHealthEntry>>> ServerHealthMap
		{
			get
			{
				return this.serverHealthMap;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002149 File Offset: 0x00000349
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002151 File Offset: 0x00000351
		internal int GroupSize
		{
			get
			{
				return this.groupSize;
			}
			set
			{
				this.groupSize = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000215A File Offset: 0x0000035A
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002162 File Offset: 0x00000362
		internal int MinimumOnlinePercent
		{
			get
			{
				return this.minimumOnlinePercent;
			}
			set
			{
				this.minimumOnlinePercent = value;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000216C File Offset: 0x0000036C
		public List<MonitorHealthEntry> GetMonitorHealthEntries(out LocalizedException exception)
		{
			if (string.IsNullOrWhiteSpace(this.identity))
			{
				throw new ArgumentNullException("The identity can't be empty.");
			}
			List<RpcGetMonitorHealthStatus.RpcMonitorHealthEntry> list = null;
			List<MonitorHealthEntry> list2 = new List<MonitorHealthEntry>();
			exception = null;
			try
			{
				list = RpcGetMonitorHealthStatus.Invoke(this.identity, this.healthSet, 30000);
			}
			catch (ActiveMonitoringServerException ex)
			{
				exception = ex;
			}
			catch (ActiveMonitoringServerTransientException ex2)
			{
				exception = ex2;
			}
			if (list == null || list.Count == 0)
			{
				list = this.CreateEmptyEntry();
			}
			bool flag = !string.IsNullOrWhiteSpace(this.healthSet);
			foreach (RpcGetMonitorHealthStatus.RpcMonitorHealthEntry rpcMonitorHealthEntry in list)
			{
				if ((!this.isHaImapctingOnly || rpcMonitorHealthEntry.IsHaImpacting) && !string.Equals(rpcMonitorHealthEntry.Name, "HealthManagerHeartbeatMonitor", StringComparison.OrdinalIgnoreCase) && (!flag || string.Equals(rpcMonitorHealthEntry.HealthSetName, this.healthSet, StringComparison.OrdinalIgnoreCase)))
				{
					MonitorHealthEntry item = new MonitorHealthEntry(this.identity, rpcMonitorHealthEntry);
					list2.Add(item);
				}
			}
			return list2;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002298 File Offset: 0x00000498
		public List<ConsolidatedHealth> GetConsolidateHealthEntries()
		{
			List<ConsolidatedHealth> list = new List<ConsolidatedHealth>();
			if (this.serverHealthMap.Count <= 0)
			{
				return null;
			}
			foreach (KeyValuePair<string, Dictionary<string, List<MonitorHealthEntry>>> keyValuePair in this.serverHealthMap)
			{
				string key = keyValuePair.Key;
				Dictionary<string, List<MonitorHealthEntry>> value = keyValuePair.Value;
				foreach (KeyValuePair<string, List<MonitorHealthEntry>> keyValuePair2 in value)
				{
					string key2 = keyValuePair2.Key;
					List<MonitorHealthEntry> value2 = keyValuePair2.Value;
					string healthGroup = null;
					MonitorServerComponentState state = MonitorServerComponentState.Unknown;
					if (value2 != null && value2.Count > 0)
					{
						MonitorHealthEntry monitorHealthEntry = value2.First<MonitorHealthEntry>();
						if (monitorHealthEntry != null)
						{
							healthGroup = monitorHealthEntry.HealthGroupName;
							state = monitorHealthEntry.CurrentHealthSetState;
						}
					}
					int monitorCount = 0;
					int haImpactingMonitorCount = 0;
					if (value2 != null)
					{
						monitorCount = value2.Count<MonitorHealthEntry>();
						haImpactingMonitorCount = value2.Count((MonitorHealthEntry che) => che.IsHaImpacting);
					}
					MonitorHealthCommon.HealthSetStatistics healthSetStats = this.GetHealthSetStats(value2);
					MonitorAlertState alertValue = this.CalculatedConsolidatedHealthSetAlertValue(healthSetStats);
					DateTime lastTransitionTime = healthSetStats.LastTransitionTime;
					ConsolidatedHealth item = new ConsolidatedHealth(key, key2, healthGroup, alertValue, state, monitorCount, haImpactingMonitorCount, lastTransitionTime, value2);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002424 File Offset: 0x00000624
		internal void SetServerHealthMap(List<MonitorHealthEntry> entries)
		{
			if (entries == null)
			{
				throw new ArgumentNullException("The MonitorHealth entries are null.");
			}
			Dictionary<string, List<MonitorHealthEntry>> dictionary = null;
			foreach (MonitorHealthEntry monitorHealthEntry in entries)
			{
				if (!this.serverHealthMap.TryGetValue(monitorHealthEntry.Server, out dictionary))
				{
					dictionary = new Dictionary<string, List<MonitorHealthEntry>>();
					this.serverHealthMap[monitorHealthEntry.Server] = dictionary;
				}
				List<MonitorHealthEntry> list = null;
				if (!dictionary.TryGetValue(monitorHealthEntry.HealthSetName, out list))
				{
					list = new List<MonitorHealthEntry>();
					dictionary[monitorHealthEntry.HealthSetName] = list;
				}
				list.Add(monitorHealthEntry);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000024E4 File Offset: 0x000006E4
		internal ConsolidatedHealth ConsolidateAcrossServers(Dictionary<string, ConsolidatedHealth> serverHealthMap)
		{
			MonitorHealthCommon.HealthSetStatistics healthSetStatistics = new MonitorHealthCommon.HealthSetStatistics();
			int num = 0;
			int num2 = 0;
			Dictionary<string, ConsolidatedHealth>.ValueCollection values = serverHealthMap.Values;
			string value = null;
			string healthGroup = null;
			List<ConsolidatedHealth> list = new List<ConsolidatedHealth>();
			Dictionary<string, ConsolidatedHealth>.ValueCollection values2 = serverHealthMap.Values;
			int num3 = values2.Count((ConsolidatedHealth health) => health == null);
			if (num3 > 0)
			{
				ConsolidatedHealth consolidatedHealth = serverHealthMap.Values.First((ConsolidatedHealth health) => health != null);
				if (consolidatedHealth != null)
				{
					Dictionary<string, ConsolidatedHealth> dictionary = new Dictionary<string, ConsolidatedHealth>();
					foreach (KeyValuePair<string, ConsolidatedHealth> keyValuePair in serverHealthMap)
					{
						string key = keyValuePair.Key;
						if (keyValuePair.Value == null)
						{
							ConsolidatedHealth value2 = new ConsolidatedHealth(key, consolidatedHealth.HealthSet, consolidatedHealth.HealthGroup);
							dictionary[key] = value2;
						}
					}
					foreach (KeyValuePair<string, ConsolidatedHealth> keyValuePair2 in dictionary)
					{
						serverHealthMap[keyValuePair2.Key] = keyValuePair2.Value;
					}
				}
			}
			foreach (ConsolidatedHealth consolidatedHealth2 in values)
			{
				if (consolidatedHealth2 != null)
				{
					this.UpdateHealthStats(healthSetStatistics, consolidatedHealth2.AlertValue, consolidatedHealth2.LastTransitionTime);
					if (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(consolidatedHealth2.HealthSet))
					{
						value = consolidatedHealth2.HealthSet;
						healthGroup = consolidatedHealth2.HealthGroup;
					}
					num += consolidatedHealth2.MonitorCount;
					num2 += consolidatedHealth2.HaImpactingMonitorCount;
					list.Add(consolidatedHealth2);
				}
				else
				{
					this.UpdateHealthStats(healthSetStatistics, MonitorAlertState.Unknown, DateTime.MinValue);
				}
			}
			if (string.IsNullOrEmpty(value))
			{
				value = "Unknown";
				healthGroup = "Unknown";
			}
			MonitorAlertState alertValue = this.CalculatedConsolidatedHealthSetAlertValue(healthSetStatistics);
			DateTime lastTransitionTime = healthSetStatistics.LastTransitionTime;
			int haImpactingMonitorCount = num2;
			MonitorHealthCommon.ServerComponentStateStatistics serverComponentStats = this.GetServerComponentStats(serverHealthMap.Values);
			MonitorServerComponentState state = this.CalculatedConsolidatedServerComponentState(serverComponentStats);
			return new ConsolidatedHealth(value, healthGroup, alertValue, state, num, haImpactingMonitorCount, lastTransitionTime, list);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002740 File Offset: 0x00000940
		private List<RpcGetMonitorHealthStatus.RpcMonitorHealthEntry> CreateEmptyEntry()
		{
			return new List<RpcGetMonitorHealthStatus.RpcMonitorHealthEntry>
			{
				new RpcGetMonitorHealthStatus.RpcMonitorHealthEntry
				{
					Name = "Unknown",
					HealthSetName = "Unknown",
					HealthGroupName = "Unknown",
					ServerComponentName = "Unknown",
					AlertValue = MonitorAlertState.Unknown.ToString(),
					CurrentHealthSetState = MonitorServerComponentState.Unknown.ToString()
				}
			};
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000027B0 File Offset: 0x000009B0
		private MonitorAlertState CalculatedConsolidatedHealthSetAlertValue(MonitorHealthCommon.HealthSetStatistics stats)
		{
			MonitorAlertState result = MonitorAlertState.Unknown;
			if (stats.TotalCount == 0)
			{
				result = MonitorAlertState.Healthy;
			}
			else if (stats.UnknownCount == stats.TotalCount)
			{
				result = MonitorAlertState.Unknown;
			}
			else if (stats.DisabledCount == stats.TotalCount)
			{
				result = MonitorAlertState.Disabled;
			}
			else if (stats.UnhealthyCount > 0)
			{
				result = MonitorAlertState.Unhealthy;
			}
			else if (stats.DegradedCount > 0)
			{
				result = MonitorAlertState.Degraded;
			}
			else if (stats.RepairingCount > 0)
			{
				result = MonitorAlertState.Repairing;
			}
			else if (stats.HealthyCount > 0)
			{
				int num = stats.HealthyCount + stats.DisabledCount + stats.UnknownCount;
				if (num == stats.TotalCount)
				{
					result = MonitorAlertState.Healthy;
				}
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002840 File Offset: 0x00000A40
		private MonitorServerComponentState CalculatedConsolidatedServerComponentState(MonitorHealthCommon.ServerComponentStateStatistics stats)
		{
			MonitorServerComponentState result = MonitorServerComponentState.Unknown;
			if (stats.TotalCount == stats.UnknownCount)
			{
				result = MonitorServerComponentState.Unknown;
			}
			else if (stats.TotalCount == stats.NotApplicableCount + stats.UnknownCount)
			{
				result = MonitorServerComponentState.NotApplicable;
			}
			else if (stats.TotalCount == stats.OnlineCount)
			{
				result = MonitorServerComponentState.Online;
			}
			else if (stats.TotalCount == stats.PartiallyOnlineCount)
			{
				result = MonitorServerComponentState.PartiallyOnline;
			}
			else if (stats.TotalCount == stats.OfflineCount)
			{
				result = MonitorServerComponentState.Offline;
			}
			else if (stats.TotalCount == stats.FunctionalCount)
			{
				result = MonitorServerComponentState.Functional;
			}
			else if (stats.TotalCount == stats.SidelinedCount)
			{
				result = MonitorServerComponentState.Sidelined;
			}
			else if (stats.OnlineCount > 0 || stats.OfflineCount > 0 || stats.PartiallyOnlineCount > 0)
			{
				int totalCount = stats.TotalCount;
				if (this.GroupSize > 0)
				{
					totalCount = this.GroupSize;
				}
				int num = totalCount * this.MinimumOnlinePercent / 100;
				if (num < 1)
				{
					num = 1;
				}
				if (stats.OnlineCount + stats.PartiallyOnlineCount >= num)
				{
					result = MonitorServerComponentState.PartiallyOnline;
				}
				else
				{
					result = MonitorServerComponentState.Offline;
				}
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000293C File Offset: 0x00000B3C
		private MonitorHealthCommon.HealthSetStatistics GetHealthSetStats(IEnumerable<MonitorHealthEntry> entries)
		{
			MonitorHealthCommon.HealthSetStatistics healthSetStatistics = new MonitorHealthCommon.HealthSetStatistics();
			foreach (MonitorHealthEntry monitorHealthEntry in entries)
			{
				this.UpdateHealthStats(healthSetStatistics, monitorHealthEntry.AlertValue, monitorHealthEntry.LastTransitionTime);
			}
			return healthSetStatistics;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002998 File Offset: 0x00000B98
		private MonitorHealthCommon.ServerComponentStateStatistics GetServerComponentStats(IEnumerable<ConsolidatedHealth> entries)
		{
			MonitorHealthCommon.ServerComponentStateStatistics serverComponentStateStatistics = new MonitorHealthCommon.ServerComponentStateStatistics();
			foreach (ConsolidatedHealth consolidatedHealth in entries)
			{
				serverComponentStateStatistics.TotalCount++;
				if (consolidatedHealth != null)
				{
					switch (consolidatedHealth.State)
					{
					case MonitorServerComponentState.NotApplicable:
						serverComponentStateStatistics.NotApplicableCount++;
						break;
					case MonitorServerComponentState.Online:
						serverComponentStateStatistics.OnlineCount++;
						break;
					case MonitorServerComponentState.PartiallyOnline:
						serverComponentStateStatistics.PartiallyOnlineCount++;
						break;
					case MonitorServerComponentState.Offline:
						serverComponentStateStatistics.OfflineCount++;
						break;
					case MonitorServerComponentState.Functional:
						serverComponentStateStatistics.FunctionalCount++;
						break;
					case MonitorServerComponentState.Sidelined:
						serverComponentStateStatistics.SidelinedCount++;
						break;
					default:
						serverComponentStateStatistics.UnknownCount++;
						break;
					}
				}
				else
				{
					serverComponentStateStatistics.UnknownCount++;
				}
			}
			return serverComponentStateStatistics;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002AA4 File Offset: 0x00000CA4
		private void UpdateHealthStats(MonitorHealthCommon.HealthSetStatistics stats, MonitorAlertState alertValue, DateTime transitionTime)
		{
			if (transitionTime > stats.LastTransitionTime)
			{
				stats.LastTransitionTime = transitionTime;
			}
			stats.TotalCount++;
			switch (alertValue)
			{
			case MonitorAlertState.Healthy:
				stats.HealthyCount++;
				return;
			case MonitorAlertState.Degraded:
				stats.DegradedCount++;
				return;
			case MonitorAlertState.Unhealthy:
				stats.UnhealthyCount++;
				return;
			case MonitorAlertState.Repairing:
				stats.RepairingCount++;
				return;
			case MonitorAlertState.Disabled:
				stats.DisabledCount++;
				return;
			default:
				stats.UnknownCount++;
				return;
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly string identity = string.Empty;

		// Token: 0x04000002 RID: 2
		private readonly string healthSet = string.Empty;

		// Token: 0x04000003 RID: 3
		private readonly bool isHaImapctingOnly;

		// Token: 0x04000004 RID: 4
		private Dictionary<string, Dictionary<string, List<MonitorHealthEntry>>> serverHealthMap = new Dictionary<string, Dictionary<string, List<MonitorHealthEntry>>>();

		// Token: 0x04000005 RID: 5
		private int groupSize;

		// Token: 0x04000006 RID: 6
		private int minimumOnlinePercent = 70;

		// Token: 0x02000003 RID: 3
		private class HealthSetStatistics
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000015 RID: 21 RVA: 0x00002B4D File Offset: 0x00000D4D
			// (set) Token: 0x06000016 RID: 22 RVA: 0x00002B55 File Offset: 0x00000D55
			internal DateTime LastTransitionTime { get; set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000017 RID: 23 RVA: 0x00002B5E File Offset: 0x00000D5E
			// (set) Token: 0x06000018 RID: 24 RVA: 0x00002B66 File Offset: 0x00000D66
			internal int TotalCount { get; set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000019 RID: 25 RVA: 0x00002B6F File Offset: 0x00000D6F
			// (set) Token: 0x0600001A RID: 26 RVA: 0x00002B77 File Offset: 0x00000D77
			internal int UnknownCount { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600001B RID: 27 RVA: 0x00002B80 File Offset: 0x00000D80
			// (set) Token: 0x0600001C RID: 28 RVA: 0x00002B88 File Offset: 0x00000D88
			internal int HealthyCount { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600001D RID: 29 RVA: 0x00002B91 File Offset: 0x00000D91
			// (set) Token: 0x0600001E RID: 30 RVA: 0x00002B99 File Offset: 0x00000D99
			internal int DegradedCount { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600001F RID: 31 RVA: 0x00002BA2 File Offset: 0x00000DA2
			// (set) Token: 0x06000020 RID: 32 RVA: 0x00002BAA File Offset: 0x00000DAA
			internal int UnhealthyCount { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000021 RID: 33 RVA: 0x00002BB3 File Offset: 0x00000DB3
			// (set) Token: 0x06000022 RID: 34 RVA: 0x00002BBB File Offset: 0x00000DBB
			internal int RepairingCount { get; set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000023 RID: 35 RVA: 0x00002BC4 File Offset: 0x00000DC4
			// (set) Token: 0x06000024 RID: 36 RVA: 0x00002BCC File Offset: 0x00000DCC
			internal int DisabledCount { get; set; }
		}

		// Token: 0x02000004 RID: 4
		private class ServerComponentStateStatistics
		{
			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000026 RID: 38 RVA: 0x00002BDD File Offset: 0x00000DDD
			// (set) Token: 0x06000027 RID: 39 RVA: 0x00002BE5 File Offset: 0x00000DE5
			internal int TotalCount { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000028 RID: 40 RVA: 0x00002BEE File Offset: 0x00000DEE
			// (set) Token: 0x06000029 RID: 41 RVA: 0x00002BF6 File Offset: 0x00000DF6
			internal int UnknownCount { get; set; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600002A RID: 42 RVA: 0x00002BFF File Offset: 0x00000DFF
			// (set) Token: 0x0600002B RID: 43 RVA: 0x00002C07 File Offset: 0x00000E07
			internal int NotApplicableCount { get; set; }

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600002C RID: 44 RVA: 0x00002C10 File Offset: 0x00000E10
			// (set) Token: 0x0600002D RID: 45 RVA: 0x00002C18 File Offset: 0x00000E18
			internal int OnlineCount { get; set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600002E RID: 46 RVA: 0x00002C21 File Offset: 0x00000E21
			// (set) Token: 0x0600002F RID: 47 RVA: 0x00002C29 File Offset: 0x00000E29
			internal int PartiallyOnlineCount { get; set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000030 RID: 48 RVA: 0x00002C32 File Offset: 0x00000E32
			// (set) Token: 0x06000031 RID: 49 RVA: 0x00002C3A File Offset: 0x00000E3A
			internal int OfflineCount { get; set; }

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000032 RID: 50 RVA: 0x00002C43 File Offset: 0x00000E43
			// (set) Token: 0x06000033 RID: 51 RVA: 0x00002C4B File Offset: 0x00000E4B
			internal int FunctionalCount { get; set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000034 RID: 52 RVA: 0x00002C54 File Offset: 0x00000E54
			// (set) Token: 0x06000035 RID: 53 RVA: 0x00002C5C File Offset: 0x00000E5C
			internal int SidelinedCount { get; set; }
		}
	}
}
