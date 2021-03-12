using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x020001B5 RID: 437
	internal class ThreadSafeQueue<T> : DisposeTrackableBase
	{
		// Token: 0x060010C6 RID: 4294 RVA: 0x00045517 File Offset: 0x00043717
		public ThreadSafeQueue() : this(true)
		{
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00045520 File Offset: 0x00043720
		public ThreadSafeQueue(bool eventBased)
		{
			this.Lock = new ReaderWriterLockSlim();
			this.LinkedList = new LinkedList<T>();
			if (eventBased)
			{
				this.DataAvailable = new ManualResetEvent(false);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x0004554D File Offset: 0x0004374D
		// (set) Token: 0x060010C9 RID: 4297 RVA: 0x00045555 File Offset: 0x00043755
		public ManualResetEvent DataAvailable { get; private set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x0004555E File Offset: 0x0004375E
		// (set) Token: 0x060010CB RID: 4299 RVA: 0x00045566 File Offset: 0x00043766
		private LinkedList<T> LinkedList { get; set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x0004556F File Offset: 0x0004376F
		// (set) Token: 0x060010CD RID: 4301 RVA: 0x00045577 File Offset: 0x00043777
		private ReaderWriterLockSlim Lock { get; set; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x00045580 File Offset: 0x00043780
		// (set) Token: 0x060010CF RID: 4303 RVA: 0x00045588 File Offset: 0x00043788
		private bool EventPaused { get; set; }

		// Token: 0x060010D0 RID: 4304 RVA: 0x00045594 File Offset: 0x00043794
		public void PauseEvent()
		{
			base.CheckDisposed();
			try
			{
				this.Lock.EnterUpgradeableReadLock();
				if (!this.EventPaused)
				{
					try
					{
						this.Lock.EnterWriteLock();
						this.EventPaused = true;
						this.ResetDataAvailableEvent();
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

		// Token: 0x060010D1 RID: 4305 RVA: 0x00045630 File Offset: 0x00043830
		public void ResumeEvent()
		{
			base.CheckDisposed();
			try
			{
				this.Lock.EnterUpgradeableReadLock();
				if (this.EventPaused)
				{
					try
					{
						this.Lock.EnterWriteLock();
						this.EventPaused = false;
						if (0 < this.LinkedList.Count)
						{
							this.SetDataAvailableEvent();
						}
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

		// Token: 0x060010D2 RID: 4306 RVA: 0x000456D8 File Offset: 0x000438D8
		public void Enqueue(T item)
		{
			base.CheckDisposed();
			try
			{
				this.Lock.EnterWriteLock();
				this.LinkedList.AddLast(item);
				this.SetDataAvailableEvent();
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

		// Token: 0x060010D3 RID: 4307 RVA: 0x00045738 File Offset: 0x00043938
		public bool Dequeue(out T item)
		{
			item = default(T);
			base.CheckDisposed();
			bool result;
			try
			{
				this.Lock.EnterUpgradeableReadLock();
				int count = this.LinkedList.Count;
				if (1 <= count)
				{
					item = this.LinkedList.First.Value;
				}
				try
				{
					this.Lock.EnterWriteLock();
					if (1 <= count)
					{
						this.LinkedList.RemoveFirst();
					}
					if (1 >= count)
					{
						this.ResetDataAvailableEvent();
					}
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
				result = (1 <= count);
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
			return result;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0004580C File Offset: 0x00043A0C
		public IList<T> Dequeue(Predicate<T> evaluateIndividually, Predicate<IList<T>> evaluateEntirely)
		{
			base.CheckDisposed();
			IList<T> result;
			try
			{
				this.Lock.EnterUpgradeableReadLock();
				int count = this.LinkedList.Count;
				if (count == 0)
				{
					try
					{
						this.Lock.EnterWriteLock();
						this.ResetDataAvailableEvent();
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
					result = new T[0];
				}
				else
				{
					List<LinkedListNode<T>> list = new List<LinkedListNode<T>>(count);
					List<T> list2 = new List<T>(count);
					for (LinkedListNode<T> linkedListNode = this.LinkedList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
					{
						if (evaluateIndividually == null || evaluateIndividually(linkedListNode.Value))
						{
							list.Add(linkedListNode);
							list2.Add(linkedListNode.Value);
						}
					}
					IList<T> list3 = new ReadOnlyCollection<T>(list2);
					if (evaluateEntirely != null && !evaluateEntirely(list3))
					{
						result = new T[0];
					}
					else
					{
						try
						{
							this.Lock.EnterWriteLock();
							foreach (LinkedListNode<T> node in list)
							{
								this.LinkedList.Remove(node);
							}
							if (list.Count == count)
							{
								this.ResetDataAvailableEvent();
							}
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
						result = list3;
					}
				}
			}
			finally
			{
				this.Lock.ExitUpgradeableReadLock();
			}
			return result;
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x000459E4 File Offset: 0x00043BE4
		public int Count
		{
			get
			{
				base.CheckDisposed();
				int count;
				try
				{
					this.Lock.EnterReadLock();
					count = this.LinkedList.Count;
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
				return count;
			}
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00045A40 File Offset: 0x00043C40
		private void SetDataAvailableEvent()
		{
			if (this.DataAvailable != null && !this.EventPaused)
			{
				this.DataAvailable.Set();
			}
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00045A5E File Offset: 0x00043C5E
		private void ResetDataAvailableEvent()
		{
			if (this.DataAvailable != null)
			{
				this.DataAvailable.Reset();
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00045A74 File Offset: 0x00043C74
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ThreadSafeQueue<T>>(this);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00045A7C File Offset: 0x00043C7C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.DataAvailable != null)
				{
					this.DataAvailable.Close();
					this.DataAvailable = null;
				}
				if (this.Lock != null)
				{
					this.Lock.Dispose();
					this.Lock = null;
				}
				GC.SuppressFinalize(this);
			}
		}
	}
}
