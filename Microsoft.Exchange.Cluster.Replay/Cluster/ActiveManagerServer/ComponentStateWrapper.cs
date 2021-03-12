using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000AE RID: 174
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ComponentStateWrapper
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x000235B3 File Offset: 0x000217B3
		internal ComponentStateWrapper(string dbName, string componentName, AmServerName sourceServerName, AmDbActionCode actionCode, Dictionary<AmServerName, RpcHealthStateInfo[]> stateInfoMap)
		{
			this.DatabaseName = dbName;
			this.Initialize(componentName, sourceServerName, actionCode, stateInfoMap);
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x000235CE File Offset: 0x000217CE
		internal string InitiatingComponentName
		{
			get
			{
				if (this.InitiatingComponent != null)
				{
					return this.InitiatingComponent.Name;
				}
				return null;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x000235EB File Offset: 0x000217EB
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x000235F3 File Offset: 0x000217F3
		internal string DatabaseName { get; private set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x000235FC File Offset: 0x000217FC
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x00023604 File Offset: 0x00021804
		internal Component InitiatingComponent { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0002360D File Offset: 0x0002180D
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x00023615 File Offset: 0x00021815
		internal AmServerName SourceServerName { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0002361E File Offset: 0x0002181E
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x00023626 File Offset: 0x00021826
		internal AmDbActionCode ActionCode { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0002362F File Offset: 0x0002182F
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x00023637 File Offset: 0x00021837
		internal Dictionary<AmServerName, RpcHealthStateInfo[]> StateInfoMap { get; set; }

		// Token: 0x06000741 RID: 1857 RVA: 0x00023654 File Offset: 0x00021854
		private void Initialize(string componentName, AmServerName sourceServerName, AmDbActionCode actionCode, Dictionary<AmServerName, RpcHealthStateInfo[]> stateInfoMap)
		{
			if (!string.IsNullOrEmpty(componentName))
			{
				this.InitiatingComponent = new Component(componentName);
			}
			this.SourceServerName = sourceServerName;
			this.ActionCode = actionCode;
			this.StateInfoMap = stateInfoMap;
			this.consolidatedHealthTable = new Dictionary<AmServerName, Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState>>();
			if (stateInfoMap != null)
			{
				foreach (KeyValuePair<AmServerName, RpcHealthStateInfo[]> keyValuePair in stateInfoMap)
				{
					AmServerName key = keyValuePair.Key;
					RpcHealthStateInfo[] value = keyValuePair.Value;
					Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState> dictionary = new Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState>();
					if (value != null && value.Length > 0)
					{
						IOrderedEnumerable<RpcHealthStateInfo> orderedEnumerable = from hs in value
						where hs != null
						orderby hs.Priority
						select hs;
						foreach (RpcHealthStateInfo rpcHealthStateInfo in orderedEnumerable)
						{
							if (rpcHealthStateInfo.DatabaseName == null || string.Equals(this.DatabaseName, rpcHealthStateInfo.DatabaseName, StringComparison.OrdinalIgnoreCase))
							{
								ComponentStateWrapper.ConsolidatedHealthState consolidatedHealthState = null;
								if (!dictionary.TryGetValue(rpcHealthStateInfo.ComponentName, out consolidatedHealthState))
								{
									consolidatedHealthState = new ComponentStateWrapper.ConsolidatedHealthState();
									consolidatedHealthState.ComponentName = rpcHealthStateInfo.ComponentName;
									consolidatedHealthState.Priority = rpcHealthStateInfo.Priority;
									dictionary[consolidatedHealthState.ComponentName] = consolidatedHealthState;
								}
								if (rpcHealthStateInfo.HealthStatus > consolidatedHealthState.HealthStatus)
								{
									consolidatedHealthState.HealthStatus = rpcHealthStateInfo.HealthStatus;
								}
							}
						}
					}
					this.consolidatedHealthTable[key] = dictionary;
				}
			}
			this.ReportComponentHealth();
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00023838 File Offset: 0x00021A38
		private void ReportComponentHealth()
		{
			StringBuilder stringBuilder = new StringBuilder(1000);
			string text = string.Format(" {0,-15} | {1,-10} | {2,-15} \n", "Component", "Priority", "HealthStatus");
			string value = string.Format("{0}\n", new string('-', text.Length));
			foreach (KeyValuePair<AmServerName, Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState>> keyValuePair in this.consolidatedHealthTable)
			{
				stringBuilder.Clear();
				string netbiosName = keyValuePair.Key.NetbiosName;
				Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState> value2 = keyValuePair.Value;
				if (value2 != null && value2.Count > 0)
				{
					stringBuilder.Append(value);
					stringBuilder.AppendFormat(text, new object[0]);
					stringBuilder.Append(value);
					foreach (ComponentStateWrapper.ConsolidatedHealthState consolidatedHealthState in value2.Values)
					{
						stringBuilder.AppendFormat(" {0,-15} | {1,-10} | {2,-15} \n", consolidatedHealthState.ComponentName, (ManagedAvailabilityPriority)consolidatedHealthState.Priority, (ServiceHealthStatus)consolidatedHealthState.HealthStatus);
					}
					stringBuilder.Append(value);
				}
				ReplayCrimsonEvents.ComponentHealthState.Log<string, string>(netbiosName, stringBuilder.ToString());
			}
			if (this.consolidatedHealthTable.Count == 0)
			{
				ReplayCrimsonEvents.ComponentHealthState.Log<string, string>("<none>", "No consolidated health entries were present.");
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000239BC File Offset: 0x00021BBC
		private Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState> GetConsolidatedHealthStateMap(AmServerName targetServerName)
		{
			Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState> result = null;
			this.consolidatedHealthTable.TryGetValue(targetServerName, out result);
			return result;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000239DC File Offset: 0x00021BDC
		private ComponentStateWrapper.ConsolidatedHealthState GetComponentHealthStateInTarget(AmServerName targetServerName, string componentName)
		{
			ComponentStateWrapper.ConsolidatedHealthState result = null;
			Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState> consolidatedHealthStateMap = this.GetConsolidatedHealthStateMap(targetServerName);
			if (consolidatedHealthStateMap != null)
			{
				consolidatedHealthStateMap.TryGetValue(componentName, out result);
			}
			return result;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00023A04 File Offset: 0x00021C04
		internal bool IsInitiatorComponentBetterThanSource(AmServerName targetServerName, List<string> failures)
		{
			bool result = true;
			if (this.ActionCode.IsAutomaticManagedAvailabilityFailover)
			{
				ComponentStateWrapper.ConsolidatedHealthState componentHealthStateInTarget = this.GetComponentHealthStateInTarget(this.SourceServerName, this.InitiatingComponentName);
				if (componentHealthStateInTarget != null)
				{
					ComponentStateWrapper.ConsolidatedHealthState componentHealthStateInTarget2 = this.GetComponentHealthStateInTarget(targetServerName, this.InitiatingComponentName);
					if (componentHealthStateInTarget2 != null && componentHealthStateInTarget2.HealthStatus != 1 && componentHealthStateInTarget2.HealthStatus >= componentHealthStateInTarget.HealthStatus)
					{
						result = false;
						failures.Add(string.Format("Initiating component {0} is not in a better state on target server (SrcHealth={1} TargetHealth={2}\n", this.InitiatingComponentName, componentHealthStateInTarget.HealthStatus, componentHealthStateInTarget2.HealthStatus));
					}
				}
			}
			return result;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00023A90 File Offset: 0x00021C90
		internal bool IsAllComponentsHealthy(AmServerName targetServerName, List<string> failures)
		{
			bool result = true;
			Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState> consolidatedHealthStateMap = this.GetConsolidatedHealthStateMap(targetServerName);
			if (consolidatedHealthStateMap != null)
			{
				foreach (KeyValuePair<string, ComponentStateWrapper.ConsolidatedHealthState> keyValuePair in consolidatedHealthStateMap)
				{
					ComponentStateWrapper.ConsolidatedHealthState value = keyValuePair.Value;
					if (value.HealthStatus != 1)
					{
						failures.Add(string.Format("Component {0} is not Healthy on target server (ConsolidatedHealthState={1})", value.ComponentName, value.HealthStatus));
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00023B1C File Offset: 0x00021D1C
		internal bool IsUptoNormalComponentsHealthy(AmServerName targetServerName, List<string> failures)
		{
			bool result = true;
			Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState> consolidatedHealthStateMap = this.GetConsolidatedHealthStateMap(targetServerName);
			if (consolidatedHealthStateMap != null)
			{
				foreach (KeyValuePair<string, ComponentStateWrapper.ConsolidatedHealthState> keyValuePair in consolidatedHealthStateMap)
				{
					ComponentStateWrapper.ConsolidatedHealthState value = keyValuePair.Value;
					if (value.Priority <= 60 && value.HealthStatus != 1)
					{
						failures.Add(string.Format("Component {0} is not Healthy on target server (ConsolidatedHealthState={1})", value.ComponentName, value.HealthStatus));
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00023BB4 File Offset: 0x00021DB4
		internal bool IsComponentsBettterThanSource(AmServerName targetServerName, List<string> failures)
		{
			bool result = true;
			Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState> consolidatedHealthStateMap = this.GetConsolidatedHealthStateMap(this.SourceServerName);
			if (consolidatedHealthStateMap == null)
			{
				return true;
			}
			Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState> consolidatedHealthStateMap2 = this.GetConsolidatedHealthStateMap(targetServerName);
			if (consolidatedHealthStateMap2 == null)
			{
				return true;
			}
			foreach (KeyValuePair<string, ComponentStateWrapper.ConsolidatedHealthState> keyValuePair in consolidatedHealthStateMap)
			{
				string key = keyValuePair.Key;
				ComponentStateWrapper.ConsolidatedHealthState value = keyValuePair.Value;
				ComponentStateWrapper.ConsolidatedHealthState consolidatedHealthState = null;
				if (consolidatedHealthStateMap2.TryGetValue(key, out consolidatedHealthState) && consolidatedHealthState != null && consolidatedHealthState.Priority <= 60 && consolidatedHealthState.HealthStatus != 1 && consolidatedHealthState.HealthStatus >= value.HealthStatus)
				{
					failures.Add(string.Format("Component {0} is not in a better state than source on target server (SrcHealth={1} TargetHealth={2}\n", key, value.HealthStatus, consolidatedHealthState.HealthStatus));
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00023C9C File Offset: 0x00021E9C
		internal bool IsComponentsAtleastSameAsSource(AmServerName targetServerName, List<string> failures)
		{
			bool result = true;
			Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState> consolidatedHealthStateMap = this.GetConsolidatedHealthStateMap(this.SourceServerName);
			if (consolidatedHealthStateMap == null)
			{
				return true;
			}
			Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState> consolidatedHealthStateMap2 = this.GetConsolidatedHealthStateMap(targetServerName);
			if (consolidatedHealthStateMap2 == null)
			{
				return true;
			}
			foreach (KeyValuePair<string, ComponentStateWrapper.ConsolidatedHealthState> keyValuePair in consolidatedHealthStateMap)
			{
				string key = keyValuePair.Key;
				ComponentStateWrapper.ConsolidatedHealthState value = keyValuePair.Value;
				ComponentStateWrapper.ConsolidatedHealthState consolidatedHealthState = null;
				if (consolidatedHealthStateMap2.TryGetValue(key, out consolidatedHealthState) && consolidatedHealthState != null && consolidatedHealthState.Priority <= 60 && consolidatedHealthState.HealthStatus != 1 && consolidatedHealthState.HealthStatus > value.HealthStatus)
				{
					failures.Add(string.Format("Component {0} is not in a same or better state than source on target server (SrcHealth={1} TargetHealth={2}\n", key, value.HealthStatus, consolidatedHealthState.HealthStatus));
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x0400032E RID: 814
		private Dictionary<AmServerName, Dictionary<string, ComponentStateWrapper.ConsolidatedHealthState>> consolidatedHealthTable;

		// Token: 0x020000AF RID: 175
		internal class ConsolidatedHealthState
		{
			// Token: 0x1700018C RID: 396
			// (get) Token: 0x0600074C RID: 1868 RVA: 0x00023D84 File Offset: 0x00021F84
			// (set) Token: 0x0600074D RID: 1869 RVA: 0x00023D8C File Offset: 0x00021F8C
			internal string ComponentName { get; set; }

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x0600074E RID: 1870 RVA: 0x00023D95 File Offset: 0x00021F95
			// (set) Token: 0x0600074F RID: 1871 RVA: 0x00023D9D File Offset: 0x00021F9D
			internal int Priority { get; set; }

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x06000750 RID: 1872 RVA: 0x00023DA6 File Offset: 0x00021FA6
			// (set) Token: 0x06000751 RID: 1873 RVA: 0x00023DAE File Offset: 0x00021FAE
			internal int HealthStatus { get; set; }
		}
	}
}
