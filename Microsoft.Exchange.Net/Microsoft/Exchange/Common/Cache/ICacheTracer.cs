using System;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x02000677 RID: 1655
	internal interface ICacheTracer<K>
	{
		// Token: 0x06001E08 RID: 7688
		void Accessed(K key, CachableItem value, AccessStatus accessStatus, DateTime timestamp);

		// Token: 0x06001E09 RID: 7689
		void Flushed(long cacheSize, DateTime timestamp);

		// Token: 0x06001E0A RID: 7690
		void ItemAdded(K key, CachableItem value, DateTime timestamp);

		// Token: 0x06001E0B RID: 7691
		void ItemRemoved(K key, CachableItem value, CacheItemRemovedReason removeReason, DateTime timestamp);

		// Token: 0x06001E0C RID: 7692
		void TraceException(string details, Exception exception);
	}
}
