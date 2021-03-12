using System;
using System.Threading;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000197 RID: 407
	internal class PendingNotifierLockTracker
	{
		// Token: 0x06000EA7 RID: 3751 RVA: 0x000383E0 File Offset: 0x000365E0
		public bool TryReleaseAllLocks(PendingNotifierLockTracker.ReleaseAllLocksCallback callback)
		{
			this.allLocksReleasedCallback = callback;
			int num = Interlocked.Exchange(ref this.pendingRequestCounter, int.MinValue);
			if (num == 0)
			{
				this.lockOwner = Thread.CurrentThread;
				this.allLocksReleasedCallback = null;
			}
			return num == 0;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00038424 File Offset: 0x00036624
		public bool TryAcquireLock()
		{
			int num = Interlocked.Increment(ref this.pendingRequestCounter);
			if (num == 1)
			{
				this.lockOwner = Thread.CurrentThread;
			}
			return num == 1;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00038450 File Offset: 0x00036650
		public bool TryAcquireLockOnlyIfSucceed()
		{
			int num = Interlocked.CompareExchange(ref this.pendingRequestCounter, 1, 0);
			if (num == 0)
			{
				this.lockOwner = Thread.CurrentThread;
			}
			return num == 0;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003847D File Offset: 0x0003667D
		public bool TryReleaseLock()
		{
			return this.TryReleaseLock(false);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00038488 File Offset: 0x00036688
		public bool TryReleaseLock(bool isCurrentRequestCompleted)
		{
			if (!isCurrentRequestCompleted && this.lockOwner != Thread.CurrentThread)
			{
				throw new OwaOperationNotSupportedException("This thread is not the owner of the lock!");
			}
			this.lockOwner = null;
			if (this.pendingRequestCounter < 0)
			{
				this.lockOwner = Thread.CurrentThread;
				if (this.allLocksReleasedCallback != null)
				{
					this.allLocksReleasedCallback();
				}
				return true;
			}
			int num = Interlocked.Decrement(ref this.pendingRequestCounter);
			if (num > 0)
			{
				this.lockOwner = Thread.CurrentThread;
			}
			return num == 0;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00038504 File Offset: 0x00036704
		public bool SetPipeAvailable(bool releaseLock)
		{
			int num = Interlocked.CompareExchange(ref this.pendingRequestAvailable, 1, 0);
			if (num != 0)
			{
				throw new OwaExistentNotificationPipeException("There is already a pending pipe for the same user context");
			}
			this.lockOwner = Thread.CurrentThread;
			return releaseLock && !this.TryReleaseLock();
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00038548 File Offset: 0x00036748
		public void SetPipeUnavailable()
		{
			if (this.lockOwner != Thread.CurrentThread)
			{
				throw new OwaOperationNotSupportedException("This thread is not the owner of the lock!");
			}
			int num = Interlocked.CompareExchange(ref this.pendingRequestAvailable, 0, 1);
			if (num != 1)
			{
				throw new OwaNotificationPipeException("The pipe is already unavailable");
			}
			this.TryAcquireLock();
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00038591 File Offset: 0x00036791
		public bool IsLockOwner()
		{
			return this.lockOwner == Thread.CurrentThread;
		}

		// Token: 0x040008DE RID: 2270
		private const int ReleaseAllLocksValue = -2147483648;

		// Token: 0x040008DF RID: 2271
		private volatile PendingNotifierLockTracker.ReleaseAllLocksCallback allLocksReleasedCallback;

		// Token: 0x040008E0 RID: 2272
		private int pendingRequestCounter = 1;

		// Token: 0x040008E1 RID: 2273
		private int pendingRequestAvailable;

		// Token: 0x040008E2 RID: 2274
		private Thread lockOwner;

		// Token: 0x02000198 RID: 408
		// (Invoke) Token: 0x06000EB1 RID: 3761
		public delegate void ReleaseAllLocksCallback();
	}
}
