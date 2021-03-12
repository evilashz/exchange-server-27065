using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect
{
	// Token: 0x020000D3 RID: 211
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConnectSubscription : ISyncWorkerData
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060005FA RID: 1530
		string AccessTokenInClearText { get; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005FB RID: 1531
		string AccessTokenSecretInClearText { get; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060005FC RID: 1532
		string UserId { get; }
	}
}
