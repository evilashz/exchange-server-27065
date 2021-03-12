using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200002F RID: 47
	internal interface IProvisioningHandler
	{
		// Token: 0x060001E6 RID: 486
		void RegisterJob(Guid jobId, CultureInfo cultureInfo, Guid ownerExchangeObjectId, ADObjectId ownerId, DelegatedPrincipal delegatedAdminOwner, SubmittedByUserAdminType migrationRequesterRole, string tenantOrganization, OrganizationId organizationId = null);

		// Token: 0x060001E7 RID: 487
		void UnregisterJob(Guid jobId);

		// Token: 0x060001E8 RID: 488
		bool IsJobRegistered(Guid jobId);

		// Token: 0x060001E9 RID: 489
		bool CanUnregisterJob(Guid jobId);

		// Token: 0x060001EA RID: 490
		bool HasCapacity(Guid jobId);

		// Token: 0x060001EB RID: 491
		bool QueueItem(Guid jobId, ObjectId itemId, IProvisioningData provisioningData);

		// Token: 0x060001EC RID: 492
		bool IsItemQueued(ObjectId itemId);

		// Token: 0x060001ED RID: 493
		bool IsItemCompleted(ObjectId itemId);

		// Token: 0x060001EE RID: 494
		void CancelItem(ObjectId itemId);

		// Token: 0x060001EF RID: 495
		ProvisionedObject DequeueItem(ObjectId itemId);
	}
}
