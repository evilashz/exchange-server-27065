using System;
using System.Threading;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000A4 RID: 164
	internal sealed class OwaRWLockWrapper
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x00013C6F File Offset: 0x00011E6F
		public OwaRWLockWrapper()
		{
			this.rwLock = new ReaderWriterLockSlim();
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00013C82 File Offset: 0x00011E82
		public bool IsWriterLockHeld
		{
			get
			{
				return this.rwLock.IsWriteLockHeld;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00013C8F File Offset: 0x00011E8F
		public bool IsReaderLockHeld
		{
			get
			{
				return this.rwLock.IsReadLockHeld;
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00013C9C File Offset: 0x00011E9C
		public bool LockWriterElastic(int suggestedTimeout)
		{
			return this.LockElastic(suggestedTimeout, true);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00013CA6 File Offset: 0x00011EA6
		public bool LockReaderElastic(int suggestedTimeout)
		{
			return this.LockElastic(suggestedTimeout, false);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00013CB0 File Offset: 0x00011EB0
		public bool LockWriter(int exactTimeout)
		{
			return this.Lock(exactTimeout, true);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00013CBA File Offset: 0x00011EBA
		public bool LockReader(int exactTimeout)
		{
			return this.Lock(exactTimeout, false);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00013CC4 File Offset: 0x00011EC4
		public void ReleaseWriterLock()
		{
			this.rwLock.ExitWriteLock();
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00013CD1 File Offset: 0x00011ED1
		public void ReleaseReaderLock()
		{
			this.rwLock.ExitReadLock();
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00013CDE File Offset: 0x00011EDE
		private static int GetProcessorCount()
		{
			return 8;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00013CE4 File Offset: 0x00011EE4
		private bool LockElastic(int suggestedTimeout, bool lockWriter)
		{
			suggestedTimeout += (int)((double)(2 * suggestedTimeout) * Math.Max(0.0, 1.0 - Math.Pow((double)OwaRWLockWrapper.waitingOnLock / (10.0 * (double)OwaRWLockWrapper.procCount), 2.0)));
			return this.Lock(suggestedTimeout, lockWriter);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00013D40 File Offset: 0x00011F40
		private bool Lock(int exactTimeout, bool lockWriter)
		{
			bool result;
			try
			{
				Interlocked.Increment(ref OwaRWLockWrapper.waitingOnLock);
				if (lockWriter)
				{
					result = this.rwLock.TryEnterWriteLock(exactTimeout);
				}
				else
				{
					result = this.rwLock.TryEnterReadLock(exactTimeout);
				}
			}
			catch (LockRecursionException innerException)
			{
				throw new OwaLockRecursionException(string.Format("Attempt to acquire {0} lock threw an exception", lockWriter ? "writer" : "reader"), innerException, this);
			}
			finally
			{
				Interlocked.Decrement(ref OwaRWLockWrapper.waitingOnLock);
			}
			return result;
		}

		// Token: 0x0400038E RID: 910
		private const double ForceSuggestedTimeoutAtThreadsPerProc = 10.0;

		// Token: 0x0400038F RID: 911
		private static int waitingOnLock = 0;

		// Token: 0x04000390 RID: 912
		private static int procCount = OwaRWLockWrapper.GetProcessorCount();

		// Token: 0x04000391 RID: 913
		private ReaderWriterLockSlim rwLock;
	}
}
