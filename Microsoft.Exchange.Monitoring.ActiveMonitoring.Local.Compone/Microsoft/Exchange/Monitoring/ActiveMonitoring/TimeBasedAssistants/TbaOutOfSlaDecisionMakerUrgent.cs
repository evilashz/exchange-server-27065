using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000533 RID: 1331
	internal sealed class TbaOutOfSlaDecisionMakerUrgent : TimeBasedAssistantsOutOfSlaDecisionMaker
	{
		// Token: 0x060020BD RID: 8381 RVA: 0x000C7ABF File Offset: 0x000C5CBF
		public TbaOutOfSlaDecisionMakerUrgent() : base(TbaOutOfSlaDecisionMakerUrgent.maxWorkcycleLengthMinutesToLookAt, TbaOutOfSlaAlertType.Urgent, TbaOutOfSlaDecisionMakerUrgent.workcycleAlertClassifications)
		{
		}

		// Token: 0x04001805 RID: 6149
		private static readonly int maxWorkcycleLengthMinutesToLookAt = (int)TimeSpan.FromHours(8.0).TotalMinutes;

		// Token: 0x04001806 RID: 6150
		private static readonly TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification[] workcycleAlertClassifications = new TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification[]
		{
			new TimeBasedAssistantsLastNCriteria.WorkcycleAlertClassification
			{
				WorkcycleMinutesMax = (int)TimeSpan.FromHours(8.0).TotalMinutes,
				AlertTimeThresholdMinutes = (int)TimeSpan.FromHours(8.0).TotalMinutes
			}
		};
	}
}
