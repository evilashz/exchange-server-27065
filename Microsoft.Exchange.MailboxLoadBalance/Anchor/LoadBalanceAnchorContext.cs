using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.AnchorService.Storage;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MigrationWorkflowService;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalance.Clients;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.Drain;
using Microsoft.Exchange.MailboxLoadBalance.LoadBalance;
using Microsoft.Exchange.MailboxLoadBalance.Logging;
using Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors;
using Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors.Policies;
using Microsoft.Exchange.MailboxLoadBalance.Providers;
using Microsoft.Exchange.MailboxLoadBalance.Provisioning;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing.Rubs;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;
using Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval;
using Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxLoadBalance.Anchor
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceAnchorContext : AnchorContext
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000037D4 File Offset: 0x000019D4
		public LoadBalanceAnchorContext() : base("MailboxLoadBalance", OrganizationCapability.Management, LoadBalanceADSettings.DefaultContext)
		{
			base.Config.UpdateConfig<TimeSpan>("IdleRunDelay", TimeSpan.FromHours(12.0));
			base.Config.UpdateConfig<TimeSpan>("ActiveRunDelay", TimeSpan.FromHours(1.0));
			this.directory = new Lazy<IDirectoryProvider>(new Func<IDirectoryProvider>(this.CreateDirectoryInstance));
			this.clientFactory = new Lazy<IClientFactory>(new Func<IClientFactory>(this.CreateClientFactoryInstance));
			this.requestQueueManager = new Lazy<IRequestQueueManager>(new Func<IRequestQueueManager>(this.CreateQueueManagerInstance));
			this.service = new Lazy<MailboxLoadBalanceService>(new Func<MailboxLoadBalanceService>(this.CreateServiceInstance));
			this.extractorFactoryContextPool = new Lazy<TopologyExtractorFactoryContextPool>(new Func<TopologyExtractorFactoryContextPool>(this.CreateExtractorFactoryPool));
			this.moveInjector = new Lazy<MoveInjector>(new Func<MoveInjector>(this.CreateMoveInjector));
			this.databaseSelector = new Lazy<DatabaseSelector>(new Func<DatabaseSelector>(this.CreateDatabaseSelector));
			this.logCollector = new Lazy<ObjectLogCollector>(new Func<ObjectLogCollector>(this.CreateLogCollector));
			this.heatMap = new Lazy<IHeatMap>(new Func<IHeatMap>(this.CreateForestHeatMap));
			this.provisioningHeatMap = new Lazy<IHeatMap>(new Func<IHeatMap>(this.CreateProvisioningHeatMap));
			this.localProvisioningHeatMap = new Lazy<IHeatMap>(new Func<IHeatMap>(this.CreateLocalProvisioningHeatMap));
			this.localServerDatabaseSelector = new Lazy<DatabaseSelector>(new Func<DatabaseSelector>(this.CreateLocalServerDatabaseSelector));
			this.CmdletPool = this.CreateCmdletPool();
			this.StorePort = new StoreAdapter(base.Logger);
			this.DrainControl = new DatabaseDrainControl(this);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003974 File Offset: 0x00001B74
		public override OrganizationCapability ActiveCapability
		{
			get
			{
				return OrganizationCapability.Management;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003980 File Offset: 0x00001B80
		public virtual DirectoryServer CentralServer
		{
			get
			{
				IAnchorADProvider anchorADProvider = this.CreateAnchorActiveDirectoryProvider();
				ADUser[] array = anchorADProvider.GetOrganizationMailboxesByCapability(base.AnchorCapability).ToArray<ADUser>();
				if (array.Length == 0)
				{
					throw new LoadBalanceAnchorMailboxNotFoundException(base.AnchorCapability.ToString());
				}
				ADUser aduser = (from user in array
				orderby user.Guid
				select user).First<ADUser>();
				string databaseServerFqdn = anchorADProvider.GetDatabaseServerFqdn(aduser.Database.ObjectGuid, false);
				return this.Directory.GetServerByFqdn(new Fqdn(databaseServerFqdn));
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003A0E File Offset: 0x00001C0E
		public IClientFactory ClientFactory
		{
			get
			{
				return this.clientFactory.Value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003A1B File Offset: 0x00001C1B
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003A23 File Offset: 0x00001C23
		public CmdletExecutionPool CmdletPool { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003A2C File Offset: 0x00001C2C
		public virtual DatabaseSelector DatabaseSelector
		{
			get
			{
				return this.databaseSelector.Value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003A39 File Offset: 0x00001C39
		public IDirectoryProvider Directory
		{
			get
			{
				return this.directory.Value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003A46 File Offset: 0x00001C46
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00003A4E File Offset: 0x00001C4E
		public DatabaseDrainControl DrainControl { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003A57 File Offset: 0x00001C57
		public virtual IHeatMap HeatMap
		{
			get
			{
				return this.heatMap.Value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003A64 File Offset: 0x00001C64
		public virtual DatabaseSelector LocalServerDatabaseSelector
		{
			get
			{
				return this.localServerDatabaseSelector.Value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003A71 File Offset: 0x00001C71
		public virtual IHeatMap LocalServerHeatMap
		{
			get
			{
				return this.localProvisioningHeatMap.Value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003A7E File Offset: 0x00001C7E
		public ObjectLogCollector LogCollector
		{
			get
			{
				return this.logCollector.Value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003A8B File Offset: 0x00001C8B
		public MoveInjector MoveInjector
		{
			get
			{
				return this.moveInjector.Value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003A98 File Offset: 0x00001C98
		public virtual IHeatMap ProvisioningHeatMap
		{
			get
			{
				return this.provisioningHeatMap.Value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003AA5 File Offset: 0x00001CA5
		public IRequestQueueManager QueueManager
		{
			get
			{
				return this.requestQueueManager.Value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003AB2 File Offset: 0x00001CB2
		public MailboxLoadBalanceService Service
		{
			get
			{
				return this.service.Value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003ABF File Offset: 0x00001CBF
		public virtual ILoadBalanceSettings Settings
		{
			get
			{
				return (ILoadBalanceSettings)base.Config;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003ACC File Offset: 0x00001CCC
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public virtual IStorePort StorePort { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003ADD File Offset: 0x00001CDD
		public TopologyExtractorFactoryContextPool TopologyExtractorFactoryContextPool
		{
			get
			{
				return this.extractorFactoryContextPool.Value;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003AEC File Offset: 0x00001CEC
		public SoftDeleteMailboxRemovalCheckRemoval CheckSoftDeletedMailboxRemoval(SoftDeletedRemovalData data)
		{
			if (!this.Settings.SoftDeletedCleanupEnabled)
			{
				return SoftDeleteMailboxRemovalCheckRemoval.DisallowRemoval("SoftDeletedRemoval is disabled on the target database '{0}', so no removal check can be performed.", new object[]
				{
					data.TargetDatabase.Name
				});
			}
			DateTime removalCutoffDate = DateTime.UtcNow.Add(TimeSpan.Zero - this.Settings.MinimumSoftDeletedMailboxCleanupAge);
			SoftDeletedMailboxRemovalCheck softDeletedMailboxRemovalCheck = new DisconnectDateCheck(data, this.Directory, removalCutoffDate);
			SoftDeletedMailboxRemovalCheck softDeletedMailboxRemovalCheck2 = new ItemCountCheck(data, this.Directory);
			SoftDeletedMailboxRemovalCheck next = new MoveHistoryCheck(data, this);
			softDeletedMailboxRemovalCheck.SetNext(softDeletedMailboxRemovalCheck2);
			softDeletedMailboxRemovalCheck2.SetNext(next);
			return softDeletedMailboxRemovalCheck.GetRemovalResult();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003B88 File Offset: 0x00001D88
		public virtual void CleanupSoftDeletedMailboxesOnDatabase(DirectoryIdentity databaseIdentity, ByteQuantifiedSize targetSize)
		{
			SoftDeletedMailboxRemover softDeletedMailboxRemover = new SoftDeletedMailboxRemover((DirectoryDatabase)this.Directory.GetDirectoryObject(databaseIdentity), this, targetSize, this.LogCollector);
			softDeletedMailboxRemover.RemoveFromDatabase();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003BBA File Offset: 0x00001DBA
		public virtual IBandSettingsProvider CreateBandSettingsStorage()
		{
			return new BandSettingsStorage(this.CreateLoadBalancingAnchorDataProvider(), this);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public override CacheProcessorBase[] CreateCacheComponents(WaitHandle stopEvent)
		{
			return new CacheProcessorBase[]
			{
				new FirstOrgCacheScanner(this, stopEvent),
				new AutomaticLoadBalanceCacheComponent(this, stopEvent)
			};
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public virtual ILoadBalance CreateLoadBalancer(ILogger logger)
		{
			if (this.Settings.LoadBalanceBlocked)
			{
				throw new AutomaticMailboxLoadBalancingNotAllowedException();
			}
			ILoadBalance result;
			using (IBandSettingsProvider bandSettingsProvider = this.CreateBandSettingsStorage())
			{
				ILoadBalance loadBalance = new BandBasedLoadBalance(bandSettingsProvider.GetBandSettings().ToList<Band>(), logger, this.Settings);
				result = loadBalance;
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003C54 File Offset: 0x00001E54
		public virtual IAnchorDataProvider CreateLoadBalancingAnchorDataProvider()
		{
			return AnchorDataProvider.CreateProviderForMigrationMailboxFolder(this, (AnchorADProvider)this.CreateAnchorActiveDirectoryProvider(), "MailboxLoadBalance");
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003C6C File Offset: 0x00001E6C
		public void CreateSoftDeletedDatabaseCleanupRequests(DirectoryIdentity databaseIdentity, ByteQuantifiedSize targetSize)
		{
			LocalDatabaseSoftDeletedCleanupRequest request = new LocalDatabaseSoftDeletedCleanupRequest(databaseIdentity, targetSize, this);
			this.QueueManager.GetProcessingQueue(this.Directory.GetDirectoryObject(databaseIdentity)).EnqueueRequest(request);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003CA0 File Offset: 0x00001EA0
		public Band[] GetActiveBands()
		{
			Band[] activeBands;
			using (ILoadBalanceService loadBalanceClientForCentralServer = this.ClientFactory.GetLoadBalanceClientForCentralServer())
			{
				activeBands = loadBalanceClientForCentralServer.GetActiveBands();
			}
			return activeBands;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003CE0 File Offset: 0x00001EE0
		public ICapacityProjection GetCapacityProjection(ByteQuantifiedSize averageMailboxSize)
		{
			HeatMapCapacityData heatMapData = this.HeatMap.ToCapacityData();
			CapacityProjectionData capacityProjectionData = CapacityProjectionData.FromSettings(this.Settings);
			AvailableCapacityProjection availableCapacityProjection = new AvailableCapacityProjection(heatMapData, capacityProjectionData, this.Settings.QueryBufferPeriodDays, averageMailboxSize, base.Logger);
			ConsumerSizeProjection consumerSizeProjection = new ConsumerSizeProjection(heatMapData, capacityProjectionData, averageMailboxSize, this.Settings.QueryBufferPeriodDays, (double)this.Settings.MaximumConsumerMailboxSizePercent / 100.0, base.Logger);
			return new MinimumCapacityProjection(base.Logger, new ICapacityProjection[]
			{
				consumerSizeProjection,
				availableCapacityProjection
			});
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003D6E File Offset: 0x00001F6E
		public virtual TopologyExtractorFactoryContext GetTopologyExtractorFactoryContext()
		{
			return this.TopologyExtractorFactoryContextPool.GetContext(this.ClientFactory, this.GetActiveBands(), LoadBalanceUtils.GetNonMovableOrgsList(this.Settings), base.Logger);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003D98 File Offset: 0x00001F98
		public void InitializeForestHeatMap()
		{
			base.Logger.LogVerbose("Using {0} as the heat map.", new object[]
			{
				this.HeatMap
			});
			base.Logger.LogVerbose("Using {0} as the provisioning heat map.", new object[]
			{
				this.ProvisioningHeatMap
			});
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public void InitializeLocalServerHeatMap()
		{
			base.Logger.LogVerbose("Using {0} as the heat map.", new object[]
			{
				this.LocalServerHeatMap
			});
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003E16 File Offset: 0x00002016
		public virtual SoftDeletedMoveHistory RetrieveSoftDeletedMailboxMoveHistory(Guid mailboxGuid, Guid targetDatabaseGuid, Guid sourceDatabaseGuid)
		{
			return SoftDeletedMoveHistory.GetHistoryForSourceDatabase(mailboxGuid, targetDatabaseGuid, sourceDatabaseGuid);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003E20 File Offset: 0x00002020
		public virtual bool TryRemoveSoftDeletedMailbox(Guid mailboxGuid, Guid databaseGuid, out Exception exception)
		{
			SoftDeletedMailboxRemovalRequest softDeletedMailboxRemovalRequest = new SoftDeletedMailboxRemovalRequest(mailboxGuid, databaseGuid, base.Logger, this.CmdletPool, this.Settings);
			this.QueueManager.MainProcessingQueue.EnqueueRequest(softDeletedMailboxRemovalRequest);
			bool flag = softDeletedMailboxRemovalRequest.WaitExecution(TimeSpan.FromMinutes(3.0));
			exception = softDeletedMailboxRemovalRequest.Exception;
			return flag && softDeletedMailboxRemovalRequest.Exception == null;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003E83 File Offset: 0x00002083
		protected virtual IAnchorADProvider CreateAnchorActiveDirectoryProvider()
		{
			return new AnchorADProvider(this, OrganizationId.ForestWideOrgId, null);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003E94 File Offset: 0x00002094
		protected virtual IClientFactory CreateClientFactoryInstance()
		{
			ClientFactory result = new ClientFactory(base.Logger, this);
			if (this.Settings.ClientCacheTimeToLive == TimeSpan.Zero)
			{
				return result;
			}
			return new CachingClientFactory(this.Settings.ClientCacheTimeToLive, result, base.Logger);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003EDE File Offset: 0x000020DE
		protected virtual DatabaseSelector CreateDatabaseSelector()
		{
			if (this.Settings.UseHeatMapProvisioning)
			{
				return new HeatMapDatabaseSelector(this.ProvisioningHeatMap, base.Logger);
			}
			return new ProvisioningLayerDatabaseSelector(this.Directory, base.Logger);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003F10 File Offset: 0x00002110
		protected virtual IDirectoryProvider CreateDirectoryInstance()
		{
			return new DirectoryProvider(this.ClientFactory, LocalServer.GetServer(), this.Settings, this.GetDirectoryListeners(), base.Logger, this);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003F35 File Offset: 0x00002135
		protected virtual TopologyExtractorFactoryContextPool CreateExtractorFactoryPool()
		{
			return new TopologyExtractorFactoryContextPool();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003F3C File Offset: 0x0000213C
		protected IHeatMap CreateForestHeatMap()
		{
			if (this.Settings.UseHeatMapProvisioning)
			{
				return new CachedHeatMap(this, new ForestHeatMapConstructionRequest(this));
			}
			return new ForestHeatMap(this);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003F5E File Offset: 0x0000215E
		protected virtual ObjectLogCollector CreateLogCollector()
		{
			return new ObjectLogCollector();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003F65 File Offset: 0x00002165
		protected override ILogger CreateLogger(string applicationName, AnchorConfig config)
		{
			return new AnchorLogger(applicationName, config, ExTraceGlobals.MailboxLoadBalanceTracer, new ExEventLog(new Guid("2822A8AF-B86C-4A21-B2D2-78E381039C3D"), "MSExchange Mailbox Load Balance"));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003F87 File Offset: 0x00002187
		protected virtual MoveInjector CreateMoveInjector()
		{
			return new MoveInjector(this);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003F90 File Offset: 0x00002190
		protected IHeatMap CreateProvisioningHeatMap()
		{
			return new ChainedHeatMap(new IHeatMap[]
			{
				this.HeatMap,
				this.LocalServerHeatMap
			});
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003FBC File Offset: 0x000021BC
		protected virtual IRequestQueueManager CreateQueueManagerInstance()
		{
			if (this.Settings.DisableWlm)
			{
				return new RequestQueueManager();
			}
			return this.CreateRubsQueue();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003FD7 File Offset: 0x000021D7
		protected virtual MailboxLoadBalanceService CreateServiceInstance()
		{
			return new MailboxLoadBalanceService(this);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003FE0 File Offset: 0x000021E0
		protected virtual IMailboxPolicy[] GetMailboxPolicies(IEventNotificationSender eventNotificationSender)
		{
			return new IMailboxPolicy[]
			{
				new PolicyActivationControl(new MailboxProvisioningConstraintPolicy(eventNotificationSender), this.Settings),
				new PolicyActivationControl(new ZeroItemsPendingUpgradePolicy(this.Settings), this.Settings)
			};
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004024 File Offset: 0x00002224
		private CmdletExecutionPool CreateCmdletPool()
		{
			return new CmdletExecutionPool(this);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000402C File Offset: 0x0000222C
		private IHeatMap CreateLocalProvisioningHeatMap()
		{
			return new CachedHeatMap(this, new LocalServerHeatMapConstructionRequest(this));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000403A File Offset: 0x0000223A
		private DatabaseSelector CreateLocalServerDatabaseSelector()
		{
			return new HeatMapDatabaseSelector(this.LocalServerHeatMap, base.Logger);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004050 File Offset: 0x00002250
		private IRequestQueueManager CreateRubsQueue()
		{
			LoadBalanceWorkload loadBalanceWorkload = new LoadBalanceWorkload(this.Settings);
			SystemWorkloadManager.Initialize(new LoadBalanceActivityLogger());
			SystemWorkloadManager.RegisterWorkload(loadBalanceWorkload);
			return loadBalanceWorkload;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000415C File Offset: 0x0000235C
		private IEnumerable<IDirectoryListener> GetDirectoryListeners()
		{
			yield return new MailboxProcessorDispatcher(this, new Func<IRequestQueueManager, IList<MailboxProcessor>>(this.GetMailboxProcessors));
			yield break;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000417C File Offset: 0x0000237C
		private IList<MailboxProcessor> GetMailboxProcessors(IRequestQueueManager queueManager)
		{
			IGetMoveInfo getMoveInfo = new GetMoveInfo(base.Logger, this.CmdletPool);
			IEventNotificationSender eventNotificationSender = new EventNotificationSender();
			IMailboxPolicy[] mailboxPolicies = this.GetMailboxPolicies(eventNotificationSender);
			return new MailboxProcessor[]
			{
				new MailboxStatisticsLogger(base.Logger, this.LogCollector),
				new MailboxPolicyProcessor(base.Logger, getMoveInfo, this.MoveInjector, mailboxPolicies)
			};
		}

		// Token: 0x04000020 RID: 32
		internal const string LoadBalanceApplicationName = "MailboxLoadBalance";

		// Token: 0x04000021 RID: 33
		private readonly Lazy<IClientFactory> clientFactory;

		// Token: 0x04000022 RID: 34
		private readonly Lazy<DatabaseSelector> databaseSelector;

		// Token: 0x04000023 RID: 35
		private readonly Lazy<IDirectoryProvider> directory;

		// Token: 0x04000024 RID: 36
		private readonly Lazy<TopologyExtractorFactoryContextPool> extractorFactoryContextPool;

		// Token: 0x04000025 RID: 37
		private readonly Lazy<IHeatMap> heatMap;

		// Token: 0x04000026 RID: 38
		private readonly Lazy<IHeatMap> localProvisioningHeatMap;

		// Token: 0x04000027 RID: 39
		private readonly Lazy<DatabaseSelector> localServerDatabaseSelector;

		// Token: 0x04000028 RID: 40
		private readonly Lazy<ObjectLogCollector> logCollector;

		// Token: 0x04000029 RID: 41
		private readonly Lazy<MoveInjector> moveInjector;

		// Token: 0x0400002A RID: 42
		private readonly Lazy<IHeatMap> provisioningHeatMap;

		// Token: 0x0400002B RID: 43
		private readonly Lazy<IRequestQueueManager> requestQueueManager;

		// Token: 0x0400002C RID: 44
		private readonly Lazy<MailboxLoadBalanceService> service;
	}
}
