using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.EventAssistants
{
	// Token: 0x0200017A RID: 378
	public static class EventAssistantsMonitoringStrings
	{
		// Token: 0x04000854 RID: 2132
		public const string EventAssistantsServiceProbeName = "EventAssistantsServiceProbe";

		// Token: 0x04000855 RID: 2133
		public const string EventAssistantsServiceMonitorName = "EventAssistantsServiceMonitor";

		// Token: 0x04000856 RID: 2134
		public const string EventAssistantsServiceRestartResponderName = "EventAssistantsServiceRestart";

		// Token: 0x04000857 RID: 2135
		public const string EventAssistantsServiceEscalateResponderName = "EventAssistantsServiceEscalate";

		// Token: 0x04000858 RID: 2136
		public const string EventAssistantsProcessRepeatedlyCrashingProbeName = "EventAssistantsProcessRepeatedlyCrashingProbe";

		// Token: 0x04000859 RID: 2137
		public const string EventAssistantsProcessRepeatedlyCrashingMonitorName = "EventAssistantsProcessRepeatedlyCrashingMonitor";

		// Token: 0x0400085A RID: 2138
		public const string EventAssistantsProcessRepeatedlyCrashingEscalateResponderName = "EventAssistantsProcessRepeatedlyCrashingEscalate";

		// Token: 0x0400085B RID: 2139
		public const string MailboxAssistantsWatermarksProbeName = "MailboxAssistantsWatermarksProbe";

		// Token: 0x0400085C RID: 2140
		public const string MailboxAssistantsWatermarksMonitorName = "MailboxAssistantsWatermarksMonitor";

		// Token: 0x0400085D RID: 2141
		public const string MailboxAssistantsWatermarksEscalationProcessingMonitorName = "MailboxAssistantsWatermarksEscalationProcessingMonitor";

		// Token: 0x0400085E RID: 2142
		public const string MailboxAssistantsWatermarksWatsonResponderName = "MailboxAssistantsWatermarksWatsonResponder";

		// Token: 0x0400085F RID: 2143
		public const string MailboxAssistantsWatermarksRestartResponderName = "MailboxAssistantsWatermarksRestart";

		// Token: 0x04000860 RID: 2144
		public const string MailboxAssistantsWatermarksEscalationNotificationResponderName = "MailboxAssistantsWatermarksEscalationNotification";

		// Token: 0x04000861 RID: 2145
		public const string MailboxAssistantsWatermarksEscalateResponderName = "MailboxAssistantsWatermarksEscalate";

		// Token: 0x04000862 RID: 2146
		public const string MailSubmissionWatermarksProbeName = "MailSubmissionWatermarksProbe";

		// Token: 0x04000863 RID: 2147
		public const string MailSubmissionWatermarksMonitorName = "MailSubmissionWatermarksMonitor";

		// Token: 0x04000864 RID: 2148
		public const string MailSubmissionWatermarksRestartResponderName = "MailSubmissionWatermarksRestart";

		// Token: 0x04000865 RID: 2149
		public const string MailSubmissionWatermarksEscalateResponderName = "MailSubmissionWatermarksEscalate";

		// Token: 0x04000866 RID: 2150
		public const string InvokeMonitoringProbeCommand = "Invoke-MonitoringProbe -Identity '{0}\\{1}\\{{Probe.StateAttribute1}}' -Server {2}";

		// Token: 0x04000867 RID: 2151
		public const string GetAllUnhealthyMonitors = "Get-ServerHealth -Identity '{0}' -HealthSet '{1}' | ?{{$_.Name -match '{2}' -and $_.AlertValue -ne 'Healthy'}}";
	}
}
