using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001FA RID: 506
	internal class DatabaseAvailabilitySiteAlert : MonitoringSiteAlert
	{
		// Token: 0x06001402 RID: 5122 RVA: 0x00050DF2 File Offset: 0x0004EFF2
		public DatabaseAvailabilitySiteAlert(string databaseName, Guid dbGuid) : base(databaseName, dbGuid)
		{
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00050DFC File Offset: 0x0004EFFC
		protected override void RaiseGreenEvent(IHealthValidationResultMinimal result)
		{
			ReplayCrimsonEvents.DatabaseLevelSiteAvailabilityCheckPassed.Log<string, int, string, Guid>(base.Identity, result.HealthyCopiesCount, EventUtil.TruncateStringInput(result.ErrorMessage, 32766), base.IdentityGuid);
			new EventNotificationItem("msexchangerepl", "DatabaseCopyAvailability", "SingleAvailableCopyCheckSucceeded", EventUtil.TruncateStringInput(result.ErrorMessage, 32766), ResultSeverityLevel.Informational)
			{
				StateAttribute1 = EventUtil.TruncateStringInput(result.ErrorMessage, 32766)
			}.Publish(false);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00050E78 File Offset: 0x0004F078
		protected override void RaiseRedEvent(IHealthValidationResultMinimal result)
		{
			ReplayCrimsonEvents.DatabaseLevelSiteAvailabilityCheckFailed.Log<string, int, string, Guid>(base.Identity, result.HealthyCopiesCount, EventUtil.TruncateStringInput(result.ErrorMessage, 32766), base.IdentityGuid);
			new EventNotificationItem("msexchangerepl", "DatabaseCopyAvailability", "SingleAvailableCopyCheckFailed", EventUtil.TruncateStringInput(result.ErrorMessage, 32766), ResultSeverityLevel.Critical)
			{
				StateAttribute1 = EventUtil.TruncateStringInput(result.ErrorMessage, 32766)
			}.Publish(false);
		}
	}
}
