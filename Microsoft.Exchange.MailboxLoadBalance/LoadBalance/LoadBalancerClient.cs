using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.Providers;
using Microsoft.Exchange.MailboxLoadBalance.Provisioning;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;
using Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval;

namespace Microsoft.Exchange.MailboxLoadBalance.LoadBalance
{
	// Token: 0x02000099 RID: 153
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LoadBalancerClient : VersionedClientBase<ILoadBalanceService>, ILoadBalanceService, IVersionedService, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		private LoadBalancerClient(Binding binding, EndpointAddress remoteAddress, IDirectoryProvider directory, ILogger logger) : base(binding, remoteAddress, logger)
		{
			this.directory = directory;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		public static LoadBalancerClient Create(string serverName, IDirectoryProvider directory, ILogger logger)
		{
			Func<Binding, EndpointAddress, ILogger, LoadBalancerClient> constructor = (Binding binding, EndpointAddress endpointAddress, ILogger l) => new LoadBalancerClient(binding, endpointAddress, directory, l);
			return VersionedClientBase<ILoadBalanceService>.CreateClient<LoadBalancerClient>(serverName, LoadBalanceService.EndpointAddress, constructor, logger);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000E42C File Offset: 0x0000C62C
		public void BeginMailboxMove(BandMailboxRebalanceData rebalanceData, LoadMetric metric)
		{
			base.CallService(delegate()
			{
				this.Channel.BeginMailboxMove(rebalanceData, metric);
			});
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000E488 File Offset: 0x0000C688
		public DatabaseSizeInfo GetDatabaseSizeInformation(DirectoryIdentity database)
		{
			return base.CallService<DatabaseSizeInfo>(() => this.Channel.GetDatabaseSizeInformation(database));
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000E50C File Offset: 0x0000C70C
		public LoadContainer GetLocalServerData(Band[] bands)
		{
			return base.CallService<LoadContainer>(delegate()
			{
				LoadContainer localServerData = this.Channel.GetLocalServerData(bands);
				localServerData.Accept(new DirectoryReconnectionVisitor(this.directory, this.Logger));
				return localServerData;
			});
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000E568 File Offset: 0x0000C768
		public HeatMapCapacityData GetCapacitySummary(DirectoryIdentity objectIdentity, bool refreshData)
		{
			return base.CallService<HeatMapCapacityData>(() => this.Channel.GetCapacitySummary(objectIdentity, refreshData));
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000E5C8 File Offset: 0x0000C7C8
		public BatchCapacityDatum GetConsumerBatchCapacity(int numberOfMailboxes, ByteQuantifiedSize expectedBatchSize)
		{
			return base.CallService<BatchCapacityDatum>(() => this.Channel.GetConsumerBatchCapacity(numberOfMailboxes, expectedBatchSize));
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000E624 File Offset: 0x0000C824
		public MailboxProvisioningResult GetLocalDatabaseForProvisioning(MailboxProvisioningData provisioningData)
		{
			return base.CallService<MailboxProvisioningResult>(() => this.Channel.GetLocalDatabaseForProvisioning(provisioningData));
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000E678 File Offset: 0x0000C878
		public MailboxProvisioningResult GetDatabaseForProvisioning(MailboxProvisioningData provisioningData)
		{
			return base.CallService<MailboxProvisioningResult>(() => this.Channel.GetDatabaseForProvisioning(provisioningData));
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000E6CC File Offset: 0x0000C8CC
		public DatabaseSizeInfo GetDatabaseSizeInformation(DirectoryDatabase database)
		{
			return base.CallService<DatabaseSizeInfo>(() => this.Channel.GetDatabaseSizeInformation(database));
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000E720 File Offset: 0x0000C920
		public SoftDeleteMailboxRemovalCheckRemoval CheckSoftDeletedMailboxRemoval(SoftDeletedRemovalData data)
		{
			return base.CallService<SoftDeleteMailboxRemovalCheckRemoval>(() => this.Channel.CheckSoftDeletedMailboxRemoval(data));
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000E760 File Offset: 0x0000C960
		public Band[] GetActiveBands()
		{
			return base.CallService<Band[]>(() => base.Channel.GetActiveBands());
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000E79C File Offset: 0x0000C99C
		public void CleanupSoftDeletedMailboxesOnDatabase(DirectoryIdentity identity, ByteQuantifiedSize targetSize)
		{
			base.CallService(delegate()
			{
				this.Channel.CleanupSoftDeletedMailboxesOnDatabase(identity, targetSize);
			});
		}

		// Token: 0x040001BD RID: 445
		private readonly IDirectoryProvider directory;
	}
}
