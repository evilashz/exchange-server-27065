using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000165 RID: 357
	internal interface IConnectedAccountsNotificationManager : IDisposable
	{
		// Token: 0x06000D3B RID: 3387
		void SendLogonTriggeredSyncNowRequest();

		// Token: 0x06000D3C RID: 3388
		void SendRefreshButtonTriggeredSyncNowRequest();
	}
}
