using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001F8 RID: 504
	internal class DatabaseRedundancySiteAlert : MonitoringSiteAlert
	{
		// Token: 0x060013FC RID: 5116 RVA: 0x00050CDE File Offset: 0x0004EEDE
		public DatabaseRedundancySiteAlert(string databaseName, Guid dbGuid) : base(databaseName, dbGuid)
		{
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00050CE8 File Offset: 0x0004EEE8
		protected override void RaiseGreenEvent(IHealthValidationResultMinimal result)
		{
			ReplayCrimsonEvents.DatabaseLevelSiteRedundancyCheckPassed.Log<string, int, string, Guid>(base.Identity, result.HealthyCopiesCount, EventUtil.TruncateStringInput(result.ErrorMessage, 32766), base.IdentityGuid);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00050D16 File Offset: 0x0004EF16
		protected override void RaiseRedEvent(IHealthValidationResultMinimal result)
		{
			ReplayCrimsonEvents.DatabaseLevelSiteRedundancyCheckFailed.Log<string, int, string, Guid>(base.Identity, result.HealthyCopiesCount, EventUtil.TruncateStringInput(result.ErrorMessage, 32766), base.IdentityGuid);
		}
	}
}
