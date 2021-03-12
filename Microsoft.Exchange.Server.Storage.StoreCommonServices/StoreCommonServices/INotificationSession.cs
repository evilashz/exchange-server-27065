using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000D8 RID: 216
	public interface INotificationSession
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000897 RID: 2199
		int RpcContext { get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000898 RID: 2200
		Guid UserGuid { get; }
	}
}
