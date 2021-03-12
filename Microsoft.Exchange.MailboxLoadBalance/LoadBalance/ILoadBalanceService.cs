using System;
using System.ServiceModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.Provisioning;
using Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval;

namespace Microsoft.Exchange.MailboxLoadBalance.LoadBalance
{
	// Token: 0x0200002D RID: 45
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[ServiceContract(SessionMode = SessionMode.Allowed)]
	internal interface ILoadBalanceService : IVersionedService, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000166 RID: 358
		[OperationContract]
		void BeginMailboxMove(BandMailboxRebalanceData rebalanceData, LoadMetric metric);

		// Token: 0x06000167 RID: 359
		[OperationContract]
		SoftDeleteMailboxRemovalCheckRemoval CheckSoftDeletedMailboxRemoval(SoftDeletedRemovalData data);

		// Token: 0x06000168 RID: 360
		[OperationContract]
		void CleanupSoftDeletedMailboxesOnDatabase(DirectoryIdentity identity, ByteQuantifiedSize targetSize);

		// Token: 0x06000169 RID: 361
		[OperationContract]
		Band[] GetActiveBands();

		// Token: 0x0600016A RID: 362
		[OperationContract]
		HeatMapCapacityData GetCapacitySummary(DirectoryIdentity objectIdentity, bool refreshData);

		// Token: 0x0600016B RID: 363
		[OperationContract]
		BatchCapacityDatum GetConsumerBatchCapacity(int numberOfMailboxes, ByteQuantifiedSize expectedBatchSize);

		// Token: 0x0600016C RID: 364
		[OperationContract]
		MailboxProvisioningResult GetDatabaseForProvisioning(MailboxProvisioningData provisioningData);

		// Token: 0x0600016D RID: 365
		[OperationContract]
		DatabaseSizeInfo GetDatabaseSizeInformation(DirectoryDatabase database);

		// Token: 0x0600016E RID: 366
		[OperationContract(Name = "GetDatabaseSizeInformation2")]
		DatabaseSizeInfo GetDatabaseSizeInformation(DirectoryIdentity database);

		// Token: 0x0600016F RID: 367
		[OperationContract]
		MailboxProvisioningResult GetLocalDatabaseForProvisioning(MailboxProvisioningData provisioningData);

		// Token: 0x06000170 RID: 368
		[OperationContract(Name = "GetLocalServerData2")]
		LoadContainer GetLocalServerData(Band[] bands);
	}
}
