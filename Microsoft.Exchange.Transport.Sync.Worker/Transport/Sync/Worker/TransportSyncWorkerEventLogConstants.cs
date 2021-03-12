using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Worker
{
	// Token: 0x02000238 RID: 568
	internal static class TransportSyncWorkerEventLogConstants
	{
		// Token: 0x04000ACC RID: 2764
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RegistryAccessDenied = new ExEventLog.EventTuple(3221488651U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000ACD RID: 2765
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PoisonSubscriptionDetected = new ExEventLog.EventTuple(3221488652U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000ACE RID: 2766
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SubscriptionCausedCrash = new ExEventLog.EventTuple(3221488653U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000ACF RID: 2767
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_HubInactivityForLongTime = new ExEventLog.EventTuple(2147746830U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AD0 RID: 2768
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DeltaSyncPartnerAuthenticationFailed = new ExEventLog.EventTuple(3221488656U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AD1 RID: 2769
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DeltaSyncServiceEndpointsLoadFailed = new ExEventLog.EventTuple(3221488658U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AD2 RID: 2770
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DeltaSyncEndpointUnreachable = new ExEventLog.EventTuple(2147746836U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AD3 RID: 2771
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DeltaSyncRequestFormatError = new ExEventLog.EventTuple(3221488661U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AD4 RID: 2772
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LinkedInProviderRequestTooManyItems = new ExEventLog.EventTuple(2147746838U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000239 RID: 569
		private enum Category : short
		{
			// Token: 0x04000AD6 RID: 2774
			SyncWorker = 1
		}

		// Token: 0x0200023A RID: 570
		internal enum Message : uint
		{
			// Token: 0x04000AD8 RID: 2776
			RegistryAccessDenied = 3221488651U,
			// Token: 0x04000AD9 RID: 2777
			PoisonSubscriptionDetected,
			// Token: 0x04000ADA RID: 2778
			SubscriptionCausedCrash,
			// Token: 0x04000ADB RID: 2779
			HubInactivityForLongTime = 2147746830U,
			// Token: 0x04000ADC RID: 2780
			DeltaSyncPartnerAuthenticationFailed = 3221488656U,
			// Token: 0x04000ADD RID: 2781
			DeltaSyncServiceEndpointsLoadFailed = 3221488658U,
			// Token: 0x04000ADE RID: 2782
			DeltaSyncEndpointUnreachable = 2147746836U,
			// Token: 0x04000ADF RID: 2783
			DeltaSyncRequestFormatError = 3221488661U,
			// Token: 0x04000AE0 RID: 2784
			LinkedInProviderRequestTooManyItems = 2147746838U
		}
	}
}
