using System;
using System.Threading;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000288 RID: 648
	internal static class ReaderWriterLockExtensions
	{
		// Token: 0x06001BD8 RID: 7128 RVA: 0x000725E8 File Offset: 0x000707E8
		public static IDisposable AcquireReadLock(this ReaderWriterLockSlim slimLock)
		{
			ReaderLockSlimWrapper readerLockSlimWrapper = new ReaderLockSlimWrapper(slimLock);
			readerLockSlimWrapper.AcquireLock();
			return readerLockSlimWrapper;
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0007260C File Offset: 0x0007080C
		public static IDisposable AcquireWriteLock(this ReaderWriterLockSlim slimLock)
		{
			WriterLockSlimWrapper writerLockSlimWrapper = new WriterLockSlimWrapper(slimLock);
			writerLockSlimWrapper.AcquireLock();
			return writerLockSlimWrapper;
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x00072630 File Offset: 0x00070830
		public static IDisposable AcquireUpgradeableLock(this ReaderWriterLockSlim slimLock)
		{
			UpgradeableLockSlimWrapper upgradeableLockSlimWrapper = new UpgradeableLockSlimWrapper(slimLock);
			upgradeableLockSlimWrapper.AcquireLock();
			return upgradeableLockSlimWrapper;
		}
	}
}
