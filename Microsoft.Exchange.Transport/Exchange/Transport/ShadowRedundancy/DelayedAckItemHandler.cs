using System;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000367 RID: 871
	// (Invoke) Token: 0x060025B0 RID: 9648
	internal delegate bool DelayedAckItemHandler(object state, DelayedAckCompletionStatus status, TimeSpan delay, string context);
}
