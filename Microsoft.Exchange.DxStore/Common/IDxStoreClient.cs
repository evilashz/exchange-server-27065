using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000055 RID: 85
	public interface IDxStoreClient<T>
	{
		// Token: 0x06000339 RID: 825
		void Initialize(CachedChannelFactory<T> channelFactory, TimeSpan? operationTimeout = null);
	}
}
