using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ThrottlingService;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Data.ThrottlingService
{
	// Token: 0x0200000C RID: 12
	internal sealed class TokenBucketMap<KeyT>
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000034A0 File Offset: 0x000016A0
		public TokenBucketMap()
		{
			this.data = new Dictionary<KeyT, DailyTokenBucket>(1024);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000034B8 File Offset: 0x000016B8
		public bool ObtainTokens(ObtainTokensRequest<KeyT> request)
		{
			if (this.cleanupTimer == null)
			{
				throw new InvalidOperationException("TokenBucketMap instance is not started.");
			}
			DateTime utcNow = DateTime.UtcNow;
			bool result;
			lock (this.data)
			{
				DailyTokenBucket dailyTokenBucket;
				if (!this.data.TryGetValue(request.MailboxGuid, out dailyTokenBucket))
				{
					dailyTokenBucket = new DailyTokenBucket();
					this.data.Add(request.MailboxGuid, dailyTokenBucket);
				}
				result = dailyTokenBucket.ObtainTokens<KeyT>(request, utcNow);
			}
			return result;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003544 File Offset: 0x00001744
		public void ExportData()
		{
			if (this.cleanupTimer == null)
			{
				throw new InvalidOperationException("TokenBucketMap instance is not started.");
			}
			lock (this.data)
			{
				TokenBucketMap<KeyT>.exportTracer.TraceDebug<int>(0L, "Tracing TokenBucketMap. Size = {0}", this.data.Count);
				foreach (KeyValuePair<KeyT, DailyTokenBucket> keyValuePair in this.data)
				{
					Trace trace = TokenBucketMap<KeyT>.exportTracer;
					long id = 0L;
					string formatString = "GUID = {0}, {1}";
					KeyT key = keyValuePair.Key;
					trace.TraceDebug<string, string>(id, formatString, key.ToString(), keyValuePair.Value.ToString());
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000361C File Offset: 0x0000181C
		public void Start()
		{
			TokenBucketMap<KeyT>.tracer.TraceDebug(0L, "Starting TokenBucketMap.");
			lock (this.data)
			{
				if (this.cleanupTimer != null)
				{
					throw new InvalidOperationException("Timer in TokenBucketMap already started");
				}
				this.cleanupTimer = new GuardedTimer(new TimerCallback(this.RemoveExpiredEntries), null, TokenBucketMap<KeyT>.CleanupInterval, TokenBucketMap<KeyT>.CleanupInterval);
			}
			TokenBucketMap<KeyT>.tracer.TraceDebug(0L, "TokenBucketMap started successfully.");
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000036B0 File Offset: 0x000018B0
		public void Stop()
		{
			TokenBucketMap<KeyT>.tracer.TraceDebug(0L, "Stopping TokenBucketMap.");
			lock (this.data)
			{
				if (this.cleanupTimer != null)
				{
					this.cleanupTimer.Dispose(true);
					this.cleanupTimer = null;
				}
			}
			TokenBucketMap<KeyT>.tracer.TraceDebug(0L, "TokenBucketMap stopped successfully.");
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003728 File Offset: 0x00001928
		private void RemoveExpiredEntries(object state)
		{
			TokenBucketMap<KeyT>.tracer.TraceDebug(0L, "Timer delegate invoked.");
			lock (this.data)
			{
				if (this.cleanupTimer == null)
				{
					TokenBucketMap<KeyT>.tracer.TraceDebug(0L, "Leaving immediately because TokenBucketMap is stopping.");
					return;
				}
				DateTime utcNow = DateTime.UtcNow;
				List<KeyT> list = new List<KeyT>(this.data.Count / 4);
				foreach (KeyValuePair<KeyT, DailyTokenBucket> keyValuePair in this.data)
				{
					if (keyValuePair.Value.IsExpired(utcNow))
					{
						ThrottlingServiceLog.LogTokenExpiry(keyValuePair.Key, keyValuePair.Value);
						list.Add(keyValuePair.Key);
					}
				}
				TokenBucketMap<KeyT>.tracer.TraceDebug<int, int>(0L, "Found {0} expired entries out of {1}; removing them.", list.Count, this.data.Count);
				foreach (KeyT key in list)
				{
					this.data.Remove(key);
				}
			}
			TokenBucketMap<KeyT>.tracer.TraceDebug(0L, "Finished removing expired entries.");
		}

		// Token: 0x0400003F RID: 63
		private const int InitialCapacity = 1024;

		// Token: 0x04000040 RID: 64
		public static readonly TimeSpan CleanupInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000041 RID: 65
		private static Trace tracer = ExTraceGlobals.ThrottlingServiceTracer;

		// Token: 0x04000042 RID: 66
		private static Trace exportTracer = ExTraceGlobals.ExportTracer;

		// Token: 0x04000043 RID: 67
		private Dictionary<KeyT, DailyTokenBucket> data;

		// Token: 0x04000044 RID: 68
		private GuardedTimer cleanupTimer;
	}
}
