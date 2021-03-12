using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.MailboxSpace
{
	// Token: 0x020001EA RID: 490
	public static class MailboxSpaceMonitoringStrings
	{
		// Token: 0x04000A40 RID: 2624
		public const string DatabaseSpaceProbeName = "DatabaseSpaceProbe";

		// Token: 0x04000A41 RID: 2625
		public const string DatabaseSizeMonitorName = "DatabaseSizeMonitor";

		// Token: 0x04000A42 RID: 2626
		public const string DatabaseSizeEscalationProcessingMonitorName = "DatabaseSizeEscalationProcessingMonitor";

		// Token: 0x04000A43 RID: 2627
		public const string StorageLogicalDriveSpaceMonitorName = "StorageLogicalDriveSpaceMonitor";

		// Token: 0x04000A44 RID: 2628
		public const string DatabaseSizeProvisioningResponderName = "DatabaseSizeProvisioning";

		// Token: 0x04000A45 RID: 2629
		public const string DatabaseSizeEscalateResponderName = "DatabaseSizeEscalate";

		// Token: 0x04000A46 RID: 2630
		public const string DatabaseSizeEscalationNotificationResponderName = "DatabaseSizeEscalationNotification";

		// Token: 0x04000A47 RID: 2631
		public const string StorageLogicalDriveSpaceEscalateResponderName = "StorageLogicalDriveSpaceEscalate";

		// Token: 0x04000A48 RID: 2632
		public const string DatabaseLogicalPhysicalSizeRatioMonitorName = "DatabaseLogicalPhysicalSizeRatioMonitor";

		// Token: 0x04000A49 RID: 2633
		public const string SetDatabaseLogicalPhysicalSizeRatioMonitorStateRepairingResponderName = "SetDatabaseLogicalPhysicalSizeRatioMonitorStateRepairing";

		// Token: 0x04000A4A RID: 2634
		public const string DatabaseLogicalPhysicalSizeRatioEscalationProcessingMonitorName = "DatabaseLogicalPhysicalSizeRatioEscalationProcessingMonitor";

		// Token: 0x04000A4B RID: 2635
		public const string SetDatabaseLogicalPhysicalSizeRatioEscalationProcessingMonitorStateRepairingResponderName = "SetDatabaseLogicalPhysicalSizeRatioEscalationProcessingMonitorStateRepairing";

		// Token: 0x04000A4C RID: 2636
		public const string DatabaseLogicalPhysicalSizeRatioEscalationNotificationResponderName = "DatabaseLogicalPhysicalSizeRatioEscalationNotification";

		// Token: 0x04000A4D RID: 2637
		public const string DatabaseLogicalPhysicalSizeRatioEscalateResponderName = "DatabaseLogicalPhysicalSizeRatioEscalate";

		// Token: 0x04000A4E RID: 2638
		public const string UnableToGetDiskCapacity = "DiskCapacityForDatabase{0}NotPopulatedByDatabaseSpaceProbe";

		// Token: 0x04000A4F RID: 2639
		public const string InvokeMonitoringProbeCommand = "Invoke-MonitoringProbe -Identity '{0}\\{1}\\{2}' -Server {3}";

		// Token: 0x04000A50 RID: 2640
		public const string GetAllUnhealthyMonitors = "Get-ServerHealth -Identity '{0}' -HealthSet '{1}' | ?{{$_.Name -match '{2}' -and $_.AlertValue -ne 'Healthy'}}";
	}
}
