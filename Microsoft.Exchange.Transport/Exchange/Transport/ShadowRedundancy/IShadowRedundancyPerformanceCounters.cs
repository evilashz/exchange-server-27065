using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000381 RID: 897
	internal interface IShadowRedundancyPerformanceCounters
	{
		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06002710 RID: 10000
		long RedundantMessageDiscardEvents { get; }

		// Token: 0x06002711 RID: 10001
		bool IsValid(ShadowRedundancyCounterId shadowRedundancyCounterName);

		// Token: 0x06002712 RID: 10002
		void IncrementCounter(ShadowRedundancyCounterId shadowRedundancyCounterName);

		// Token: 0x06002713 RID: 10003
		void IncrementCounterBy(ShadowRedundancyCounterId shadowRedundancyCounterName, long value);

		// Token: 0x06002714 RID: 10004
		void DecrementCounter(ShadowRedundancyCounterId shadowRedundancyCounterName);

		// Token: 0x06002715 RID: 10005
		void DecrementCounterBy(ShadowRedundancyCounterId shadowRedundancyCounterName, long value);

		// Token: 0x06002716 RID: 10006
		void DelayedAckExpired(long messageCount);

		// Token: 0x06002717 RID: 10007
		void DelayedAckDeliveredAfterExpiry(long messageCount);

		// Token: 0x06002718 RID: 10008
		void UpdateShadowQueueLength(string hostname, int changeAmount);

		// Token: 0x06002719 RID: 10009
		ITimerCounter ShadowSelectionLatencyCounter();

		// Token: 0x0600271A RID: 10010
		ITimerCounter ShadowNegotiationLatencyCounter();

		// Token: 0x0600271B RID: 10011
		IAverageCounter ShadowSuccessfulNegotiationLatencyCounter();

		// Token: 0x0600271C RID: 10012
		ITimerCounter ShadowHeartbeatLatencyCounter(string hostname);

		// Token: 0x0600271D RID: 10013
		void ShadowFailure(string hostname);

		// Token: 0x0600271E RID: 10014
		void HeartbeatFailure(string hostname);

		// Token: 0x0600271F RID: 10015
		void TrackMessageMadeRedundant(bool success);

		// Token: 0x06002720 RID: 10016
		void SubmitMessagesFromShadowQueue(string hostname, int count);

		// Token: 0x06002721 RID: 10017
		void SmtpTimeout();

		// Token: 0x06002722 RID: 10018
		void SmtpClientFailureAfterAccept();

		// Token: 0x06002723 RID: 10019
		void MessageShadowed(string shadowServer, bool remote);
	}
}
