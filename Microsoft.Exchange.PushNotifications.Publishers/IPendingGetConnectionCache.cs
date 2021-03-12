using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000AC RID: 172
	internal interface IPendingGetConnectionCache
	{
		// Token: 0x060005E4 RID: 1508
		bool TryGetConnection(string connectionId, out IPendingGetConnection connection);

		// Token: 0x060005E5 RID: 1509
		IPendingGetConnection AddOrGetConnection(string connectionId);
	}
}
