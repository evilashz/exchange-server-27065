using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001F9 RID: 505
	internal class DatabaseAvailabilityAlert : MonitoringAlert
	{
		// Token: 0x060013FF RID: 5119 RVA: 0x00050D44 File Offset: 0x0004EF44
		public DatabaseAvailabilityAlert(string databaseName, Guid dbGuid) : base(databaseName, dbGuid)
		{
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00050D4E File Offset: 0x0004EF4E
		protected override void RaiseGreenEvent(IHealthValidationResultMinimal result)
		{
			ReplayCrimsonEvents.DatabaseLevelAvailabilityCheckPassed.Log<string, int, string, Guid>(base.Identity, result.HealthyCopiesCount, EventUtil.TruncateStringInput(result.ErrorMessage, 32766), base.IdentityGuid);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00050D7C File Offset: 0x0004EF7C
		protected override void RaiseRedEvent(IHealthValidationResultMinimal result)
		{
			ReplayEventLogConstants.Tuple_MonitoringDatabaseAvailabilityCheckFailed.LogEvent(null, new object[]
			{
				base.Identity,
				result.HealthyCopiesCount,
				EventUtil.TruncateStringInput(result.ErrorMessage, 32766)
			});
			ReplayCrimsonEvents.DatabaseLevelAvailabilityCheckFailed.Log<string, int, string, Guid>(base.Identity, result.HealthyCopiesCount, EventUtil.TruncateStringInput(result.ErrorMessage, 32766), base.IdentityGuid);
		}
	}
}
