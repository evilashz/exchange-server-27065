using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Win32;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200007D RID: 125
	internal sealed class Throttle : Base, IThreadPool
	{
		// Token: 0x06000397 RID: 919 RVA: 0x000116DF File Offset: 0x0000F8DF
		public Throttle(string name, int initialThrottleValue) : this(null, name, initialThrottleValue, Throttle.unThrottledThreadPool)
		{
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000116EF File Offset: 0x0000F8EF
		public static Throttle CreateParentThrottle(string name, int initialThrottleValue)
		{
			return new Throttle(Configuration.ParametersRegistryKeyPath, name, initialThrottleValue, Throttle.unThrottledThreadPool);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00011702 File Offset: 0x0000F902
		public Throttle(string name, int initialThrottleValue, Throttle baseThreadPool) : this(Configuration.ParametersRegistryKeyPath, name, initialThrottleValue, baseThreadPool)
		{
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00011714 File Offset: 0x0000F914
		private Throttle(string registryKeyPath, string name, int initialThrottleValue, IThreadPool baseThreadPool)
		{
			this.name = name;
			this.baseThreadPool = baseThreadPool;
			this.currentThrottle = Throttle.GetInitialThrottleValue(registryKeyPath, name, initialThrottleValue);
			this.openThrottle = this.currentThrottle;
			base.TracePfd("PFD AIS {0} {1}: constructed", new object[]
			{
				24151,
				this
			});
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00011795 File Offset: 0x0000F995
		public int ThrottleValue
		{
			get
			{
				return this.currentThrottle;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0001179D File Offset: 0x0000F99D
		public int OpenThrottleValue
		{
			get
			{
				return this.openThrottle;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600039D RID: 925 RVA: 0x000117A5 File Offset: 0x0000F9A5
		public bool IsOverThrottle
		{
			get
			{
				return this.activeWorkItems > this.currentThrottle || (this.baseThreadPool is Throttle && ((Throttle)this.baseThreadPool).IsOverThrottle);
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000117D8 File Offset: 0x0000F9D8
		public void QueueUserWorkItem(WaitCallback callback, object state)
		{
			lock (this.queue)
			{
				this.queue.Enqueue(new Throttle.WorkItemState(callback, state));
				ExTraceGlobals.ThrottleTracer.TraceDebug<Throttle>((long)this.GetHashCode(), "{0}: Enqueued new workitem", this);
				this.TraceState();
				this.AddPendingWorkItemsIfNecessary();
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00011848 File Offset: 0x0000FA48
		public void SetThrottle(int newThrottleValue)
		{
			lock (this.queue)
			{
				this.currentThrottle = newThrottleValue;
				base.TracePfd("PFD AIS {0} {1}: New throttle value: {2}", new object[]
				{
					28247,
					this,
					this.currentThrottle
				});
				this.TraceState();
				this.AddPendingWorkItemsIfNecessary();
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000118C8 File Offset: 0x0000FAC8
		public void OpenThrottle()
		{
			this.SetThrottle(this.openThrottle);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000118D6 File Offset: 0x0000FAD6
		public void CloseThrottle()
		{
			this.SetThrottle(0);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000118E0 File Offset: 0x0000FAE0
		public override void ExportToQueryableObject(QueryableObject queryableObject)
		{
			base.ExportToQueryableObject(queryableObject);
			QueryableThrottle queryableThrottle = queryableObject as QueryableThrottle;
			if (queryableThrottle != null)
			{
				queryableThrottle.ThrottleName = this.name;
				queryableThrottle.CurrentThrottle = this.currentThrottle;
				queryableThrottle.ActiveWorkItems = this.activeWorkItems;
				queryableThrottle.PendingWorkItemsOnBase = this.pendingWorkItemsOnBase;
				queryableThrottle.QueueLength = this.queue.Count;
				queryableThrottle.OverThrottle = this.IsOverThrottle;
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001194C File Offset: 0x0000FB4C
		private static int GetInitialThrottleValue(string registryKeyPath, string throttleName, int defaultValue)
		{
			if (string.IsNullOrEmpty(registryKeyPath))
			{
				return defaultValue;
			}
			lock (Throttle.registryCache)
			{
				Dictionary<string, object> dictionary = null;
				if (Throttle.registryCache.TryGetValue(registryKeyPath, out dictionary))
				{
					if (dictionary == null)
					{
						return defaultValue;
					}
					object obj2 = null;
					string key = throttleName + "Throttle";
					if (dictionary.TryGetValue(key, out obj2))
					{
						if (obj2 == null || !(obj2 is int))
						{
							return defaultValue;
						}
						return (int)obj2;
					}
				}
			}
			RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(registryKeyPath);
			if (registryKey == null)
			{
				lock (Throttle.registryCache)
				{
					Throttle.registryCache[registryKeyPath] = null;
				}
				return defaultValue;
			}
			using (registryKey)
			{
				string key2 = throttleName + "Throttle";
				object value = registryKey.GetValue(key2);
				lock (Throttle.registryCache)
				{
					Dictionary<string, object> dictionary2 = null;
					if (!Throttle.registryCache.TryGetValue(registryKeyPath, out dictionary2))
					{
						dictionary2 = new Dictionary<string, object>();
						Throttle.registryCache[registryKeyPath] = dictionary2;
					}
					if (value is int)
					{
						dictionary2[key2] = value;
						return (int)value;
					}
					dictionary2[key2] = null;
				}
			}
			return defaultValue;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		private void AddPendingWorkItemsIfNecessary()
		{
			ExTraceGlobals.ThrottleTracer.TraceDebug<Throttle>((long)this.GetHashCode(), "{0}: AddPendingWorkItemsIfNecessary", this);
			while (this.pendingWorkItemsOnBase < Math.Min(this.currentThrottle - this.activeWorkItems, this.queue.Count))
			{
				this.baseThreadPool.QueueUserWorkItem(Throttle.waitCallback, this);
				this.pendingWorkItemsOnBase++;
			}
			this.TraceState();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00011B54 File Offset: 0x0000FD54
		private static void WaitCallback(object state)
		{
			((Throttle)state).InternalWaitCallback();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00011B64 File Offset: 0x0000FD64
		private void InternalWaitCallback()
		{
			Throttle.WorkItemState workItemState;
			lock (this.queue)
			{
				ExTraceGlobals.ThrottleTracer.TraceDebug<Throttle>((long)this.GetHashCode(), "{0}: WaitCallback", this);
				this.TraceState();
				this.pendingWorkItemsOnBase--;
				if (this.queue.Count <= 0)
				{
					ExTraceGlobals.ThrottleTracer.TraceDebug<Throttle>((long)this.GetHashCode(), "{0}: Queue is empty.  Doing nothing", this);
					return;
				}
				if (this.activeWorkItems >= this.currentThrottle)
				{
					ExTraceGlobals.ThrottleTracer.TraceDebug<Throttle>((long)this.GetHashCode(), "{0}: ActiveWorkItems already meets or exceeds throttle.  Doing nothing.", this);
					this.TraceState();
					return;
				}
				workItemState = this.queue.Dequeue();
				this.activeWorkItems++;
				ExTraceGlobals.ThrottleTracer.TraceDebug<Throttle>((long)this.GetHashCode(), "{0}: Calling workitem...", this);
				this.TraceState();
			}
			try
			{
				workItemState.WorkItem(workItemState.State);
			}
			finally
			{
				lock (this.queue)
				{
					this.activeWorkItems--;
					ExTraceGlobals.ThrottleTracer.TraceDebug<Throttle>((long)this.GetHashCode(), "{0}: Workitem has returned.", this);
					this.TraceState();
					this.AddPendingWorkItemsIfNecessary();
				}
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00011CD8 File Offset: 0x0000FED8
		private void TraceState()
		{
			ExTraceGlobals.ThrottleTracer.TraceDebug((long)this.GetHashCode(), "{0}: currentThrottle: {1}; pendingWorkItemsOnBase: {2}; activeWorkItems: {3}; queue.Count: {4}", new object[]
			{
				this,
				this.currentThrottle,
				this.pendingWorkItemsOnBase,
				this.activeWorkItems,
				this.queue.Count
			});
		}

		// Token: 0x0400020A RID: 522
		private const string ThrottleSuffix = "Throttle";

		// Token: 0x0400020B RID: 523
		private static IThreadPool unThrottledThreadPool = new UnThrottledThreadPool();

		// Token: 0x0400020C RID: 524
		private static WaitCallback waitCallback = new WaitCallback(Throttle.WaitCallback);

		// Token: 0x0400020D RID: 525
		private static Dictionary<string, Dictionary<string, object>> registryCache = new Dictionary<string, Dictionary<string, object>>();

		// Token: 0x0400020E RID: 526
		private readonly string name;

		// Token: 0x0400020F RID: 527
		private int currentThrottle = int.MaxValue;

		// Token: 0x04000210 RID: 528
		private int openThrottle = int.MaxValue;

		// Token: 0x04000211 RID: 529
		private int pendingWorkItemsOnBase;

		// Token: 0x04000212 RID: 530
		private int activeWorkItems;

		// Token: 0x04000213 RID: 531
		private Queue<Throttle.WorkItemState> queue = new Queue<Throttle.WorkItemState>();

		// Token: 0x04000214 RID: 532
		private IThreadPool baseThreadPool;

		// Token: 0x0200007E RID: 126
		private struct WorkItemState
		{
			// Token: 0x060003A9 RID: 937 RVA: 0x00011D6B File Offset: 0x0000FF6B
			public WorkItemState(WaitCallback workItem, object state)
			{
				this.WorkItem = workItem;
				this.State = state;
			}

			// Token: 0x04000215 RID: 533
			public WaitCallback WorkItem;

			// Token: 0x04000216 RID: 534
			public object State;
		}
	}
}
