using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Inference
{
	// Token: 0x0200049B RID: 1179
	public static class InferenceStrings
	{
		// Token: 0x06001DB6 RID: 7606 RVA: 0x000B379B File Offset: 0x000B199B
		internal static string DisableInferenceComponentResponderName(string monitorName)
		{
			return monitorName.Replace("Monitor", "DisableInferenceComponent");
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x000B37AD File Offset: 0x000B19AD
		internal static string InferenceEscalateResponderName(string monitorName)
		{
			return monitorName.Replace("Monitor", "Escalate");
		}

		// Token: 0x040014AE RID: 5294
		internal const string Monitor = "Monitor";

		// Token: 0x040014AF RID: 5295
		internal const string InferenceClassificationSLAMonitorName = "InferenceClassificationSLAMonitor";

		// Token: 0x040014B0 RID: 5296
		internal const string InferenceTrainingFailurePercentageMonitorName = "InferenceTrainingFailurePercentageMonitor";

		// Token: 0x040014B1 RID: 5297
		internal const string InferenceMailboxAssistantsCrashProbeName = "InferenceMailboxAssistantsCrashProbe";

		// Token: 0x040014B2 RID: 5298
		internal const string InferenceMailboxAssistantsCrashMonitorName = "InferenceMailboxAssistantsCrashMonitor";

		// Token: 0x040014B3 RID: 5299
		internal const string InferenceDeliveryCrashProbeName = "InferenceDeliveryCrashProbe";

		// Token: 0x040014B4 RID: 5300
		internal const string InferenceDeliveryCrashMonitorName = "InferenceDeliveryCrashMonitor";

		// Token: 0x040014B5 RID: 5301
		internal const string InferenceComponentDisabledProbeName = "InferenceComponentDisabledProbe";

		// Token: 0x040014B6 RID: 5302
		internal const string InferenceComponentDisabledMonitorName = "InferenceComponentDisabledMonitor";

		// Token: 0x040014B7 RID: 5303
		internal const string InferenceActivityLogSyntheticProbeName = "InferenceActivityLogSyntheticProbe";
	}
}
