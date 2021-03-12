using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000EF RID: 239
	internal class SynchronizationObject : DisposeTrackableBase
	{
		// Token: 0x060009F3 RID: 2547 RVA: 0x00041DE8 File Offset: 0x0003FFE8
		public IDisposable CreateReadLock()
		{
			return new SynchronizationObject.LockedReadLock(this.rwLock);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00041DF5 File Offset: 0x0003FFF5
		public IDisposable CreateUpgradeableReadLock()
		{
			return new SynchronizationObject.LockedUpgradeableReadLock(this.rwLock);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00041E02 File Offset: 0x00040002
		public IDisposable CreateWriteLock()
		{
			return new SynchronizationObject.LockedWriteLock(this.rwLock);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00041E0F File Offset: 0x0004000F
		[Conditional("DEBUG")]
		public void ThrowIfSetterUnsafe()
		{
			if (!this.rwLock.IsWriteLockHeld)
			{
				throw new InvalidOperationException("object is not locked for write");
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00041E29 File Offset: 0x00040029
		[Conditional("DEBUG")]
		public void ThrowIfGetterUnsafe()
		{
			if (!this.rwLock.IsReadLockHeld && !this.rwLock.IsUpgradeableReadLockHeld && !this.rwLock.IsWriteLockHeld)
			{
				throw new InvalidOperationException("object is not locked for either read or write");
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00041E5D File Offset: 0x0004005D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SynchronizationObject>(this);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00041E65 File Offset: 0x00040065
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.rwLock != null)
				{
					this.rwLock.Dispose();
					this.rwLock = null;
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0400068C RID: 1676
		private ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

		// Token: 0x020000F0 RID: 240
		private abstract class LockedLock : IDisposable
		{
			// Token: 0x060009FB RID: 2555 RVA: 0x00041E9E File Offset: 0x0004009E
			protected LockedLock(ReaderWriterLockSlim rwLock)
			{
				if (rwLock == null)
				{
					throw new ArgumentNullException("rwLock");
				}
				this.rwLock = rwLock;
			}

			// Token: 0x060009FC RID: 2556
			public abstract void Dispose();

			// Token: 0x1700026A RID: 618
			// (get) Token: 0x060009FD RID: 2557 RVA: 0x00041EBB File Offset: 0x000400BB
			protected ReaderWriterLockSlim Lock
			{
				get
				{
					return this.rwLock;
				}
			}

			// Token: 0x1700026B RID: 619
			// (get) Token: 0x060009FE RID: 2558 RVA: 0x00041EC3 File Offset: 0x000400C3
			// (set) Token: 0x060009FF RID: 2559 RVA: 0x00041ECB File Offset: 0x000400CB
			protected bool UnlockedLockAcquired
			{
				get
				{
					return this.unlockedLockAcquired;
				}
				set
				{
					this.unlockedLockAcquired = value;
				}
			}

			// Token: 0x0400068D RID: 1677
			private readonly ReaderWriterLockSlim rwLock;

			// Token: 0x0400068E RID: 1678
			private bool unlockedLockAcquired;
		}

		// Token: 0x020000F1 RID: 241
		private sealed class LockedReadLock : SynchronizationObject.LockedLock
		{
			// Token: 0x06000A00 RID: 2560 RVA: 0x00041ED4 File Offset: 0x000400D4
			public LockedReadLock(ReaderWriterLockSlim rwLock) : base(rwLock)
			{
				if (!base.Lock.IsReadLockHeld && !base.Lock.IsUpgradeableReadLockHeld && !base.Lock.IsWriteLockHeld)
				{
					base.Lock.EnterReadLock();
					base.UnlockedLockAcquired = true;
				}
			}

			// Token: 0x06000A01 RID: 2561 RVA: 0x00041F21 File Offset: 0x00040121
			public override void Dispose()
			{
				if (base.UnlockedLockAcquired && base.Lock.IsReadLockHeld)
				{
					base.Lock.ExitReadLock();
				}
			}
		}

		// Token: 0x020000F2 RID: 242
		private sealed class LockedUpgradeableReadLock : SynchronizationObject.LockedLock
		{
			// Token: 0x06000A02 RID: 2562 RVA: 0x00041F43 File Offset: 0x00040143
			public LockedUpgradeableReadLock(ReaderWriterLockSlim rwLock) : base(rwLock)
			{
				if (!base.Lock.IsUpgradeableReadLockHeld && !base.Lock.IsWriteLockHeld)
				{
					base.Lock.EnterUpgradeableReadLock();
					base.UnlockedLockAcquired = true;
				}
			}

			// Token: 0x06000A03 RID: 2563 RVA: 0x00041F78 File Offset: 0x00040178
			public override void Dispose()
			{
				if (base.UnlockedLockAcquired && base.Lock.IsUpgradeableReadLockHeld)
				{
					base.Lock.ExitUpgradeableReadLock();
				}
			}
		}

		// Token: 0x020000F3 RID: 243
		private sealed class LockedWriteLock : SynchronizationObject.LockedLock
		{
			// Token: 0x06000A04 RID: 2564 RVA: 0x00041F9A File Offset: 0x0004019A
			public LockedWriteLock(ReaderWriterLockSlim rwLock) : base(rwLock)
			{
				if (!base.Lock.IsWriteLockHeld)
				{
					base.Lock.EnterWriteLock();
					base.UnlockedLockAcquired = true;
				}
			}

			// Token: 0x06000A05 RID: 2565 RVA: 0x00041FC2 File Offset: 0x000401C2
			public override void Dispose()
			{
				if (base.UnlockedLockAcquired && base.Lock.IsWriteLockHeld)
				{
					base.Lock.ExitWriteLock();
				}
			}
		}
	}
}
