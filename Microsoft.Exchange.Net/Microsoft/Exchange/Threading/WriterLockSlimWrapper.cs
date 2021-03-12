using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Threading
{
	// Token: 0x02000B08 RID: 2824
	public struct WriterLockSlimWrapper : IDisposable
	{
		// Token: 0x06003CCB RID: 15563 RVA: 0x0009E6CD File Offset: 0x0009C8CD
		public WriterLockSlimWrapper(ReaderWriterLockSlim rwLockSlim)
		{
			ArgumentValidator.ThrowIfNull("rwLockSlim", rwLockSlim);
			this.rwLockSlim = rwLockSlim;
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x0009E6E1 File Offset: 0x0009C8E1
		public void AcquireLock()
		{
			this.rwLockSlim.EnterWriteLock();
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x0009E6EE File Offset: 0x0009C8EE
		public void Dispose()
		{
			if (this.rwLockSlim.IsWriteLockHeld)
			{
				this.rwLockSlim.ExitWriteLock();
			}
		}

		// Token: 0x0400355B RID: 13659
		private readonly ReaderWriterLockSlim rwLockSlim;
	}
}
