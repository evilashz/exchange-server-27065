using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Threading
{
	// Token: 0x02000B09 RID: 2825
	public struct UpgradeableLockSlimWrapper : IDisposable
	{
		// Token: 0x06003CCE RID: 15566 RVA: 0x0009E708 File Offset: 0x0009C908
		public UpgradeableLockSlimWrapper(ReaderWriterLockSlim rwLockSlim)
		{
			ArgumentValidator.ThrowIfNull("rwLockSlim", rwLockSlim);
			this.rwLockSlim = rwLockSlim;
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x0009E71C File Offset: 0x0009C91C
		public void AcquireLock()
		{
			this.rwLockSlim.EnterUpgradeableReadLock();
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x0009E729 File Offset: 0x0009C929
		public void Dispose()
		{
			if (this.rwLockSlim.IsUpgradeableReadLockHeld)
			{
				this.rwLockSlim.ExitUpgradeableReadLock();
			}
		}

		// Token: 0x0400355C RID: 13660
		private readonly ReaderWriterLockSlim rwLockSlim;
	}
}
