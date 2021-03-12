using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001C7 RID: 455
	internal enum PusherEventType
	{
		// Token: 0x04000990 RID: 2448
		Distribute,
		// Token: 0x04000991 RID: 2449
		Push,
		// Token: 0x04000992 RID: 2450
		PushFailed,
		// Token: 0x04000993 RID: 2451
		ConcurrentLimit,
		// Token: 0x04000994 RID: 2452
		PusherThreadStart,
		// Token: 0x04000995 RID: 2453
		PusherThreadCleanup,
		// Token: 0x04000996 RID: 2454
		PusherThreadEnd
	}
}
