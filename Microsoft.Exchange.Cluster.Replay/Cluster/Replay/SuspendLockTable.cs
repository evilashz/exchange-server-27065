using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200015D RID: 349
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SuspendLockTable
	{
		// Token: 0x06000E16 RID: 3606 RVA: 0x0003D42C File Offset: 0x0003B62C
		internal static void AddSuspendLock(StateLock suspendLock)
		{
			SuspendLockTable.s_suspendLocks.AddInstance(suspendLock);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0003D439 File Offset: 0x0003B639
		internal static void RemoveInstance(StateLock suspendLock)
		{
			SuspendLockTable.s_suspendLocks.RemoveInstance(suspendLock);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0003D446 File Offset: 0x0003B646
		internal static bool TryGetSuspendLock(string identity, out StateLock suspendLock)
		{
			return SuspendLockTable.s_suspendLocks.TryGetInstance(identity, out suspendLock);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0003D454 File Offset: 0x0003B654
		internal static StateLock GetNewOrExistingStateLock(string dbName, string identity)
		{
			return SuspendLockTable.s_suspendLocks.GetNewOrExistingStateLock(dbName, identity);
		}

		// Token: 0x040005D2 RID: 1490
		private static SuspendLockTable.SuspendLocks s_suspendLocks = new SuspendLockTable.SuspendLocks();

		// Token: 0x0200015E RID: 350
		[ClassAccessLevel(AccessLevel.Implementation)]
		private class SuspendLocks : SafeInstanceTable<StateLock>
		{
			// Token: 0x06000E1B RID: 3611 RVA: 0x0003D470 File Offset: 0x0003B670
			internal StateLock GetNewOrExistingStateLock(string dbName, string identity)
			{
				this.m_rwLock.AcquireWriterLock(-1);
				StateLock result;
				try
				{
					StateLock stateLock;
					if (this.m_instances.ContainsKey(identity))
					{
						stateLock = this.m_instances[identity];
					}
					else
					{
						stateLock = StateLock.ConstructStateLock(dbName, identity);
						this.m_instances.Add(identity, stateLock);
					}
					result = stateLock;
				}
				finally
				{
					this.m_rwLock.ReleaseWriterLock();
				}
				return result;
			}
		}
	}
}
