using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Net.Logging;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Common.SyncHealthLog;
using Microsoft.Exchange.Transport.Sync.Worker.Health;
using Microsoft.Exchange.Transport.Sync.Worker.Throttling;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregationConfiguration : IRemoteServerHealthConfiguration
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002D99 File Offset: 0x00000F99
		protected AggregationConfiguration()
		{
			this.InitializeConfiguration();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002DD4 File Offset: 0x00000FD4
		private AggregationConfiguration(ITransportConfiguration transportConfiguration)
		{
			SyncUtilities.ThrowIfArgumentNull("transportConfiguration", transportConfiguration);
			this.transportConfiguration = transportConfiguration;
			this.UpdateConfiguration(this.transportConfiguration.LocalServer.TransportServer);
			this.InitializeConfiguration();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002E44 File Offset: 0x00001044
		protected void InitializeConfiguration()
		{
			this.disabledSubscriptionAgents = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.initialRetryInMilliseconds = 2000;
			this.retryBackoffFactor = 2;
			this.maxItemsForDBInManualConcurrencyMode = 0;
			this.delayTresholdForAcceptingNewWorkItems = 0;
			this.contentAggregationProxyServer = WebRequest.DefaultWebProxy;
			SyncPoisonHandler.PoisonContextExpiry = TimeSpan.FromDays(2.0);
			this.terminateSlowSyncEnabled = true;
			this.syncDurationThreshold = TimeSpan.FromMinutes(10.0);
			this.remoteRoundtripTimeThreshold = TimeSpan.FromSeconds(4.0);
			this.delayedSubscriptionThreshold = TimeSpan.FromHours(4.0);
			this.disableSubscriptionThreshold = TimeSpan.FromDays(5.0);
			this.extendedDisableSubscriptionThreshold = TimeSpan.FromDays(30.0);
			this.outageDetectionThreshold = TimeSpan.FromHours(28.0);
			this.hubInactivityThreshold = TimeSpan.FromHours(1.0);
			this.healthCheckRetryInterval = TimeSpan.FromMinutes(5.0);
			this.subscriptionNotificationEnabled = true;
			this.maxTransientErrorsPerItem = 3;
			this.maxMappingTableSizeInMemory = ByteQuantifiedSize.FromMB(150UL);
			this.ReadAppConfig();
			this.remoteServerHealthManagementEnabled = AggregationConfiguration.GetConfigBoolValue("RemoteServerHealthManagementEnabled", true);
			this.remoteServerPoisonMarkingEnabled = AggregationConfiguration.GetConfigBoolValue("RemoteServerPoisonMarkingEnabled", false);
			AggregationConfiguration.LoadConfigSlidingCounterValues("RemoteServerSlidingLatencyCounterWindowSize", TimeSpan.FromMinutes(15.0), out this.remoteServerLatencySlidingCounterWindowSize, "RemoteServerSlidingLatencyCounterBucketLength", TimeSpan.FromSeconds(30.0), out this.remoteServerLatencySlidingCounterBucketLength);
			this.remoteServerLatencyThreshold = AggregationConfiguration.GetConfigTimeSpanValue("RemoteServerLatencyThreshold", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromSeconds(3.0));
			this.remoteServerBackoffCountLimit = AggregationConfiguration.GetConfigIntValue("RemoteServerBackoffCountLimit", 0, int.MaxValue, 5);
			this.remoteServerBackoffTimeSpan = AggregationConfiguration.GetConfigTimeSpanValue("RemoteServerBackoffTimeSpan", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromHours(1.5));
			this.remoteServerHealthDataExpiryPeriod = AggregationConfiguration.GetConfigTimeSpanValue("RemoteServerHealthDataExpiryPeriod", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(1.5));
			this.remoteServerHealthDataExpiryAndPersistanceFrequency = AggregationConfiguration.GetConfigTimeSpanValue("RemoteServerHealthDataExpiryAndPersistanceFrequency", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromMinutes(30.0));
			this.remoteServerAllowedCapacityUsagePercentage = AggregationConfiguration.GetConfigDoubleValue("RemoteServerAllowedCapacityUsagePercentage", 0.0, 100.0, 20.0);
			this.alwaysLoadSubscription = AggregationConfiguration.GetConfigBoolValue("AlwaysLoadSubscription", false);
			this.reportDataLoggingDisabled = AggregationConfiguration.GetConfigBoolValue("ReportDataLoggingDisabled", false);
			this.cloudStatisticsCollectionDisabled = AggregationConfiguration.GetConfigBoolValue("CloudStatisticsCollectionDisabled", false);
			this.maxItemsForMailboxServerInUnknownHealthState = AggregationConfiguration.GetConfigIntValue("MaxItemsForMailboxServerInUnknownHealthState", 0, int.MaxValue, 5);
			this.maxDownloadItemsInFirstDeltaSyncConnection = AggregationConfiguration.GetConfigIntValue("MaxDownloadItemsInFirstDeltaSyncConnection", 0, int.MaxValue, AggregationConfiguration.DefaultMaxDownloadItemsInFirstDeltaSyncConnection);
			AggregationConfiguration.ReadEnumCollectionConfigValueAndUpdateHashSet<AggregationType>("AggregationTypesToBeSyncedInRecoveryMode", this.aggregationTypesToBeSyncedInRecoveryMode, new AggregationType[]
			{
				AggregationType.Migration
			});
			AggregationConfiguration.ReadEnumCollectionConfigValueAndUpdateHashSet<AggregationSubscriptionType>("SubscriptionTypesToBeSyncedInRecoveryMode", this.subscriptionTypesToBeSyncedInRecoveryMode, new AggregationSubscriptionType[]
			{
				AggregationSubscriptionType.DeltaSyncMail
			});
			string configName = "SyncPhasesToBeSyncedInRecoveryMode";
			HashSet<SyncPhase> configValues = this.syncPhasesToBeSyncedInRecoveryMode;
			SyncPhase[] defaultValues = new SyncPhase[1];
			AggregationConfiguration.ReadEnumCollectionConfigValueAndUpdateHashSet<SyncPhase>(configName, configValues, defaultValues);
			AggregationConfiguration.ReadEnumCollectionConfigValueAndUpdateHashSet<SyncResourceMonitorType>("SyncResourceMonitorsDisabled", this.syncResourceMonitorsDisabled, new SyncResourceMonitorType[0]);
			this.suggestedConcurrencyOverride = AggregationConfiguration.GetConfigIntValue("SuggestedConcurrencyOverride", int.MinValue, int.MaxValue, int.MinValue);
			this.maxDownloadSizePerConnectionForAggregation = AggregationConfiguration.GetConfigByteQuantifiedSizeValue("MaxDownloadSizePerConnectionForAggregation", ByteQuantifiedSize.FromMB(50UL));
			this.maxDownloadSizePerConnectionForPeopleConnection = AggregationConfiguration.GetConfigByteQuantifiedSizeValue("MaxDownloadSizePerConnectionForPeopleConnection", ByteQuantifiedSize.FromMB(50UL));
			this.minFreeSpaceRequired = AggregationConfiguration.GetConfigByteQuantifiedSizeValue("MinFreeSpaceRequired", ByteQuantifiedSize.FromGB(1024UL));
			this.maxItemsForDBInUnknownHealthState = AggregationConfiguration.GetConfigIntValue("MaxItemsForDBInUnknownHealthState", 0, int.MaxValue, 2);
			this.maxItemsForTransportServerInUnknownHealthState = AggregationConfiguration.GetConfigIntValue("MaxItemsForTransportServerInUnknownHealthState", 0, int.MaxValue, 5);
			this.maxPendingMessagesInTransportQueueForTheServer = AggregationConfiguration.GetConfigIntValue("MaxPendingMessagesInTransportQueueForTheServer", 0, int.MaxValue, 300);
			this.maxPendingMessagesInTransportQueuePerUser = AggregationConfiguration.GetConfigIntValue("MaxPendingMessagesInTransportQueuePerUser", 0, int.MaxValue, 100);
			this.maxDownloadItemsPerConnectionForAggregation = AggregationConfiguration.GetConfigIntValue("MaxDownloadItemsPerConnectionForAggregation", 0, this.maxPendingMessagesInTransportQueuePerUser, 100);
			this.maxDownloadItemsPerConnectionForPeopleConnection = AggregationConfiguration.GetConfigIntValue("MaxDownloadItemsPerConnectionForPeopleConnection", 0, int.MaxValue, 500);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00003274 File Offset: 0x00001474
		public static AggregationConfiguration Instance
		{
			get
			{
				if (AggregationConfiguration.instance == null)
				{
					lock (AggregationConfiguration.instanceInitializationLock)
					{
						if (AggregationConfiguration.instance == null)
						{
							AggregationConfiguration.instance = new AggregationConfiguration(Components.Configuration);
						}
					}
				}
				return AggregationConfiguration.instance;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000032D0 File Offset: 0x000014D0
		public static bool IsDatacenterMode
		{
			get
			{
				if (!AggregationConfiguration.checkedDatacenterMode)
				{
					try
					{
						AggregationConfiguration.datacenterMode = (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled || SyncUtilities.IsEnabledInEnterprise());
					}
					catch (CannotDetermineExchangeModeException ex)
					{
						AggregationConfiguration.diag.TraceError<string>(0L, "Failed to determine the datacenter mode. Will Defaulting to Enterprise mode: {0}", ex.Message);
					}
					AggregationConfiguration.checkedDatacenterMode = true;
				}
				return AggregationConfiguration.datacenterMode;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00003348 File Offset: 0x00001548
		public virtual Server LocalTransportServer
		{
			get
			{
				return this.transportConfiguration.LocalServer.TransportServer;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000335A File Offset: 0x0000155A
		public virtual bool IsEnabled
		{
			get
			{
				return AggregationConfiguration.IsDatacenterMode && this.LocalTransportServer.TransportSyncEnabled;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003370 File Offset: 0x00001570
		public virtual int MaximumPendingWorkItems
		{
			get
			{
				return this.LocalTransportServer.MaxActiveTransportSyncJobsPerProcessor * AggregationConfiguration.processorCount;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00003383 File Offset: 0x00001583
		public virtual int MaximumNumberOfAttempts
		{
			get
			{
				return this.LocalTransportServer.MaxNumberOfTransportSyncAttempts;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003390 File Offset: 0x00001590
		public int InitialRetryInMilliseconds
		{
			get
			{
				return this.initialRetryInMilliseconds;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003398 File Offset: 0x00001598
		public int RetryBackoffFactor
		{
			get
			{
				return this.retryBackoffFactor;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000033A0 File Offset: 0x000015A0
		public int DelayTresholdForAcceptingNewWorkItems
		{
			get
			{
				return this.delayTresholdForAcceptingNewWorkItems;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000033A8 File Offset: 0x000015A8
		public int MaxItemsForDBInUnknownHealthState
		{
			get
			{
				return this.maxItemsForDBInUnknownHealthState;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000033B0 File Offset: 0x000015B0
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000033B8 File Offset: 0x000015B8
		public int MaxItemsForTransportServerInUnknownHealthState
		{
			get
			{
				return this.maxItemsForTransportServerInUnknownHealthState;
			}
			set
			{
				this.maxItemsForTransportServerInUnknownHealthState = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000033C1 File Offset: 0x000015C1
		public int MaxItemsForMailboxServerInUnknownHealthState
		{
			get
			{
				return this.maxItemsForMailboxServerInUnknownHealthState;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000033C9 File Offset: 0x000015C9
		public int MaxItemsForDBInManualConcurrencyMode
		{
			get
			{
				return this.maxItemsForDBInManualConcurrencyMode;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000033D1 File Offset: 0x000015D1
		public AggregationSubscriptionType EnabledAggregationTypes
		{
			get
			{
				return this.enabledAggregationTypes;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000033D9 File Offset: 0x000015D9
		public ProtocolLog HttpProtocolLog
		{
			get
			{
				return this.httpProtocolLog;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000033E1 File Offset: 0x000015E1
		public SyncLog SyncLog
		{
			get
			{
				return this.syncLog;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000033E9 File Offset: 0x000015E9
		public SyncHealthLog SyncHealthLog
		{
			get
			{
				return this.syncHealthLog;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000033F4 File Offset: 0x000015F4
		public int RemoteConnectionTimeout
		{
			get
			{
				return (int)this.LocalTransportServer.TransportSyncRemoteConnectionTimeout.TotalMilliseconds;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003415 File Offset: 0x00001615
		public IWebProxy ContentAggregationProxyServer
		{
			get
			{
				return this.contentAggregationProxyServer;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003420 File Offset: 0x00001620
		public virtual long MaxDownloadSizePerItem
		{
			get
			{
				return (long)this.LocalTransportServer.TransportSyncMaxDownloadSizePerItem.ToBytes();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003440 File Offset: 0x00001640
		public int MaxDownloadItemsInFirstDeltaSyncConnection
		{
			get
			{
				return this.maxDownloadItemsInFirstDeltaSyncConnection;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003448 File Offset: 0x00001648
		public HashSet<AggregationSubscriptionType> SubscriptionTypesToBeSyncedInRecoveryMode
		{
			get
			{
				return this.subscriptionTypesToBeSyncedInRecoveryMode;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003450 File Offset: 0x00001650
		public HashSet<AggregationType> AggregationTypesToBeSyncedInRecoveryMode
		{
			get
			{
				return this.aggregationTypesToBeSyncedInRecoveryMode;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003458 File Offset: 0x00001658
		public HashSet<SyncPhase> SyncPhasesToBeSyncedInRecoveryMode
		{
			get
			{
				return this.syncPhasesToBeSyncedInRecoveryMode;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003460 File Offset: 0x00001660
		public HashSet<SyncResourceMonitorType> SyncResourceMonitorsDisabled
		{
			get
			{
				return this.syncResourceMonitorsDisabled;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003468 File Offset: 0x00001668
		public int? SuggestedConcurrencyOverride
		{
			get
			{
				if (this.suggestedConcurrencyOverride >= 0)
				{
					return new int?(this.suggestedConcurrencyOverride);
				}
				return null;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003493 File Offset: 0x00001693
		public bool TerminateSlowSyncEnabled
		{
			get
			{
				return this.terminateSlowSyncEnabled;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000349B File Offset: 0x0000169B
		public TimeSpan SyncDurationThreshold
		{
			get
			{
				return this.syncDurationThreshold;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000034A3 File Offset: 0x000016A3
		public TimeSpan RemoteRoundtripTimeThreshold
		{
			get
			{
				return this.remoteRoundtripTimeThreshold;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000034AB File Offset: 0x000016AB
		public TimeSpan DelayedSubscriptionThreshold
		{
			get
			{
				return this.delayedSubscriptionThreshold;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000034B3 File Offset: 0x000016B3
		public TimeSpan DisableSubscriptionThreshold
		{
			get
			{
				return this.disableSubscriptionThreshold;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000034BB File Offset: 0x000016BB
		public TimeSpan ExtendedDisableSubscriptionThreshold
		{
			get
			{
				return this.extendedDisableSubscriptionThreshold;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000034C3 File Offset: 0x000016C3
		public TimeSpan OutageDetectionThreshold
		{
			get
			{
				return this.outageDetectionThreshold;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000034CB File Offset: 0x000016CB
		public TimeSpan HubInactivityThreshold
		{
			get
			{
				return this.hubInactivityThreshold;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000034D3 File Offset: 0x000016D3
		public TimeSpan HealthCheckRetryInterval
		{
			get
			{
				return this.healthCheckRetryInterval;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000034DB File Offset: 0x000016DB
		public ICollection<string> DisabledSubscriptionAgents
		{
			get
			{
				return this.disabledSubscriptionAgents;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000034E3 File Offset: 0x000016E3
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000034EB File Offset: 0x000016EB
		public bool SubscriptionNotificationEnabled
		{
			get
			{
				return this.subscriptionNotificationEnabled;
			}
			set
			{
				this.subscriptionNotificationEnabled = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000034F4 File Offset: 0x000016F4
		public int MaxTransientErrorsPerItem
		{
			get
			{
				return this.maxTransientErrorsPerItem;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000034FC File Offset: 0x000016FC
		public TimeSpan RemoteServerLatencySlidingCounterWindowSize
		{
			get
			{
				return this.remoteServerLatencySlidingCounterWindowSize;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003504 File Offset: 0x00001704
		public TimeSpan RemoteServerLatencySlidingCounterBucketLength
		{
			get
			{
				return this.remoteServerLatencySlidingCounterBucketLength;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000350C File Offset: 0x0000170C
		public TimeSpan RemoteServerLatencyThreshold
		{
			get
			{
				return this.remoteServerLatencyThreshold;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003514 File Offset: 0x00001714
		public int RemoteServerBackoffCountLimit
		{
			get
			{
				return this.remoteServerBackoffCountLimit;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000351C File Offset: 0x0000171C
		public TimeSpan RemoteServerBackoffTimeSpan
		{
			get
			{
				return this.remoteServerBackoffTimeSpan;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003524 File Offset: 0x00001724
		public TimeSpan RemoteServerHealthDataExpiryPeriod
		{
			get
			{
				return this.remoteServerHealthDataExpiryPeriod;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000352C File Offset: 0x0000172C
		public TimeSpan RemoteServerHealthDataExpiryAndPersistanceFrequency
		{
			get
			{
				return this.remoteServerHealthDataExpiryAndPersistanceFrequency;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003534 File Offset: 0x00001734
		public double RemoteServerAllowedCapacityUsagePercentage
		{
			get
			{
				return this.remoteServerAllowedCapacityUsagePercentage;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000353C File Offset: 0x0000173C
		public TimeSpan RemoteServerCapacityUsageThreshold
		{
			get
			{
				double num = (double)((long)this.MaximumPendingWorkItems * this.RemoteServerLatencySlidingCounterWindowSize.Ticks);
				double num2 = this.RemoteServerAllowedCapacityUsagePercentage / 100.0;
				double num3 = num * num2;
				return TimeSpan.FromTicks((long)num3);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000357D File Offset: 0x0000177D
		public bool RemoteServerHealthManagementEnabled
		{
			get
			{
				return this.remoteServerHealthManagementEnabled;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003585 File Offset: 0x00001785
		public bool RemoteServerPoisonMarkingEnabled
		{
			get
			{
				return this.remoteServerPoisonMarkingEnabled;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000358D File Offset: 0x0000178D
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003595 File Offset: 0x00001795
		public bool ReportDataLoggingDisabled
		{
			get
			{
				return this.reportDataLoggingDisabled;
			}
			set
			{
				this.reportDataLoggingDisabled = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000359E File Offset: 0x0000179E
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000035A6 File Offset: 0x000017A6
		public bool CloudStatisticsCollectionDisabled
		{
			get
			{
				return this.cloudStatisticsCollectionDisabled;
			}
			set
			{
				this.cloudStatisticsCollectionDisabled = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000035AF File Offset: 0x000017AF
		public ByteQuantifiedSize MaxMappingTableSizeInMemory
		{
			get
			{
				return this.maxMappingTableSizeInMemory;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000035B7 File Offset: 0x000017B7
		public ByteQuantifiedSize MinFreeSpaceRequired
		{
			get
			{
				return this.minFreeSpaceRequired;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000035BF File Offset: 0x000017BF
		public bool AlwaysLoadSubscription
		{
			get
			{
				return this.alwaysLoadSubscription;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000035C7 File Offset: 0x000017C7
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000035CF File Offset: 0x000017CF
		public int MaxPendingMessagesInTransportQueueForTheServer
		{
			get
			{
				return this.maxPendingMessagesInTransportQueueForTheServer;
			}
			set
			{
				this.maxPendingMessagesInTransportQueueForTheServer = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000035D8 File Offset: 0x000017D8
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000035E0 File Offset: 0x000017E0
		public int MaxPendingMessagesInTransportQueuePerUser
		{
			get
			{
				return this.maxPendingMessagesInTransportQueuePerUser;
			}
			set
			{
				this.maxPendingMessagesInTransportQueuePerUser = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000035E9 File Offset: 0x000017E9
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000035F1 File Offset: 0x000017F1
		internal int MaxDownloadItemsPerConnectionForAggregation
		{
			get
			{
				return this.maxDownloadItemsPerConnectionForAggregation;
			}
			set
			{
				this.maxDownloadItemsPerConnectionForAggregation = value;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000035FC File Offset: 0x000017FC
		internal ByteQuantifiedSize GetMaxDownloadSizePerConnection(AggregationType aggregationType)
		{
			if (aggregationType == AggregationType.Aggregation)
			{
				return this.maxDownloadSizePerConnectionForAggregation;
			}
			if (aggregationType == AggregationType.Migration)
			{
				return this.LocalTransportServer.TransportSyncMaxDownloadSizePerConnection;
			}
			if (aggregationType != AggregationType.PeopleConnection)
			{
				throw new InvalidOperationException("Unknown aggregation type: " + aggregationType);
			}
			return this.maxDownloadSizePerConnectionForPeopleConnection;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000364C File Offset: 0x0000184C
		internal int GetMaxDownloadItemsPerConnection(AggregationType aggregationType)
		{
			if (aggregationType == AggregationType.Aggregation)
			{
				return this.maxDownloadItemsPerConnectionForAggregation;
			}
			if (aggregationType == AggregationType.Migration)
			{
				return this.LocalTransportServer.TransportSyncMaxDownloadItemsPerConnection;
			}
			if (aggregationType != AggregationType.PeopleConnection)
			{
				throw new InvalidOperationException("Unknown aggregation type: " + aggregationType);
			}
			return this.maxDownloadItemsPerConnectionForPeopleConnection;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000369C File Offset: 0x0000189C
		internal void UpdateConfiguration(Server server)
		{
			SyncPoisonHandler.PoisonDetectionEnabled = (AggregationConfiguration.IsDatacenterMode && server.TransportSyncAccountsPoisonDetectionEnabled);
			SyncPoisonHandler.PoisonSubscriptionThreshold = server.TransportSyncAccountsPoisonAccountThreshold;
			SyncPoisonHandler.PoisonItemThreshold = server.TransportSyncAccountsPoisonItemThreshold;
			SyncPoisonHandler.MaxPoisonousItemsPerSubscriptionThreshold = server.TransportSyncAccountsSuccessivePoisonItemThreshold;
			this.enabledAggregationTypes = AggregationConfiguration.CreateAggregationTypes(server);
			string httpTransportSyncProxyServer = this.LocalTransportServer.HttpTransportSyncProxyServer;
			Uri address;
			if (httpTransportSyncProxyServer.Length == 0)
			{
				this.contentAggregationProxyServer = WebRequest.DefaultWebProxy;
			}
			else if (Uri.TryCreate(httpTransportSyncProxyServer, UriKind.Absolute, out address))
			{
				this.contentAggregationProxyServer = new WebProxy(address);
			}
			else
			{
				ExTraceGlobals.CommonTracer.TraceDebug<string>(0L, "Unable to create Uri for '{0}'.  Not modifying contentAggregationProxyServer.", httpTransportSyncProxyServer);
			}
			ProtocolLogConfiguration protocolLogConfiguration = AggregationConfiguration.CreateHttpProtocolLogConfiguration(server);
			if (this.httpProtocolLog == null)
			{
				this.httpProtocolLog = new ProtocolLog(protocolLogConfiguration);
			}
			else
			{
				this.httpProtocolLog.ConfigureLog(protocolLogConfiguration.LogFilePath, protocolLogConfiguration.AgeQuota, protocolLogConfiguration.DirectorySizeQuota, protocolLogConfiguration.PerFileSizeQuota, protocolLogConfiguration.ProtocolLoggingLevel);
			}
			SyncLogConfiguration syncLogConfiguration = AggregationConfiguration.CreateSyncLogConfiguration(server);
			if (this.syncLog == null)
			{
				this.syncLog = new SyncLog(syncLogConfiguration);
			}
			else
			{
				this.syncLog.ConfigureLog(syncLogConfiguration.Enabled, syncLogConfiguration.LogFilePath, syncLogConfiguration.AgeQuotaInHours, syncLogConfiguration.DirectorySizeQuota, syncLogConfiguration.PerFileSizeQuota, syncLogConfiguration.SyncLoggingLevel);
			}
			SyncLogSession syncLogSession = this.SyncLog.OpenSession();
			SyncPoisonHandler.TransportSyncEnabled = this.IsEnabled;
			CommonLoggingHelper.SyncLogSession = syncLogSession;
			SyncHealthLogConfiguration syncHealthLogConfiguration = SyncHealthLogConfiguration.CreateSyncHubHealthLogConfiguration(server);
			if (!AggregationConfiguration.IsDatacenterMode)
			{
				syncHealthLogConfiguration.SyncHealthLogEnabled = false;
			}
			if (this.syncHealthLog == null)
			{
				this.syncHealthLog = new SyncHealthLog(syncHealthLogConfiguration);
				return;
			}
			this.syncHealthLog.Configure(syncHealthLogConfiguration);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003824 File Offset: 0x00001A24
		private static AggregationSubscriptionType CreateAggregationTypes(Server server)
		{
			AggregationSubscriptionType aggregationSubscriptionType = AggregationSubscriptionType.Unknown;
			if (server.TransportSyncPopEnabled)
			{
				aggregationSubscriptionType |= AggregationSubscriptionType.Pop;
			}
			if (server.WindowsLiveHotmailTransportSyncEnabled)
			{
				aggregationSubscriptionType |= AggregationSubscriptionType.DeltaSyncMail;
			}
			if (server.TransportSyncImapEnabled)
			{
				aggregationSubscriptionType |= AggregationSubscriptionType.IMAP;
			}
			if (server.TransportSyncFacebookEnabled)
			{
				aggregationSubscriptionType |= AggregationSubscriptionType.Facebook;
			}
			if (server.TransportSyncLinkedInEnabled)
			{
				aggregationSubscriptionType |= AggregationSubscriptionType.LinkedIn;
			}
			return aggregationSubscriptionType;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003874 File Offset: 0x00001A74
		private static ProtocolLogConfiguration CreateHttpProtocolLogConfiguration(Server server)
		{
			ProtocolLogConfiguration protocolLogConfiguration = new ProtocolLogConfiguration("HTTP");
			protocolLogConfiguration.IsEnabled = (AggregationConfiguration.IsDatacenterMode && server.HttpProtocolLogEnabled);
			if (server.HttpProtocolLogFilePath != null && !string.IsNullOrEmpty(server.HttpProtocolLogFilePath.PathName))
			{
				protocolLogConfiguration.LogFilePath = server.HttpProtocolLogFilePath.PathName;
			}
			protocolLogConfiguration.AgeQuota = (long)server.HttpProtocolLogMaxAge.TotalHours;
			protocolLogConfiguration.DirectorySizeQuota = (long)server.HttpProtocolLogMaxDirectorySize.ToKB();
			protocolLogConfiguration.PerFileSizeQuota = (long)server.HttpProtocolLogMaxFileSize.ToKB();
			switch (server.HttpProtocolLogLoggingLevel)
			{
			case ProtocolLoggingLevel.None:
				protocolLogConfiguration.ProtocolLoggingLevel = ProtocolLoggingLevel.None;
				break;
			case ProtocolLoggingLevel.Verbose:
				protocolLogConfiguration.ProtocolLoggingLevel = ProtocolLoggingLevel.All;
				break;
			}
			return protocolLogConfiguration;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000393C File Offset: 0x00001B3C
		private static SyncLogConfiguration CreateSyncLogConfiguration(Server server)
		{
			SyncLogConfiguration syncLogConfiguration = new SyncLogConfiguration();
			syncLogConfiguration.Enabled = (AggregationConfiguration.IsDatacenterMode && server.TransportSyncLogEnabled);
			syncLogConfiguration.SyncLoggingLevel = server.TransportSyncLogLoggingLevel;
			syncLogConfiguration.AgeQuotaInHours = (long)server.TransportSyncLogMaxAge.TotalHours;
			syncLogConfiguration.DirectorySizeQuota = (long)server.TransportSyncLogMaxDirectorySize.ToKB();
			syncLogConfiguration.PerFileSizeQuota = (long)server.TransportSyncLogMaxFileSize.ToKB();
			if (server.TransportSyncLogFilePath != null && !string.IsNullOrEmpty(server.TransportSyncLogFilePath.PathName))
			{
				syncLogConfiguration.LogFilePath = server.TransportSyncLogFilePath.PathName;
			}
			else
			{
				syncLogConfiguration.LogFilePath = AggregationConfiguration.defaultRelativeSyncLogPath;
			}
			return syncLogConfiguration;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000039F0 File Offset: 0x00001BF0
		private static bool GetConfigBoolValue(string configName, bool defaultValue)
		{
			return AggregationConfiguration.GetConfigValue<bool>(configName, null, null, defaultValue, new AggregationConfiguration.TryParseValue<bool>(bool.TryParse));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003A22 File Offset: 0x00001C22
		private static int GetConfigIntValue(string configName, int minValue, int maxValue, int defaultValue)
		{
			return AggregationConfiguration.GetConfigValue<int>(configName, new int?(minValue), new int?(maxValue), defaultValue, new AggregationConfiguration.TryParseValue<int>(int.TryParse));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003A43 File Offset: 0x00001C43
		private static double GetConfigDoubleValue(string configName, double minValue, double maxValue, double defaultValue)
		{
			return AggregationConfiguration.GetConfigValue<double>(configName, new double?(minValue), new double?(maxValue), defaultValue, new AggregationConfiguration.TryParseValue<double>(double.TryParse));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003A64 File Offset: 0x00001C64
		private static TimeSpan GetConfigTimeSpanValue(string configName, TimeSpan minValue, TimeSpan maxValue, TimeSpan defaultValue)
		{
			return AggregationConfiguration.GetConfigValue<TimeSpan>(configName, new TimeSpan?(minValue), new TimeSpan?(maxValue), defaultValue, new AggregationConfiguration.TryParseValue<TimeSpan>(TimeSpan.TryParse));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003A88 File Offset: 0x00001C88
		private static ByteQuantifiedSize GetConfigByteQuantifiedSizeValue(string configName, ByteQuantifiedSize defaultValue)
		{
			return AggregationConfiguration.GetConfigValue<ByteQuantifiedSize>(configName, null, null, defaultValue, new AggregationConfiguration.TryParseValue<ByteQuantifiedSize>(ByteQuantifiedSize.TryParse));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003ABC File Offset: 0x00001CBC
		private static void ReadEnumCollectionConfigValueAndUpdateHashSet<T>(string configName, HashSet<T> configValues, params T[] defaultValues)
		{
			configValues.Clear();
			string text;
			if (!AggregationConfiguration.TryGetStringConfigValue(configName, out text) || text == null)
			{
				if (defaultValues != null)
				{
					configValues.UnionWith(defaultValues);
					return;
				}
			}
			else
			{
				string[] array = text.Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text2 in array)
				{
					T item = default(T);
					Exception ex = null;
					try
					{
						item = (T)((object)Enum.Parse(typeof(T), text2, true));
					}
					catch (ArgumentException ex2)
					{
						ex = ex2;
					}
					catch (OverflowException ex3)
					{
						ex = ex3;
					}
					if (ex != null)
					{
						ExTraceGlobals.CommonTracer.TraceWarning<string, string, Exception>(0L, "Ignoring value '{0}' for config {1} due to {2}", text2, configName, ex);
					}
					else
					{
						configValues.Add(item);
					}
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003B94 File Offset: 0x00001D94
		private static bool TryGetStringConfigValue(string configName, out string configValue)
		{
			configValue = null;
			try
			{
				configValue = ConfigurationManager.AppSettings[configName];
				return true;
			}
			catch (ConfigurationErrorsException arg)
			{
				ExTraceGlobals.CommonTracer.TraceWarning<string, ConfigurationErrorsException>(0L, "failed to read config {0}: {1}", configName, arg);
			}
			return false;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003BE0 File Offset: 0x00001DE0
		private static T GetConfigValue<T>(string configName, T? minValue, T? maxValue, T defaultValue, AggregationConfiguration.TryParseValue<T> tryParseValue) where T : struct, IComparable<T>
		{
			SyncUtilities.ThrowIfArgumentNull("configName", configName);
			SyncUtilities.ThrowIfArgumentNull("tryParseValue", tryParseValue);
			string text;
			if (!AggregationConfiguration.TryGetStringConfigValue(configName, out text))
			{
				return defaultValue;
			}
			if (string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.CommonTracer.TraceDebug<string>(0L, "cannot apply null/empty config {0}", configName);
				return defaultValue;
			}
			T t = defaultValue;
			if (!tryParseValue(text, out t))
			{
				ExTraceGlobals.CommonTracer.TraceWarning<string, string>(0L, "cannot apply config {0} with invalid value: {1}", configName, text);
				return defaultValue;
			}
			if (minValue != null && t.CompareTo(minValue.Value) < 0)
			{
				ExTraceGlobals.CommonTracer.TraceWarning<string, T, T>(0L, "cannot apply config:{0}, value:{1} is less than minValue:{2}", configName, t, minValue.Value);
				return defaultValue;
			}
			if (maxValue != null && t.CompareTo(maxValue.Value) > 0)
			{
				ExTraceGlobals.CommonTracer.TraceWarning<string, T, T>(0L, "cannot apply config:{0}, value:{1} is greater than maxValue:{2}", configName, t, maxValue.Value);
				return defaultValue;
			}
			return t;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003CC8 File Offset: 0x00001EC8
		private static void LoadConfigSlidingCounterValues(string windowSizeConfigName, TimeSpan windowSizeDefaultValue, out TimeSpan windowSizeValue, string bucketLengthConfigName, TimeSpan bucketLengthDefaultValue, out TimeSpan bucketLengthValue)
		{
			windowSizeValue = AggregationConfiguration.GetConfigTimeSpanValue(windowSizeConfigName, TimeSpan.Zero, TimeSpan.MaxValue, windowSizeDefaultValue);
			bucketLengthValue = AggregationConfiguration.GetConfigTimeSpanValue(bucketLengthConfigName, TimeSpan.Zero, TimeSpan.MaxValue, bucketLengthDefaultValue);
			try
			{
				SlidingWindow.ValidateSlidingWindowAndBucketLength(windowSizeValue, bucketLengthValue);
			}
			catch (ExAssertException arg)
			{
				ExTraceGlobals.CommonTracer.TraceError<string, string, ExAssertException>(0L, "cannot apply config:{0} and {1}, applying the default ones. Failure Reason:{2}", windowSizeConfigName, bucketLengthConfigName, arg);
				windowSizeValue = windowSizeDefaultValue;
				bucketLengthValue = bucketLengthDefaultValue;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003D50 File Offset: 0x00001F50
		private void ReadAppConfig()
		{
			string[] array = new string[]
			{
				"DelayedSubscriptionThreshold",
				"DisableSubscriptionThreshold",
				"ExtendedDisableSubscriptionThreshold",
				"SyncPoisonContextExpiry",
				"HubInactivityThreshold",
				"DisabledSubscriptionAgents",
				"InitialRetryInMilliseconds",
				"RetryBackoffFactor",
				"MaxItemsForDBInManualConcurrencyMode",
				"SubscriptionNotificationEnabled",
				"OutageDetectionThreshold",
				"DelayTresholdForAcceptingNewWorkItems",
				"TerminateSlowSyncEnabled",
				"SyncDurationThreshold",
				"RemoteRoundtripTimeThreshold",
				"MaxTransientErrorsPerItem",
				"HealthCheckRetryInterval",
				"MaxMappingTableSizeInMemory",
				"MaxXsoFolderSyncStateCacheSizeInMemory"
			};
			foreach (string text in array)
			{
				string text2 = null;
				try
				{
					text2 = ConfigurationManager.AppSettings[text];
				}
				catch (ConfigurationErrorsException arg)
				{
					ExTraceGlobals.CommonTracer.TraceWarning<string, ConfigurationErrorsException>(0L, "failed to read config {0}: {1}", text, arg);
				}
				if (string.IsNullOrEmpty(text2))
				{
					ExTraceGlobals.CommonTracer.TraceDebug<string>(0L, "cannot apply null/empty config {0}", text);
				}
				else
				{
					bool flag = true;
					string key;
					switch (key = text)
					{
					case "SyncPoisonContextExpiry":
					{
						TimeSpan poisonContextExpiry;
						if (TimeSpan.TryParse(text2, out poisonContextExpiry))
						{
							SyncPoisonHandler.PoisonContextExpiry = poisonContextExpiry;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "TerminateSlowSyncEnabled":
					{
						bool flag2;
						if (bool.TryParse(text2, out flag2))
						{
							this.terminateSlowSyncEnabled = flag2;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "SyncDurationThreshold":
					{
						TimeSpan poisonContextExpiry;
						if (TimeSpan.TryParse(text2, out poisonContextExpiry))
						{
							this.syncDurationThreshold = poisonContextExpiry;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "RemoteRoundtripTimeThreshold":
					{
						TimeSpan poisonContextExpiry;
						if (TimeSpan.TryParse(text2, out poisonContextExpiry))
						{
							this.remoteRoundtripTimeThreshold = poisonContextExpiry;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "DelayedSubscriptionThreshold":
					{
						TimeSpan poisonContextExpiry;
						if (TimeSpan.TryParse(text2, out poisonContextExpiry))
						{
							this.delayedSubscriptionThreshold = poisonContextExpiry;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "DisableSubscriptionThreshold":
					{
						TimeSpan poisonContextExpiry;
						if (TimeSpan.TryParse(text2, out poisonContextExpiry))
						{
							this.disableSubscriptionThreshold = poisonContextExpiry;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "ExtendedDisableSubscriptionThreshold":
					{
						TimeSpan poisonContextExpiry;
						if (TimeSpan.TryParse(text2, out poisonContextExpiry))
						{
							this.extendedDisableSubscriptionThreshold = poisonContextExpiry;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "HubInactivityThreshold":
					{
						TimeSpan poisonContextExpiry;
						if (TimeSpan.TryParse(text2, out poisonContextExpiry))
						{
							this.hubInactivityThreshold = poisonContextExpiry;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "HealthCheckRetryInterval":
					{
						TimeSpan poisonContextExpiry;
						if (TimeSpan.TryParse(text2, out poisonContextExpiry))
						{
							this.healthCheckRetryInterval = poisonContextExpiry;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "DisabledSubscriptionAgents":
					{
						string[] array3 = text2.Split(new char[]
						{
							','
						});
						foreach (string text3 in array3)
						{
							string item = text3.Trim();
							if (!this.disabledSubscriptionAgents.Contains(item))
							{
								this.disabledSubscriptionAgents.Add(item);
							}
						}
						break;
					}
					case "InitialRetryInMilliseconds":
					{
						int num2;
						if (int.TryParse(text2, out num2))
						{
							this.initialRetryInMilliseconds = num2;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "RetryBackoffFactor":
					{
						int num2;
						if (int.TryParse(text2, out num2))
						{
							this.retryBackoffFactor = num2;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "DelayTresholdForAcceptingNewWorkItems":
					{
						int num2;
						if (int.TryParse(text2, out num2))
						{
							this.delayTresholdForAcceptingNewWorkItems = num2;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "MaxItemsForDBInManualConcurrencyMode":
					{
						int num2;
						if (int.TryParse(text2, out num2))
						{
							this.maxItemsForDBInManualConcurrencyMode = num2;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "SubscriptionNotificationEnabled":
					{
						bool flag2;
						if (bool.TryParse(text2, out flag2))
						{
							this.subscriptionNotificationEnabled = flag2;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "MaxTransientErrorsPerItem":
					{
						int num2;
						if (int.TryParse(text2, out num2))
						{
							this.maxTransientErrorsPerItem = num2;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "OutageDetectionThreshold":
					{
						TimeSpan poisonContextExpiry;
						if (TimeSpan.TryParse(text2, out poisonContextExpiry))
						{
							this.outageDetectionThreshold = poisonContextExpiry;
						}
						else
						{
							flag = false;
						}
						break;
					}
					case "MaxMappingTableSizeInMemory":
					{
						ByteQuantifiedSize byteQuantifiedSize;
						if (ByteQuantifiedSize.TryParse(text2, out byteQuantifiedSize))
						{
							this.maxMappingTableSizeInMemory = byteQuantifiedSize;
						}
						else
						{
							flag = false;
						}
						break;
					}
					}
					if (!flag)
					{
						ExTraceGlobals.CommonTracer.TraceWarning<string, string>(0L, "cannot apply config {0} with invalid value: {1}", text, text2);
					}
				}
			}
		}

		// Token: 0x04000012 RID: 18
		internal static readonly int DefaultMaxDownloadItemsInFirstDeltaSyncConnection = 1;

		// Token: 0x04000013 RID: 19
		private static readonly string defaultRelativeSyncLogPath = "TransportRoles\\Logs\\SyncLog\\Hub";

		// Token: 0x04000014 RID: 20
		private static readonly Trace diag = ExTraceGlobals.AggregationConfigurationTracer;

		// Token: 0x04000015 RID: 21
		protected static object instanceInitializationLock = new object();

		// Token: 0x04000016 RID: 22
		protected static AggregationConfiguration instance;

		// Token: 0x04000017 RID: 23
		private static bool datacenterMode;

		// Token: 0x04000018 RID: 24
		private static bool checkedDatacenterMode;

		// Token: 0x04000019 RID: 25
		protected static int processorCount = Environment.ProcessorCount;

		// Token: 0x0400001A RID: 26
		private ITransportConfiguration transportConfiguration;

		// Token: 0x0400001B RID: 27
		private int initialRetryInMilliseconds;

		// Token: 0x0400001C RID: 28
		private int retryBackoffFactor;

		// Token: 0x0400001D RID: 29
		private int maxItemsForDBInUnknownHealthState;

		// Token: 0x0400001E RID: 30
		private int maxItemsForMailboxServerInUnknownHealthState;

		// Token: 0x0400001F RID: 31
		private int maxItemsForTransportServerInUnknownHealthState;

		// Token: 0x04000020 RID: 32
		private int delayTresholdForAcceptingNewWorkItems;

		// Token: 0x04000021 RID: 33
		private int maxItemsForDBInManualConcurrencyMode;

		// Token: 0x04000022 RID: 34
		private AggregationSubscriptionType enabledAggregationTypes;

		// Token: 0x04000023 RID: 35
		private ProtocolLog httpProtocolLog;

		// Token: 0x04000024 RID: 36
		private SyncLog syncLog;

		// Token: 0x04000025 RID: 37
		protected SyncHealthLog syncHealthLog;

		// Token: 0x04000026 RID: 38
		private IWebProxy contentAggregationProxyServer;

		// Token: 0x04000027 RID: 39
		private bool terminateSlowSyncEnabled;

		// Token: 0x04000028 RID: 40
		private TimeSpan syncDurationThreshold;

		// Token: 0x04000029 RID: 41
		private TimeSpan remoteRoundtripTimeThreshold;

		// Token: 0x0400002A RID: 42
		private TimeSpan delayedSubscriptionThreshold;

		// Token: 0x0400002B RID: 43
		private TimeSpan disableSubscriptionThreshold;

		// Token: 0x0400002C RID: 44
		private TimeSpan extendedDisableSubscriptionThreshold;

		// Token: 0x0400002D RID: 45
		private TimeSpan outageDetectionThreshold;

		// Token: 0x0400002E RID: 46
		private TimeSpan hubInactivityThreshold;

		// Token: 0x0400002F RID: 47
		private TimeSpan healthCheckRetryInterval;

		// Token: 0x04000030 RID: 48
		private HashSet<string> disabledSubscriptionAgents;

		// Token: 0x04000031 RID: 49
		private bool subscriptionNotificationEnabled;

		// Token: 0x04000032 RID: 50
		private int maxTransientErrorsPerItem;

		// Token: 0x04000033 RID: 51
		private TimeSpan remoteServerLatencySlidingCounterWindowSize;

		// Token: 0x04000034 RID: 52
		private TimeSpan remoteServerLatencySlidingCounterBucketLength;

		// Token: 0x04000035 RID: 53
		private TimeSpan remoteServerLatencyThreshold;

		// Token: 0x04000036 RID: 54
		private int remoteServerBackoffCountLimit;

		// Token: 0x04000037 RID: 55
		private TimeSpan remoteServerBackoffTimeSpan;

		// Token: 0x04000038 RID: 56
		private TimeSpan remoteServerHealthDataExpiryPeriod;

		// Token: 0x04000039 RID: 57
		private TimeSpan remoteServerHealthDataExpiryAndPersistanceFrequency;

		// Token: 0x0400003A RID: 58
		private double remoteServerAllowedCapacityUsagePercentage;

		// Token: 0x0400003B RID: 59
		private bool remoteServerHealthManagementEnabled;

		// Token: 0x0400003C RID: 60
		private bool remoteServerPoisonMarkingEnabled;

		// Token: 0x0400003D RID: 61
		private bool reportDataLoggingDisabled;

		// Token: 0x0400003E RID: 62
		private bool cloudStatisticsCollectionDisabled;

		// Token: 0x0400003F RID: 63
		private ByteQuantifiedSize maxMappingTableSizeInMemory;

		// Token: 0x04000040 RID: 64
		private ByteQuantifiedSize minFreeSpaceRequired;

		// Token: 0x04000041 RID: 65
		private ByteQuantifiedSize maxDownloadSizePerConnectionForAggregation;

		// Token: 0x04000042 RID: 66
		private ByteQuantifiedSize maxDownloadSizePerConnectionForPeopleConnection;

		// Token: 0x04000043 RID: 67
		private bool alwaysLoadSubscription;

		// Token: 0x04000044 RID: 68
		private int maxDownloadItemsPerConnectionForAggregation;

		// Token: 0x04000045 RID: 69
		private int maxDownloadItemsPerConnectionForPeopleConnection;

		// Token: 0x04000046 RID: 70
		private int maxDownloadItemsInFirstDeltaSyncConnection;

		// Token: 0x04000047 RID: 71
		private HashSet<AggregationType> aggregationTypesToBeSyncedInRecoveryMode = new HashSet<AggregationType>();

		// Token: 0x04000048 RID: 72
		private HashSet<AggregationSubscriptionType> subscriptionTypesToBeSyncedInRecoveryMode = new HashSet<AggregationSubscriptionType>();

		// Token: 0x04000049 RID: 73
		private HashSet<SyncPhase> syncPhasesToBeSyncedInRecoveryMode = new HashSet<SyncPhase>();

		// Token: 0x0400004A RID: 74
		private HashSet<SyncResourceMonitorType> syncResourceMonitorsDisabled = new HashSet<SyncResourceMonitorType>();

		// Token: 0x0400004B RID: 75
		private int suggestedConcurrencyOverride;

		// Token: 0x0400004C RID: 76
		private int maxPendingMessagesInTransportQueueForTheServer;

		// Token: 0x0400004D RID: 77
		private int maxPendingMessagesInTransportQueuePerUser;

		// Token: 0x02000005 RID: 5
		// (Invoke) Token: 0x0600007C RID: 124
		internal delegate bool TryParseValue<T>(string stringValue, out T configValue);
	}
}
