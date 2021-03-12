using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000033 RID: 51
	internal class AmDbLock
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000EB2E File Offset: 0x0000CD2E
		internal AmDbLock()
		{
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000EB54 File Offset: 0x0000CD54
		internal bool IsExiting
		{
			get
			{
				return this.m_isExiting;
			}
			set
			{
				this.m_isExiting = value;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000EB60 File Offset: 0x0000CD60
		internal bool Lock(Guid dbGuid, AmDbLockReason lockReason)
		{
			bool flag = false;
			int num = 0;
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			AmTrace.Debug("Trying to acquire lock for db={0} requester={1} reason={2}", new object[]
			{
				dbGuid,
				currentManagedThreadId,
				lockReason
			});
			do
			{
				lock (this.m_locker)
				{
					AmDbLock.LockData lockData = null;
					if (this.m_lockTable.TryGetValue(dbGuid, out lockData))
					{
						AmTrace.Debug("lock already held for db={0} heldby={1} current={2} reason={3}", new object[]
						{
							dbGuid,
							lockData.ThreadId,
							currentManagedThreadId,
							lockData.Reason
						});
						if (lockData.ThreadId == currentManagedThreadId)
						{
							lockData.RefCount++;
							flag = true;
							AmTrace.Debug("same thread has requested lock db={0} heldby={1} refcount={2}", new object[]
							{
								dbGuid,
								lockData.ThreadId,
								lockData.RefCount
							});
						}
					}
					else
					{
						lockData = new AmDbLock.LockData(currentManagedThreadId, lockReason);
						this.m_lockTable[dbGuid] = lockData;
						flag = true;
						AmTrace.Debug("lock created for db={0} owner={1} reason={2}", new object[]
						{
							dbGuid,
							lockData.ThreadId,
							lockData.Reason
						});
					}
				}
				if (flag)
				{
					break;
				}
				AmTrace.Debug("sleeping to get the lock again for db={0} requester={1} reason={2} sleptSoFar={3}", new object[]
				{
					dbGuid,
					currentManagedThreadId,
					lockReason,
					num
				});
				Thread.Sleep(500);
				num += 500;
				AmDbLock.WarnIfSleepingForEver(dbGuid, num, lockReason);
			}
			while (!this.IsExiting);
			if (this.IsExiting && flag)
			{
				this.Release(dbGuid, lockReason);
				flag = false;
			}
			AmTrace.Debug("Got lock!! db={0} requester={1}", new object[]
			{
				dbGuid,
				currentManagedThreadId
			});
			return flag;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000ED90 File Offset: 0x0000CF90
		internal void Release(Guid dbGuid, AmDbLockReason lockReason)
		{
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			AmTrace.Debug("releasing lock for db={0} requester={1} reason={2}", new object[]
			{
				dbGuid,
				currentManagedThreadId,
				lockReason
			});
			lock (this.m_locker)
			{
				AmDbLock.LockData lockData = null;
				if (this.m_lockTable.TryGetValue(dbGuid, out lockData) && lockData.ThreadId == currentManagedThreadId)
				{
					lockData.RefCount--;
					if (lockData.RefCount == 0)
					{
						this.m_lockTable.Remove(dbGuid);
					}
				}
			}
			AmTrace.Debug("lock released for db={0} requester={1}", new object[]
			{
				dbGuid,
				currentManagedThreadId
			});
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000EE68 File Offset: 0x0000D068
		private static void WarnIfSleepingForEver(Guid dbGuid, int timeSlept, AmDbLockReason lockReason)
		{
			if (timeSlept % 10000 == 0)
			{
				ReplayEventLogConstants.Tuple_DatabaseOperationLockIsTakingLongTime.LogEvent(null, new object[]
				{
					dbGuid,
					timeSlept,
					lockReason
				});
			}
		}

		// Token: 0x040000F4 RID: 244
		private const int LockRetrySleepTime = 500;

		// Token: 0x040000F5 RID: 245
		private const int WarnTimeDuration = 10000;

		// Token: 0x040000F6 RID: 246
		private bool m_isExiting;

		// Token: 0x040000F7 RID: 247
		private object m_locker = new object();

		// Token: 0x040000F8 RID: 248
		private Dictionary<Guid, AmDbLock.LockData> m_lockTable = new Dictionary<Guid, AmDbLock.LockData>();

		// Token: 0x02000034 RID: 52
		internal class LockData
		{
			// Token: 0x0600025C RID: 604 RVA: 0x0000EEAC File Offset: 0x0000D0AC
			internal LockData(int threadId, AmDbLockReason lockReason)
			{
				this.m_reason = lockReason;
				this.m_threadId = threadId;
				this.m_refCount = 1;
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x0600025D RID: 605 RVA: 0x0000EEC9 File Offset: 0x0000D0C9
			// (set) Token: 0x0600025E RID: 606 RVA: 0x0000EED1 File Offset: 0x0000D0D1
			internal int RefCount
			{
				get
				{
					return this.m_refCount;
				}
				set
				{
					this.m_refCount = value;
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x0600025F RID: 607 RVA: 0x0000EEDA File Offset: 0x0000D0DA
			internal int ThreadId
			{
				get
				{
					return this.m_threadId;
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x06000260 RID: 608 RVA: 0x0000EEE2 File Offset: 0x0000D0E2
			internal AmDbLockReason Reason
			{
				get
				{
					return this.m_reason;
				}
			}

			// Token: 0x040000F9 RID: 249
			private int m_refCount;

			// Token: 0x040000FA RID: 250
			private int m_threadId;

			// Token: 0x040000FB RID: 251
			private AmDbLockReason m_reason;
		}
	}
}
