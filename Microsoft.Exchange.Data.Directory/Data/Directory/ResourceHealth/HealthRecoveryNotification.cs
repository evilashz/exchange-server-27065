using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009F4 RID: 2548
	// (Invoke) Token: 0x0600764A RID: 30282
	internal delegate void HealthRecoveryNotification(ResourceKey key, WorkloadClassification classification, Guid notificationCookie);
}
