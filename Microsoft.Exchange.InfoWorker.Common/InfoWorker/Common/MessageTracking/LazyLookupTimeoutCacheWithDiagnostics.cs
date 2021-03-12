using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000306 RID: 774
	internal abstract class LazyLookupTimeoutCacheWithDiagnostics<K, T> : LazyLookupTimeoutCache<K, T>
	{
		// Token: 0x060016F1 RID: 5873 RVA: 0x0006A642 File Offset: 0x00068842
		protected LazyLookupTimeoutCacheWithDiagnostics(int buckets, int maxBucketSize, bool shouldCallbackOnDispose, TimeSpan slidingLiveTime, TimeSpan absoluteLiveTime) : base(buckets, maxBucketSize, shouldCallbackOnDispose, slidingLiveTime, absoluteLiveTime)
		{
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0006A65E File Offset: 0x0006885E
		protected LazyLookupTimeoutCacheWithDiagnostics(int buckets, int maxBucketSize, bool shouldCallbackOnDispose, TimeSpan absoluteLiveTime) : base(buckets, maxBucketSize, shouldCallbackOnDispose, absoluteLiveTime)
		{
		}

		// Token: 0x060016F3 RID: 5875
		protected abstract T Create(K key, ref bool shouldAdd);

		// Token: 0x060016F4 RID: 5876 RVA: 0x0006A678 File Offset: 0x00068878
		protected sealed override T CreateOnCacheMiss(K key, ref bool shouldAdd)
		{
			T result = default(T);
			lock (this)
			{
				if (this.reprievedItems.TryGetValue(key, out result))
				{
					this.LogToCommonDiagnostics("ReprieveListUsed", key, null);
					shouldAdd = false;
					return result;
				}
			}
			this.LogToCommonDiagnostics("CacheMissStart", key, null);
			T result2 = this.Create(key, ref shouldAdd);
			this.LogToCommonDiagnostics("CachMissDone", key, shouldAdd ? null : new KeyValuePair<string, object>?(new KeyValuePair<string, object>("Cached", shouldAdd.ToString())));
			return result2;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0006A738 File Offset: 0x00068938
		protected override void HandleRemove(K key, T value, RemoveReason reason)
		{
			this.LogToCommonDiagnostics("KeyRemoved", key, new KeyValuePair<string, object>?(new KeyValuePair<string, object>("Reason", Names<RemoveReason>.Map[(int)reason])));
			bool flag = false;
			if (reason == RemoveReason.Expired)
			{
				lock (this)
				{
					if (!this.reprievedItems.ContainsKey(key))
					{
						this.reprievedItems.Add(key, value);
						flag = true;
					}
				}
			}
			if (flag)
			{
				using (ActivityContext.SuppressThreadScope())
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.BackgroundLookup), key);
				}
			}
			base.HandleRemove(key, value, reason);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0006A7F4 File Offset: 0x000689F4
		private void BackgroundLookup(object backGroundLookupArgObj)
		{
			Exception ex = null;
			K k = (K)((object)backGroundLookupArgObj);
			this.LogToCommonDiagnostics("AsyncCacheLookupStart", k, null);
			bool flag = false;
			T value = default(T);
			try
			{
				value = this.Create(k, ref flag);
			}
			catch (TrackingTransientException ex2)
			{
				ex = ex2;
			}
			catch (TrackingFatalException ex3)
			{
				ex = ex3;
			}
			catch (TransientException ex4)
			{
				ex = ex4;
			}
			catch (DataSourceOperationException ex5)
			{
				ex = ex5;
			}
			catch (DataValidationException ex6)
			{
				ex = ex6;
			}
			if (ex != null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<K, Exception>(this.GetHashCode(), "Exception in background lookup thread, Key={0}, Exception={1}", k, ex);
				KeyValuePair<string, object> value2 = new KeyValuePair<string, object>("Exception", ex.ToString());
				this.LogToCommonDiagnostics("AsyncCacheLookupException", k, new KeyValuePair<string, object>?(value2));
				lock (this)
				{
					this.reprievedItems.Remove(k);
				}
				return;
			}
			lock (this)
			{
				this.TryPerformAdd(k, value);
				this.reprievedItems.Remove(k);
			}
			this.LogToCommonDiagnostics("AsyncCacheLookupDone", k, flag ? null : new KeyValuePair<string, object>?(new KeyValuePair<string, object>("Cached", flag.ToString())));
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0006A980 File Offset: 0x00068B80
		private void LogToCommonDiagnostics(string eventToAdd, K key, KeyValuePair<string, object>? additionalPair)
		{
			if (ServerCache.Instance.WriteToStatsLogs && ServerCache.Instance.HostId == HostId.ECPApplicationPool)
			{
				KeyValuePair<string, object>[] eventData;
				if (additionalPair != null)
				{
					eventData = new KeyValuePair<string, object>[]
					{
						new KeyValuePair<string, object>("Event", eventToAdd),
						new KeyValuePair<string, object>("Type", base.GetType().Name),
						new KeyValuePair<string, object>("Key", key.ToString()),
						additionalPair.Value
					};
				}
				else
				{
					eventData = new KeyValuePair<string, object>[]
					{
						new KeyValuePair<string, object>("Event", eventToAdd),
						new KeyValuePair<string, object>("Type", base.GetType().Name),
						new KeyValuePair<string, object>("Key", key.ToString())
					};
				}
				CommonDiagnosticsLog.Instance.LogEvent(CommonDiagnosticsLog.Source.DeliveryReportsCache, eventData);
			}
		}

		// Token: 0x04000EAF RID: 3759
		internal Dictionary<K, T> reprievedItems = new Dictionary<K, T>(64);
	}
}
