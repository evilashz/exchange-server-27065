using System;

namespace Microsoft.Office.CompliancePolicy.Monitor
{
	// Token: 0x0200007D RID: 125
	internal static class MonitoringNotificationContextTemplate
	{
		// Token: 0x040001EA RID: 490
		public const string PolicySyncTimeExceededEventError = "UnifiedPolicySync.PolicySyncTimeExceededError";

		// Token: 0x040001EB RID: 491
		public const string PolicySyncPermanentError = "UnifiedPolicySync.PermanentError";

		// Token: 0x040001EC RID: 492
		public const string PolicySyncPermanentStatusPublishError = "UnifiedPolicySync.PermanentStatusPublishError";

		// Token: 0x040001ED RID: 493
		public const string PolicySyncSendNotificationError = "UnifiedPolicySync.SendNotificationError";

		// Token: 0x040001EE RID: 494
		public const string PermanentSyncFailure = "NotificationId={0}\r\nTimestamp={1}\r\n{2}";

		// Token: 0x040001EF RID: 495
		public const string PermanentSyncStatusFailure = "Timestamp={0};Sync status notification Id={1}";

		// Token: 0x040001F0 RID: 496
		public const string ExpectedSyncTimeExceeded = "NotificationId={0};Timestamp={1}\r\nLatencies:\r\nTotalProcessTime={2},TryCount={3}\r\nCurrentCycle:NotifyPickUpDelay={4};Initialization={5};{6}";

		// Token: 0x040001F1 RID: 497
		public const string SendNotificationError = "Workload={0};Timestamp={1}";
	}
}
