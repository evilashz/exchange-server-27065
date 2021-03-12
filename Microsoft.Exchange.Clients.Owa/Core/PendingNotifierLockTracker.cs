using System;
using System.Threading;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000209 RID: 521
	internal class PendingNotifierLockTracker
	{
		// Token: 0x06001198 RID: 4504 RVA: 0x0006A32C File Offset: 0x0006852C
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

		// Token: 0x06001199 RID: 4505 RVA: 0x0006A370 File Offset: 0x00068570
		public bool TryAcquireLock()
		{
			int num = Interlocked.Increment(ref this.pendingRequestCounter);
			if (num == 1)
			{
				this.lockOwner = Thread.CurrentThread;
			}
			return num == 1;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0006A39C File Offset: 0x0006859C
		public bool TryAcquireLockOnlyIfSucceed()
		{
			int num = Interlocked.CompareExchange(ref this.pendingRequestCounter, 1, 0);
			if (num == 0)
			{
				this.lockOwner = Thread.CurrentThread;
			}
			return num == 0;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0006A3C9 File Offset: 0x000685C9
		public bool TryReleaseLock()
		{
			return this.TryReleaseLock(false);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0006A3D4 File Offset: 0x000685D4
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

		// Token: 0x0600119D RID: 4509 RVA: 0x0006A450 File Offset: 0x00068650
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

		// Token: 0x0600119E RID: 4510 RVA: 0x0006A494 File Offset: 0x00068694
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

		// Token: 0x0600119F RID: 4511 RVA: 0x0006A4DD File Offset: 0x000686DD
		public bool IsLockOwner()
		{
			return this.lockOwner == Thread.CurrentThread;
		}

		// Token: 0x04000BDB RID: 3035
		private const int ReleaseAllLocksValue = -2147483648;

		// Token: 0x04000BDC RID: 3036
		private volatile PendingNotifierLockTracker.ReleaseAllLocksCallback allLocksReleasedCallback;

		// Token: 0x04000BDD RID: 3037
		private int pendingRequestCounter = 1;

		// Token: 0x04000BDE RID: 3038
		private int pendingRequestAvailable;

		// Token: 0x04000BDF RID: 3039
		private Thread lockOwner;

		// Token: 0x0200020A RID: 522
		// (Invoke) Token: 0x060011A2 RID: 4514
		public delegate void ReleaseAllLocksCallback();
	}
}
