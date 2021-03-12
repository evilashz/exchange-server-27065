using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.Provisioning;
using Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.LoadBalance
{
	// Token: 0x02000094 RID: 148
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MissingCapabilityLoadBalanceClientDecorator : DisposeTrackableBase, ILoadBalanceService, IVersionedService, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000558 RID: 1368 RVA: 0x0000E0F5 File Offset: 0x0000C2F5
		protected MissingCapabilityLoadBalanceClientDecorator(ILoadBalanceService service, DirectoryServer targetServer)
		{
			this.TargetServer = targetServer;
			this.service = service;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000E10B File Offset: 0x0000C30B
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0000E113 File Offset: 0x0000C313
		protected DirectoryServer TargetServer { get; set; }

		// Token: 0x0600055B RID: 1371 RVA: 0x0000E11C File Offset: 0x0000C31C
		public virtual void BeginMailboxMove(BandMailboxRebalanceData rebalanceData, LoadMetric metric)
		{
			this.service.BeginMailboxMove(rebalanceData, metric);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000E12B File Offset: 0x0000C32B
		public virtual SoftDeleteMailboxRemovalCheckRemoval CheckSoftDeletedMailboxRemoval(SoftDeletedRemovalData data)
		{
			return this.service.CheckSoftDeletedMailboxRemoval(data);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000E139 File Offset: 0x0000C339
		public virtual void CleanupSoftDeletedMailboxesOnDatabase(DirectoryIdentity identity, ByteQuantifiedSize targetSize)
		{
			this.service.CleanupSoftDeletedMailboxesOnDatabase(identity, targetSize);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000E148 File Offset: 0x0000C348
		public virtual void ExchangeVersionInformation(VersionInformation clientVersion, out VersionInformation serverVersion)
		{
			this.service.ExchangeVersionInformation(clientVersion, out serverVersion);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000E157 File Offset: 0x0000C357
		public virtual Band[] GetActiveBands()
		{
			return this.service.GetActiveBands();
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000E164 File Offset: 0x0000C364
		public virtual HeatMapCapacityData GetCapacitySummary(DirectoryIdentity objectIdentity, bool refreshData)
		{
			return this.service.GetCapacitySummary(objectIdentity, refreshData);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0000E173 File Offset: 0x0000C373
		public virtual DatabaseSizeInfo GetDatabaseSizeInformation(DirectoryDatabase database)
		{
			return this.service.GetDatabaseSizeInformation(database);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000E181 File Offset: 0x0000C381
		public virtual DatabaseSizeInfo GetDatabaseSizeInformation(DirectoryIdentity database)
		{
			return this.service.GetDatabaseSizeInformation(database);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000E18F File Offset: 0x0000C38F
		public virtual LoadContainer GetLocalServerData(Band[] bands)
		{
			return this.service.GetLocalServerData(bands);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		public BatchCapacityDatum GetConsumerBatchCapacity(int numberOfMailboxes, ByteQuantifiedSize expectedBatchSize)
		{
			return new BatchCapacityDatum
			{
				MaximumNumberOfMailboxes = 0
			};
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000E1BB File Offset: 0x0000C3BB
		public MailboxProvisioningResult GetLocalDatabaseForProvisioning(MailboxProvisioningData provisioningData)
		{
			throw new NotSupportedException("GetLocalDatabaseForProvisioning is not cross version compat.");
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000E1C7 File Offset: 0x0000C3C7
		public MailboxProvisioningResult GetDatabaseForProvisioning(MailboxProvisioningData provisioningData)
		{
			throw new NotSupportedException("GetDatabaseForProvisioning is not cross version compat.");
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000E1D3 File Offset: 0x0000C3D3
		protected override void InternalDispose(bool disposing)
		{
			if (this.service != null)
			{
				this.service.Dispose();
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MissingCapabilityLoadBalanceClientDecorator>(this);
		}

		// Token: 0x040001B8 RID: 440
		private readonly ILoadBalanceService service;
	}
}
