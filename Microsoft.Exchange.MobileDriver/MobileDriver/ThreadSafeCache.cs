using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200004C RID: 76
	internal class ThreadSafeCache<TKey, TValue> : DisposeTrackableBase
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x0000A4E6 File Offset: 0x000086E6
		public ThreadSafeCache(ThreadSafeCache<TKey, TValue>.ValueCreatorDelegate valCreator, ThreadSafeCache<TKey, TValue>.ValueDestroyerDelegate valDestroyer) : this(valCreator, valDestroyer, null)
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000A4F4 File Offset: 0x000086F4
		public ThreadSafeCache(ThreadSafeCache<TKey, TValue>.ValueCreatorDelegate valCreator, ThreadSafeCache<TKey, TValue>.ValueDestroyerDelegate valDestroyer, IEqualityComparer<TKey> comparer)
		{
			if (valCreator == null)
			{
				throw new ArgumentNullException("valCreator");
			}
			this.ValueCreator = valCreator;
			this.ValueDestroyer = valDestroyer;
			this.Dictionary = ((comparer == null) ? new Dictionary<TKey, TValue>() : new Dictionary<TKey, TValue>(comparer));
			this.Lock = new ReaderWriterLockSlim();
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000A544 File Offset: 0x00008744
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000A54C File Offset: 0x0000874C
		private Dictionary<TKey, TValue> Dictionary { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000A555 File Offset: 0x00008755
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000A55D File Offset: 0x0000875D
		private ThreadSafeCache<TKey, TValue>.ValueCreatorDelegate ValueCreator { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000A566 File Offset: 0x00008766
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000A56E File Offset: 0x0000876E
		private ThreadSafeCache<TKey, TValue>.ValueDestroyerDelegate ValueDestroyer { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000A577 File Offset: 0x00008777
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000A57F File Offset: 0x0000877F
		private ReaderWriterLockSlim Lock { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000A588 File Offset: 0x00008788
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000A590 File Offset: 0x00008790
		public bool NonCached { get; set; }

		// Token: 0x170000AB RID: 171
		public TValue this[TKey key]
		{
			get
			{
				if (this.NonCached)
				{
					this.Remove(key);
				}
				base.CheckDisposed();
				try
				{
					this.Lock.EnterReadLock();
					if (this.Dictionary.ContainsKey(key))
					{
						return this.Dictionary[key];
					}
				}
				finally
				{
					try
					{
						this.Lock.ExitReadLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
				TValue tvalue = this.ValueCreator(key);
				bool flag = true;
				TValue result;
				try
				{
					try
					{
						this.Lock.EnterUpgradeableReadLock();
						if (this.Dictionary.ContainsKey(key))
						{
							return this.Dictionary[key];
						}
						try
						{
							this.Lock.EnterWriteLock();
							this.Dictionary[key] = tvalue;
						}
						finally
						{
							try
							{
								this.Lock.ExitWriteLock();
							}
							catch (SynchronizationLockException)
							{
							}
						}
					}
					finally
					{
						try
						{
							this.Lock.ExitUpgradeableReadLock();
						}
						catch (SynchronizationLockException)
						{
						}
					}
					flag = false;
					result = tvalue;
				}
				finally
				{
					if (flag && this.ValueDestroyer != null)
					{
						this.ValueDestroyer(tvalue);
					}
				}
				return result;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A6E8 File Offset: 0x000088E8
		public void Remove(TKey key)
		{
			base.CheckDisposed();
			try
			{
				this.Lock.EnterUpgradeableReadLock();
				if (this.Dictionary.ContainsKey(key))
				{
					TValue tvalue = this.Dictionary[key];
					try
					{
						this.Lock.EnterWriteLock();
						IDisposable disposable = tvalue as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
						else if (this.ValueDestroyer != null)
						{
							this.ValueDestroyer(tvalue);
						}
						this.Dictionary.Remove(key);
					}
					finally
					{
						try
						{
							this.Lock.ExitWriteLock();
						}
						catch (SynchronizationLockException)
						{
						}
					}
				}
			}
			finally
			{
				try
				{
					this.Lock.ExitUpgradeableReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A7C0 File Offset: 0x000089C0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ThreadSafeCache<TKey, TValue>>(this);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A7C8 File Offset: 0x000089C8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				foreach (TValue tvalue in this.Dictionary.Values)
				{
					IDisposable disposable = tvalue as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
					else if (this.ValueDestroyer != null)
					{
						this.ValueDestroyer(tvalue);
					}
				}
				this.Dictionary.Clear();
				this.Dictionary = null;
				this.Lock.Dispose();
			}
		}

		// Token: 0x0200004D RID: 77
		// (Invoke) Token: 0x060001E2 RID: 482
		public delegate TValue ValueCreatorDelegate(TKey key);

		// Token: 0x0200004E RID: 78
		// (Invoke) Token: 0x060001E6 RID: 486
		public delegate void ValueDestroyerDelegate(TValue val);
	}
}
