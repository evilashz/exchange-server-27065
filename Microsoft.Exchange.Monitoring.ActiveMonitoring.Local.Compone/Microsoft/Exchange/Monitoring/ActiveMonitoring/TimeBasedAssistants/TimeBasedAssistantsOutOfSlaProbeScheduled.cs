using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000539 RID: 1337
	internal class TimeBasedAssistantsOutOfSlaProbeScheduled : TimeBasedAssistantsOutOfSlaProbe
	{
		// Token: 0x060020CB RID: 8395 RVA: 0x000C7F45 File Offset: 0x000C6145
		protected override TimeBasedAssistantsOutOfSlaDecisionMaker CreateDecisionMakerInstance()
		{
			return new TbaOutOfSlaDecisionMakerScheduled();
		}
	}
}
