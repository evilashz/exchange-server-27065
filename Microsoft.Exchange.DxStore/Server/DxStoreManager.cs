using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DxStore;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.DxStore.Server
{
	// Token: 0x02000058 RID: 88
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
	public class DxStoreManager : IDxStoreManager
	{
		// Token: 0x06000354 RID: 852 RVA: 0x00007D04 File Offset: 0x00005F04
		public DxStoreManager(IDxStoreConfigProvider configProvider, IDxStoreEventLogger eventLogger)
		{
			this.EventLogger = eventLogger;
			this.ConfigProvider = configProvider;
			this.instanceMap = new Dictionary<string, Tuple<InstanceGroupConfig, DxStoreInstanceChecker>>();
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00007D3B File Offset: 0x00005F3B
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ManagerTracer;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00007D42 File Offset: 0x00005F42
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00007D4A File Offset: 0x00005F4A
		public IDxStoreEventLogger EventLogger { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00007D53 File Offset: 0x00005F53
		// (set) Token: 0x06000359 RID: 857 RVA: 0x00007D5B File Offset: 0x00005F5B
		public InstanceManagerConfig Config { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00007D64 File Offset: 0x00005F64
		// (set) Token: 0x0600035B RID: 859 RVA: 0x00007D6C File Offset: 0x00005F6C
		public bool IsStopping { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00007D75 File Offset: 0x00005F75
		// (set) Token: 0x0600035D RID: 861 RVA: 0x00007D7D File Offset: 0x00005F7D
		public ManualResetEvent StopEvent { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00007D86 File Offset: 0x00005F86
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00007D8E File Offset: 0x00005F8E
		public IDxStoreConfigProvider ConfigProvider { get; set; }

		// Token: 0x06000360 RID: 864 RVA: 0x00007DA8 File Offset: 0x00005FA8
		public void Start()
		{
			lock (this.locker)
			{
				if (!this.isInitialized)
				{
					this.Config = this.ConfigProvider.GetManagerConfig();
					DxStoreManager.Tracer.TraceDebug<string>((long)this.Config.Identity.GetHashCode(), "{0}: Starting Manager", this.Config.Identity);
					this.EventLogger.Log(DxEventSeverity.Info, 0, "{0}: Starting DxStoreManager", new object[]
					{
						this.Config.Identity
					});
					this.configCheckerTimer = new GuardedTimer(delegate(object unused)
					{
						this.MonitorGroups("Periodic scan", false);
					}, null, TimeSpan.Zero, this.Config.InstanceMonitorInterval);
					this.isInitialized = true;
					this.EventLogger.Log(DxEventSeverity.Info, 0, "{0}: Started DxStoreManager", new object[]
					{
						this.Config.Identity
					});
				}
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00007ED0 File Offset: 0x000060D0
		public void Stop(TimeSpan? timeout = null)
		{
			this.IsStopping = true;
			if (!this.isInitialized)
			{
				return;
			}
			timeout = new TimeSpan?(timeout ?? this.Config.ManagerStopTimeout);
			DxStoreManager.Tracer.TraceDebug<string>((long)this.Config.Identity.GetHashCode(), "{0}: Stopping Manager", this.Config.Identity);
			Exception ex = Utils.RunOperation(this.Config.Identity, "DxStoreManager.Stop()", delegate
			{
				this.StopInternal(timeout);
			}, this.EventLogger, LogOptions.LogAll, true, timeout, null, null, null, null);
			DxStoreManager.Tracer.TraceDebug<string, string>((long)this.Config.Identity.GetHashCode(), "{0}: Stop completed {1}", this.Config.Identity, (ex != null) ? ex.ToString() : string.Empty);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00007FD8 File Offset: 0x000061D8
		public void StopInternal(TimeSpan? timeout)
		{
			lock (this.locker)
			{
				DxStoreManager.Tracer.TraceDebug<string>((long)this.Config.Identity.GetHashCode(), "{0}: Attempting to dispose config checker timer", this.Config.Identity);
				this.configCheckerTimer.Dispose(true);
				DxStoreManager.Tracer.TraceDebug<string>((long)this.Config.Identity.GetHashCode(), "{0}: Attempting to stop all instance checkers", this.Config.Identity);
				string[] array = this.instanceMap.Keys.ToArray<string>();
				foreach (string groupName in array)
				{
					this.StopInstance(groupName, false);
				}
				if (this.managerServiceHost != null)
				{
					DxStoreManager.Tracer.TraceDebug<string, string>((long)this.Config.Identity.GetHashCode(), "{0}: Closing manager service host Timeout: {1}", this.Config.Identity, (timeout != null) ? timeout.Value.ToString() : "<timeout not specified>");
					if (timeout != null)
					{
						this.managerServiceHost.Close(timeout.Value);
					}
					else
					{
						this.managerServiceHost.Close();
					}
				}
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00008138 File Offset: 0x00006338
		public void StartInstance(string groupName, bool isForce)
		{
			lock (this.locker)
			{
				if (this.GetInstanceContainer(groupName) == null)
				{
					InstanceGroupConfig groupConfig = this.ConfigProvider.GetGroupConfig(groupName, false);
					if (groupConfig == null)
					{
						throw new DxStoreManagerGroupNotFoundException(groupName);
					}
					this.StartInstanceInternal(groupConfig, isForce);
				}
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000081A0 File Offset: 0x000063A0
		public void RestartInstance(string groupName, bool isForce)
		{
			lock (this.locker)
			{
				InstanceGroupConfig groupConfig = this.ConfigProvider.GetGroupConfig(groupName, false);
				this.RestartInstanceInternal(groupConfig, isForce);
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000081F0 File Offset: 0x000063F0
		public void RemoveInstance(string groupName)
		{
			lock (this.locker)
			{
				this.StopInstanceInternal(groupName, true);
				this.ConfigProvider.RemoveGroupConfig(groupName);
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00008240 File Offset: 0x00006440
		public void StopInstance(string groupName, bool isDisable = false)
		{
			lock (this.locker)
			{
				this.StopInstanceInternal(groupName, true);
				if (isDisable)
				{
					this.ConfigProvider.DisableGroup(groupName);
				}
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00008294 File Offset: 0x00006494
		public InstanceGroupConfig GetInstanceConfig(string groupName, bool isForce = false)
		{
			InstanceGroupConfig result = null;
			lock (this.locker)
			{
				if (!isForce)
				{
					Tuple<InstanceGroupConfig, DxStoreInstanceChecker> instanceContainer = this.GetInstanceContainer(groupName);
					if (instanceContainer != null)
					{
						result = instanceContainer.Item1;
					}
				}
				else
				{
					result = this.ConfigProvider.GetGroupConfig(groupName, true);
				}
			}
			return result;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000831C File Offset: 0x0000651C
		public void TriggerRefresh(string reason, bool isForceRefreshCache)
		{
			DateTimeOffset now = DateTimeOffset.Now;
			if (now - this.lastTriggerRefreshInitiatedTime < TimeSpan.FromSeconds(30.0))
			{
				this.EventLogger.LogPeriodic("TriggerRefresh", this.Config.Settings.PeriodicExceptionLoggingDuration, DxEventSeverity.Warning, 0, "TriggerRefresh postponed to avoid refresh flood (LastRequestTime: {0})", new object[]
				{
					this.lastTriggerRefreshInitiatedTime
				});
				this.isTriggerRefreshPostponed = isForceRefreshCache;
				return;
			}
			this.lastTriggerRefreshInitiatedTime = DateTimeOffset.Now;
			this.isTriggerRefreshPostponed = false;
			Task.Factory.StartNew(delegate()
			{
				this.MonitorGroups(reason, isForceRefreshCache);
			});
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000083E0 File Offset: 0x000065E0
		public void RegisterServiceHostIfRequired()
		{
			if (this.managerServiceHost == null)
			{
				ServiceHost serviceHost = new ServiceHost(this, new Uri[0]);
				ServiceEndpoint endpoint = this.Config.GetEndpoint(this.Config.Self, true, null);
				serviceHost.AddServiceEndpoint(endpoint);
				serviceHost.Open();
				this.managerServiceHost = serviceHost;
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000845C File Offset: 0x0000665C
		public void MonitorGroups(string reason, bool isForceRefreshCache = false)
		{
			lock (this.locker)
			{
				Utils.RunOperation(this.Config.Identity, "MonitorGroups", delegate
				{
					this.MonitorGroupsInternal(this.isTriggerRefreshPostponed || isForceRefreshCache);
				}, this.EventLogger, LogOptions.LogException | this.Config.Settings.AdditionalLogOptions, true, null, null, null, null, reason);
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00008520 File Offset: 0x00006720
		public void MonitorGroupsInternal(bool isForceRefreshCache)
		{
			this.isTriggerRefreshPostponed = false;
			this.RegisterServiceHostIfRequired();
			this.ConfigProvider.RefreshTopology(isForceRefreshCache);
			InstanceGroupConfig[] allGroupConfigs = this.ConfigProvider.GetAllGroupConfigs();
			this.RemoveStartProcessLimitForNonExistentGroups(allGroupConfigs);
			InstanceGroupConfig[] array = allGroupConfigs;
			for (int i = 0; i < array.Length; i++)
			{
				InstanceGroupConfig instanceGroupConfig = array[i];
				InstanceGroupConfig tmpGroup = instanceGroupConfig;
				Utils.RunOperation(tmpGroup.Identity, "CheckGroup", delegate
				{
					this.CheckGroup(tmpGroup);
				}, this.EventLogger, LogOptions.LogPeriodic | LogOptions.LogExceptionFull | instanceGroupConfig.Settings.AdditionalLogOptions, true, null, null, null, null, null);
			}
			this.StopRemovedGroups(allGroupConfigs);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00008774 File Offset: 0x00006974
		public void StopRemovedGroups(InstanceGroupConfig[] groups)
		{
			if (this.instanceMap.Count == 0)
			{
				return;
			}
			IEnumerable<string> enumerable = from groupName in this.instanceMap.Keys
			let isFound = groups.Any((InstanceGroupConfig g) => Utils.IsEqual(groupName, g.Name, StringComparison.OrdinalIgnoreCase))
			where !isFound
			select groupName;
			foreach (string groupName2 in enumerable)
			{
				this.StopInstanceInternal(groupName2, true);
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000883C File Offset: 0x00006A3C
		public void CheckGroup(InstanceGroupConfig group)
		{
			if (!group.IsConfigurationReady)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			Tuple<InstanceGroupConfig, DxStoreInstanceChecker> instanceContainer = this.GetInstanceContainer(group.Name);
			if (instanceContainer == null)
			{
				flag = (group.IsAutomaticActionsAllowed && group.IsMembersContainSelf);
			}
			else if (!group.IsAutomaticActionsAllowed || !group.IsMembersContainSelf)
			{
				flag2 = true;
			}
			else
			{
				InstanceGroupConfig item = instanceContainer.Item1;
				DxStoreInstanceChecker item2 = instanceContainer.Item2;
				if (item2.IsRestartRequested || group.IsRestartRequested)
				{
					flag3 = true;
				}
			}
			if (flag3)
			{
				this.RestartInstanceInternal(group, false);
			}
			else if (flag2)
			{
				this.StopInstanceInternal(group.Name, false);
			}
			else if (flag)
			{
				this.StartInstanceInternal(group, false);
			}
			if (!group.IsMembersContainSelf && !group.IsConfigurationManagedExternally && DxStoreInstance.RemoveGroupStorage(this.EventLogger, group))
			{
				this.RemoveGroupConfig(group);
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00008904 File Offset: 0x00006B04
		public void RemoveGroupConfig(InstanceGroupConfig group)
		{
			this.EventLogger.Log(DxEventSeverity.Info, 0, "{0}: Removing group configuration", new object[]
			{
				group.Name
			});
			this.ConfigProvider.RemoveGroupConfig(group.Name);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00008948 File Offset: 0x00006B48
		public Tuple<InstanceGroupConfig, DxStoreInstanceChecker> GetInstanceContainer(string groupName)
		{
			Tuple<InstanceGroupConfig, DxStoreInstanceChecker> result;
			this.instanceMap.TryGetValue(groupName, out result);
			return result;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00008968 File Offset: 0x00006B68
		public bool CheckStartProcessLimits(InstanceGroupConfig group, bool isForce)
		{
			DateTimeOffset now = DateTimeOffset.Now;
			DxStoreManager.InstanceStartStats instanceStartStats;
			if (!this.instanceStartMap.TryGetValue(group.Identity, out instanceStartStats))
			{
				instanceStartStats = new DxStoreManager.InstanceStartStats();
				this.ResetStartProcessLimit(instanceStartStats, now);
				this.instanceStartMap[group.Identity] = instanceStartStats;
			}
			else
			{
				TimeSpan t = now - instanceStartStats.FirstStartRequestedTime;
				TimeSpan t2 = now - instanceStartStats.LastStartRequestedTime;
				if (t2 < group.Settings.InstanceStartSilenceDuration)
				{
					if (instanceStartStats.TotalStartRequestsFromFirstReported > group.Settings.InstanceStartHoldupDurationMaxAllowedStarts)
					{
						if (t < group.Settings.InstanceStartHoldUpDuration)
						{
							this.EventLogger.LogPeriodic(group.Identity, TimeSpan.FromMinutes(10.0), DxEventSeverity.Warning, 0, "Instance start request exceeded maximum allowed ({3}). (FirstStartTime: {0}, LastStartTime: {1}, TotalRequests: {2}", new object[]
							{
								instanceStartStats.FirstStartRequestedTime,
								instanceStartStats.LastStartRequestedTime,
								instanceStartStats.TotalStartRequestsFromFirstReported,
								isForce ? "but allowing due to force flag" : "start rejected"
							});
							if (!isForce)
							{
								return false;
							}
						}
						else
						{
							this.ResetStartProcessLimit(instanceStartStats, now);
						}
					}
					instanceStartStats.LastStartRequestedTime = now;
					instanceStartStats.TotalStartRequestsFromFirstReported++;
				}
				else
				{
					this.ResetStartProcessLimit(instanceStartStats, now);
				}
			}
			return true;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00008AB0 File Offset: 0x00006CB0
		public void RemoveStartProcessLimitForNonExistentGroups(InstanceGroupConfig[] groups)
		{
			if (groups != null)
			{
				HashSet<string> hashSet = new HashSet<string>(this.instanceStartMap.Keys);
				foreach (InstanceGroupConfig instanceGroupConfig in groups)
				{
					hashSet.Remove(instanceGroupConfig.Identity);
				}
				using (HashSet<string>.Enumerator enumerator = hashSet.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string key = enumerator.Current;
						this.instanceStartMap.Remove(key);
					}
					return;
				}
			}
			this.instanceStartMap.Clear();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00008B4C File Offset: 0x00006D4C
		public void ResetStartProcessLimit(DxStoreManager.InstanceStartStats stats, DateTimeOffset now)
		{
			stats.LastStartRequestedTime = now;
			stats.FirstStartRequestedTime = now;
			stats.TotalStartRequestsFromFirstReported = 1;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00008B64 File Offset: 0x00006D64
		private void StartInstanceInternal(InstanceGroupConfig group, bool isForce = false)
		{
			DxStoreManager.Tracer.TraceDebug<string, string, bool>((long)this.Config.Identity.GetHashCode(), "{0}: Starting instance checker {1} (group.IsRestartRequested: {2})", this.Config.Identity, group.Identity, group.IsRestartRequested);
			if (group.IsRestartRequested)
			{
				this.ConfigProvider.SetRestartRequired(group.Name, false);
				group.IsRestartRequested = false;
			}
			else if (!this.CheckStartProcessLimits(group, isForce))
			{
				return;
			}
			DxStoreInstanceChecker dxStoreInstanceChecker = new DxStoreInstanceChecker(this, group);
			this.instanceMap[group.Name] = new Tuple<InstanceGroupConfig, DxStoreInstanceChecker>(group, dxStoreInstanceChecker);
			dxStoreInstanceChecker.Start();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00008BFC File Offset: 0x00006DFC
		private void RestartInstanceInternal(InstanceGroupConfig group, bool isForce = false)
		{
			DxStoreManager.Tracer.TraceDebug<string, string>((long)this.Config.Identity.GetHashCode(), "{0}: Restarting instance checker {1}", this.Config.Identity, group.Identity);
			this.StopInstanceInternal(group.Name, false);
			this.StartInstanceInternal(group, isForce);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00008C50 File Offset: 0x00006E50
		private void StopInstanceInternal(string groupName, bool isBestEffort = false)
		{
			Tuple<InstanceGroupConfig, DxStoreInstanceChecker> instanceContainer = this.GetInstanceContainer(groupName);
			if (instanceContainer != null)
			{
				DxStoreManager.Tracer.TraceDebug<string, string, bool>((long)this.Config.Identity.GetHashCode(), "{0}: Stopping instance checker {1} (BestEffort: {2})", this.Config.Identity, instanceContainer.Item1.Identity, isBestEffort);
				instanceContainer.Item2.Stop(isBestEffort);
				this.instanceMap.Remove(groupName);
			}
		}

		// Token: 0x040001B8 RID: 440
		private readonly object locker = new object();

		// Token: 0x040001B9 RID: 441
		private readonly Dictionary<string, Tuple<InstanceGroupConfig, DxStoreInstanceChecker>> instanceMap;

		// Token: 0x040001BA RID: 442
		private ServiceHost managerServiceHost;

		// Token: 0x040001BB RID: 443
		private GuardedTimer configCheckerTimer;

		// Token: 0x040001BC RID: 444
		private bool isInitialized;

		// Token: 0x040001BD RID: 445
		private DateTimeOffset lastTriggerRefreshInitiatedTime;

		// Token: 0x040001BE RID: 446
		private bool isTriggerRefreshPostponed;

		// Token: 0x040001BF RID: 447
		private Dictionary<string, DxStoreManager.InstanceStartStats> instanceStartMap = new Dictionary<string, DxStoreManager.InstanceStartStats>();

		// Token: 0x02000059 RID: 89
		public class InstanceStartStats
		{
			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06000379 RID: 889 RVA: 0x00008CB8 File Offset: 0x00006EB8
			// (set) Token: 0x0600037A RID: 890 RVA: 0x00008CC0 File Offset: 0x00006EC0
			public DateTimeOffset FirstStartRequestedTime { get; set; }

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x0600037B RID: 891 RVA: 0x00008CC9 File Offset: 0x00006EC9
			// (set) Token: 0x0600037C RID: 892 RVA: 0x00008CD1 File Offset: 0x00006ED1
			public DateTimeOffset LastStartRequestedTime { get; set; }

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x0600037D RID: 893 RVA: 0x00008CDA File Offset: 0x00006EDA
			// (set) Token: 0x0600037E RID: 894 RVA: 0x00008CE2 File Offset: 0x00006EE2
			public int TotalStartRequestsFromFirstReported { get; set; }
		}
	}
}
