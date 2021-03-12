using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200023F RID: 575
	internal sealed class Subscriptions : IDisposable
	{
		// Token: 0x06000F21 RID: 3873 RVA: 0x0004A9F8 File Offset: 0x00048BF8
		public Subscriptions()
		{
			ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "Subscriptions constructor called");
			this.subscriptions = new Dictionary<string, SubscriptionBase>();
			this.subscriptionsByUser = new Dictionary<BudgetKey, int>();
			EventQueue.PollingInterval = new TimeSpan(0, 0, Global.EventQueuePollingInterval);
			PerformanceMonitor.UpdateActiveSubscriptionsCounter(0L);
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0004AA5A File Offset: 0x00048C5A
		public static void Initialize()
		{
			if (Subscriptions.Singleton == null)
			{
				Subscriptions.Singleton = new Subscriptions();
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0004AA6D File Offset: 0x00048C6D
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x0004AA74 File Offset: 0x00048C74
		public static Subscriptions Singleton { get; private set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0004AA7C File Offset: 0x00048C7C
		internal List<SubscriptionBase> SubscriptionsList
		{
			get
			{
				List<SubscriptionBase> result;
				lock (this.lockObject)
				{
					result = new List<SubscriptionBase>(this.subscriptions.Values);
				}
				return result;
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0004AAC8 File Offset: 0x00048CC8
		internal void Add(SubscriptionBase subscription)
		{
			ExTraceGlobals.SubscriptionsTracer.TraceDebug<string>((long)this.GetHashCode(), "Subscriptions.Add called. Before lock.  SubscriptionId: {0}", subscription.SubscriptionId);
			lock (this.lockObject)
			{
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<string>((long)this.GetHashCode(), "Subscriptions.Add. After lock.  SubscriptionId: {0}", subscription.SubscriptionId);
				this.subscriptions.Add(subscription.SubscriptionId, subscription);
				this.IncrementSubscriptionsForUser(subscription);
				PerformanceMonitor.UpdateActiveSubscriptionsCounter((long)this.subscriptions.Count);
				this.InitializeDeleteExpiredSubscriptionsTimer();
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0004AB6C File Offset: 0x00048D6C
		private void IncrementSubscriptionsForUser(SubscriptionBase subscription)
		{
			lock (this.lockObject)
			{
				int num = 0;
				this.subscriptionsByUser.TryGetValue(subscription.BudgetKey, out num);
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "[Subscriptions::IncrementSubscriptionsForUser] User '{0}' had {1} subscriptions.  Incrementing to {2}.", subscription.BudgetKey.ToString(), num, num + 1);
				this.subscriptionsByUser[subscription.BudgetKey] = num + 1;
			}
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0004ABF8 File Offset: 0x00048DF8
		private void DecrementSubscriptionsForUser(SubscriptionBase subscription)
		{
			lock (this.lockObject)
			{
				int num = 0;
				if (!this.subscriptionsByUser.TryGetValue(subscription.BudgetKey, out num))
				{
					ExTraceGlobals.SubscriptionsTracer.TraceError<string>((long)this.GetHashCode(), "[Subscriptions::DecrementSubscriptionsForUser] User {0} was not found in the SubscriptionsByUser dictionary.", subscription.BudgetKey.ToString());
					throw new InvalidOperationException(string.Format("[Subscriptions::DecrementSubscriptionsForUser] User {0} was not found in the SubscriptionsByUser dictionary.", subscription.BudgetKey.ToString()));
				}
				num--;
				if (num <= 0)
				{
					ExTraceGlobals.SubscriptionsTracer.TraceDebug<string>((long)this.GetHashCode(), "[Subscriptions::DecrementSubscriptionsForUser] User '{0}' decremented to 0.  Removing reference from cache.", subscription.BudgetKey.ToString());
					this.subscriptionsByUser.Remove(subscription.BudgetKey);
				}
				else
				{
					ExTraceGlobals.SubscriptionsTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "[Subscriptions::DecrementSubscriptionsForUser] User '{0}' had {1} subscriptions.  Decrementing to {2}.", subscription.BudgetKey.ToString(), num + 1, num);
					this.subscriptionsByUser[subscription.BudgetKey] = num;
				}
			}
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0004ACFC File Offset: 0x00048EFC
		internal int GetSubscriptionCountForUser(BudgetKey budgetKey)
		{
			int result;
			lock (this.lockObject)
			{
				int num = 0;
				this.subscriptionsByUser.TryGetValue(budgetKey, out num);
				result = num;
			}
			return result;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0004AD4C File Offset: 0x00048F4C
		private void InitializeDeleteExpiredSubscriptionsTimer()
		{
			if (this.deleteExpiredSubscriptionsTimer == null)
			{
				ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "InitializeDeleteExpiredSubscriptionsTimer. Initializing timer.");
				TimerCallback callback = new TimerCallback(this.DeleteExpiredSubscriptions);
				this.deleteExpiredSubscriptionsTimer = new Timer(callback, null, 300000, 300000);
			}
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0004AD9C File Offset: 0x00048F9C
		private SubscriptionBase Remove(string subscriptionId)
		{
			ExTraceGlobals.SubscriptionsTracer.TraceDebug<string>((long)this.GetHashCode(), "Subscriptions.Remove called. Before lock.  SubscriptionId: {0}", subscriptionId);
			SubscriptionBase result;
			lock (this.lockObject)
			{
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<string>((long)this.GetHashCode(), "Subscriptions.Remove. After lock.  SubscriptionId: {0}", subscriptionId);
				SubscriptionBase subscription = this.GetSubscription(subscriptionId);
				this.subscriptions.Remove(subscriptionId);
				this.DecrementSubscriptionsForUser(subscription);
				PerformanceMonitor.UpdateActiveSubscriptionsCounter((long)this.subscriptions.Count);
				result = subscription;
			}
			return result;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0004AE34 File Offset: 0x00049034
		internal void Delete(string subscriptionId)
		{
			try
			{
				SubscriptionBase subscriptionBase = this.Remove(subscriptionId);
				subscriptionBase.Dispose();
			}
			catch (SubscriptionNotFoundException)
			{
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0004AE68 File Offset: 0x00049068
		internal SubscriptionBase Get(string subscriptionId)
		{
			ExTraceGlobals.SubscriptionsTracer.TraceDebug<string>((long)this.GetHashCode(), "Subscriptions.Get called. Before lock.  SubscriptionId: {0}", subscriptionId);
			SubscriptionBase subscription;
			lock (this.lockObject)
			{
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<string>((long)this.GetHashCode(), "Subscriptions.Get. After lock.  SubscriptionId: {0}", subscriptionId);
				subscription = this.GetSubscription(subscriptionId);
			}
			return subscription;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0004AEDC File Offset: 0x000490DC
		private SubscriptionBase GetSubscription(string subscriptionId)
		{
			ExTraceGlobals.SubscriptionsTracer.TraceDebug<string>((long)this.GetHashCode(), "Subscriptions.GetSubscription called. SubscriptionId: {0}", subscriptionId);
			SubscriptionBase result;
			lock (this.lockObject)
			{
				if (this.subscriptions == null || !this.subscriptions.TryGetValue(subscriptionId, out result))
				{
					throw new SubscriptionNotFoundException();
				}
			}
			return result;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0004AF4C File Offset: 0x0004914C
		internal void DeleteExpiredSubscriptions(object state)
		{
			ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "Subscriptions.DeleteExpiredSubscriptions called.");
			this.RemoveExpiredSubscriptions();
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0004AF6C File Offset: 0x0004916C
		internal void RemoveExpiredSubscriptions()
		{
			ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "Subscriptions.RemoveExpiredSubscriptions called. Before lock.");
			List<SubscriptionBase> list;
			lock (this.lockObject)
			{
				ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "Subscriptions.RemoveExpiredSubscriptions. After lock.");
				list = new List<SubscriptionBase>(this.subscriptions.Values);
			}
			List<SubscriptionBase> list2 = new List<SubscriptionBase>();
			if (this.isDisposed)
			{
				return;
			}
			foreach (SubscriptionBase subscriptionBase in list)
			{
				if (subscriptionBase.IsExpired)
				{
					list2.Add(subscriptionBase);
					ExTraceGlobals.SubscriptionsTracer.TraceDebug<string>((long)this.GetHashCode(), "Subscriptions.RemoveExpiredSubscriptions. Expired SubscriptionId: {0}", subscriptionBase.SubscriptionId);
				}
			}
			ExTraceGlobals.SubscriptionsTracer.TraceDebug<int>((long)this.GetHashCode(), "Subscriptions.RemoveExpiredSubscriptions. Expired count: {0}", list2.Count);
			foreach (SubscriptionBase subscriptionBase2 in list2)
			{
				this.Delete(subscriptionBase2.SubscriptionId);
			}
			PerformanceMonitor.UpdateActiveSubscriptionsCounter((long)this.subscriptions.Count);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0004B0C8 File Offset: 0x000492C8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0004B0D4 File Offset: 0x000492D4
		private void Dispose(bool isDisposing)
		{
			ExTraceGlobals.SubscriptionsTracer.TraceDebug<bool, bool>((long)this.GetHashCode(), "Subscriptions.Dispose called. Before lock. IsDisposed: {0} IsDisposing: {1}", this.isDisposed, isDisposing);
			lock (this.lockObject)
			{
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<bool>((long)this.GetHashCode(), "Subscriptions.Dispose. After lock.  IsDisposed: {0}", this.isDisposed);
				if (!this.isDisposed)
				{
					if (isDisposing)
					{
						if (this.deleteExpiredSubscriptionsTimer != null)
						{
							this.deleteExpiredSubscriptionsTimer.Dispose();
							this.deleteExpiredSubscriptionsTimer = null;
							ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "Subscriptions.Dispose. After timer dispose.");
						}
						if (this.subscriptions != null)
						{
							foreach (SubscriptionBase subscriptionBase in this.subscriptions.Values)
							{
								subscriptionBase.Dispose();
							}
							this.subscriptions.Clear();
							this.subscriptionsByUser.Clear();
							PerformanceMonitor.UpdateActiveSubscriptionsCounter(0L);
							this.subscriptions = null;
							this.subscriptionsByUser = null;
							ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "Subscriptions.Dispose. After subscriptions dispose.");
						}
					}
					this.isDisposed = true;
				}
			}
		}

		// Token: 0x04000B98 RID: 2968
		private const int DeleteExpiredSubscriptionsTimerDueTime = 300000;

		// Token: 0x04000B99 RID: 2969
		private const int DeleteExpiredSubscriptionsTimerPeriod = 300000;

		// Token: 0x04000B9A RID: 2970
		private bool isDisposed;

		// Token: 0x04000B9B RID: 2971
		private object lockObject = new object();

		// Token: 0x04000B9C RID: 2972
		private Dictionary<string, SubscriptionBase> subscriptions;

		// Token: 0x04000B9D RID: 2973
		private Dictionary<BudgetKey, int> subscriptionsByUser;

		// Token: 0x04000B9E RID: 2974
		private Timer deleteExpiredSubscriptionsTimer;
	}
}
