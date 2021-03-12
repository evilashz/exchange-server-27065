using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DxStore;
using Microsoft.Win32;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000069 RID: 105
	public class DxStoreRegistryConfigProvider : IDxStoreConfigProvider, IServerNameResolver, ITopologyProvider
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x0000CA1B File Offset: 0x0000AC1B
		public DxStoreRegistryConfigProvider()
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000CA2E File Offset: 0x0000AC2E
		public DxStoreRegistryConfigProvider(string componentName)
		{
			this.Initialize(componentName, null, null, null, false);
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ConfigTracer;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000CA53 File Offset: 0x0000AC53
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000CA5B File Offset: 0x0000AC5B
		public string Self { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000CA64 File Offset: 0x0000AC64
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		public string DefaultStorageBaseDir { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000CA75 File Offset: 0x0000AC75
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0000CA7D File Offset: 0x0000AC7D
		public string DefaultManagerKeyName { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000CA86 File Offset: 0x0000AC86
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x0000CA8E File Offset: 0x0000AC8E
		public string DefaultInstanceProcessFullPath { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000CA97 File Offset: 0x0000AC97
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0000CA9F File Offset: 0x0000AC9F
		public string ManagerConfigKeyName { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
		public InstanceManagerConfig ManagerConfig { get; set; }

		// Token: 0x06000450 RID: 1104 RVA: 0x0000CABC File Offset: 0x0000ACBC
		public void Initialize(string componentName, string self = null, string baseComponentKeyName = null, IDxStoreEventLogger eventLogger = null, bool isZeroboxMode = false)
		{
			this.componentName = componentName;
			this.Self = ((!string.IsNullOrEmpty(self)) ? self : Environment.MachineName);
			this.isZeroboxMode = isZeroboxMode;
			if (string.IsNullOrEmpty(this.DefaultStorageBaseDir))
			{
				this.DefaultStorageBaseDir = Utils.CombinePathNullSafe(isZeroboxMode ? "C:\\Exchange" : ExchangeSetupContext.InstallPath, string.Format("DxStore\\Database\\{0}", this.componentName));
				if (isZeroboxMode)
				{
					this.DefaultStorageBaseDir = this.DefaultStorageBaseDir + "\\ZeroBox\\" + this.Self;
				}
			}
			if (string.IsNullOrEmpty(baseComponentKeyName))
			{
				baseComponentKeyName = string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\DxStore\\{1}", "v15", this.componentName);
				if (isZeroboxMode)
				{
					baseComponentKeyName = baseComponentKeyName + "\\Zerobox\\" + this.Self;
				}
			}
			this.eventLogger = eventLogger;
			this.ManagerConfigKeyName = baseComponentKeyName;
			this.ManagerConfig = this.GetManagerConfig();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000CB97 File Offset: 0x0000AD97
		public virtual string ResolveName(string shortServerName)
		{
			return shortServerName;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000CB9A File Offset: 0x0000AD9A
		public virtual TopologyInfo GetLocalServerTopology(bool isForceRefresh = false)
		{
			return null;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000CF20 File Offset: 0x0000B120
		public void RefreshTopology(bool isForceRefresh = false)
		{
			TopologyInfo topology = null;
			bool flag = DxStoreRegistryConfigProvider.Tracer.IsTraceEnabled(TraceType.DebugTrace);
			Utils.RunOperation(this.ManagerConfig.Identity, "RefreshTopology", delegate
			{
				topology = this.GetLocalServerTopology(isForceRefresh);
			}, this.eventLogger, LogOptions.LogException, true, new TimeSpan?(TimeSpan.FromMinutes(1.0)), new TimeSpan?(this.ManagerConfig.Settings.PeriodicExceptionLoggingDuration), null, null, null);
			if (topology != null)
			{
				if (topology.IsConfigured)
				{
					if (flag)
					{
						DxStoreRegistryConfigProvider.Tracer.TraceDebug<string, string>(0L, "RefreshConfig found a valid topology '{0}' members: {1}", topology.Name, topology.Members.JoinWithComma("<null>"));
					}
					if (!topology.IsAllMembersVersionCompatible)
					{
						DxStoreRegistryConfigProvider.Tracer.TraceDebug<string>(0L, "RefreshConfig found that some of the members are version compatible - will be retrying in next iteration", topology.Name);
						return;
					}
					bool flag2 = false;
					InstanceGroupMemberConfig[] configuredMembers = Utils.EmptyArray<InstanceGroupMemberConfig>();
					string[] serversToRemove = Utils.EmptyArray<string>();
					InstanceGroupConfig groupConfig = this.GetGroupConfig(topology.Name, false);
					if (groupConfig != null)
					{
						if (flag)
						{
							DxStoreRegistryConfigProvider.Tracer.TraceDebug<string, string>(0L, "Group {0} already exist with configured members {1}", topology.Name, (from m in groupConfig.Members
							select m.Name).JoinWithComma("<null>"));
						}
						flag2 = true;
						configuredMembers = groupConfig.Members;
						if (!groupConfig.Settings.IsAppendOnlyMembership)
						{
							serversToRemove = (from m in configuredMembers
							let isFound = topology.Members.Any((string tm) => string.Equals(tm, m.Name))
							where !isFound && !m.IsManagedExternally
							select m.Name).ToArray<string>();
						}
					}
					string[] serversToAdd = (from tm in topology.Members
					let isFound = configuredMembers.Any((InstanceGroupMemberConfig m) => string.Equals(tm, m.Name))
					where !isFound
					select tm).ToArray<string>();
					this.UpdateMembers(topology.Name, serversToRemove, serversToAdd, !flag2, !flag2);
					return;
				}
				else
				{
					InstanceGroupConfig[] allGroupConfigs = this.GetAllGroupConfigs();
					foreach (InstanceGroupConfig instanceGroupConfig in allGroupConfigs)
					{
						if (instanceGroupConfig.IsMember(instanceGroupConfig.Self, true))
						{
							string[] array2 = (from member in instanceGroupConfig.Members
							where !member.IsManagedExternally
							select member into m
							select m.Name).ToArray<string>();
							if (flag)
							{
								DxStoreRegistryConfigProvider.Tracer.TraceDebug<string, string>(0L, "{0}: Removing members '{1}' from group since local node is not part of group member any more", instanceGroupConfig.Identity, array2.JoinWithComma("<null>"));
							}
							this.RemoveMembers(instanceGroupConfig.Name, array2);
						}
					}
				}
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000D296 File Offset: 0x0000B496
		public void RemoveMembers(string groupName, string[] membersToRemove)
		{
			this.UpdateMembers(groupName, membersToRemove, null, false, false);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
		public void UpdateMembers(string groupName, string[] serversToRemove, string[] serversToAdd, bool isEnableGroup = false, bool isSetAsDefaultGroup = false)
		{
			bool flag = DxStoreRegistryConfigProvider.Tracer.IsTraceEnabled(TraceType.DebugTrace);
			bool flag2 = false;
			lock (this.locker)
			{
				if (serversToRemove != null && serversToRemove.Length > 0)
				{
					if (flag)
					{
						DxStoreRegistryConfigProvider.Tracer.TraceDebug<string, string>(0L, "{0}: UpdateMembers - Removing members: '{1}'", groupName, serversToRemove.JoinWithComma("<null>"));
					}
					flag2 = true;
					foreach (string memberName in serversToRemove)
					{
						this.RemoveGroupMemberConfig(groupName, memberName);
					}
				}
				if (serversToAdd != null && serversToAdd.Length > 0)
				{
					IEnumerable<InstanceGroupMemberConfig> enumerable = serversToAdd.Select(delegate(string s)
					{
						string networkAddress = this.ManagerConfig.NameResolver.ResolveNameBestEffort(s) ?? s;
						return new InstanceGroupMemberConfig
						{
							Name = s,
							NetworkAddress = networkAddress
						};
					});
					if (flag)
					{
						DxStoreRegistryConfigProvider.Tracer.TraceDebug<string, string>(0L, "{0}: UpdateMembers - Adding members: '{1}'", groupName, serversToAdd.JoinWithComma("<null>"));
					}
					flag2 = true;
					foreach (InstanceGroupMemberConfig cfg in enumerable)
					{
						this.SetGroupMemberConfig(groupName, cfg);
					}
				}
				if (isEnableGroup)
				{
					DxStoreRegistryConfigProvider.Tracer.TraceDebug<string>(0L, "{0}: UpdateMembers - Enabling group", groupName);
					flag2 = true;
					this.EnableGroup(groupName);
				}
				if (isSetAsDefaultGroup)
				{
					DxStoreRegistryConfigProvider.Tracer.TraceDebug<string>(0L, "{0}: UpdateMembers - Setting as default group", groupName);
					flag2 = true;
					this.SetDefaultGroupName(groupName);
				}
				if (!flag2)
				{
					DxStoreRegistryConfigProvider.Tracer.TraceDebug<string>(0L, "{0}: UpdateMembers - No configuration change detected", groupName);
				}
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000D470 File Offset: 0x0000B670
		public InstanceManagerConfig GetManagerConfig()
		{
			InstanceManagerConfig instanceManagerConfig = new InstanceManagerConfig();
			instanceManagerConfig.NameResolver = this;
			instanceManagerConfig.Self = this.Self;
			instanceManagerConfig.ComponentName = this.componentName;
			instanceManagerConfig.IsZeroboxMode = this.isZeroboxMode;
			instanceManagerConfig.NetworkAddress = instanceManagerConfig.NameResolver.ResolveNameBestEffort(this.Self);
			instanceManagerConfig.DefaultTimeout = new WcfTimeout();
			using (RegistryKey registryKey = this.OpenManagerConfigKey(false))
			{
				instanceManagerConfig.BaseStorageDir = Environment.ExpandEnvironmentVariables(RegUtils.GetProperty<string>(registryKey, "BaseStorageDir", this.DefaultStorageBaseDir));
				instanceManagerConfig.InstanceMonitorInterval = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "InstanceMonitorIntervalInMSec", TimeSpan.FromSeconds(15.0));
				instanceManagerConfig.EndpointPortNumber = RegUtils.GetProperty<int>(registryKey, "EndpointPortNumber", 808);
				instanceManagerConfig.EndpointProtocolName = RegUtils.GetProperty<string>(registryKey, "EndpointProtocolName", "net.tcp");
				instanceManagerConfig.DefaultTimeout = RegUtils.GetWcfTimeoutProperty(registryKey, "DefaultTimeout", new WcfTimeout());
				instanceManagerConfig.ManagerStopTimeout = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "ManagerStopTimeoutInMSec", TimeSpan.FromMinutes(1.0));
				CommonSettings defaultSettings = this.CreateDefaultCommonSettings(instanceManagerConfig.EndpointPortNumber, instanceManagerConfig.EndpointProtocolName, instanceManagerConfig.DefaultTimeout);
				instanceManagerConfig.Settings = new CommonSettings();
				this.GetInheritableSettings(this.GetManagerSettingsKeyName(), instanceManagerConfig.Settings, defaultSettings);
			}
			return instanceManagerConfig;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		public InstanceGroupConfig GetGroupConfig(string groupName, bool isFillDefaultValueIfNotExist = false)
		{
			InstanceGroupConfig instanceGroupConfig = null;
			groupName = this.ResolveGroupName(groupName);
			if (string.IsNullOrEmpty(groupName) && isFillDefaultValueIfNotExist)
			{
				groupName = "B1563499-EA40-4101-A9E6-59A8EB26FF1E";
			}
			using (RegistryKey registryKey = this.OpenGroupConfigKey(groupName, false))
			{
				if (registryKey != null || isFillDefaultValueIfNotExist)
				{
					instanceGroupConfig = new InstanceGroupConfig();
					instanceGroupConfig.NameResolver = this;
					instanceGroupConfig.Self = this.Self;
					instanceGroupConfig.ComponentName = this.componentName;
					instanceGroupConfig.IsZeroboxMode = this.isZeroboxMode;
					instanceGroupConfig.Name = groupName;
					instanceGroupConfig.IsExistInConfigProvider = (registryKey != null);
					instanceGroupConfig.Identity = string.Format("Group/{0}/{1}/{2}", this.componentName, groupName, this.Self);
					string defaultGroupName = this.GetDefaultGroupName();
					instanceGroupConfig.IsAutomaticActionsAllowed = RegUtils.GetBoolProperty(registryKey, "IsAutomaticActionsAllowed", false);
					instanceGroupConfig.IsRestartRequested = RegUtils.GetBoolProperty(registryKey, "IsRestartRequested", false);
					instanceGroupConfig.IsConfigurationManagedExternally = RegUtils.GetBoolProperty(registryKey, "IsConfigurationManagedExternally", false);
					instanceGroupConfig.ConfigInProgressExpiryTime = RegUtils.GetTimeProperty(registryKey, "ConfigInProgressExpiryTime");
					instanceGroupConfig.IsDefaultGroup = Utils.IsEqual(groupName, defaultGroupName, StringComparison.OrdinalIgnoreCase);
					instanceGroupConfig.IsConfigurationReady = (DateTimeOffset.Now > instanceGroupConfig.ConfigInProgressExpiryTime);
					instanceGroupConfig.Members = this.GetGroupMemberConfigs(groupName);
					instanceGroupConfig.Settings = this.GetGroupSettings(groupName);
				}
			}
			return instanceGroupConfig;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000D710 File Offset: 0x0000B910
		public CommonSettings CreateDefaultCommonSettings(int defaultEndPointPortNumber, string defaultProtocolName, WcfTimeout defaultTimeout)
		{
			CommonSettings input = new CommonSettings
			{
				InstanceProcessName = this.DefaultInstanceProcessFullPath,
				AccessEndpointPortNumber = defaultEndPointPortNumber,
				AccessEndpointProtocolName = defaultProtocolName,
				StoreAccessWcfTimeout = defaultTimeout,
				StoreAccessHttpTimeoutInMSec = 60000,
				InstanceEndpointPortNumber = defaultEndPointPortNumber,
				InstanceEndpointProtocolName = defaultProtocolName,
				StoreInstanceWcfTimeout = defaultTimeout,
				DurationToWaitBeforeRestart = TimeSpan.FromMinutes(2.0),
				InstanceHealthCheckPeriodicInterval = TimeSpan.FromSeconds(30.0),
				TruncationPeriodicCheckInterval = TimeSpan.FromSeconds(15.0),
				TruncationLimit = 1000,
				TruncationPaddingLength = 500,
				StateMachineStopTimeout = TimeSpan.FromSeconds(30.0),
				LeaderPromotionTimeout = TimeSpan.FromSeconds(30.0),
				PaxosCommandExecutionTimeout = TimeSpan.FromSeconds(30.0),
				GroupHealthCheckDuration = TimeSpan.FromSeconds(15.0),
				GroupHealthCheckAggressiveDuration = TimeSpan.FromSeconds(10.0),
				GroupStatusWaitTimeout = TimeSpan.FromSeconds(15.0),
				MaxEntriesToKeep = 10,
				MaximumAllowedInstanceNumberLag = 5,
				DefaultHealthCheckRequiredNodePercent = 51,
				MemberReconfigureTimeout = TimeSpan.FromSeconds(30.0),
				PaxosUpdateTimeout = TimeSpan.FromSeconds(30.0),
				SnapshotUpdateInterval = TimeSpan.FromSeconds(30.0),
				PeriodicExceptionLoggingDuration = TimeSpan.FromMinutes(5.0),
				PeriodicTimeoutLoggingDuration = TimeSpan.FromMinutes(5.0),
				ServiceHostCloseTimeout = TimeSpan.FromSeconds(60.0),
				MaxAllowedLagToCatchup = 200,
				DefaultSnapshotFileName = "DxStoreSnapshot.xml",
				IsAllowDynamicReconfig = false,
				IsAppendOnlyMembership = true,
				IsKillInstanceProcessWhenParentDies = true,
				AdditionalLogOptions = LogOptions.None,
				InstanceStartHoldUpDuration = TimeSpan.FromHours(1.0),
				InstanceStartHoldupDurationMaxAllowedStarts = 10,
				InstanceStartSilenceDuration = TimeSpan.FromMinutes(5.0),
				InstanceMemoryCommitSizeLimitInMb = 500,
				IsUseHttpTransportForInstanceCommunication = true,
				IsUseHttpTransportForClientCommunication = true,
				IsUseBinarySerializerForClientCommunication = false,
				IsUseEncryption = true,
				StartupDelay = TimeSpan.Zero
			};
			return this.UpdateDefaultCommonSettings(input);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000D95A File Offset: 0x0000BB5A
		public virtual CommonSettings UpdateDefaultCommonSettings(CommonSettings input)
		{
			return input;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000D960 File Offset: 0x0000BB60
		public void GetInheritableSettings(string keyName, CommonSettings settings, CommonSettings defaultSettings)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(keyName))
			{
				settings.InstanceProcessName = RegUtils.GetProperty<string>(registryKey, "InstanceProcessName", defaultSettings.InstanceProcessName);
				settings.TruncationLimit = RegUtils.GetProperty<int>(registryKey, "TruncationLimit", defaultSettings.TruncationLimit);
				settings.TruncationPaddingLength = RegUtils.GetProperty<int>(registryKey, "TruncationPaddingLength", defaultSettings.TruncationPaddingLength);
				settings.DurationToWaitBeforeRestart = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "DurationToWaitBeforeRestartInMSec", defaultSettings.DurationToWaitBeforeRestart);
				settings.AccessEndpointPortNumber = RegUtils.GetProperty<int>(registryKey, "AccessEndpointPortNumber", defaultSettings.AccessEndpointPortNumber);
				settings.AccessEndpointProtocolName = RegUtils.GetProperty<string>(registryKey, "AccessEndpointProtocolName", defaultSettings.AccessEndpointProtocolName);
				settings.InstanceEndpointPortNumber = RegUtils.GetProperty<int>(registryKey, "InstanceEndpointPortNumber", defaultSettings.InstanceEndpointPortNumber);
				settings.InstanceEndpointProtocolName = RegUtils.GetProperty<string>(registryKey, "InstanceEndpointProtocolName", defaultSettings.InstanceEndpointProtocolName);
				settings.IsAllowDynamicReconfig = RegUtils.GetBoolProperty(registryKey, "IsAllowDynamicReconfig", defaultSettings.IsAllowDynamicReconfig);
				settings.IsAppendOnlyMembership = RegUtils.GetBoolProperty(registryKey, "IsAppendOnlyMembership", defaultSettings.IsAppendOnlyMembership);
				settings.IsKillInstanceProcessWhenParentDies = RegUtils.GetBoolProperty(registryKey, "IsKillInstanceProcessWhenParentDies", defaultSettings.IsKillInstanceProcessWhenParentDies);
				settings.StoreAccessWcfTimeout = RegUtils.GetWcfTimeoutProperty(registryKey, "StoreAccessWcfTimeout", defaultSettings.StoreAccessWcfTimeout);
				settings.StoreAccessHttpTimeoutInMSec = RegUtils.GetProperty<int>(registryKey, "StoreAccessHttpTimeoutInMSec", defaultSettings.StoreAccessHttpTimeoutInMSec);
				settings.StoreInstanceWcfTimeout = RegUtils.GetWcfTimeoutProperty(registryKey, "StoreInstanceWcfTimeout", defaultSettings.StoreInstanceWcfTimeout);
				settings.TruncationPeriodicCheckInterval = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "TruncationPeriodicCheckIntervalInMSec", defaultSettings.TruncationPeriodicCheckInterval);
				settings.InstanceHealthCheckPeriodicInterval = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "InstanceHealthCheckPeriodicIntervalInMSec", defaultSettings.InstanceHealthCheckPeriodicInterval);
				settings.StateMachineStopTimeout = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "StateMachineStopTimeoutInMSec", defaultSettings.StateMachineStopTimeout);
				settings.LeaderPromotionTimeout = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "LeaderPromotionTimeoutInMSec", defaultSettings.LeaderPromotionTimeout);
				settings.PaxosCommandExecutionTimeout = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "PaxosCommandExecutionTimeoutInMSec", defaultSettings.PaxosCommandExecutionTimeout);
				settings.GroupHealthCheckDuration = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "GroupHealthCheckDurationInMSec", defaultSettings.GroupHealthCheckDuration);
				settings.GroupHealthCheckAggressiveDuration = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "GroupHealthCheckAggressiveDurationInMSec", defaultSettings.GroupHealthCheckAggressiveDuration);
				settings.GroupStatusWaitTimeout = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "GroupStatusWaitTimeoutInMSec", defaultSettings.GroupStatusWaitTimeout);
				settings.MemberReconfigureTimeout = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "MemberReconfigureTimeoutInMSec", defaultSettings.MemberReconfigureTimeout);
				settings.PaxosUpdateTimeout = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "PaxosUpdateTimeoutInMSec", defaultSettings.PaxosUpdateTimeout);
				settings.SnapshotUpdateInterval = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "SnapshotUpdateIntervalInMSec", defaultSettings.SnapshotUpdateInterval);
				settings.PeriodicExceptionLoggingDuration = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "PeriodicExceptionLoggingDurationInMSec", defaultSettings.PeriodicExceptionLoggingDuration);
				settings.PeriodicTimeoutLoggingDuration = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "PeriodicTimeoutLoggingDurationInMSec", defaultSettings.PeriodicTimeoutLoggingDuration);
				settings.ServiceHostCloseTimeout = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "ServiceHostCloseTimeoutInMSec", defaultSettings.ServiceHostCloseTimeout);
				settings.MaxAllowedLagToCatchup = RegUtils.GetProperty<int>(registryKey, "MaxAllowedLagToCatchup", defaultSettings.MaxAllowedLagToCatchup);
				settings.DefaultSnapshotFileName = RegUtils.GetProperty<string>(registryKey, "DefaultSnapshotFileName", defaultSettings.DefaultSnapshotFileName);
				settings.MaxEntriesToKeep = RegUtils.GetProperty<int>(registryKey, "MaxEntriesToKeep", defaultSettings.MaxEntriesToKeep);
				settings.MaximumAllowedInstanceNumberLag = RegUtils.GetProperty<int>(registryKey, "MaximumAllowedInstanceNumberLag", defaultSettings.MaximumAllowedInstanceNumberLag);
				settings.DefaultHealthCheckRequiredNodePercent = RegUtils.GetProperty<int>(registryKey, "DefaultHealthCheckRequiredNodePercent", defaultSettings.DefaultHealthCheckRequiredNodePercent);
				settings.AdditionalLogOptions = (LogOptions)RegUtils.GetProperty<int>(registryKey, "AdditionalLogOptions", defaultSettings.AdditionalLogOptionsAsInt);
				settings.InstanceStartSilenceDuration = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "InstanceStartSilenceDurationInMSec", defaultSettings.InstanceStartSilenceDuration);
				settings.InstanceStartHoldupDurationMaxAllowedStarts = RegUtils.GetProperty<int>(registryKey, "InstanceStartHoldupDurationMaxAllowedStarts", defaultSettings.InstanceStartHoldupDurationMaxAllowedStarts);
				settings.InstanceStartHoldUpDuration = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "InstanceStartHoldUpDurationInMSec", defaultSettings.InstanceStartHoldUpDuration);
				settings.InstanceMemoryCommitSizeLimitInMb = RegUtils.GetProperty<int>(registryKey, "InstanceMemoryCommitSizeLimitInMb", defaultSettings.InstanceMemoryCommitSizeLimitInMb);
				settings.IsUseHttpTransportForInstanceCommunication = RegUtils.GetBoolProperty(registryKey, "IsUseHttpTransportForInstanceCommunication", defaultSettings.IsUseHttpTransportForInstanceCommunication);
				settings.IsUseHttpTransportForClientCommunication = RegUtils.GetBoolProperty(registryKey, "IsUseHttpTransportForClientCommunication", defaultSettings.IsUseHttpTransportForClientCommunication);
				settings.IsUseBinarySerializerForClientCommunication = RegUtils.GetBoolProperty(registryKey, "IsUseBinarySerializerForClientCommunication", defaultSettings.IsUseBinarySerializerForClientCommunication);
				settings.IsUseEncryption = RegUtils.GetBoolProperty(registryKey, "IsUseEncryption", defaultSettings.IsUseEncryption);
				settings.StartupDelay = RegUtils.GetLongPropertyAsTimeSpan(registryKey, "StartupDelayInMSec", defaultSettings.StartupDelay);
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000DD80 File Offset: 0x0000BF80
		public InstanceGroupSettings GetGroupSettings(string groupName)
		{
			InstanceGroupSettings instanceGroupSettings = new InstanceGroupSettings();
			this.GetInheritableSettings(this.GetGroupSettingsKeyName(groupName), instanceGroupSettings, this.ManagerConfig.Settings);
			using (RegistryKey registryKey = this.OpenGroupSettingsKey(groupName, false))
			{
				instanceGroupSettings.PaxosStorageDir = Environment.ExpandEnvironmentVariables(RegUtils.GetProperty<string>(registryKey, "PaxosStorageDir", this.ConstructDefaultStorageDir(groupName, "Paxos")));
				instanceGroupSettings.SnapshotStorageDir = Environment.ExpandEnvironmentVariables(RegUtils.GetProperty<string>(registryKey, "SnapshotStorageDir", this.ConstructDefaultStorageDir(groupName, "Snapshot")));
			}
			return instanceGroupSettings;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000DE18 File Offset: 0x0000C018
		public InstanceGroupMemberConfig[] GetGroupMemberConfigs(string groupName)
		{
			List<InstanceGroupMemberConfig> list = new List<InstanceGroupMemberConfig>();
			string[] groupMemberNames = this.GetGroupMemberNames(groupName);
			foreach (string text in groupMemberNames)
			{
				InstanceGroupMemberConfig instanceGroupMemberConfig = new InstanceGroupMemberConfig
				{
					Name = text
				};
				using (RegistryKey registryKey = this.OpenGroupMemberConfigKey(groupName, text, false))
				{
					instanceGroupMemberConfig.NetworkAddress = RegUtils.GetProperty<string>(registryKey, "NetworkAddress", string.Empty);
					instanceGroupMemberConfig.IsWitness = RegUtils.GetBoolProperty(registryKey, "IsWitness", false);
					instanceGroupMemberConfig.IsManagedExternally = RegUtils.GetBoolProperty(registryKey, "IsManagedExternally", false);
					list.Add(instanceGroupMemberConfig);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		public string GetDefaultGroupName()
		{
			string property;
			using (RegistryKey registryKey = this.OpenGroupsContainerKey(false))
			{
				property = RegUtils.GetProperty<string>(registryKey, "DefaultGroupName", string.Empty);
			}
			return property;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000DF1C File Offset: 0x0000C11C
		public void SetDefaultGroupName(string groupName)
		{
			using (RegistryKey registryKey = this.OpenGroupsContainerKey(true))
			{
				RegUtils.SetProperty<string>(registryKey, "DefaultGroupName", groupName);
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000DF5C File Offset: 0x0000C15C
		public void RemoveDefaultGroupName()
		{
			using (RegistryKey registryKey = this.OpenGroupsContainerKey(true))
			{
				if (registryKey != null)
				{
					registryKey.DeleteValue("DefaultGroupName");
				}
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000DF9C File Offset: 0x0000C19C
		public void DisableGroup(string groupName)
		{
			using (RegistryKey registryKey = this.OpenGroupConfigKey(groupName, true))
			{
				RegUtils.SetProperty<bool>(registryKey, "IsAutomaticActionsAllowed", false);
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		public void EnableGroup(string groupName)
		{
			lock (this.locker)
			{
				using (RegistryKey registryKey = this.OpenGroupConfigKey(groupName, true))
				{
					RegUtils.SetProperty<bool>(registryKey, "IsAutomaticActionsAllowed", true);
				}
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000E044 File Offset: 0x0000C244
		public void SetRestartRequired(string groupName, bool isRestartRequired)
		{
			lock (this.locker)
			{
				using (RegistryKey registryKey = this.OpenGroupConfigKey(groupName, true))
				{
					RegUtils.SetProperty<bool>(registryKey, "IsRestartRequested", isRestartRequired);
				}
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000E0AC File Offset: 0x0000C2AC
		public void SetGroupMemberConfig(string groupName, InstanceGroupMemberConfig cfg)
		{
			lock (this.locker)
			{
				using (RegistryKey registryKey = this.OpenGroupMemberConfigKey(groupName, cfg.Name, true))
				{
					RegUtils.SetProperty<bool>(registryKey, "IsWitness", cfg.IsWitness);
					if (!string.IsNullOrEmpty(cfg.NetworkAddress))
					{
						RegUtils.SetProperty<string>(registryKey, "NetworkAddress", cfg.NetworkAddress);
					}
				}
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000E13C File Offset: 0x0000C33C
		public string[] GetAllGroupNames()
		{
			string[] result = Utils.EmptyArray<string>();
			lock (this.locker)
			{
				using (RegistryKey registryKey = this.OpenGroupsContainerKey(false))
				{
					if (registryKey != null)
					{
						result = registryKey.GetSubKeyNames();
					}
				}
			}
			return result;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		public string[] GetGroupMemberNames(string groupName)
		{
			string[] result = Utils.EmptyArray<string>();
			lock (this.locker)
			{
				using (RegistryKey registryKey = this.OpenGroupMembersContainerKey(groupName, false))
				{
					if (registryKey != null)
					{
						result = registryKey.GetSubKeyNames();
					}
				}
			}
			return result;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000E220 File Offset: 0x0000C420
		public InstanceGroupConfig[] GetAllGroupConfigs()
		{
			List<InstanceGroupConfig> list = new List<InstanceGroupConfig>();
			lock (this.locker)
			{
				string[] allGroupNames = this.GetAllGroupNames();
				list.AddRange(from groupName in allGroupNames
				select this.GetGroupConfig(groupName, false));
			}
			return list.ToArray();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000E290 File Offset: 0x0000C490
		public void RemoveGroupConfig(string groupName)
		{
			lock (this.locker)
			{
				using (RegistryKey registryKey = this.OpenGroupsContainerKey(true))
				{
					if (registryKey != null)
					{
						DxStoreRegistryConfigProvider.Tracer.TraceDebug<string, string>((long)groupName.GetHashCode(), "{0}/{1}: Removing group", groupName, this.Self);
						registryKey.DeleteSubKeyTree(groupName, false);
						if (Utils.IsEqual(groupName, this.GetDefaultGroupName(), StringComparison.OrdinalIgnoreCase))
						{
							this.RemoveDefaultGroupName();
						}
					}
				}
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000E328 File Offset: 0x0000C528
		public void RemoveGroupMemberConfig(string groupName, string memberName)
		{
			lock (this.locker)
			{
				using (RegistryKey registryKey = this.OpenGroupMembersContainerKey(groupName, true))
				{
					if (registryKey != null)
					{
						DxStoreRegistryConfigProvider.Tracer.TraceDebug<string, string, string>((long)groupName.GetHashCode(), "{0}/{1}: Removing member: {2}", groupName, this.Self, memberName);
						registryKey.DeleteSubKeyTree(memberName, false);
					}
				}
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		internal string GetManagerSettingsKeyName()
		{
			return Utils.CombinePathNullSafe(this.ManagerConfigKeyName, "Settings");
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000E3BE File Offset: 0x0000C5BE
		internal string GetGroupsContainerKeyName()
		{
			return Utils.CombinePathNullSafe(this.ManagerConfigKeyName, "Groups");
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		internal string GetGroupConfigKeyName(string groupName)
		{
			return Utils.CombinePathNullSafe(this.GetGroupsContainerKeyName(), groupName);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000E3DE File Offset: 0x0000C5DE
		internal string GetGroupSettingsKeyName(string groupName)
		{
			return Utils.CombinePathNullSafe(this.GetGroupConfigKeyName(groupName), "Settings");
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000E3F1 File Offset: 0x0000C5F1
		internal string GetGroupMembersContainerKeyName(string groupName)
		{
			return Utils.CombinePathNullSafe(this.GetGroupConfigKeyName(groupName), "Members");
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000E404 File Offset: 0x0000C604
		internal string GetGroupMemberConfigKeyName(string groupName, string memberName)
		{
			return Utils.CombinePathNullSafe(this.GetGroupMembersContainerKeyName(groupName), memberName);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000E414 File Offset: 0x0000C614
		internal RegistryKey OpenKey(string keyName, bool isWritable)
		{
			RegistryKey registryKey = null;
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey(keyName, isWritable);
				if (registryKey == null && isWritable)
				{
					registryKey = Registry.LocalMachine.CreateSubKey(keyName);
				}
			}
			catch (Exception ex)
			{
				DxStoreRegistryConfigProvider.Tracer.TraceError<string, string>(0L, "Failed to open key {0} - error: {1}", keyName, ex.Message);
			}
			return registryKey;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000E470 File Offset: 0x0000C670
		internal RegistryKey OpenManagerConfigKey(bool isWritable = false)
		{
			return this.OpenKey(this.ManagerConfigKeyName, isWritable);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000E47F File Offset: 0x0000C67F
		internal RegistryKey OpenGroupsContainerKey(bool isWritable = false)
		{
			return this.OpenKey(this.GetGroupsContainerKeyName(), isWritable);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000E48E File Offset: 0x0000C68E
		internal RegistryKey OpenGroupConfigKey(string groupName, bool isWritable = false)
		{
			return this.OpenKey(this.GetGroupConfigKeyName(groupName), isWritable);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000E49E File Offset: 0x0000C69E
		internal RegistryKey OpenGroupSettingsKey(string groupName, bool isWritable = false)
		{
			return this.OpenKey(this.GetGroupSettingsKeyName(groupName), isWritable);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000E4AE File Offset: 0x0000C6AE
		internal RegistryKey OpenGroupMembersContainerKey(string groupName, bool isWritable = false)
		{
			return this.OpenKey(this.GetGroupMembersContainerKeyName(groupName), isWritable);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000E4BE File Offset: 0x0000C6BE
		internal RegistryKey OpenGroupMemberConfigKey(string groupName, string memberName, bool isWritable = false)
		{
			return this.OpenKey(this.GetGroupMemberConfigKeyName(groupName, memberName), isWritable);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		private string ResolveGroupName(string groupName)
		{
			if (string.IsNullOrEmpty(groupName) || Utils.IsEqual(groupName, "B1563499-EA40-4101-A9E6-59A8EB26FF1E", StringComparison.OrdinalIgnoreCase))
			{
				string defaultGroupName = this.GetDefaultGroupName();
				if (!string.IsNullOrEmpty(defaultGroupName))
				{
					groupName = defaultGroupName;
				}
			}
			return groupName;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000E506 File Offset: 0x0000C706
		private string ConstructDefaultStorageDir(string groupName, string storageType)
		{
			return Utils.CombinePathNullSafe(this.DefaultStorageBaseDir, string.Format("{0}\\Storage\\{1}", groupName, storageType));
		}

		// Token: 0x0400020B RID: 523
		private readonly object locker = new object();

		// Token: 0x0400020C RID: 524
		private string componentName;

		// Token: 0x0400020D RID: 525
		private bool isZeroboxMode;

		// Token: 0x0400020E RID: 526
		private IDxStoreEventLogger eventLogger;
	}
}
