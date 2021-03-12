using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002C9 RID: 713
	// (Invoke) Token: 0x060013DE RID: 5086
	internal delegate bool TryEnqueueNextHopDelegate(MessageTrackingLogEntry terminalEvent, TrackingContext context, Queue<MailItemTracker> remainingTrackers);
}
