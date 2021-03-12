using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x020004F3 RID: 1267
	internal static class TransportSyncStrings
	{
		// Token: 0x040016F7 RID: 5879
		internal const string DatabaseConsistencyProbeName = "TransportSyncManager.DatabaseConsistency.Probe";

		// Token: 0x040016F8 RID: 5880
		internal const string DatabaseConsistencyMonitorName = "TransportSyncManager.DatabaseConsistency.Monitor";

		// Token: 0x040016F9 RID: 5881
		internal const string DatabaseConsistencyRestartResponderName = "TransportSyncManager.DatabaseConsistency.Restart";

		// Token: 0x040016FA RID: 5882
		internal const string DatabaseConsistencyEscalateResponderName = "TransportSyncManager.DatabaseConsistency.Escalate";

		// Token: 0x040016FB RID: 5883
		internal const string ServiceAvailabilityProbeName = "TransportSyncManager.Started.Probe";

		// Token: 0x040016FC RID: 5884
		internal const string ServiceAvailabilityMonitorName = "TransportSyncManager.Started.Monitor";

		// Token: 0x040016FD RID: 5885
		internal const string ServiceAvailabilityEscalateResponderName = "TransportSyncManager.Service.Escalate";

		// Token: 0x040016FE RID: 5886
		internal const string ServiceAvailabilityRestartResponderName = "TransportSyncManager.Service.Restart";

		// Token: 0x040016FF RID: 5887
		internal const string SubscriptionSlaMissedMonitorName = "TransportSync.NotDispatchingWithin1HourSla.Monitor";

		// Token: 0x04001700 RID: 5888
		internal const string SubscriptionSlaMissedResponderName = "TransportSync.NotDispatchingWithin1HourSla.Escalate";

		// Token: 0x04001701 RID: 5889
		internal const string DeltaSyncEndpointUnreachableMonitorName = "DeltaSync.EndpointUnreachable.Monitor";

		// Token: 0x04001702 RID: 5890
		internal const string DeltaSyncEndpointUnreachableResponderName = "DeltaSync.EndpointUnreachable.Escalate";

		// Token: 0x04001703 RID: 5891
		internal const string DeltaSyncPartnerAuthenticationFailedMonitorName = "DeltaSync.PartnerAuthentication.Failed.Monitor";

		// Token: 0x04001704 RID: 5892
		internal const string DeltaSyncPartnerAuthenticationFailedResponderName = "DeltaSync.PartnerAuthentication.Failed.Escalate";

		// Token: 0x04001705 RID: 5893
		internal const string DeltaSyncServiceEndpointsLoadFailedMonitorName = "DeltaSync.ServiceEndpointsLoad.Failed.Monitor";

		// Token: 0x04001706 RID: 5894
		internal const string DeltaSyncServiceEndpointsLoadFailedResponderName = "DeltaSync.ServiceEndpointsLoad.Failed.Escalate";

		// Token: 0x04001707 RID: 5895
		internal const string RegistryAccessDeniedMonitorName = "Registry.AccessDenied.Monitor";

		// Token: 0x04001708 RID: 5896
		internal const string RegistryAccessDeniedResponderName = "Registry.AccessDenied.Escalate";
	}
}
