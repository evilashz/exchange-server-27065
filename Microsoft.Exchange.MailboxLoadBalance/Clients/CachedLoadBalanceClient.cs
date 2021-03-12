using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.LoadBalance;
using Microsoft.Exchange.MailboxLoadBalance.Provisioning;
using Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.Clients
{
	// Token: 0x0200002E RID: 46
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CachedLoadBalanceClient : CachedClient, ILoadBalanceService, IVersionedService, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000171 RID: 369 RVA: 0x00006F02 File Offset: 0x00005102
		public CachedLoadBalanceClient(ILoadBalanceService client) : base(client as IWcfClient)
		{
			this.client = client;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006F17 File Offset: 0x00005117
		public SoftDeleteMailboxRemovalCheckRemoval CheckSoftDeletedMailboxRemoval(SoftDeletedRemovalData data)
		{
			return this.client.CheckSoftDeletedMailboxRemoval(data);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006F25 File Offset: 0x00005125
		public void CleanupSoftDeletedMailboxesOnDatabase(DirectoryIdentity identity, ByteQuantifiedSize targetSize)
		{
			this.client.CleanupSoftDeletedMailboxesOnDatabase(identity, targetSize);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006F34 File Offset: 0x00005134
		void ILoadBalanceService.BeginMailboxMove(BandMailboxRebalanceData rebalanceData, LoadMetric metric)
		{
			this.client.BeginMailboxMove(rebalanceData, metric);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006F43 File Offset: 0x00005143
		void IVersionedService.ExchangeVersionInformation(VersionInformation clientVersion, out VersionInformation serverVersion)
		{
			this.client.ExchangeVersionInformation(clientVersion, out serverVersion);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006F52 File Offset: 0x00005152
		Band[] ILoadBalanceService.GetActiveBands()
		{
			return this.client.GetActiveBands();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006F5F File Offset: 0x0000515F
		public HeatMapCapacityData GetCapacitySummary(DirectoryIdentity objectIdentity, bool refreshData)
		{
			return this.client.GetCapacitySummary(objectIdentity, refreshData);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006F6E File Offset: 0x0000516E
		DatabaseSizeInfo ILoadBalanceService.GetDatabaseSizeInformation(DirectoryIdentity database)
		{
			return this.client.GetDatabaseSizeInformation(database);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006F7C File Offset: 0x0000517C
		DatabaseSizeInfo ILoadBalanceService.GetDatabaseSizeInformation(DirectoryDatabase database)
		{
			return this.client.GetDatabaseSizeInformation(database);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006F8A File Offset: 0x0000518A
		LoadContainer ILoadBalanceService.GetLocalServerData(Band[] bands)
		{
			return this.client.GetLocalServerData(bands);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006F98 File Offset: 0x00005198
		public BatchCapacityDatum GetConsumerBatchCapacity(int numberOfMailboxes, ByteQuantifiedSize expectedBatchSize)
		{
			return this.client.GetConsumerBatchCapacity(numberOfMailboxes, expectedBatchSize);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006FA7 File Offset: 0x000051A7
		public MailboxProvisioningResult GetLocalDatabaseForProvisioning(MailboxProvisioningData provisioningData)
		{
			return this.client.GetLocalDatabaseForProvisioning(provisioningData);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006FB5 File Offset: 0x000051B5
		public MailboxProvisioningResult GetDatabaseForProvisioning(MailboxProvisioningData provisioningData)
		{
			return this.client.GetDatabaseForProvisioning(provisioningData);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006FC3 File Offset: 0x000051C3
		internal override void Cleanup()
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006FC5 File Offset: 0x000051C5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CachedLoadBalanceClient>(this);
		}

		// Token: 0x04000091 RID: 145
		private readonly ILoadBalanceService client;
	}
}
