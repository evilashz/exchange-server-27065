using System;
using System.Threading;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000844 RID: 2116
	internal class UpgradeableReadLockScope : ReaderWriterLockScopeBase
	{
		// Token: 0x06004971 RID: 18801 RVA: 0x0012DF7D File Offset: 0x0012C17D
		private UpgradeableReadLockScope(ReaderWriterLockSlim readerWriterLock) : base(readerWriterLock)
		{
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x0012DF86 File Offset: 0x0012C186
		protected override void Acquire()
		{
			base.ScopedReaderWriterLock.EnterUpgradeableReadLock();
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x0012DF93 File Offset: 0x0012C193
		protected override void Release()
		{
			if (!this.upgradeableReadLockExited)
			{
				base.ScopedReaderWriterLock.ExitUpgradeableReadLock();
				this.upgradeableReadLockExited = true;
			}
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x0012DFB0 File Offset: 0x0012C1B0
		internal WriteLockScope Upgrade()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.lockChanged && !this.upgradeableReadLockExited)
			{
				this.lockChanged = true;
				return WriteLockScope.Create(base.ScopedReaderWriterLock);
			}
			return null;
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x0012DFFC File Offset: 0x0012C1FC
		internal ReadLockScope Downgrade()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!this.lockChanged && !this.upgradeableReadLockExited)
			{
				this.lockChanged = true;
				ReadLockScope result = ReadLockScope.Create(base.ScopedReaderWriterLock);
				this.Release();
				return result;
			}
			return null;
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x0012E050 File Offset: 0x0012C250
		internal static UpgradeableReadLockScope Create(ReaderWriterLockSlim readerWriterLock)
		{
			if (readerWriterLock == null)
			{
				throw new ArgumentNullException("readerWriterLock");
			}
			UpgradeableReadLockScope upgradeableReadLockScope = new UpgradeableReadLockScope(readerWriterLock);
			upgradeableReadLockScope.Acquire();
			return upgradeableReadLockScope;
		}

		// Token: 0x04002C49 RID: 11337
		private bool upgradeableReadLockExited;

		// Token: 0x04002C4A RID: 11338
		private bool lockChanged;
	}
}
