using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002F5 RID: 757
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OrganizationCache
	{
		// Token: 0x06002267 RID: 8807 RVA: 0x0008A296 File Offset: 0x00088496
		internal OrganizationCache()
		{
			this.cache = new Dictionary<object, OrganizationCache.MiniPropertyBag>();
			this.cacheLock = new ReaderWriterLockSlim();
			this.concurrencyLocks = new Dictionary<string, object>();
			this.concurrencyLocksGuard = new ReaderWriterLockSlim();
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x0008A2CC File Offset: 0x000884CC
		internal bool TryLookupCache<T>(ICacheConsumer consumer, string attribute, out T result)
		{
			try
			{
				this.cacheLock.EnterReadLock();
				OrganizationCache.MiniPropertyBag miniPropertyBag;
				if (this.cache.TryGetValue(consumer.Id, out miniPropertyBag) && miniPropertyBag.TryGet<T>(attribute, out result))
				{
					return true;
				}
			}
			finally
			{
				try
				{
					this.cacheLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			result = default(T);
			return false;
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x0008A340 File Offset: 0x00088540
		internal T Get<T>(ICacheConsumer consumer, string attribute, Func<T> invokeQuery)
		{
			T t;
			if (this.TryLookupCache<T>(consumer, attribute, out t))
			{
				return t;
			}
			T result;
			lock (this.GetConcurrencyLock(attribute))
			{
				if (this.TryLookupCache<T>(consumer, attribute, out t))
				{
					result = t;
				}
				else
				{
					t = StorageGlobals.ProtectedADCall<T>(invokeQuery);
					this.cacheLock.EnterWriteLock();
					try
					{
						OrganizationCache.MiniPropertyBag miniPropertyBag;
						if (!this.cache.TryGetValue(consumer.Id, out miniPropertyBag))
						{
							miniPropertyBag = new OrganizationCache.MiniPropertyBag();
							this.cache[consumer.Id] = miniPropertyBag;
						}
						miniPropertyBag[attribute] = t;
						result = t;
					}
					finally
					{
						this.cacheLock.ExitWriteLock();
					}
				}
			}
			return result;
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x0008A404 File Offset: 0x00088604
		internal void InvalidateCache(object id)
		{
			try
			{
				this.cacheLock.EnterWriteLock();
				if (this.testBridge != null)
				{
					this.testBridge(id);
				}
				this.cache.Remove(id);
			}
			finally
			{
				try
				{
					this.cacheLock.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0008A46C File Offset: 0x0008866C
		private object GetConcurrencyLock(string attribute)
		{
			try
			{
				this.concurrencyLocksGuard.EnterReadLock();
				object obj;
				if (this.concurrencyLocks.TryGetValue(attribute, out obj))
				{
					return obj;
				}
			}
			finally
			{
				try
				{
					this.concurrencyLocksGuard.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			object result;
			try
			{
				this.concurrencyLocksGuard.EnterWriteLock();
				object obj;
				if (this.concurrencyLocks.TryGetValue(attribute, out obj))
				{
					result = obj;
				}
				else
				{
					this.concurrencyLocks[attribute] = new object();
					result = this.concurrencyLocks[attribute];
				}
			}
			finally
			{
				try
				{
					this.concurrencyLocksGuard.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x0008A530 File Offset: 0x00088730
		// (set) Token: 0x0600226D RID: 8813 RVA: 0x0008A538 File Offset: 0x00088738
		internal Action<object> TestBridge
		{
			get
			{
				return this.testBridge;
			}
			set
			{
				this.testBridge = value;
			}
		}

		// Token: 0x040013F9 RID: 5113
		private readonly ReaderWriterLockSlim cacheLock;

		// Token: 0x040013FA RID: 5114
		private readonly Dictionary<object, OrganizationCache.MiniPropertyBag> cache;

		// Token: 0x040013FB RID: 5115
		private readonly Dictionary<string, object> concurrencyLocks;

		// Token: 0x040013FC RID: 5116
		private readonly ReaderWriterLockSlim concurrencyLocksGuard;

		// Token: 0x040013FD RID: 5117
		private Action<object> testBridge;

		// Token: 0x020002F6 RID: 758
		internal class MiniPropertyBag
		{
			// Token: 0x0600226E RID: 8814 RVA: 0x0008A541 File Offset: 0x00088741
			internal MiniPropertyBag()
			{
				this.cache = new Dictionary<string, object>();
			}

			// Token: 0x17000B9F RID: 2975
			internal object this[string attribute]
			{
				set
				{
					this.cache[attribute] = value;
				}
			}

			// Token: 0x06002270 RID: 8816 RVA: 0x0008A564 File Offset: 0x00088764
			internal bool TryGet<T>(string attribute, out T value)
			{
				object obj;
				if (this.cache.TryGetValue(attribute, out obj))
				{
					value = (T)((object)obj);
					return true;
				}
				value = default(T);
				return false;
			}

			// Token: 0x040013FE RID: 5118
			private readonly Dictionary<string, object> cache;
		}
	}
}
