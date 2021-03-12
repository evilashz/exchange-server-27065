using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B42 RID: 2882
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PerTenantQueryController<T>
	{
		// Token: 0x0600681B RID: 26651 RVA: 0x001B8693 File Offset: 0x001B6893
		public PerTenantQueryController(IEqualityComparer<T> comparer)
		{
			this.dictionary = new Dictionary<T, PerTenantQueryController<T>.SynchronizedQueue>(comparer);
		}

		// Token: 0x0600681C RID: 26652 RVA: 0x001B86B4 File Offset: 0x001B68B4
		public bool EnqueueResult(T tenantId, RightsManagementAsyncResult result)
		{
			PerTenantQueryController<T>.SynchronizedQueue tenantQueue = this.GetTenantQueue(tenantId);
			return tenantQueue.Enqueue(result);
		}

		// Token: 0x0600681D RID: 26653 RVA: 0x001B8700 File Offset: 0x001B6900
		public void InvokeCallbacks(T tenantId, object result)
		{
			IEnumerable<RightsManagementAsyncResult> enumerable;
			lock (this.syncRoot)
			{
				PerTenantQueryController<T>.SynchronizedQueue tenantQueue = this.GetTenantQueue(tenantId);
				enumerable = tenantQueue.DequeueAll();
				this.RemoveTenantQueue(tenantId);
			}
			foreach (RightsManagementAsyncResult state in enumerable)
			{
				ThreadPool.QueueUserWorkItem(delegate(object o)
				{
					RightsManagementAsyncResult rightsManagementAsyncResult = (RightsManagementAsyncResult)o;
					rightsManagementAsyncResult.AddBreadCrumb(Constants.State.PerTenantQueryControllerInvokeCallback);
					rightsManagementAsyncResult.InvokeCallback(result);
				}, state);
			}
		}

		// Token: 0x0600681E RID: 26654 RVA: 0x001B87B8 File Offset: 0x001B69B8
		private PerTenantQueryController<T>.SynchronizedQueue GetTenantQueue(T tenantId)
		{
			PerTenantQueryController<T>.SynchronizedQueue synchronizedQueue;
			lock (this.syncRoot)
			{
				if (this.dictionary.TryGetValue(tenantId, out synchronizedQueue))
				{
					return synchronizedQueue;
				}
				synchronizedQueue = new PerTenantQueryController<T>.SynchronizedQueue();
				this.dictionary.Add(tenantId, synchronizedQueue);
			}
			ExTraceGlobals.RightsManagementTracer.TraceDebug<T>((long)this.GetHashCode(), "Created a new queue for tenant {0}", tenantId);
			return synchronizedQueue;
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x001B8834 File Offset: 0x001B6A34
		private void RemoveTenantQueue(T tenantId)
		{
			lock (this.syncRoot)
			{
				if (!this.dictionary.ContainsKey(tenantId))
				{
					return;
				}
				this.dictionary.Remove(tenantId);
			}
			ExTraceGlobals.RightsManagementTracer.TraceDebug<T>((long)this.GetHashCode(), "Removed queue for tenant {0}", tenantId);
		}

		// Token: 0x04003B3C RID: 15164
		private readonly object syncRoot = new object();

		// Token: 0x04003B3D RID: 15165
		private readonly Dictionary<T, PerTenantQueryController<T>.SynchronizedQueue> dictionary;

		// Token: 0x02000B43 RID: 2883
		private sealed class SynchronizedQueue
		{
			// Token: 0x06006820 RID: 26656 RVA: 0x001B88A4 File Offset: 0x001B6AA4
			public SynchronizedQueue()
			{
				this.queue = new Queue<RightsManagementAsyncResult>();
			}

			// Token: 0x06006821 RID: 26657 RVA: 0x001B88C4 File Offset: 0x001B6AC4
			public bool Enqueue(RightsManagementAsyncResult asyncResult)
			{
				int count;
				lock (this.syncRoot)
				{
					count = this.queue.Count;
					this.queue.Enqueue(asyncResult);
				}
				ExTraceGlobals.RightsManagementTracer.TraceDebug<int>((long)this.GetHashCode(), "Number of elements in the queue : {0}", count + 1);
				return count == 0;
			}

			// Token: 0x06006822 RID: 26658 RVA: 0x001B8934 File Offset: 0x001B6B34
			public IEnumerable<RightsManagementAsyncResult> DequeueAll()
			{
				Queue<RightsManagementAsyncResult> queue = null;
				lock (this.syncRoot)
				{
					queue = this.queue;
					this.queue = new Queue<RightsManagementAsyncResult>();
				}
				ExTraceGlobals.RightsManagementTracer.TraceDebug<int>((long)this.GetHashCode(), "Dequeued elements from the queue. Number of entries: {0}", queue.Count);
				return queue;
			}

			// Token: 0x04003B3E RID: 15166
			private readonly object syncRoot = new object();

			// Token: 0x04003B3F RID: 15167
			private Queue<RightsManagementAsyncResult> queue;
		}
	}
}
