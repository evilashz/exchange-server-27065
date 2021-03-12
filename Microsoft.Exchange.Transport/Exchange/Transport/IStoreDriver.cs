using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000025 RID: 37
	internal interface IStoreDriver
	{
		// Token: 0x060000C7 RID: 199
		void Start(bool initiallyPaused);

		// Token: 0x060000C8 RID: 200
		void Retire();

		// Token: 0x060000C9 RID: 201
		void Stop();

		// Token: 0x060000CA RID: 202
		void Pause();

		// Token: 0x060000CB RID: 203
		void Continue();

		// Token: 0x060000CC RID: 204
		void DoLocalDelivery(NextHopConnection connection);

		// Token: 0x060000CD RID: 205
		void ExpireOldSubmissionConnections();

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CE RID: 206
		string CurrentState { get; }
	}
}
