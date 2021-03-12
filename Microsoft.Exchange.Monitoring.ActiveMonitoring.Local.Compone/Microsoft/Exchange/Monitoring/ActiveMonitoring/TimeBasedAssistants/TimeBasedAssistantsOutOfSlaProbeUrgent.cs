using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000538 RID: 1336
	internal class TimeBasedAssistantsOutOfSlaProbeUrgent : TimeBasedAssistantsOutOfSlaProbe
	{
		// Token: 0x060020C9 RID: 8393 RVA: 0x000C7F36 File Offset: 0x000C6136
		protected override TimeBasedAssistantsOutOfSlaDecisionMaker CreateDecisionMakerInstance()
		{
			return new TbaOutOfSlaDecisionMakerUrgent();
		}
	}
}
