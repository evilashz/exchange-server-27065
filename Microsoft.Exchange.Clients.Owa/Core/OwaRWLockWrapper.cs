using System;
using System.Threading;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001F8 RID: 504
	internal sealed class OwaRWLockWrapper
	{
		// Token: 0x06001088 RID: 4232 RVA: 0x00065970 File Offset: 0x00063B70
		public OwaRWLockWrapper()
		{
			this.rwLock = new ReaderWriterLock();
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00065983 File Offset: 0x00063B83
		public void LockWriterElastic(int suggestedTimeout)
		{
			this.LockElastic(suggestedTimeout, true);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0006598D File Offset: 0x00063B8D
		public void LockReaderElastic(int suggestedTimeout)
		{
			this.LockElastic(suggestedTimeout, false);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00065997 File Offset: 0x00063B97
		public void LockWriter(int exactTimeout)
		{
			this.Lock(exactTimeout, true);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x000659A1 File Offset: 0x00063BA1
		public void LockReader(int exactTimeout)
		{
			this.Lock(exactTimeout, false);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x000659AB File Offset: 0x00063BAB
		public void ReleaseWriterLock()
		{
			if (this.rwLock.IsWriterLockHeld)
			{
				Interlocked.Decrement(ref this.numberOfWriterLocksHeld);
			}
			this.rwLock.ReleaseWriterLock();
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x000659D1 File Offset: 0x00063BD1
		public void ReleaseReaderLock()
		{
			this.rwLock.ReleaseReaderLock();
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x000659DE File Offset: 0x00063BDE
		public void ReleaseLock()
		{
			if (this.rwLock.IsWriterLockHeld)
			{
				Interlocked.Exchange(ref this.numberOfWriterLocksHeld, 0);
			}
			this.rwLock.ReleaseLock();
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00065A06 File Offset: 0x00063C06
		public void DowngradeFromWriterLock(ref LockCookie cookieForDowngrade)
		{
			this.rwLock.DowngradeFromWriterLock(ref cookieForDowngrade);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00065A14 File Offset: 0x00063C14
		private static int GetProcessorCount()
		{
			return 8;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00065A18 File Offset: 0x00063C18
		private void LockElastic(int suggestedTimeout, bool lockWriter)
		{
			suggestedTimeout += (int)((double)(2 * suggestedTimeout) * Math.Max(0.0, 1.0 - Math.Pow((double)OwaRWLockWrapper.waitingOnLock / (10.0 * (double)OwaRWLockWrapper.procCount), 2.0)));
			this.Lock(suggestedTimeout, lockWriter);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00065A74 File Offset: 0x00063C74
		private void Lock(int exactTimeout, bool lockWriter)
		{
			try
			{
				Interlocked.Increment(ref OwaRWLockWrapper.waitingOnLock);
				if (lockWriter)
				{
					this.rwLock.AcquireWriterLock(exactTimeout);
					Interlocked.Increment(ref this.numberOfWriterLocksHeld);
				}
				else
				{
					this.rwLock.AcquireReaderLock(exactTimeout);
				}
			}
			catch (ApplicationException innerException)
			{
				throw new OwaLockTimeoutException(string.Format("Attempt to acquire {0} lock on user context timed out", lockWriter ? "writer" : "reader"), innerException, this);
			}
			finally
			{
				Interlocked.Decrement(ref OwaRWLockWrapper.waitingOnLock);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00065B04 File Offset: 0x00063D04
		public bool IsWriterLockHeld
		{
			get
			{
				return this.rwLock.IsWriterLockHeld;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x00065B11 File Offset: 0x00063D11
		public bool IsReaderLockHeld
		{
			get
			{
				return this.rwLock.IsReaderLockHeld;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x00065B1E File Offset: 0x00063D1E
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x00065B26 File Offset: 0x00063D26
		public int NumberOfWriterLocksHeld
		{
			get
			{
				return this.numberOfWriterLocksHeld;
			}
			private set
			{
				this.numberOfWriterLocksHeld = value;
			}
		}

		// Token: 0x04000B3F RID: 2879
		private const double ForceSuggestedTimeoutAtThreadsPerProc = 10.0;

		// Token: 0x04000B40 RID: 2880
		private static int waitingOnLock = 0;

		// Token: 0x04000B41 RID: 2881
		private static int procCount = OwaRWLockWrapper.GetProcessorCount();

		// Token: 0x04000B42 RID: 2882
		private ReaderWriterLock rwLock;

		// Token: 0x04000B43 RID: 2883
		private int numberOfWriterLocksHeld;
	}
}
