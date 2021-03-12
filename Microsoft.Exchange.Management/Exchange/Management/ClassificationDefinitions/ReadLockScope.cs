using System;
using System.Threading;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000842 RID: 2114
	internal class ReadLockScope : ReaderWriterLockScopeBase
	{
		// Token: 0x06004969 RID: 18793 RVA: 0x0012DEE5 File Offset: 0x0012C0E5
		private ReadLockScope(ReaderWriterLockSlim readerWriterLock) : base(readerWriterLock)
		{
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x0012DEEE File Offset: 0x0012C0EE
		protected override void Acquire()
		{
			base.ScopedReaderWriterLock.EnterReadLock();
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x0012DEFB File Offset: 0x0012C0FB
		protected override void Release()
		{
			base.ScopedReaderWriterLock.ExitReadLock();
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x0012DF08 File Offset: 0x0012C108
		internal static ReadLockScope Create(ReaderWriterLockSlim readerWriterLock)
		{
			if (readerWriterLock == null)
			{
				throw new ArgumentNullException("readerWriterLock");
			}
			ReadLockScope readLockScope = new ReadLockScope(readerWriterLock);
			readLockScope.Acquire();
			return readLockScope;
		}
	}
}
