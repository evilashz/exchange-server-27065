using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001F7 RID: 503
	internal class DatabaseRedundancyAlert : MonitoringAlert
	{
		// Token: 0x060013F8 RID: 5112 RVA: 0x00050BD8 File Offset: 0x0004EDD8
		public DatabaseRedundancyAlert(string databaseName, Guid dbGuid) : base(databaseName, dbGuid)
		{
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00050BE2 File Offset: 0x0004EDE2
		protected override TimeSpan DatabaseHealthCheckGreenTransitionSuppression
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckOneCopyGreenTransitionSuppressionInSec);
			}
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00050BF0 File Offset: 0x0004EDF0
		protected override void RaiseGreenEvent(IHealthValidationResultMinimal result)
		{
			ReplayEventLogConstants.Tuple_MonitoringDatabaseRedundancyCheckPassed.LogEvent(null, new object[]
			{
				base.Identity,
				result.HealthyCopiesCount,
				EventUtil.TruncateStringInput(result.ErrorMessage, 32766)
			});
			ReplayCrimsonEvents.DatabaseLevelRedundancyCheckPassed.Log<string, int, string, Guid>(base.Identity, result.HealthyCopiesCount, EventUtil.TruncateStringInput(result.ErrorMessage, 32766), base.IdentityGuid);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00050C68 File Offset: 0x0004EE68
		protected override void RaiseRedEvent(IHealthValidationResultMinimal result)
		{
			ReplayEventLogConstants.Tuple_MonitoringDatabaseRedundancyCheckFailed.LogEvent(null, new object[]
			{
				base.Identity,
				result.HealthyCopiesCount,
				EventUtil.TruncateStringInput(result.ErrorMessage, 32766)
			});
			ReplayCrimsonEvents.DatabaseLevelRedundancyCheckFailed.Log<string, int, string, Guid>(base.Identity, result.HealthyCopiesCount, EventUtil.TruncateStringInput(result.ErrorMessage, 32766), base.IdentityGuid);
		}
	}
}
