using System;
using System.Linq;
using System.ServiceModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.Providers;
using Microsoft.Exchange.MailboxLoadBalance.Provisioning;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;
using Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval;
using Microsoft.Exchange.MailboxLoadBalance.TopologyExtractors;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.LoadBalance
{
	// Token: 0x0200009A RID: 154
	[ClassAccessLevel(AccessLevel.Implementation)]
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
	internal class LoadBalanceService : VersionedServiceBase, ILoadBalanceService, IVersionedService, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x0000E7D6 File Offset: 0x0000C9D6
		public LoadBalanceService(MailboxLoadBalanceService service, LoadBalanceAnchorContext serviceContext) : base(serviceContext.Logger)
		{
			if (service == null)
			{
				throw new ArgumentNullException("service");
			}
			this.serviceImpl = service;
			this.serviceContext = serviceContext;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000E800 File Offset: 0x0000CA00
		public static ServiceEndpointAddress EndpointAddress
		{
			get
			{
				return LoadBalanceService.EndpointAddressHook.Value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0000E80C File Offset: 0x0000CA0C
		protected override VersionInformation ServiceVersion
		{
			get
			{
				return LoadBalancerVersionInformation.LoadBalancerVersion;
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000E840 File Offset: 0x0000CA40
		public void BeginMailboxMove(BandMailboxRebalanceData rebalanceData, LoadMetric metric)
		{
			base.ForwardExceptions(delegate()
			{
				rebalanceData.ConvertToFromSerializationFormat();
				this.serviceImpl.MoveMailboxes(rebalanceData);
			});
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000E894 File Offset: 0x0000CA94
		public SoftDeleteMailboxRemovalCheckRemoval CheckSoftDeletedMailboxRemoval(SoftDeletedRemovalData data)
		{
			return base.ForwardExceptions<SoftDeleteMailboxRemovalCheckRemoval>(() => this.serviceContext.CheckSoftDeletedMailboxRemoval(data));
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000E8F0 File Offset: 0x0000CAF0
		public void CleanupSoftDeletedMailboxesOnDatabase(DirectoryIdentity databaseIdentity, ByteQuantifiedSize targetSize)
		{
			base.ForwardExceptions(delegate()
			{
				this.serviceContext.CreateSoftDeletedDatabaseCleanupRequests(databaseIdentity, targetSize);
			});
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000E92A File Offset: 0x0000CB2A
		public Band[] GetActiveBands()
		{
			return base.ForwardExceptions<Band[]>(new Func<Band[]>(this.GetLocalBandDefinition));
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000E960 File Offset: 0x0000CB60
		public HeatMapCapacityData GetCapacitySummary(DirectoryIdentity objectIdentity, bool refreshData)
		{
			return base.ForwardExceptions<HeatMapCapacityData>(() => this.GetCapacityDatum(objectIdentity, refreshData));
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000EA28 File Offset: 0x0000CC28
		public BatchCapacityDatum GetConsumerBatchCapacity(int numberOfMailboxes, ByteQuantifiedSize expectedBatchSize)
		{
			return base.ForwardExceptions<BatchCapacityDatum>(delegate()
			{
				if (numberOfMailboxes <= 0)
				{
					throw new ArgumentOutOfRangeException("numberOfMailboxes", numberOfMailboxes, "Number of mailbox must be greater than zero.");
				}
				BatchCapacityProjection batchCapacityProjection = new BatchCapacityProjection(numberOfMailboxes);
				ICapacityProjection capacityProjection = this.serviceContext.GetCapacityProjection(expectedBatchSize / numberOfMailboxes);
				return new MinimumCapacityProjection(this.Logger, new ICapacityProjection[]
				{
					batchCapacityProjection,
					capacityProjection
				}).GetCapacity();
			});
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000EA88 File Offset: 0x0000CC88
		public MailboxProvisioningResult GetDatabaseForProvisioning(MailboxProvisioningData provisioningData)
		{
			return base.ForwardExceptions<MailboxProvisioningResult>(() => this.serviceContext.DatabaseSelector.GetDatabase(provisioningData));
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
		public DatabaseSizeInfo GetDatabaseSizeInformation(DirectoryDatabase database)
		{
			return base.ForwardExceptions<DatabaseSizeInfo>(() => this.GetDatabaseSize(database));
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000EB40 File Offset: 0x0000CD40
		public DatabaseSizeInfo GetDatabaseSizeInformation(DirectoryIdentity database)
		{
			return base.ForwardExceptions<DatabaseSizeInfo>(() => this.GetDatabaseSize((DirectoryDatabase)this.serviceContext.Directory.GetDirectoryObject(database)));
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000EB98 File Offset: 0x0000CD98
		public MailboxProvisioningResult GetLocalDatabaseForProvisioning(MailboxProvisioningData provisioningData)
		{
			return base.ForwardExceptions<MailboxProvisioningResult>(() => this.serviceContext.LocalServerDatabaseSelector.GetDatabase(provisioningData));
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public LoadContainer GetLocalServerData(Band[] bands)
		{
			return base.ForwardExceptions<LoadContainer>(delegate()
			{
				this.serviceContext.LocalServerHeatMap.UpdateBands(bands);
				LoadContainer loadTopology = this.serviceContext.LocalServerHeatMap.GetLoadTopology();
				bool convertBandToBandData = !this.ClientVersion[3];
				return (LoadContainer)loadTopology.ToSerializationFormat(convertBandToBandData);
			});
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000EC67 File Offset: 0x0000CE67
		protected internal static IDisposable SetEndpointAddress(ServiceEndpointAddress endpointAddress)
		{
			return LoadBalanceService.EndpointAddressHook.SetTestHook(endpointAddress);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000EC74 File Offset: 0x0000CE74
		private HeatMapCapacityData GetCapacityDatum(DirectoryIdentity objectIdentity, bool refreshData)
		{
			DirectoryIdentity identity = this.serviceContext.Directory.GetLocalServer().Identity;
			if (objectIdentity.ObjectType == DirectoryObjectType.Server)
			{
				return this.GetServerCapacityDatum(objectIdentity, refreshData, identity);
			}
			if (objectIdentity.ObjectType == DirectoryObjectType.Forest)
			{
				return this.GetLocalForestCapacityDatum();
			}
			if (objectIdentity.ObjectType == DirectoryObjectType.DatabaseAvailabilityGroup)
			{
				return this.GetDagCapacityDatum(objectIdentity);
			}
			if (objectIdentity.ObjectType != DirectoryObjectType.Database)
			{
				throw new CannotRetrieveCapacityDataException(objectIdentity.ToString());
			}
			return this.GetDatabaseCapacityDatum(objectIdentity, refreshData, identity);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		private HeatMapCapacityData GetDagCapacityDatum(DirectoryIdentity objectIdentity)
		{
			TopologyExtractorFactoryContext topologyExtractorFactoryContext = this.serviceContext.GetTopologyExtractorFactoryContext();
			DirectoryDatabaseAvailabilityGroup directoryObject = (DirectoryDatabaseAvailabilityGroup)this.serviceContext.Directory.GetDirectoryObject(objectIdentity);
			TopologyExtractorFactory loadBalancingCentralFactory = topologyExtractorFactoryContext.GetLoadBalancingCentralFactory();
			LoadContainer loadContainer = loadBalancingCentralFactory.GetExtractor(directoryObject).ExtractTopology();
			return loadContainer.ToCapacityData();
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000ED38 File Offset: 0x0000CF38
		private HeatMapCapacityData GetDatabaseCapacityDatum(DirectoryIdentity objectIdentity, bool refreshData, DirectoryIdentity localServerIdentity)
		{
			DirectoryDatabase directoryDatabase = (DirectoryDatabase)this.serviceContext.Directory.GetDirectoryObject(objectIdentity);
			DirectoryServer directoryServer = directoryDatabase.ActivationOrder.FirstOrDefault<DirectoryServer>();
			if (directoryServer == null || localServerIdentity.Equals(directoryServer.Identity))
			{
				TopologyExtractorFactoryContext topologyExtractorFactoryContext = this.serviceContext.GetTopologyExtractorFactoryContext();
				TopologyExtractorFactory loadBalancingLocalFactory = topologyExtractorFactoryContext.GetLoadBalancingLocalFactory(refreshData);
				LoadContainer loadContainer = loadBalancingLocalFactory.GetExtractor(directoryDatabase).ExtractTopology();
				return loadContainer.ToCapacityData();
			}
			HeatMapCapacityData capacitySummary;
			using (ILoadBalanceService loadBalanceClientForDatabase = this.serviceContext.ClientFactory.GetLoadBalanceClientForDatabase(directoryDatabase))
			{
				capacitySummary = loadBalanceClientForDatabase.GetCapacitySummary(objectIdentity, refreshData);
			}
			return capacitySummary;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
		private DatabaseSizeInfo GetDatabaseSize(DirectoryDatabase database)
		{
			DatabaseSizeInfo databaseSpaceData;
			using (IPhysicalDatabase physicalDatabaseConnection = this.serviceContext.ClientFactory.GetPhysicalDatabaseConnection(database))
			{
				databaseSpaceData = physicalDatabaseConnection.GetDatabaseSpaceData();
			}
			return databaseSpaceData;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000EE28 File Offset: 0x0000D028
		private Band[] GetLocalBandDefinition()
		{
			Band[] result;
			using (IBandSettingsProvider bandSettingsProvider = this.serviceContext.CreateBandSettingsStorage())
			{
				result = bandSettingsProvider.GetBandSettings().ToArray<Band>();
			}
			return result;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000EE6C File Offset: 0x0000D06C
		private HeatMapCapacityData GetLocalForestCapacityDatum()
		{
			return this.serviceContext.HeatMap.ToCapacityData();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000EE80 File Offset: 0x0000D080
		private HeatMapCapacityData GetServerCapacityDatum(DirectoryIdentity objectIdentity, bool refreshData, DirectoryIdentity localServerIdentity)
		{
			if (objectIdentity.Equals(localServerIdentity))
			{
				return this.serviceContext.LocalServerHeatMap.ToCapacityData();
			}
			DirectoryServer server = (DirectoryServer)this.serviceContext.Directory.GetDirectoryObject(objectIdentity);
			HeatMapCapacityData capacitySummary;
			using (ILoadBalanceService loadBalanceClientForServer = this.serviceContext.ClientFactory.GetLoadBalanceClientForServer(server, false))
			{
				capacitySummary = loadBalanceClientForServer.GetCapacitySummary(objectIdentity, refreshData);
			}
			return capacitySummary;
		}

		// Token: 0x040001BE RID: 446
		private const string EndpointSuffix = "Microsoft.Exchange.MailboxLoadBalance.LoadBalanceService";

		// Token: 0x040001BF RID: 447
		private static readonly Hookable<ServiceEndpointAddress> EndpointAddressHook = Hookable<ServiceEndpointAddress>.Create(true, new ServiceEndpointAddress("Microsoft.Exchange.MailboxLoadBalance.LoadBalanceService"));

		// Token: 0x040001C0 RID: 448
		private readonly LoadBalanceAnchorContext serviceContext;

		// Token: 0x040001C1 RID: 449
		private readonly MailboxLoadBalanceService serviceImpl;
	}
}
