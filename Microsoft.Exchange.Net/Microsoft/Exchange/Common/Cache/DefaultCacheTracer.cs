using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x0200067F RID: 1663
	internal class DefaultCacheTracer<TCacheKey> : ICacheTracer<TCacheKey> where TCacheKey : class
	{
		// Token: 0x06001E23 RID: 7715 RVA: 0x000374DD File Offset: 0x000356DD
		public DefaultCacheTracer(ITracer tracer, string cacheName)
		{
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			ArgumentValidator.ThrowIfNull("cacheName", cacheName);
			this.cacheName = cacheName;
			this.tracer = tracer;
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0003750C File Offset: 0x0003570C
		public static string GetAccessStatusString(AccessStatus status)
		{
			switch (status)
			{
			case AccessStatus.Hit:
				return "Hit";
			case AccessStatus.Miss:
				return "Miss";
			default:
				throw new ArgumentOutOfRangeException("status", status, "Unexpected value of status parameter. The GetAccessStatusString() method is out of sync with the AccessStatus enum definition.");
			}
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0003754C File Offset: 0x0003574C
		public static string GetRemovedReasonString(CacheItemRemovedReason reason)
		{
			switch (reason)
			{
			case CacheItemRemovedReason.Removed:
				return "Removed";
			case CacheItemRemovedReason.Expired:
				return "Expired";
			case CacheItemRemovedReason.Scavenged:
				return "Scavenged";
			case CacheItemRemovedReason.OverWritten:
				return "OverWritten";
			case CacheItemRemovedReason.Clear:
				return "Clear";
			default:
				throw new ArgumentOutOfRangeException("reason", reason, "Unexpected value of reason parameter. The GetRemovedReasonString() method is out of sync with the CacheItemRemovedReason enum definition.");
			}
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x000375AC File Offset: 0x000357AC
		public void Flushed(long cacheSize, DateTime timestamp)
		{
			this.Trace<long>(timestamp, DefaultCacheTracer<TCacheKey>.CacheOperation.Flushed, default(TCacheKey), 0L, cacheSize);
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000375D0 File Offset: 0x000357D0
		public void Accessed(TCacheKey key, CachableItem value, AccessStatus accessStatus, DateTime timestamp)
		{
			long itemSize = (value != null) ? value.ItemSize : 0L;
			this.Trace<string>(timestamp, DefaultCacheTracer<TCacheKey>.CacheOperation.Accessed, key, itemSize, DefaultCacheTracer<TCacheKey>.GetAccessStatusString(accessStatus));
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x000375FC File Offset: 0x000357FC
		public void ItemAdded(TCacheKey key, CachableItem value, DateTime timestamp)
		{
			long itemSize = (value != null) ? value.ItemSize : 0L;
			this.Trace<string>(timestamp, DefaultCacheTracer<TCacheKey>.CacheOperation.Added, key, itemSize, string.Empty);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00037628 File Offset: 0x00035828
		public void ItemRemoved(TCacheKey key, CachableItem value, CacheItemRemovedReason removeReason, DateTime timestamp)
		{
			long itemSize = (value != null) ? value.ItemSize : 0L;
			this.Trace<string>(timestamp, DefaultCacheTracer<TCacheKey>.CacheOperation.Removed, key, itemSize, DefaultCacheTracer<TCacheKey>.GetRemovedReasonString(removeReason));
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00037654 File Offset: 0x00035854
		public void TraceException(string details, Exception exception)
		{
			this.tracer.TraceError((long)this.GetHashCode(), string.Format("{0}: {1}, {2}, Exception: {3}", new object[]
			{
				ExDateTime.UtcNow,
				this.cacheName,
				details,
				exception
			}));
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x000376A3 File Offset: 0x000358A3
		protected virtual string GetKeyString(TCacheKey key)
		{
			if (key != null)
			{
				return key.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x000376C0 File Offset: 0x000358C0
		private static string GetCacheOperationString(DefaultCacheTracer<TCacheKey>.CacheOperation operation)
		{
			switch (operation)
			{
			case DefaultCacheTracer<TCacheKey>.CacheOperation.Accessed:
				return "Accessed";
			case DefaultCacheTracer<TCacheKey>.CacheOperation.Added:
				return "Added";
			case DefaultCacheTracer<TCacheKey>.CacheOperation.Flushed:
				return "Flushed";
			case DefaultCacheTracer<TCacheKey>.CacheOperation.Removed:
				return "Removed";
			default:
				throw new ArgumentOutOfRangeException("operation", operation, "Unexpected value of operation parameter. The GetCacheOperationString() method is out of sync with the CacheOperation enum definition.");
			}
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00037714 File Offset: 0x00035914
		private void Trace<TRelatedInfo>(DateTime timestamp, DefaultCacheTracer<TCacheKey>.CacheOperation cacheOperation, TCacheKey key, long itemSize, TRelatedInfo relatedInfo)
		{
			this.tracer.TraceDebug((long)this.GetHashCode(), "{0}: {1}, {2}, {3}, {4}, {5}", new object[]
			{
				timestamp,
				this.cacheName,
				DefaultCacheTracer<TCacheKey>.GetCacheOperationString(cacheOperation),
				this.GetKeyString(key),
				itemSize,
				relatedInfo
			});
		}

		// Token: 0x04001E32 RID: 7730
		private readonly string cacheName;

		// Token: 0x04001E33 RID: 7731
		private readonly ITracer tracer;

		// Token: 0x02000680 RID: 1664
		private enum CacheOperation
		{
			// Token: 0x04001E35 RID: 7733
			Accessed,
			// Token: 0x04001E36 RID: 7734
			Added,
			// Token: 0x04001E37 RID: 7735
			Flushed,
			// Token: 0x04001E38 RID: 7736
			Removed
		}
	}
}
