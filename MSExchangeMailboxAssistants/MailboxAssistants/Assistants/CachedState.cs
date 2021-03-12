using System;
using System.Threading;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000012 RID: 18
	internal sealed class CachedState
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00004DEE File Offset: 0x00002FEE
		public CachedState(int sizeOfCachedState)
		{
			this.cachedState = new object[sizeOfCachedState];
			this.rwLock = new ReaderWriterLock();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004E0D File Offset: 0x0000300D
		public void LockForRead()
		{
			this.rwLock.AcquireReaderLock(-1);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004E1B File Offset: 0x0000301B
		public void LockForWrite()
		{
			this.rwLock.AcquireWriterLock(-1);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004E29 File Offset: 0x00003029
		public void ReleaseReaderLock()
		{
			this.rwLock.ReleaseReaderLock();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004E36 File Offset: 0x00003036
		public void ReleaseWriterLock()
		{
			this.rwLock.ReleaseWriterLock();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004E43 File Offset: 0x00003043
		public LockCookie UpgradeToWriterLock()
		{
			return this.rwLock.UpgradeToWriterLock(-1);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004E51 File Offset: 0x00003051
		public void DowngradeFromWriterLock(ref LockCookie cookie)
		{
			this.rwLock.DowngradeFromWriterLock(ref cookie);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004E5F File Offset: 0x0000305F
		public object[] State
		{
			get
			{
				return this.cachedState;
			}
		}

		// Token: 0x040000E4 RID: 228
		private ReaderWriterLock rwLock;

		// Token: 0x040000E5 RID: 229
		private object[] cachedState;
	}
}
