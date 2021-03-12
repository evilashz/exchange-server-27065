using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000066 RID: 102
	internal class CallInfoCache
	{
		// Token: 0x1700012D RID: 301
		internal UMCallInfoEx this[Guid key]
		{
			get
			{
				CallInfoCache.CacheEntry cacheEntry = null;
				lock (this.cache)
				{
					this.cache.TryGetValue(key, out cacheEntry);
				}
				if (cacheEntry == null)
				{
					return null;
				}
				return cacheEntry.CallInfo;
			}
			set
			{
				lock (this.cache)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoipPlatformTracer, this, "Adding a new entry to the CallInfoCache: {0}={1}/{2}.", new object[]
					{
						key,
						value.CallState,
						value.EventCause
					});
					this.cache[key] = new CallInfoCache.CacheEntry(key, value, Constants.CallInfoExpirationTime, new TimerCallback(this.RemoveCacheEntry));
				}
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00015848 File Offset: 0x00013A48
		private void RemoveCacheEntry(object state)
		{
			CallInfoCache.CacheEntry cacheEntry = state as CallInfoCache.CacheEntry;
			lock (this.cache)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoipPlatformTracer, this, "Removing {0} from CallInfoCache.", new object[]
				{
					cacheEntry.Key
				});
				this.cache.Remove(cacheEntry.Key);
				cacheEntry.Dispose();
			}
		}

		// Token: 0x04000199 RID: 409
		private Dictionary<Guid, CallInfoCache.CacheEntry> cache = new Dictionary<Guid, CallInfoCache.CacheEntry>();

		// Token: 0x02000067 RID: 103
		private class CacheEntry : DisposableBase
		{
			// Token: 0x06000479 RID: 1145 RVA: 0x000158DB File Offset: 0x00013ADB
			internal CacheEntry(Guid key, UMCallInfoEx callInfo, TimeSpan expirationTime, TimerCallback cacheEntryExpired)
			{
				this.key = key;
				this.callInfo = callInfo;
				this.expirationTimer = new Timer(cacheEntryExpired, this, expirationTime, TimeSpan.Zero);
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x0600047A RID: 1146 RVA: 0x00015905 File Offset: 0x00013B05
			public Guid Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001590D File Offset: 0x00013B0D
			public UMCallInfoEx CallInfo
			{
				get
				{
					return this.callInfo;
				}
			}

			// Token: 0x0600047C RID: 1148 RVA: 0x00015915 File Offset: 0x00013B15
			protected override void InternalDispose(bool disposing)
			{
				if (disposing && this.expirationTimer != null)
				{
					this.expirationTimer.Dispose();
					this.expirationTimer = null;
				}
			}

			// Token: 0x0600047D RID: 1149 RVA: 0x00015934 File Offset: 0x00013B34
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<CallInfoCache.CacheEntry>(this);
			}

			// Token: 0x0400019A RID: 410
			private Guid key;

			// Token: 0x0400019B RID: 411
			private UMCallInfoEx callInfo;

			// Token: 0x0400019C RID: 412
			private Timer expirationTimer;
		}
	}
}
