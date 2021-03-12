using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200007D RID: 125
	public interface ICache
	{
		// Token: 0x06000499 RID: 1177
		bool FlushAllDirtyEntries(Context context);
	}
}
