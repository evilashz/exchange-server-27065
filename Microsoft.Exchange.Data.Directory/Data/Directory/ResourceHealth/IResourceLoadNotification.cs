using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009FC RID: 2556
	internal interface IResourceLoadNotification
	{
		// Token: 0x0600766A RID: 30314
		Guid SubscribeToHealthNotifications(WorkloadClassification classification, HealthRecoveryNotification delegateToFire);

		// Token: 0x0600766B RID: 30315
		bool UnsubscribeFromHealthNotifications(Guid registrationKey);
	}
}
