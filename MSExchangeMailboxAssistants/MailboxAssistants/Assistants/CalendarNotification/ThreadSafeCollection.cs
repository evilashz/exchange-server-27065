using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000F9 RID: 249
	internal class ThreadSafeCollection<T> : DisposeTrackableBase, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x00043B98 File Offset: 0x00041D98
		public ThreadSafeCollection(IEnumerable<T> collection, IComparer<T> sortComparer, IEqualityComparer<T> searchComparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.syncObj = new SynchronizationObject();
			this.list = new List<T>(collection);
			this.sortComparer = sortComparer;
			this.searchComparer = (searchComparer ?? EqualityComparer<T>.Default);
			this.rwLock = new ReaderWriterLockSlim();
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00043BF4 File Offset: 0x00041DF4
		public ThreadSafeCollection(int capacity, IComparer<T> sortComparer, IEqualityComparer<T> searchComparer)
		{
			if (0 >= capacity)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this.syncObj = new SynchronizationObject();
			this.list = new List<T>(capacity);
			this.sortComparer = sortComparer;
			this.searchComparer = (searchComparer ?? EqualityComparer<T>.Default);
			this.rwLock = new ReaderWriterLockSlim();
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00043C4F File Offset: 0x00041E4F
		public SynchronizationObject SyncObj
		{
			get
			{
				return this.syncObj;
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00043C58 File Offset: 0x00041E58
		public bool RemoveAt(int index, out T item)
		{
			base.CheckDisposed();
			if (0 > index)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			item = default(T);
			this.rwLock.EnterUpgradeableReadLock();
			bool result;
			try
			{
				if (this.list.Count - 1 < index)
				{
					result = false;
				}
				else
				{
					item = this.list[index];
					this.rwLock.EnterWriteLock();
					try
					{
						this.list.RemoveAt(index);
					}
					finally
					{
						this.rwLock.ExitWriteLock();
					}
					result = true;
				}
			}
			finally
			{
				this.rwLock.ExitUpgradeableReadLock();
			}
			return result;
		}

		// Token: 0x17000275 RID: 629
		public T this[int index]
		{
			get
			{
				base.CheckDisposed();
				this.rwLock.EnterReadLock();
				T result;
				try
				{
					result = this.list[index];
				}
				finally
				{
					this.rwLock.ExitReadLock();
				}
				return result;
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00043D74 File Offset: 0x00041F74
		public int Add(T item)
		{
			base.CheckDisposed();
			this.rwLock.EnterUpgradeableReadLock();
			int result;
			try
			{
				if (this.list.Exists((T t) => this.searchComparer.Equals(item, t)))
				{
					throw new ArgumentOutOfRangeException("item");
				}
				this.rwLock.EnterWriteLock();
				try
				{
					result = ((IList)this.list).Add(item);
				}
				finally
				{
					this.rwLock.ExitWriteLock();
				}
			}
			finally
			{
				this.rwLock.ExitUpgradeableReadLock();
			}
			return result;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00043E2C File Offset: 0x0004202C
		public void Clear()
		{
			base.CheckDisposed();
			this.rwLock.EnterWriteLock();
			try
			{
				this.list.Clear();
			}
			finally
			{
				this.rwLock.ExitWriteLock();
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00043E74 File Offset: 0x00042074
		public int Count
		{
			get
			{
				base.CheckDisposed();
				this.rwLock.EnterReadLock();
				int count;
				try
				{
					count = this.list.Count;
				}
				finally
				{
					this.rwLock.ExitReadLock();
				}
				return count;
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00043EC0 File Offset: 0x000420C0
		public bool Contains(T item)
		{
			base.CheckDisposed();
			this.rwLock.EnterReadLock();
			bool result;
			try
			{
				result = this.list.Contains(item);
			}
			finally
			{
				this.rwLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00043F0C File Offset: 0x0004210C
		public bool Remove(Predicate<T> match)
		{
			base.CheckDisposed();
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			this.rwLock.EnterWriteLock();
			bool result;
			try
			{
				result = (0 < this.list.RemoveAll(match));
			}
			finally
			{
				this.rwLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00043F8C File Offset: 0x0004218C
		public bool Remove(T item)
		{
			base.CheckDisposed();
			return this.Remove((T t) => this.searchComparer.Equals(item, t));
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00043FE8 File Offset: 0x000421E8
		public int Update(T originalItem, T item)
		{
			base.CheckDisposed();
			if (!this.searchComparer.Equals(originalItem, item))
			{
				throw new ArgumentOutOfRangeException("item");
			}
			this.rwLock.EnterWriteLock();
			int result;
			try
			{
				if (this.list.RemoveAll((T t) => this.searchComparer.Equals(originalItem, t)) == 0)
				{
					result = -this.list.Count;
				}
				else
				{
					result = ((IList)this.list).Add(item);
				}
			}
			finally
			{
				this.rwLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0004409C File Offset: 0x0004229C
		public void ForEach(Action<T> action)
		{
			base.CheckDisposed();
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			this.rwLock.EnterReadLock();
			try
			{
				this.list.ForEach(action);
			}
			finally
			{
				this.rwLock.ExitReadLock();
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000440F4 File Offset: 0x000422F4
		public IList<T> Find(Predicate<T> match)
		{
			base.CheckDisposed();
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			this.rwLock.EnterReadLock();
			IList<T> result;
			try
			{
				result = this.list.FindAll(match);
			}
			finally
			{
				this.rwLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0004414C File Offset: 0x0004234C
		public void Sort()
		{
			base.CheckDisposed();
			this.rwLock.EnterWriteLock();
			try
			{
				this.list.Sort(this.sortComparer);
			}
			finally
			{
				this.rwLock.ExitWriteLock();
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0004419C File Offset: 0x0004239C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ThreadSafeCollection<T>>(this);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000441A4 File Offset: 0x000423A4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.rwLock != null)
				{
					this.rwLock.Dispose();
					this.rwLock = null;
				}
				if (this.syncObj != null)
				{
					this.syncObj.Dispose();
					this.syncObj = null;
				}
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x000441DD File Offset: 0x000423DD
		void ICollection<!0>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x000441E8 File Offset: 0x000423E8
		public void CopyTo(T[] array, int arrayIndex)
		{
			base.CheckDisposed();
			this.rwLock.EnterReadLock();
			try
			{
				this.list.CopyTo(array, arrayIndex);
			}
			finally
			{
				this.rwLock.ExitReadLock();
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00044234 File Offset: 0x00042434
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00044237 File Offset: 0x00042437
		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0004423E File Offset: 0x0004243E
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040006A9 RID: 1705
		private List<T> list;

		// Token: 0x040006AA RID: 1706
		private IComparer<T> sortComparer;

		// Token: 0x040006AB RID: 1707
		private IEqualityComparer<T> searchComparer;

		// Token: 0x040006AC RID: 1708
		private ReaderWriterLockSlim rwLock;

		// Token: 0x040006AD RID: 1709
		private SynchronizationObject syncObj;
	}
}
