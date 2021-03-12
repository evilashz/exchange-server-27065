using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200037E RID: 894
	public class ShadowRedundancyEventLogger
	{
		// Token: 0x060026F6 RID: 9974 RVA: 0x0009645C File Offset: 0x0009465C
		internal virtual void LogShadowRedundancyMessagesResubmitted(int resubmittedCount, string serverFqdn, ResubmitReason resubmitReason)
		{
			ShadowRedundancyEventLogger.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ShadowRedundancyMessagesResubmitted, null, new object[]
			{
				resubmittedCount,
				serverFqdn,
				resubmitReason
			});
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x00096498 File Offset: 0x00094698
		internal virtual void LogShadowRedundancyMessageResubmitSuppressed(int messageCount, string serverFqdn, string reason)
		{
			ShadowRedundancyEventLogger.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ShadowRedundancyMessageResubmitSuppressed, null, new object[]
			{
				messageCount,
				serverFqdn,
				reason
			});
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000964D0 File Offset: 0x000946D0
		internal virtual void LogPrimaryServerDatabaseStateChanged(string serverFqdn, string oldState, string newState)
		{
			ShadowRedundancyEventLogger.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ShadowRedundancyPrimaryServerDatabaseStateChanged, null, new object[]
			{
				serverFqdn,
				oldState,
				newState
			});
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x00096504 File Offset: 0x00094704
		internal virtual void LogPrimaryServerHeartbeatFailed(string serverFqdn)
		{
			ShadowRedundancyEventLogger.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ShadowRedundancyPrimaryServerHeartbeatFailed, null, new object[]
			{
				serverFqdn
			});
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0009652E File Offset: 0x0009472E
		internal virtual void LogMessageDeferredDueToShadowFailure()
		{
			ShadowRedundancyEventLogger.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ShadowRedundancyMessageDeferredDueToShadowFailure, null, new object[0]);
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x00096548 File Offset: 0x00094748
		internal virtual void LogHeartbeatForcedReset(string primaryServer, TimeSpan cutoffTime)
		{
			ShadowRedundancyEventLogger.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ShadowRedundancyForcedHeartbeatReset, null, new object[]
			{
				primaryServer,
				cutoffTime
			});
		}

		// Token: 0x040013D5 RID: 5077
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.ShadowRedundancyTracer.Category, TransportEventLog.GetEventSource());
	}
}
