using System;

namespace Microsoft.Exchange.Collections.TimeoutCache
{
	// Token: 0x020006A4 RID: 1700
	// (Invoke) Token: 0x06001F61 RID: 8033
	internal delegate bool HandleBeforeAdd<K, T>(K key, T value, TimeoutCacheBucket<K, T> bucket);
}
