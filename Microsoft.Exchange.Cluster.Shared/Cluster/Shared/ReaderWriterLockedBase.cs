using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000081 RID: 129
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ReaderWriterLockedBase
	{
		// Token: 0x06000367 RID: 871 RVA: 0x0000DD50 File Offset: 0x0000BF50
		protected void ReaderLockedOperation(Action operation)
		{
			try
			{
				this.m_rwLock.EnterReadLock();
				operation();
			}
			finally
			{
				try
				{
					this.m_rwLock.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000DDA0 File Offset: 0x0000BFA0
		protected void WriterLockedOperation(Action operation)
		{
			try
			{
				this.m_rwLock.EnterWriteLock();
				operation();
			}
			finally
			{
				try
				{
					this.m_rwLock.ExitWriteLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x04000293 RID: 659
		private ReaderWriterLockSlim m_rwLock = new ReaderWriterLockSlim();
	}
}
