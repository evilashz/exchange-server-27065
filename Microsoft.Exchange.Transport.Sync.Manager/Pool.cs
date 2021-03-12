using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class Pool<TItem> where TItem : class
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002559 File Offset: 0x00000759
		internal Pool(int capacity, int maxCapacity, TimeSpan expiryInterval) : this(capacity, maxCapacity, true, expiryInterval, ContentAggregationConfig.PoolExpiryCheckInterval)
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000256C File Offset: 0x0000076C
		internal Pool(int capacity, int maxCapacity, bool assertOnMaxCapacity, TimeSpan expiryInterval, TimeSpan expiryCheckInterval)
		{
			this.itemsAvailable = new Queue<PoolItem<TItem>>(capacity);
			this.itemsInUse = new Dictionary<uint, PoolItem<TItem>>(maxCapacity);
			this.maxCapacity = maxCapacity;
			this.assertOnMaxCapacity = assertOnMaxCapacity;
			this.maxNumberOfAttemptsBeforePoolBackOff = ContentAggregationConfig.MaxNumberOfAttemptsBeforePoolBackOff;
			this.BackOffInterval = ContentAggregationConfig.PoolBackOffTimeInterval;
			this.expiryInterval = expiryInterval;
			this.expiryTimer = new GuardedTimer(new TimerCallback(this.ExpireUnusedItems), null, expiryCheckInterval, expiryCheckInterval);
			this.syncLogSession = this.GetSyncLogSession();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000025F8 File Offset: 0x000007F8
		internal int ItemsAvailableCount
		{
			get
			{
				int count;
				lock (this.syncRoot)
				{
					count = this.itemsAvailable.Count;
				}
				return count;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002640 File Offset: 0x00000840
		internal int ItemsInUseCount
		{
			get
			{
				int count;
				lock (this.syncRoot)
				{
					count = this.itemsInUse.Count;
				}
				return count;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002688 File Offset: 0x00000888
		private bool MaximumCapacityReached
		{
			get
			{
				bool result;
				lock (this.syncRoot)
				{
					result = (this.itemsInUse.Count + this.itemsAvailable.Count >= this.maxCapacity);
				}
				return result;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026E8 File Offset: 0x000008E8
		internal void AddDiagnosticInfoTo(XElement parentElement)
		{
			lock (this.syncRoot)
			{
				XElement xelement = new XElement("ItemsInPool");
				xelement.Add(new XElement("count", this.ItemsAvailableCount));
				foreach (PoolItem<TItem> poolItem in this.itemsAvailable)
				{
					XElement xelement2 = new XElement("Item");
					xelement2.Add(new XElement("poolItemId", poolItem.ID));
					xelement2.Add(new XElement("creationTime", poolItem.CreationTime.ToString("o")));
					xelement2.Add(new XElement("lastUsedTime", poolItem.LastUsedTime.ToString("o")));
					xelement.Add(xelement2);
				}
				parentElement.Add(xelement);
				XElement xelement3 = new XElement("ItemsInUse");
				xelement3.Add(new XElement("count", this.ItemsInUseCount));
				foreach (PoolItem<TItem> poolItem2 in this.itemsInUse.Values)
				{
					XElement xelement4 = new XElement("Item");
					xelement4.Add(new XElement("poolItemId", poolItem2.ID));
					xelement4.Add(new XElement("creationTime", poolItem2.CreationTime.ToString("o")));
					xelement4.Add(new XElement("lastUsedTime", poolItem2.LastUsedTime.ToString("o")));
					xelement3.Add(xelement4);
				}
				parentElement.Add(xelement3);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000295C File Offset: 0x00000B5C
		internal void Shutdown()
		{
			lock (this.syncRoot)
			{
				this.shuttingDown = true;
				while (this.itemsAvailable.Count > 0)
				{
					PoolItem<TItem> poolItem = this.itemsAvailable.Dequeue();
					this.DestroyItem(poolItem.Item);
				}
				if (this.expiryTimer != null)
				{
					this.expiryTimer.Dispose(false);
					this.expiryTimer = null;
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000029E4 File Offset: 0x00000BE4
		internal PoolItem<TItem> GetItem(out bool needsBackOff)
		{
			needsBackOff = false;
			PoolItem<TItem> poolItem;
			lock (this.syncRoot)
			{
				if (this.shuttingDown)
				{
					this.syncLogSession.LogVerbose((TSLID)91UL, Guid.Empty, null, "We're shutting down, so we won't create a new item.", new object[0]);
					return null;
				}
				if (this.itemsAvailable.Count > 0)
				{
					poolItem = this.itemsAvailable.Dequeue();
				}
				else
				{
					if (this.MaximumCapacityReached)
					{
						this.syncLogSession.LogError((TSLID)92UL, Guid.Empty, null, "Maximum capacity for resource pool is reached. A resource leak is very likely to be happening.", new object[0]);
						StackTrace stackTrace = new StackTrace();
						this.LogEvent(TransportSyncManagerEventLogConstants.Tuple_SyncManagerResourcePoolLimitReached, null, new object[]
						{
							this.maxCapacity,
							stackTrace.ToString()
						});
						needsBackOff = true;
						return null;
					}
					ExDateTime utcNow = ExDateTime.UtcNow;
					if (this.poolInBackOffMode)
					{
						if (utcNow - this.backOffStartedTime < this.BackOffInterval)
						{
							needsBackOff = true;
							this.syncLogSession.LogError((TSLID)93UL, Guid.Empty, null, "Failed to create new item in the pool as we're backing off.", new object[0]);
							return null;
						}
						this.backOffStartedTime = ExDateTime.MinValue;
						this.poolInBackOffMode = false;
						this.failureCount = 0;
					}
					bool flag2 = false;
					TItem titem = this.CreateItem(out flag2);
					if (flag2)
					{
						if ((int)(this.failureCount += 1) >= this.maxNumberOfAttemptsBeforePoolBackOff)
						{
							this.poolInBackOffMode = (needsBackOff = true);
							this.backOffStartedTime = utcNow;
						}
					}
					else
					{
						this.backOffStartedTime = ExDateTime.MinValue;
						this.poolInBackOffMode = (needsBackOff = false);
						this.failureCount = 0;
					}
					if (titem == null)
					{
						this.syncLogSession.LogError((TSLID)94UL, Guid.Empty, null, "Failed to create new item in the pool.", new object[0]);
						return null;
					}
					TItem item = titem;
					uint num = Pool<TItem>.currentId;
					Pool<TItem>.currentId = num + 1U;
					poolItem = new PoolItem<TItem>(item, num);
				}
				this.itemsInUse.Add(poolItem.ID, poolItem);
			}
			poolItem.LastUsedTime = ExDateTime.UtcNow;
			return poolItem;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002C2C File Offset: 0x00000E2C
		internal void ReturnItem(PoolItem<TItem> poolItem)
		{
			this.ReturnItem(poolItem, true);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C38 File Offset: 0x00000E38
		internal void ReturnItem(PoolItem<TItem> poolItem, bool reuse)
		{
			SyncUtilities.ThrowIfArgumentNull("item", poolItem);
			lock (this.syncRoot)
			{
				this.itemsInUse.Remove(poolItem.ID);
				if (this.shuttingDown || !reuse || this.MaximumCapacityReached)
				{
					this.DestroyItem(poolItem.Item);
					poolItem = null;
				}
				else
				{
					poolItem.LastUsedTime = ExDateTime.UtcNow;
					this.itemsAvailable.Enqueue(poolItem);
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002CCC File Offset: 0x00000ECC
		protected virtual GlobalSyncLogSession GetSyncLogSession()
		{
			return ContentAggregationConfig.SyncLogSession;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002CD3 File Offset: 0x00000ED3
		protected virtual bool LogEvent(ExEventLog.EventTuple eventTuple, string periodicKey, params object[] messageArgs)
		{
			return ContentAggregationConfig.EventLogger.LogEvent(eventTuple, periodicKey, messageArgs);
		}

		// Token: 0x06000027 RID: 39
		protected abstract void DestroyItem(TItem item);

		// Token: 0x06000028 RID: 40
		protected abstract TItem CreateItem(out bool needsBackOff);

		// Token: 0x06000029 RID: 41 RVA: 0x00002CE4 File Offset: 0x00000EE4
		private void ExpireUnusedItems(object state)
		{
			lock (this.syncRoot)
			{
				if (!this.shuttingDown)
				{
					int count = this.itemsAvailable.Count;
					for (int i = 0; i < count; i++)
					{
						PoolItem<TItem> poolItem = this.itemsAvailable.Dequeue();
						if (ExDateTime.UtcNow - poolItem.LastUsedTime >= this.expiryInterval)
						{
							this.DestroyItem(poolItem.Item);
						}
						else
						{
							this.itemsAvailable.Enqueue(poolItem);
						}
					}
				}
			}
		}

		// Token: 0x04000009 RID: 9
		private readonly GlobalSyncLogSession syncLogSession;

		// Token: 0x0400000A RID: 10
		private readonly int maxNumberOfAttemptsBeforePoolBackOff;

		// Token: 0x0400000B RID: 11
		private readonly TimeSpan BackOffInterval;

		// Token: 0x0400000C RID: 12
		private readonly object syncRoot = new object();

		// Token: 0x0400000D RID: 13
		private readonly Queue<PoolItem<TItem>> itemsAvailable;

		// Token: 0x0400000E RID: 14
		private readonly Dictionary<uint, PoolItem<TItem>> itemsInUse;

		// Token: 0x0400000F RID: 15
		private readonly int maxCapacity;

		// Token: 0x04000010 RID: 16
		private readonly bool assertOnMaxCapacity;

		// Token: 0x04000011 RID: 17
		private static uint currentId;

		// Token: 0x04000012 RID: 18
		private bool shuttingDown;

		// Token: 0x04000013 RID: 19
		private byte failureCount;

		// Token: 0x04000014 RID: 20
		private bool poolInBackOffMode;

		// Token: 0x04000015 RID: 21
		private ExDateTime backOffStartedTime;

		// Token: 0x04000016 RID: 22
		private TimeSpan expiryInterval;

		// Token: 0x04000017 RID: 23
		private GuardedTimer expiryTimer;
	}
}
