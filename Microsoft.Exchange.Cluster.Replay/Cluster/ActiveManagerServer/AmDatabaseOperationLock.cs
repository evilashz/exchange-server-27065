using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000029 RID: 41
	internal class AmDatabaseOperationLock : IDisposable
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		internal int ThreadId
		{
			get
			{
				return this.m_threadId;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000BBFC File Offset: 0x00009DFC
		internal AmDbLockReason Reason
		{
			get
			{
				return this.m_reason;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000BC04 File Offset: 0x00009E04
		internal Guid DatabaseGuid
		{
			get
			{
				return this.m_dbId;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000BC0C File Offset: 0x00009E0C
		public static int GetThreadId()
		{
			return Environment.CurrentManagedThreadId;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000BC14 File Offset: 0x00009E14
		public static AmDatabaseOperationLock Lock(Guid dbId, AmDbLockReason reason, TimeSpan? timeout)
		{
			AmDatabaseOperationLock amDatabaseOperationLock = null;
			AmDatabaseOperationLock amDatabaseOperationLock2 = new AmDatabaseOperationLock();
			amDatabaseOperationLock2.m_reason = reason;
			amDatabaseOperationLock2.m_dbId = dbId;
			amDatabaseOperationLock2.m_threadId = AmDatabaseOperationLock.GetThreadId();
			lock (AmDatabaseOperationLock.s_lockTable)
			{
				if (AmDatabaseOperationLock.s_lockTable.TryGetValue(dbId, out amDatabaseOperationLock) && amDatabaseOperationLock != null)
				{
					AmTrace.Error("AmDatabaseOperationLock: conflict on db {0}. Requesting for {1} but held by {2} for {3}", new object[]
					{
						dbId,
						reason,
						amDatabaseOperationLock.ThreadId,
						amDatabaseOperationLock.Reason
					});
					if (timeout == null || timeout.Value.Ticks == 0L || amDatabaseOperationLock.m_waiter != null)
					{
						throw new AmDbLockConflictException(dbId, reason.ToString(), amDatabaseOperationLock.Reason.ToString());
					}
					amDatabaseOperationLock2.m_waiterEvent = new ManualResetEvent(false);
					amDatabaseOperationLock2.m_mustWaitForLock = true;
					amDatabaseOperationLock.m_waiter = amDatabaseOperationLock2;
				}
				else
				{
					AmDatabaseOperationLock.s_lockTable[dbId] = amDatabaseOperationLock2;
					amDatabaseOperationLock2.m_holdingLock = true;
				}
			}
			if (amDatabaseOperationLock2.m_mustWaitForLock)
			{
				amDatabaseOperationLock2.WaitForLock(timeout.Value, amDatabaseOperationLock);
			}
			AmTrace.Debug("AmDatabaseOperationLock({0},{1}) : lock obtained", new object[]
			{
				dbId,
				reason
			});
			return amDatabaseOperationLock2;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000BD84 File Offset: 0x00009F84
		[Conditional("DEBUG")]
		public static void AssertLockIsHeldByMe(Guid dbId)
		{
			lock (AmDatabaseOperationLock.s_lockTable)
			{
				AmDatabaseOperationLock amDatabaseOperationLock = null;
				if (AmDatabaseOperationLock.s_lockTable.TryGetValue(dbId, out amDatabaseOperationLock) && amDatabaseOperationLock != null)
				{
					AmDatabaseOperationLock.GetThreadId();
				}
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000BDD8 File Offset: 0x00009FD8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000BDE8 File Offset: 0x00009FE8
		public void Unlock()
		{
			lock (this)
			{
				if (this.m_holdingLock)
				{
					AmTrace.Debug("AmDatabaseOperationLock({0},{1}) : releasing lock", new object[]
					{
						this.DatabaseGuid,
						this.Reason
					});
					lock (AmDatabaseOperationLock.s_lockTable)
					{
						AmDatabaseOperationLock amDatabaseOperationLock = null;
						if (AmDatabaseOperationLock.s_lockTable.TryGetValue(this.DatabaseGuid, out amDatabaseOperationLock) && amDatabaseOperationLock == this)
						{
							if (amDatabaseOperationLock.m_waiter != null)
							{
								AmTrace.Debug("AmDatabaseOperationLock({0},{1}) : granting lock to waiter:{2}", new object[]
								{
									this.DatabaseGuid,
									this.Reason,
									amDatabaseOperationLock.m_waiter.Reason
								});
								AmDatabaseOperationLock.s_lockTable[this.DatabaseGuid] = amDatabaseOperationLock.m_waiter;
								amDatabaseOperationLock.m_waiter.m_holdingLock = true;
								amDatabaseOperationLock.m_waiter.m_waiterEvent.Set();
								amDatabaseOperationLock.m_waiter = null;
							}
							else
							{
								AmDatabaseOperationLock.s_lockTable[this.DatabaseGuid] = null;
							}
						}
					}
				}
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000BF5C File Offset: 0x0000A15C
		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.m_disposed)
				{
					this.Unlock();
					if (this.m_waiterEvent != null)
					{
						((IDisposable)this.m_waiterEvent).Dispose();
					}
					this.m_disposed = true;
				}
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		private void WaitForLock(TimeSpan timeout, AmDatabaseOperationLock origHolder)
		{
			AmDatabaseOperationLock amDatabaseOperationLock = null;
			long num = timeout.Ticks / 10000L;
			int num2 = (int)TimeSpan.FromHours(1.0).TotalMilliseconds;
			int num3;
			if (num > (long)num2)
			{
				num3 = num2;
			}
			else
			{
				num3 = (int)num;
			}
			AmTrace.Debug("AmDatabaseOperationLock({0},{1}) : waiting {2} ms for lock", new object[]
			{
				this.DatabaseGuid,
				this.Reason,
				num3
			});
			if (this.m_waiterEvent.WaitOne(num3, false))
			{
				return;
			}
			AmTrace.Error("AmDatabaseOperationLock: timeout", new object[0]);
			lock (AmDatabaseOperationLock.s_lockTable)
			{
				AmDatabaseOperationLock.s_lockTable.TryGetValue(this.DatabaseGuid, out amDatabaseOperationLock);
				if (amDatabaseOperationLock == origHolder)
				{
					amDatabaseOperationLock.m_waiter = null;
					throw new AmDbLockConflictException(this.DatabaseGuid, this.Reason.ToString(), origHolder.Reason.ToString());
				}
				AmTrace.Debug("AmDatabaseOperationLock: got lock after unlikely race", new object[0]);
			}
		}

		// Token: 0x040000B9 RID: 185
		private static Dictionary<Guid, AmDatabaseOperationLock> s_lockTable = new Dictionary<Guid, AmDatabaseOperationLock>();

		// Token: 0x040000BA RID: 186
		private bool m_disposed;

		// Token: 0x040000BB RID: 187
		private bool m_holdingLock;

		// Token: 0x040000BC RID: 188
		private bool m_mustWaitForLock;

		// Token: 0x040000BD RID: 189
		private AmDatabaseOperationLock m_waiter;

		// Token: 0x040000BE RID: 190
		private ManualResetEvent m_waiterEvent;

		// Token: 0x040000BF RID: 191
		private int m_threadId;

		// Token: 0x040000C0 RID: 192
		private AmDbLockReason m_reason;

		// Token: 0x040000C1 RID: 193
		private Guid m_dbId;
	}
}
