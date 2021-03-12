using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000F8 RID: 248
	public interface IPendingRequestNotifier
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600083C RID: 2108
		// (remove) Token: 0x0600083D RID: 2109
		event DataAvailableEventHandler DataAvailable;

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600083E RID: 2110
		bool ShouldThrottle { get; }

		// Token: 0x0600083F RID: 2111
		string ReadDataAndResetState();

		// Token: 0x06000840 RID: 2112
		void ConnectionAliveTimer();
	}
}
