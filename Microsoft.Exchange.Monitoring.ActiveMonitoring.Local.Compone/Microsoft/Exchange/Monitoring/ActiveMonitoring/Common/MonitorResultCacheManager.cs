using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000549 RID: 1353
	internal class MonitorResultCacheManager
	{
		// Token: 0x06002181 RID: 8577 RVA: 0x000CBE30 File Offset: 0x000CA030
		private MonitorResultCacheManager()
		{
			this.Initialize();
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x000CBE60 File Offset: 0x000CA060
		internal static MonitorResultCacheManager Instance
		{
			get
			{
				if (MonitorResultCacheManager.instance == null)
				{
					lock (MonitorResultCacheManager.instanceCreationLocker)
					{
						if (MonitorResultCacheManager.instance == null)
						{
							MonitorResultCacheManager.instance = new MonitorResultCacheManager();
						}
					}
				}
				return MonitorResultCacheManager.instance;
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000CBEB8 File Offset: 0x000CA0B8
		internal static DateTime GetDefinitionCreationTime(MonitorDefinition definition)
		{
			DateTime result = (definition != null) ? definition.CreatedTime : DateTime.MinValue;
			if (result.Kind == DateTimeKind.Utc)
			{
				result = result.ToLocalTime();
			}
			return result;
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x000CBEEC File Offset: 0x000CA0EC
		internal static DateTime GetResultCreationTime(MonitorResult result)
		{
			DateTime result2 = (result != null) ? result.ExecutionStartTime : DateTime.MinValue;
			if (result2.Kind == DateTimeKind.Utc)
			{
				result2 = result2.ToLocalTime();
			}
			return result2;
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x000CBF20 File Offset: 0x000CA120
		internal byte[] GetCachedSerializedMonitorHealthEntries(string healthSetName)
		{
			byte[] result;
			lock (this.locker)
			{
				byte[] array = this.serializedRpcReply;
				if (!string.IsNullOrWhiteSpace(healthSetName))
				{
					array = this.PrepareSerializedEntries(this.healthEntriesMap, healthSetName);
				}
				result = array;
			}
			return result;
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x000CBF7C File Offset: 0x000CA17C
		internal DateTime UpdateHealthEntries(RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry[] definitions, RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry[] results, DateTime definitionHeadTime, DateTime definitionTailTime, bool isFullUpdate)
		{
			DateTime result;
			lock (this.updateLocker)
			{
				result = this.UpdateHealthEntriesInternal(definitions, results, definitionHeadTime, definitionTailTime, isFullUpdate);
			}
			return result;
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x000CBFC8 File Offset: 0x000CA1C8
		internal RpcGetMonitorHealthStatus.RpcMonitorHealthEntry FindMonitorHealthEntry(string monitorName, string targetResource)
		{
			RpcGetMonitorHealthStatus.RpcMonitorHealthEntry result = null;
			foreach (KeyValuePair<int, RpcGetMonitorHealthStatus.RpcMonitorHealthEntry> keyValuePair in this.healthEntriesMap)
			{
				RpcGetMonitorHealthStatus.RpcMonitorHealthEntry value = keyValuePair.Value;
				if (string.Equals(value.Name, monitorName, StringComparison.OrdinalIgnoreCase) && string.Equals(value.TargetResource, targetResource, StringComparison.OrdinalIgnoreCase))
				{
					result = value;
					break;
				}
			}
			return result;
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x000CC03C File Offset: 0x000CA23C
		internal HealthSetEscalationState LockHealthSetEscalationStateIfRequired(string healthSetName, EscalationState escalationState, string lockOwnerId)
		{
			TimeSpan value = TimeSpan.FromMinutes(1.0);
			DateTime utcNow = DateTime.UtcNow;
			HealthSetEscalationState result;
			lock (this.locker)
			{
				HealthSetEscalationState healthSetEscalationState;
				if (!this.healthSetsMap.TryGetValue(healthSetName, out healthSetEscalationState))
				{
					healthSetEscalationState = new HealthSetEscalationState(healthSetName, EscalationState.Green, utcNow);
					healthSetEscalationState.LockOwnerId = lockOwnerId;
					healthSetEscalationState.LockedUntilTime = utcNow.Add(value);
					this.healthSetsMap[healthSetName] = healthSetEscalationState;
				}
				else if (healthSetEscalationState.LockedUntilTime < utcNow)
				{
					healthSetEscalationState.LockOwnerId = null;
					healthSetEscalationState.LockedUntilTime = DateTime.MinValue;
					this.healthSetsMap[healthSetName] = healthSetEscalationState;
					if (healthSetEscalationState.EscalationState < escalationState && escalationState >= EscalationState.Yellow)
					{
						healthSetEscalationState.LockOwnerId = lockOwnerId;
						healthSetEscalationState.LockedUntilTime = utcNow.Add(value);
						this.healthSetsMap[healthSetName] = healthSetEscalationState;
					}
				}
				result = healthSetEscalationState;
			}
			return result;
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000CC12C File Offset: 0x000CA32C
		internal bool SetHealthSetEscalationState(string healthSetName, EscalationState escalationState, string lockOwnerId)
		{
			DateTime utcNow = DateTime.UtcNow;
			lock (this.locker)
			{
				HealthSetEscalationState healthSetEscalationState;
				if (this.healthSetsMap.TryGetValue(healthSetName, out healthSetEscalationState) && healthSetEscalationState.EscalationState != escalationState && healthSetEscalationState.LockOwnerId == lockOwnerId)
				{
					healthSetEscalationState.EscalationState = escalationState;
					healthSetEscalationState.StateTransitionTime = utcNow;
					healthSetEscalationState.LockOwnerId = null;
					healthSetEscalationState.LockedUntilTime = DateTime.MinValue;
					this.healthSetsMap[healthSetName] = healthSetEscalationState;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000CC1D8 File Offset: 0x000CA3D8
		private DateTime UpdateHealthEntriesInternal(RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry[] definitions, RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry[] results, DateTime definitionHeadTime, DateTime definitionTailTime, bool isFullUpdate)
		{
			WTFDiagnostics.TraceDebug<int, int, DateTime, DateTime, bool>(ExTraceGlobals.ResultCacheTracer, this.traceContext, "UpdateHealthEntriesInternal: DefinitionsCount:{0} ResultsCount:{1} HeadTime:{2} TailTime:{3} IsFullUpdate:{4}", definitions.Length, results.Length, definitionHeadTime, definitionTailTime, isFullUpdate, null, "UpdateHealthEntriesInternal", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitorResultCacheManager.cs", 346);
			DateTime d = DateTime.UtcNow.ToLocalTime();
			DateTime result = this.lastUpdateTime;
			this.lastUpdateTime = d;
			foreach (RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry rpcShortMonitorDefinitionEntry in definitions)
			{
				this.definitionsMap[rpcShortMonitorDefinitionEntry.Id] = rpcShortMonitorDefinitionEntry;
			}
			foreach (RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry rpcShortMonitorResultEntry in results)
			{
				this.resultsMap[rpcShortMonitorResultEntry.WorkItemId] = rpcShortMonitorResultEntry;
			}
			if (definitionTailTime != DateTime.MinValue && d - definitionTailTime > this.stableDuration)
			{
				this.PruneObsoleteEntries(definitionHeadTime);
			}
			DateTime.UtcNow.ToLocalTime();
			ConcurrentDictionary<int, RpcGetMonitorHealthStatus.RpcMonitorHealthEntry> concurrentDictionary = new ConcurrentDictionary<int, RpcGetMonitorHealthStatus.RpcMonitorHealthEntry>();
			ConcurrentDictionary<string, int> concurrentDictionary2 = new ConcurrentDictionary<string, int>();
			foreach (RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry rpcShortMonitorDefinitionEntry2 in this.definitionsMap.Values)
			{
				RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry result2 = null;
				this.resultsMap.TryGetValue(rpcShortMonitorDefinitionEntry2.Id, out result2);
				RpcGetMonitorHealthStatus.RpcMonitorHealthEntry rpcMonitorHealthEntry = this.CreateMonitorHealthEntry(rpcShortMonitorDefinitionEntry2, result2);
				concurrentDictionary[rpcShortMonitorDefinitionEntry2.Id] = rpcMonitorHealthEntry;
				if (string.Compare(rpcMonitorHealthEntry.AlertValue, MonitorAlertState.Healthy.ToString(), true) == 0 || string.Compare(rpcMonitorHealthEntry.AlertValue, MonitorAlertState.Disabled.ToString(), true) == 0 || string.Compare(rpcMonitorHealthEntry.AlertValue, MonitorAlertState.Unknown.ToString(), true) == 0)
				{
					concurrentDictionary2.AddOrUpdate(rpcMonitorHealthEntry.HealthSetName, 0, (string key, int existingValue) => existingValue);
				}
				else
				{
					concurrentDictionary2.AddOrUpdate(rpcMonitorHealthEntry.HealthSetName, 1, (string key, int existingValue) => ++existingValue);
				}
			}
			byte[] array = this.PrepareSerializedEntries(concurrentDictionary, null);
			lock (this.locker)
			{
				this.healthEntriesMap = concurrentDictionary;
				this.serializedRpcReply = array;
			}
			this.PublishGreenEventsIfRequired(concurrentDictionary2);
			return result;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000CC450 File Offset: 0x000CA650
		private void PublishGreenEventsIfRequired(ConcurrentDictionary<string, int> healthSetsRedMonitorCountMap)
		{
			foreach (KeyValuePair<string, int> keyValuePair in healthSetsRedMonitorCountMap)
			{
				string key = keyValuePair.Key;
				if (keyValuePair.Value == 0)
				{
					bool flag = false;
					lock (this.locker)
					{
						HealthSetEscalationState healthSetEscalationState;
						if (!this.healthSetsMap.TryGetValue(key, out healthSetEscalationState))
						{
							flag = true;
							this.healthSetsMap[key] = new HealthSetEscalationState(key, EscalationState.Green, DateTime.UtcNow);
						}
						else
						{
							flag = (healthSetEscalationState.EscalationState != EscalationState.Green);
							if (flag)
							{
								healthSetEscalationState.ResetToGreen();
								this.healthSetsMap[key] = healthSetEscalationState;
							}
						}
					}
					if (flag)
					{
						ManagedAvailabilityCrimsonEvents.HealthyHealthSet.Log<string, string, string>(key, string.Empty, string.Format(Strings.EscalationMessageHealthy, key));
					}
				}
			}
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x000CC574 File Offset: 0x000CA774
		private byte[] PrepareSerializedEntries(ConcurrentDictionary<int, RpcGetMonitorHealthStatus.RpcMonitorHealthEntry> healthEntriesMap, string healthSetToFilter = null)
		{
			RpcGetMonitorHealthStatus.Reply reply = new RpcGetMonitorHealthStatus.Reply();
			if (string.IsNullOrWhiteSpace(healthSetToFilter))
			{
				reply.HealthEntries = healthEntriesMap.Values.ToList<RpcGetMonitorHealthStatus.RpcMonitorHealthEntry>();
			}
			else
			{
				reply.HealthEntries = (from entry in healthEntriesMap.Values
				where string.Equals(entry.HealthSetName, healthSetToFilter, StringComparison.OrdinalIgnoreCase)
				select entry).ToList<RpcGetMonitorHealthStatus.RpcMonitorHealthEntry>();
			}
			byte[] array = SerializationServices.Serialize(reply);
			WTFDiagnostics.TraceDebug<int, int>(ExTraceGlobals.ResultCacheTracer, this.traceContext, "PrepareSerializedEntries: Size = {0} managed bytes ({1} kb).", array.Length, array.Length / 1024, null, "PrepareSerializedEntries", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitorResultCacheManager.cs", 510);
			return array;
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x000CC618 File Offset: 0x000CA818
		private void Initialize()
		{
			this.degradedToUnhealthyTransitionDuration = TimeSpan.FromSeconds((double)RegistryHelper.GetProperty<int>("MonitorResultCacheManagerDegradedToUnhealthyDurationInSec", 60, null, null, false));
			this.stableDuration = TimeSpan.FromSeconds((double)RegistryHelper.GetProperty<int>("MonitorResultCacheStableDurationInSec", 180, null, null, false));
			this.previousPruningTime = DateTime.MinValue;
			this.resultsMap = new ConcurrentDictionary<int, RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry>(5, 1000);
			this.definitionsMap = new ConcurrentDictionary<int, RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry>(5, 1000);
			ConcurrentDictionary<int, RpcGetMonitorHealthStatus.RpcMonitorHealthEntry> concurrentDictionary = new ConcurrentDictionary<int, RpcGetMonitorHealthStatus.RpcMonitorHealthEntry>();
			byte[] array = this.PrepareSerializedEntries(concurrentDictionary, null);
			ConcurrentDictionary<string, HealthSetEscalationState> concurrentDictionary2 = new ConcurrentDictionary<string, HealthSetEscalationState>();
			lock (this.locker)
			{
				this.healthEntriesMap = concurrentDictionary;
				this.serializedRpcReply = array;
				this.healthSetsMap = concurrentDictionary2;
			}
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000CC710 File Offset: 0x000CA910
		private int PruneObsoleteEntries(DateTime lowestValidTime)
		{
			if (lowestValidTime <= this.previousPruningTime)
			{
				return 0;
			}
			IEnumerable<int> enumerable = from kv in this.definitionsMap
			select kv.Value into d
			where d.CreatedTime < lowestValidTime
			select d.Id;
			int num = 0;
			foreach (int key in enumerable)
			{
				RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry rpcShortMonitorDefinitionEntry;
				this.definitionsMap.TryRemove(key, out rpcShortMonitorDefinitionEntry);
				RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry rpcShortMonitorResultEntry;
				this.resultsMap.TryRemove(key, out rpcShortMonitorResultEntry);
				num++;
			}
			WTFDiagnostics.TraceDebug<DateTime, int>(ExTraceGlobals.ResultCacheTracer, this.traceContext, "PruneObsoleteEntries(lowestValidTime={0}) => {1} entries updated.", lowestValidTime, num, null, "PruneObsoleteEntries", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitorResultCacheManager.cs", 582);
			this.previousPruningTime = lowestValidTime;
			return num;
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000CC83C File Offset: 0x000CAA3C
		private RpcGetMonitorHealthStatus.RpcMonitorHealthEntry CreateMonitorHealthEntry(RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry definition, RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry result)
		{
			RpcGetMonitorHealthStatus.RpcMonitorHealthEntry rpcMonitorHealthEntry = new RpcGetMonitorHealthStatus.RpcMonitorHealthEntry();
			rpcMonitorHealthEntry.Name = definition.Name;
			rpcMonitorHealthEntry.TargetResource = definition.TargetResource;
			rpcMonitorHealthEntry.Description = string.Empty;
			rpcMonitorHealthEntry.IsHaImpacting = definition.IsHaImpacting;
			rpcMonitorHealthEntry.RecurranceInterval = definition.RecurranceInterval;
			rpcMonitorHealthEntry.TimeoutSeconds = definition.TimeoutSeconds;
			rpcMonitorHealthEntry.DefinitionCreatedTime = definition.CreatedTime;
			rpcMonitorHealthEntry.HealthSetName = definition.HealthSetName;
			rpcMonitorHealthEntry.HealthSetDescription = string.Empty;
			rpcMonitorHealthEntry.HealthGroupName = definition.HealthGroupName;
			rpcMonitorHealthEntry.ServicePriority = definition.ServicePriority;
			MonitorAlertState monitorAlertState = this.CalculateAlertState(definition, result);
			rpcMonitorHealthEntry.AlertValue = monitorAlertState.ToString();
			rpcMonitorHealthEntry.ServerComponentName = definition.ServerComponentName;
			MonitorServerComponentState serverComponentState = this.GetServerComponentState(rpcMonitorHealthEntry.ServerComponentName);
			rpcMonitorHealthEntry.CurrentHealthSetState = serverComponentState.ToString();
			if (result != null)
			{
				rpcMonitorHealthEntry.FirstAlertObservedTime = result.FirstAlertObservedTime;
				rpcMonitorHealthEntry.LastTransitionTime = result.LastTransitionTime;
				rpcMonitorHealthEntry.LastExecutionTime = result.LastExecutionTime;
				rpcMonitorHealthEntry.LastExecutionResult = result.LastExecutionResult;
				rpcMonitorHealthEntry.ResultId = result.ResultId;
				rpcMonitorHealthEntry.WorkItemId = result.WorkItemId;
				rpcMonitorHealthEntry.Error = result.Error;
				rpcMonitorHealthEntry.Exception = result.Exception;
				rpcMonitorHealthEntry.IsNotified = result.IsNotified;
				rpcMonitorHealthEntry.LastFailedProbeId = result.LastFailedProbeId;
				rpcMonitorHealthEntry.LastFailedProbeResultId = result.LastFailedProbeResultId;
			}
			rpcMonitorHealthEntry.IsStale = false;
			return rpcMonitorHealthEntry;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000CC9A8 File Offset: 0x000CABA8
		private MonitorAlertState CalculateAlertState(RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry definition, RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry result)
		{
			MonitorAlertState result2 = MonitorAlertState.Unknown;
			if (!definition.Enabled)
			{
				result2 = MonitorAlertState.Disabled;
			}
			else if (result != null)
			{
				if (result.IsAlert)
				{
					bool flag = MonitorStateHelper.IsMonitorRepairing(definition.Name, definition.TargetResource, new DateTime?(result.FirstAlertObservedTime));
					if (flag)
					{
						result2 = MonitorAlertState.Repairing;
					}
					else if (DateTime.UtcNow < result.FirstAlertObservedTimeUtc + this.degradedToUnhealthyTransitionDuration)
					{
						result2 = MonitorAlertState.Degraded;
					}
					else
					{
						result2 = MonitorAlertState.Unhealthy;
					}
				}
				else
				{
					result2 = MonitorAlertState.Healthy;
				}
			}
			return result2;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000CCA1C File Offset: 0x000CAC1C
		private MonitorServerComponentState GetServerComponentState(string serverComponentName)
		{
			ServerComponentEnum serverComponentEnum = ServerComponentEnum.None;
			MonitorServerComponentState result = MonitorServerComponentState.Unknown;
			if (Enum.TryParse<ServerComponentEnum>(serverComponentName, out serverComponentEnum))
			{
				if (serverComponentEnum != ServerComponentEnum.None)
				{
					try
					{
						if (ServerComponentStateManager.IsOnline(serverComponentEnum))
						{
							result = MonitorServerComponentState.Online;
						}
						else
						{
							result = MonitorServerComponentState.Offline;
						}
						return result;
					}
					catch (Exception ex)
					{
						WTFDiagnostics.TraceError<string>(ExTraceGlobals.ResultCacheTracer, this.traceContext, "ServerComponentStateManager.IsOnline() failed with {0}. Assuming Unknown", ex.Message, null, "GetServerComponentState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitorResultCacheManager.cs", 728);
						return MonitorServerComponentState.Unknown;
					}
				}
				result = MonitorServerComponentState.NotApplicable;
			}
			return result;
		}

		// Token: 0x04001875 RID: 6261
		private static MonitorResultCacheManager instance;

		// Token: 0x04001876 RID: 6262
		private static object instanceCreationLocker = new object();

		// Token: 0x04001877 RID: 6263
		private TimeSpan degradedToUnhealthyTransitionDuration;

		// Token: 0x04001878 RID: 6264
		private TimeSpan stableDuration;

		// Token: 0x04001879 RID: 6265
		private object locker = new object();

		// Token: 0x0400187A RID: 6266
		private object updateLocker = new object();

		// Token: 0x0400187B RID: 6267
		private DateTime previousPruningTime;

		// Token: 0x0400187C RID: 6268
		private ConcurrentDictionary<int, RpcUpdateHealthStatusImpl.RpcShortMonitorResultEntry> resultsMap;

		// Token: 0x0400187D RID: 6269
		private ConcurrentDictionary<int, RpcUpdateHealthStatusImpl.RpcShortMonitorDefinitionEntry> definitionsMap;

		// Token: 0x0400187E RID: 6270
		private ConcurrentDictionary<int, RpcGetMonitorHealthStatus.RpcMonitorHealthEntry> healthEntriesMap;

		// Token: 0x0400187F RID: 6271
		private ConcurrentDictionary<string, HealthSetEscalationState> healthSetsMap;

		// Token: 0x04001880 RID: 6272
		private byte[] serializedRpcReply;

		// Token: 0x04001881 RID: 6273
		private TracingContext traceContext = TracingContext.Default;

		// Token: 0x04001882 RID: 6274
		private DateTime lastUpdateTime;
	}
}
