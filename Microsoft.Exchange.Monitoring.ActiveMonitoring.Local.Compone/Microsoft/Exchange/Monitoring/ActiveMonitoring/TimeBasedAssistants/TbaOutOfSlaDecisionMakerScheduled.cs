using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000534 RID: 1332
	internal sealed class TbaOutOfSlaDecisionMakerScheduled : TimeBasedAssistantsOutOfSlaDecisionMaker
	{
		// Token: 0x060020BF RID: 8383 RVA: 0x000C7B5C File Offset: 0x000C5D5C
		public TbaOutOfSlaDecisionMakerScheduled() : base(TbaOutOfSlaDecisionMakerScheduled.maxWorkcycleLengthMinutesToLookAt, TbaOutOfSlaAlertType.Scheduled, TbaOutOfSlaDecisionMakerScheduled.workcycleAlertClassifications)
		{
		}

		// Token: 0x04001807 RID: 6151
		private static readonly int maxWorkcycleLengthMinutesToLookAt = (int)TimeSpan.FromHours(24.0).TotalMinutes;

		// Token: 0x04001808 RID: 6152
		private static readonly TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification[] workcycleAlertClassifications = new TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification[]
		{
			new TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification
			{
				WorkcycleMinutesMax = (int)TimeSpan.FromHours(24.0).TotalMinutes,
				AlertTimeThresholdMinutes = (int)TimeSpan.FromHours(12.0).TotalMinutes
			}
		};
	}
}
