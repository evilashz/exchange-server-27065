using System;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000A7 RID: 167
	internal interface IDirectoryCacheProvider
	{
		// Token: 0x06000934 RID: 2356
		TObject Get<TObject>(DirectoryCacheRequest cacheRequest) where TObject : ADRawEntry, new();

		// Token: 0x06000935 RID: 2357
		void Put(AddDirectoryCacheRequest cacheRequest);

		// Token: 0x06000936 RID: 2358
		void Remove(RemoveDirectoryCacheRequest cacheRequest);

		// Token: 0x06000937 RID: 2359
		void TestOnlyResetAllCaches();

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000938 RID: 2360
		ADCacheResultState ResultState { get; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000939 RID: 2361
		bool IsNewProxyObject { get; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600093A RID: 2362
		int RetryCount { get; }
	}
}
