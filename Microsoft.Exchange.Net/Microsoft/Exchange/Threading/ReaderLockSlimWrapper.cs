using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Threading
{
	// Token: 0x02000B0A RID: 2826
	public struct ReaderLockSlimWrapper : IDisposable
	{
		// Token: 0x06003CD1 RID: 15569 RVA: 0x0009E743 File Offset: 0x0009C943
		public ReaderLockSlimWrapper(ReaderWriterLockSlim rwLockSlim)
		{
			ArgumentValidator.ThrowIfNull("rwLockSlim", rwLockSlim);
			this.rwLockSlim = rwLockSlim;
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x0009E757 File Offset: 0x0009C957
		public void AcquireLock()
		{
			this.rwLockSlim.EnterReadLock();
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x0009E764 File Offset: 0x0009C964
		public void Dispose()
		{
			if (this.rwLockSlim.IsReadLockHeld)
			{
				this.rwLockSlim.ExitReadLock();
			}
		}

		// Token: 0x0400355D RID: 13661
		private readonly ReaderWriterLockSlim rwLockSlim;
	}
}
