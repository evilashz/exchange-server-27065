using System;
using System.Threading;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000843 RID: 2115
	internal class WriteLockScope : ReaderWriterLockScopeBase
	{
		// Token: 0x0600496D RID: 18797 RVA: 0x0012DF31 File Offset: 0x0012C131
		private WriteLockScope(ReaderWriterLockSlim readerWriterLock) : base(readerWriterLock)
		{
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x0012DF3A File Offset: 0x0012C13A
		protected override void Acquire()
		{
			base.ScopedReaderWriterLock.EnterWriteLock();
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x0012DF47 File Offset: 0x0012C147
		protected override void Release()
		{
			base.ScopedReaderWriterLock.ExitWriteLock();
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x0012DF54 File Offset: 0x0012C154
		internal static WriteLockScope Create(ReaderWriterLockSlim readerWriterLock)
		{
			if (readerWriterLock == null)
			{
				throw new ArgumentNullException("readerWriterLock");
			}
			WriteLockScope writeLockScope = new WriteLockScope(readerWriterLock);
			writeLockScope.Acquire();
			return writeLockScope;
		}
	}
}
