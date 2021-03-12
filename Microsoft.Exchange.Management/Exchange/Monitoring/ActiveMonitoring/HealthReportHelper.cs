using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x020004F6 RID: 1270
	internal class HealthReportHelper
	{
		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x000B6720 File Offset: 0x000B4920
		internal Dictionary<string, Dictionary<string, List<MonitorHealthEntry>>> ServerHealthMap
		{
			get
			{
				return this.serverHealthMap;
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x000B6728 File Offset: 0x000B4928
		// (set) Token: 0x06002D88 RID: 11656 RVA: 0x000B6730 File Offset: 0x000B4930
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

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x000B6739 File Offset: 0x000B4939
		// (set) Token: 0x06002D8A RID: 11658 RVA: 0x000B6741 File Offset: 0x000B4941
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

		// Token: 0x06002D8B RID: 11659 RVA: 0x000B674C File Offset: 0x000B494C
		internal void ProcessEntry(MonitorHealthEntry healthEntry)
		{
			Dictionary<string, List<MonitorHealthEntry>> dictionary = null;
			if (!this.serverHealthMap.TryGetValue(healthEntry.Server, out dictionary))
			{
				dictionary = new Dictionary<string, List<MonitorHealthEntry>>();
				this.serverHealthMap[healthEntry.Server] = dictionary;
			}
			List<MonitorHealthEntry> list = null;
			if (!dictionary.TryGetValue(healthEntry.HealthSetName, out list))
			{
				list = new List<MonitorHealthEntry>();
				dictionary[healthEntry.HealthSetName] = list;
			}
			list.Add(healthEntry);
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000B67BC File Offset: 0x000B49BC
		internal void ProcessHealth(Action<ConsolidatedHealth> action)
		{
			int num = 0;
			foreach (KeyValuePair<string, Dictionary<string, List<MonitorHealthEntry>>> keyValuePair in this.serverHealthMap)
			{
				Dictionary<string, List<MonitorHealthEntry>> value = keyValuePair.Value;
				if (value != null)
				{
					num += value.Count;
				}
			}
			foreach (KeyValuePair<string, Dictionary<string, List<MonitorHealthEntry>>> keyValuePair2 in this.serverHealthMap)
			{
				string key = keyValuePair2.Key;
				Dictionary<string, List<MonitorHealthEntry>> value2 = keyValuePair2.Value;
				foreach (KeyValuePair<string, List<MonitorHealthEntry>> keyValuePair3 in value2)
				{
					string key2 = keyValuePair3.Key;
					List<MonitorHealthEntry> value3 = keyValuePair3.Value;
					string healthGroup = null;
					MonitorServerComponentState state = MonitorServerComponentState.Unknown;
					if (value3 != null && value3.Count > 0)
					{
						MonitorHealthEntry monitorHealthEntry = value3.First<MonitorHealthEntry>();
						if (monitorHealthEntry != null)
						{
							healthGroup = monitorHealthEntry.HealthGroupName;
							state = monitorHealthEntry.CurrentHealthSetState;
						}
					}
					int monitorCount = 0;
					int haImpactingMonitorCount = 0;
					if (value3 != null)
					{
						monitorCount = value3.Count<MonitorHealthEntry>();
						haImpactingMonitorCount = value3.Count((MonitorHealthEntry che) => che.IsHaImpacting);
					}
					HealthReportHelper.HealthSetStatistics healthSetStats = this.GetHealthSetStats(value3);
					MonitorAlertState alertValue = this.CalculatedConsolidatedHealthSetAlertValue(healthSetStats);
					DateTime lastTransitionTime = healthSetStats.LastTransitionTime;
					ConsolidatedHealth obj = new ConsolidatedHealth(key, key2, healthGroup, alertValue, state, monitorCount, haImpactingMonitorCount, lastTransitionTime, value3);
					action(obj);
				}
			}
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000B69A4 File Offset: 0x000B4BA4
		internal ConsolidatedHealth ConsolidateAcrossServers(Dictionary<string, ConsolidatedHealth> serverHealthMap)
		{
			HealthReportHelper.HealthSetStatistics healthSetStatistics = new HealthReportHelper.HealthSetStatistics();
			int num = 0;
			int num2 = 0;
			Dictionary<string, ConsolidatedHealth>.ValueCollection values = serverHealthMap.Values;
			string text = null;
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
							ConsolidatedHealth value = new ConsolidatedHealth(key, consolidatedHealth.HealthSet, consolidatedHealth.HealthGroup);
							dictionary[key] = value;
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
					if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(consolidatedHealth2.HealthSet))
					{
						text = consolidatedHealth2.HealthSet;
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
			if (string.IsNullOrEmpty(text))
			{
				text = "Unknown";
				healthGroup = "Unknown";
			}
			MonitorAlertState alertValue = this.CalculatedConsolidatedHealthSetAlertValue(healthSetStatistics);
			DateTime lastTransitionTime = healthSetStatistics.LastTransitionTime;
			int haImpactingMonitorCount = num2;
			HealthReportHelper.ServerComponentStateStatistics serverComponentStats = this.GetServerComponentStats(serverHealthMap.Values);
			MonitorServerComponentState state = this.CalculatedConsolidatedServerComponentState(serverComponentStats);
			return new ConsolidatedHealth(text, healthGroup, alertValue, state, num, haImpactingMonitorCount, lastTransitionTime, list);
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000B6C00 File Offset: 0x000B4E00
		private MonitorAlertState CalculatedConsolidatedHealthSetAlertValue(HealthReportHelper.HealthSetStatistics stats)
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

		// Token: 0x06002D8F RID: 11663 RVA: 0x000B6C90 File Offset: 0x000B4E90
		private MonitorServerComponentState CalculatedConsolidatedServerComponentState(HealthReportHelper.ServerComponentStateStatistics stats)
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

		// Token: 0x06002D90 RID: 11664 RVA: 0x000B6D8C File Offset: 0x000B4F8C
		private HealthReportHelper.HealthSetStatistics GetHealthSetStats(IEnumerable<MonitorHealthEntry> entries)
		{
			HealthReportHelper.HealthSetStatistics healthSetStatistics = new HealthReportHelper.HealthSetStatistics();
			foreach (MonitorHealthEntry monitorHealthEntry in entries)
			{
				this.UpdateHealthStats(healthSetStatistics, monitorHealthEntry.AlertValue, monitorHealthEntry.LastTransitionTime);
			}
			return healthSetStatistics;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000B6DE8 File Offset: 0x000B4FE8
		private void UpdateHealthStats(HealthReportHelper.HealthSetStatistics stats, MonitorAlertState alertValue, DateTime transitionTime)
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

		// Token: 0x06002D92 RID: 11666 RVA: 0x000B6E94 File Offset: 0x000B5094
		private HealthReportHelper.ServerComponentStateStatistics GetServerComponentStats(IEnumerable<ConsolidatedHealth> entries)
		{
			HealthReportHelper.ServerComponentStateStatistics serverComponentStateStatistics = new HealthReportHelper.ServerComponentStateStatistics();
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

		// Token: 0x040020B2 RID: 8370
		private Dictionary<string, Dictionary<string, List<MonitorHealthEntry>>> serverHealthMap = new Dictionary<string, Dictionary<string, List<MonitorHealthEntry>>>();

		// Token: 0x040020B3 RID: 8371
		private int groupSize;

		// Token: 0x040020B4 RID: 8372
		private int minimumOnlinePercent = 70;

		// Token: 0x020004F7 RID: 1271
		private class HealthSetStatistics
		{
			// Token: 0x17000D94 RID: 3476
			// (get) Token: 0x06002D97 RID: 11671 RVA: 0x000B6FBB File Offset: 0x000B51BB
			// (set) Token: 0x06002D98 RID: 11672 RVA: 0x000B6FC3 File Offset: 0x000B51C3
			internal DateTime LastTransitionTime { get; set; }

			// Token: 0x17000D95 RID: 3477
			// (get) Token: 0x06002D99 RID: 11673 RVA: 0x000B6FCC File Offset: 0x000B51CC
			// (set) Token: 0x06002D9A RID: 11674 RVA: 0x000B6FD4 File Offset: 0x000B51D4
			internal int TotalCount { get; set; }

			// Token: 0x17000D96 RID: 3478
			// (get) Token: 0x06002D9B RID: 11675 RVA: 0x000B6FDD File Offset: 0x000B51DD
			// (set) Token: 0x06002D9C RID: 11676 RVA: 0x000B6FE5 File Offset: 0x000B51E5
			internal int UnknownCount { get; set; }

			// Token: 0x17000D97 RID: 3479
			// (get) Token: 0x06002D9D RID: 11677 RVA: 0x000B6FEE File Offset: 0x000B51EE
			// (set) Token: 0x06002D9E RID: 11678 RVA: 0x000B6FF6 File Offset: 0x000B51F6
			internal int HealthyCount { get; set; }

			// Token: 0x17000D98 RID: 3480
			// (get) Token: 0x06002D9F RID: 11679 RVA: 0x000B6FFF File Offset: 0x000B51FF
			// (set) Token: 0x06002DA0 RID: 11680 RVA: 0x000B7007 File Offset: 0x000B5207
			internal int DegradedCount { get; set; }

			// Token: 0x17000D99 RID: 3481
			// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x000B7010 File Offset: 0x000B5210
			// (set) Token: 0x06002DA2 RID: 11682 RVA: 0x000B7018 File Offset: 0x000B5218
			internal int UnhealthyCount { get; set; }

			// Token: 0x17000D9A RID: 3482
			// (get) Token: 0x06002DA3 RID: 11683 RVA: 0x000B7021 File Offset: 0x000B5221
			// (set) Token: 0x06002DA4 RID: 11684 RVA: 0x000B7029 File Offset: 0x000B5229
			internal int RepairingCount { get; set; }

			// Token: 0x17000D9B RID: 3483
			// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x000B7032 File Offset: 0x000B5232
			// (set) Token: 0x06002DA6 RID: 11686 RVA: 0x000B703A File Offset: 0x000B523A
			internal int DisabledCount { get; set; }
		}

		// Token: 0x020004F8 RID: 1272
		private class ServerComponentStateStatistics
		{
			// Token: 0x17000D9C RID: 3484
			// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x000B704B File Offset: 0x000B524B
			// (set) Token: 0x06002DA9 RID: 11689 RVA: 0x000B7053 File Offset: 0x000B5253
			internal int TotalCount { get; set; }

			// Token: 0x17000D9D RID: 3485
			// (get) Token: 0x06002DAA RID: 11690 RVA: 0x000B705C File Offset: 0x000B525C
			// (set) Token: 0x06002DAB RID: 11691 RVA: 0x000B7064 File Offset: 0x000B5264
			internal int UnknownCount { get; set; }

			// Token: 0x17000D9E RID: 3486
			// (get) Token: 0x06002DAC RID: 11692 RVA: 0x000B706D File Offset: 0x000B526D
			// (set) Token: 0x06002DAD RID: 11693 RVA: 0x000B7075 File Offset: 0x000B5275
			internal int NotApplicableCount { get; set; }

			// Token: 0x17000D9F RID: 3487
			// (get) Token: 0x06002DAE RID: 11694 RVA: 0x000B707E File Offset: 0x000B527E
			// (set) Token: 0x06002DAF RID: 11695 RVA: 0x000B7086 File Offset: 0x000B5286
			internal int OnlineCount { get; set; }

			// Token: 0x17000DA0 RID: 3488
			// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000B708F File Offset: 0x000B528F
			// (set) Token: 0x06002DB1 RID: 11697 RVA: 0x000B7097 File Offset: 0x000B5297
			internal int PartiallyOnlineCount { get; set; }

			// Token: 0x17000DA1 RID: 3489
			// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x000B70A0 File Offset: 0x000B52A0
			// (set) Token: 0x06002DB3 RID: 11699 RVA: 0x000B70A8 File Offset: 0x000B52A8
			internal int OfflineCount { get; set; }

			// Token: 0x17000DA2 RID: 3490
			// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x000B70B1 File Offset: 0x000B52B1
			// (set) Token: 0x06002DB5 RID: 11701 RVA: 0x000B70B9 File Offset: 0x000B52B9
			internal int FunctionalCount { get; set; }

			// Token: 0x17000DA3 RID: 3491
			// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x000B70C2 File Offset: 0x000B52C2
			// (set) Token: 0x06002DB7 RID: 11703 RVA: 0x000B70CA File Offset: 0x000B52CA
			internal int SidelinedCount { get; set; }
		}
	}
}
