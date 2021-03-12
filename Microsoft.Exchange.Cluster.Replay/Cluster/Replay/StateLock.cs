using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200015B RID: 347
	internal sealed class StateLock : IIdentityGuid
	{
		// Token: 0x06000DF2 RID: 3570 RVA: 0x0003C74C File Offset: 0x0003A94C
		private StateLock(string dbName, string identity)
		{
			this.m_databaseName = dbName;
			this.m_identity = identity;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003C76D File Offset: 0x0003A96D
		public static StateLock GetNewOrExistingStateLock(string dbName, string identity)
		{
			return SuspendLockTable.GetNewOrExistingStateLock(dbName, identity);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003C776 File Offset: 0x0003A976
		internal static StateLock ConstructStateLock(string dbName, string identity)
		{
			return new StateLock(dbName, identity);
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0003C77F File Offset: 0x0003A97F
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x0003C787 File Offset: 0x0003A987
		public bool SuspendWanted { get; set; }

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003C790 File Offset: 0x0003A990
		public bool TryEnterSuspend(bool fWait, out LockOwner currentOwner)
		{
			ExTraceGlobals.StateLockTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "{0}: TryEnterSuspend(fWait={1})", this.m_databaseName, fWait);
			if (this.CurrentOwner == LockOwner.Suspend)
			{
				currentOwner = LockOwner.Suspend;
				ExTraceGlobals.StateLockTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: TryEnterSuspend(): Suspend already owns the lock. Leaving.", this.m_databaseName);
				return true;
			}
			this.SuspendWanted = true;
			bool flag = this.TryEnterInternal(LockOwner.Suspend, fWait, new TimeSpan?(TimeSpan.FromMilliseconds((double)RegistryParameters.SuspendLockTimeoutInMsec)), out currentOwner);
			if (!flag)
			{
				ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner>((long)this.GetHashCode(), "{0}: TryEnterSuspend(): Could not acquire Suspend lock since '{1}' owns the lock.", this.m_databaseName, currentOwner);
			}
			return flag;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003C82A File Offset: 0x0003AA2A
		public bool TryEnterAcll(bool fWait, TimeSpan? timeout, out LockOwner currentOwner)
		{
			ExTraceGlobals.StateLockTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "{0}: TryEnterAcll(fWait={1})", this.m_databaseName, fWait);
			return this.TryEnterInternal(LockOwner.AttemptCopyLastLogs, fWait, timeout, out currentOwner);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003C854 File Offset: 0x0003AA54
		public bool TryEnterAcll(bool fWait, TimeSpan? timeout, out LockOwner currentOwner, ActionToRunBeforeWaitingForLock actionBeforeWaitForLock)
		{
			ExTraceGlobals.StateLockTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "{0}: TryEnterAcll(fWait={1})", this.m_databaseName, fWait);
			return this.TryEnterInternal(LockOwner.AttemptCopyLastLogs, fWait, timeout, out currentOwner, actionBeforeWaitForLock);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0003C880 File Offset: 0x0003AA80
		public bool TryEnter(LockOwner attemptOwner, bool fWait)
		{
			LockOwner lockOwner;
			return this.TryEnter(attemptOwner, fWait, out lockOwner);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0003C898 File Offset: 0x0003AA98
		public bool TryEnter(LockOwner attemptOwner, bool fWait, out LockOwner currentOwner)
		{
			ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner, bool>((long)this.GetHashCode(), "{0}: TryEnter(attemptOwner={1}, fWait={2})", this.m_databaseName, attemptOwner, fWait);
			if (attemptOwner == LockOwner.Suspend)
			{
				throw new ArgumentException("StateLock.TryEnter is not for use with LockOwner.Suspend. Use StateLockRemote.TryEnter or StateLock.TryEnterSuspend instead.", "attemptOwner");
			}
			if (attemptOwner == LockOwner.AttemptCopyLastLogs)
			{
				throw new ArgumentException("StateLock.TryEnter is not for use with LockOwner.AttemptCopyLastLogs. Use StateLock.TryEnterAcll instead.", "attemptOwner");
			}
			return this.TryEnterInternal(attemptOwner, fWait, new TimeSpan?(TimeSpan.FromMilliseconds((double)RegistryParameters.SuspendLockTimeoutInMsec)), out currentOwner);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0003C906 File Offset: 0x0003AB06
		private bool TryEnterInternal(LockOwner attemptOwner, bool fWait, TimeSpan? timeout, out LockOwner currentOwner)
		{
			return this.TryEnterInternal(attemptOwner, fWait, timeout, out currentOwner, null);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0003C914 File Offset: 0x0003AB14
		private bool TryEnterInternal(LockOwner attemptOwner, bool fWait, TimeSpan? timeout, out LockOwner currentOwner, ActionToRunBeforeWaitingForLock actionBeforeWaitForLock)
		{
			ExTraceGlobals.StateLockTracer.TraceFunction((long)this.GetHashCode(), "TryEnterInternal {0}: attemptOwner='{1}', fWait='{2}', timeout='{3}'", new object[]
			{
				this.m_databaseName,
				attemptOwner,
				fWait,
				(timeout != null) ? timeout.Value.ToString() : "<null>"
			});
			bool flag = false;
			LockOwner? lockOwner = null;
			lock (this)
			{
				if (attemptOwner == LockOwner.AttemptCopyLastLogs)
				{
					if (this.CurrentOwner == LockOwner.AttemptCopyLastLogs)
					{
						currentOwner = LockOwner.AttemptCopyLastLogs;
						ExTraceGlobals.StateLockTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: TryEnterAcll(): ACLL already owns the lock. Is another thread trying to run ACLL? Leaving.", this.m_databaseName);
						return false;
					}
					if (this.m_pendingOwners.Contains(attemptOwner))
					{
						currentOwner = LockOwner.AttemptCopyLastLogs;
						ExTraceGlobals.StateLockTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: TryEnterAcll(): ACLL is already on the pending owners list. Is another thread trying to run ACLL? Leaving.", this.m_databaseName);
						return false;
					}
				}
				if (!fWait)
				{
					if (attemptOwner == this.CurrentOwner)
					{
						ExTraceGlobals.StateLockTracer.TraceDebug<string>((long)this.GetHashCode(), "TryEnterInternal {0}: attemptOwner == this.CurrentOwner", this.m_databaseName);
						LockOwner lockOwner2;
						if (this.ShouldGiveUpLock(out lockOwner2))
						{
							flag = false;
							lockOwner = new LockOwner?(lockOwner2);
							ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner>((long)this.GetHashCode(), "TryEnterInternal {0}: fGotLock='false' because of ShouldGiveUpLock(), highestPending='{1}'", this.m_databaseName, lockOwner2);
						}
						else
						{
							this.m_depthCount++;
							flag = true;
							ExTraceGlobals.StateLockTracer.TraceDebug<string, int>((long)this.GetHashCode(), "TryEnterInternal {0}: fGotLock='true', m_depthCount='{1}'", this.m_databaseName, this.m_depthCount);
						}
					}
					else
					{
						ExTraceGlobals.StateLockTracer.TraceDebug<string>((long)this.GetHashCode(), "TryEnterInternal {0}: attemptOwner != this.CurrentOwner", this.m_databaseName);
						flag = this.Arbitrate(new LockOwner?(attemptOwner));
					}
				}
				else
				{
					if (attemptOwner != LockOwner.Suspend)
					{
						this.AddToPendingList(attemptOwner);
					}
					flag = this.Arbitrate(null);
				}
				lockOwner = new LockOwner?(lockOwner ?? this.CurrentOwner);
			}
			if (!flag && fWait)
			{
				if (actionBeforeWaitForLock != null)
				{
					actionBeforeWaitForLock();
				}
				flag = this.WaitForLock(attemptOwner, null, timeout ?? TimeSpan.FromMilliseconds((double)RegistryParameters.SuspendLockTimeoutInMsec));
				lockOwner = new LockOwner?(this.CurrentOwner);
				if (!flag)
				{
					this.RemovePendingLock(attemptOwner);
				}
			}
			currentOwner = lockOwner.Value;
			ExTraceGlobals.StateLockTracer.TraceDebug<string, bool, LockOwner>((long)this.GetHashCode(), "{0}: TryEnter returning, fGotLock={1}, currentOwner={2}", this.m_databaseName, flag, currentOwner);
			return flag;
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003CBB4 File Offset: 0x0003ADB4
		public void LeaveSuspend()
		{
			lock (this)
			{
				ExTraceGlobals.StateLockTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: LeaveSuspend()", this.m_databaseName);
				this.ClearSuspendWanted();
				if (this.CurrentOwner == LockOwner.Suspend)
				{
					this.Arbitrate(null);
				}
				DiagCore.RetailAssert(this.CurrentOwner != LockOwner.Suspend, "LeaveSuspend() failed to change current owner from Suspend.", new object[0]);
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0003CC44 File Offset: 0x0003AE44
		public void LeaveAttemptCopyLastLogs()
		{
			lock (this)
			{
				if (this.CurrentOwner == LockOwner.AttemptCopyLastLogs)
				{
					this.Leave(LockOwner.AttemptCopyLastLogs);
				}
			}
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0003CC8C File Offset: 0x0003AE8C
		public void Leave(LockOwner leavingOwner)
		{
			ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner>((long)this.GetHashCode(), "{0}: Leave(leavingOwner={1})", this.m_databaseName, leavingOwner);
			if (leavingOwner == LockOwner.Suspend)
			{
				throw new ArgumentException("leavingOwner");
			}
			lock (this)
			{
				if (this.CurrentOwner == leavingOwner && --this.m_depthCount == 0)
				{
					ExTraceGlobals.StateLockTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: leaving owner has lock, releasing it", this.m_databaseName);
					this.SetCurrentOwner(LockOwner.Idle);
					this.Arbitrate(null);
				}
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0003CD40 File Offset: 0x0003AF40
		public LockOwner CurrentOwner
		{
			get
			{
				return this.m_currentOwner;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0003CD48 File Offset: 0x0003AF48
		public TimeSpan LockHeldDuration
		{
			get
			{
				return DateTime.UtcNow.Subtract(this.m_lockAcquiredTimeUtc);
			}
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0003CD68 File Offset: 0x0003AF68
		public bool ShouldGiveUpLock()
		{
			LockOwner lockOwner;
			return this.ShouldGiveUpLock(out lockOwner);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003CD80 File Offset: 0x0003AF80
		public bool ShouldGiveUpLock(out LockOwner highestPending)
		{
			LockOwner owner;
			this.UpdateHighestPriorityPending(false, out owner, out highestPending);
			bool flag = this.Priority(owner) < this.Priority(highestPending);
			ExTraceGlobals.StateLockTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "{0}: ShouldGiveUpLock returns {1}", this.m_databaseName, flag);
			return flag;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003CDC8 File Offset: 0x0003AFC8
		public bool Arbitrate(LockOwner? attemptOwner)
		{
			ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner?>((long)this.GetHashCode(), "{0}: Arbitrate(attemptOwner = {1})", this.m_databaseName, attemptOwner);
			bool result;
			lock (this)
			{
				bool suspendWanted = this.SuspendWanted;
				if (this.CurrentOwner == LockOwner.Idle || (this.CurrentOwner == LockOwner.Suspend && !suspendWanted))
				{
					LockOwner lockOwner = this.HighestPriorityPending;
					bool flag2 = false;
					if (attemptOwner != null && (attemptOwner == LockOwner.Suspend || this.Priority(attemptOwner.Value) > this.Priority(lockOwner)))
					{
						lockOwner = attemptOwner.Value;
						flag2 = true;
						ExTraceGlobals.StateLockTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: AttemptOwner is highest priority", this.m_databaseName);
					}
					ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner>((long)this.GetHashCode(), "{0}: Highest priority pending is {1}", this.m_databaseName, lockOwner);
					if (lockOwner != this.CurrentOwner)
					{
						this.SetCurrentOwner(lockOwner);
						if (!flag2 && lockOwner != LockOwner.Suspend)
						{
							this.RemoveFromPendingList(lockOwner);
						}
					}
					ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner>((long)this.GetHashCode(), "{0}: CurrentOwner is now {1}", this.m_databaseName, lockOwner);
					result = flag2;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0003CF0C File Offset: 0x0003B10C
		private bool WaitForLock(LockOwner desiredLockOwner, WaitHandle cancelEvent, TimeSpan timeout)
		{
			ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner>((long)this.GetHashCode(), "{0}: WaitForLock(desiredLockOwner = {1})", this.m_databaseName, desiredLockOwner);
			bool flag = false;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			while (!flag)
			{
				if (desiredLockOwner != LockOwner.Suspend && this.CurrentOwner == LockOwner.Suspend)
				{
					flag = true;
					ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner>((long)this.GetHashCode(), "{0}: WaitForLock() returning 'false' because Suspend has the lock and '{1}' wants it.", this.m_databaseName, desiredLockOwner);
					break;
				}
				if (desiredLockOwner != LockOwner.AttemptCopyLastLogs && this.CurrentOwner == LockOwner.AttemptCopyLastLogs)
				{
					flag = true;
					ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner>((long)this.GetHashCode(), "{0}: WaitForLock() returning 'false' because AttemptCopyLastLogs has the lock and '{1}' wants it.", this.m_databaseName, desiredLockOwner);
					break;
				}
				if (desiredLockOwner == LockOwner.Suspend && !this.SuspendWanted)
				{
					flag = true;
					ExTraceGlobals.StateLockTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: WaitForLock() returning 'false' because SuspendWanted is no longer true.", this.m_databaseName);
					break;
				}
				if (this.CurrentOwner == desiredLockOwner)
				{
					ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner, long>((long)this.GetHashCode(), "{0}: WaitForLock() returning 'true' because '{1}' got the lock after {2}ms.", this.m_databaseName, desiredLockOwner, stopwatch.ElapsedMilliseconds);
					break;
				}
				TimeSpan elapsed = stopwatch.Elapsed;
				if (elapsed > timeout)
				{
					flag = true;
					ExTraceGlobals.StateLockTracer.TraceError((long)this.GetHashCode(), "{0}: WaitForLock() waited '{1}' to enter '{2}' and will now timeout. CurrentOwner = '{3}'", new object[]
					{
						this.m_databaseName,
						elapsed,
						desiredLockOwner,
						this.CurrentOwner
					});
					break;
				}
				if (cancelEvent != null)
				{
					flag = cancelEvent.WaitOne(100, false);
				}
				else
				{
					Thread.Sleep(100);
				}
			}
			ExTraceGlobals.StateLockTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "{0}: WaitForLock() returning {1}", this.m_databaseName, !flag);
			return !flag;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0003D0A8 File Offset: 0x0003B2A8
		private void RemovePendingLock(LockOwner attemptOwner)
		{
			lock (this)
			{
				ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner>((long)this.GetHashCode(), "{0}: RemovePendingLock({1})", this.m_databaseName, attemptOwner);
				if (attemptOwner == LockOwner.Suspend)
				{
					this.ClearSuspendWanted();
				}
				else
				{
					this.RemoveFromPendingList(attemptOwner);
				}
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003D110 File Offset: 0x0003B310
		private void ClearSuspendWanted()
		{
			ExTraceGlobals.StateLockTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: ClearSuspendWanted()", this.m_databaseName);
			this.SuspendWanted = false;
			this.UpdateHighestPriorityPending(false);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003D13C File Offset: 0x0003B33C
		private void AddToPendingList(LockOwner pendingOwner)
		{
			lock (this)
			{
				ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner>((long)this.GetHashCode(), "{0}: AddToPendingList({1})", this.m_databaseName, pendingOwner);
				this.m_pendingOwners.Add(pendingOwner);
				this.UpdateHighestPriorityPending(true);
			}
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0003D1A4 File Offset: 0x0003B3A4
		private void RemoveFromPendingList(LockOwner removingOwner)
		{
			lock (this)
			{
				ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner>((long)this.GetHashCode(), "{0}: RemoveFromPendingList({1})", this.m_databaseName, removingOwner);
				this.m_pendingOwners.Remove(removingOwner);
				this.UpdateHighestPriorityPending(true);
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003D20C File Offset: 0x0003B40C
		private void UpdateHighestPriorityPending(bool fPendingListChanged)
		{
			LockOwner lockOwner;
			LockOwner lockOwner2;
			this.UpdateHighestPriorityPending(fPendingListChanged, out lockOwner, out lockOwner2);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0003D224 File Offset: 0x0003B424
		private void UpdateHighestPriorityPending(bool fPendingListChanged, out LockOwner currentOwner, out LockOwner highestPriorityPending)
		{
			LockOwner lockOwner = LockOwner.Idle;
			highestPriorityPending = LockOwner.Idle;
			lock (this)
			{
				bool suspendWanted = this.SuspendWanted;
				currentOwner = this.CurrentOwner;
				if (suspendWanted && currentOwner != LockOwner.Suspend)
				{
					this.m_highestPriorityPending = LockOwner.Suspend;
				}
				else if (fPendingListChanged || (!suspendWanted && currentOwner == LockOwner.Suspend))
				{
					foreach (LockOwner lockOwner2 in this.m_pendingOwners)
					{
						if (this.Priority(lockOwner2) > this.Priority(lockOwner))
						{
							lockOwner = lockOwner2;
						}
					}
					this.m_highestPriorityPending = lockOwner;
				}
				highestPriorityPending = this.m_highestPriorityPending;
			}
			ExTraceGlobals.StateLockTracer.TraceDebug<string, LockOwner>((long)this.GetHashCode(), "{0}: m_highestPriorityPending = {1}", this.m_databaseName, this.m_highestPriorityPending);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003D310 File Offset: 0x0003B510
		private void SetCurrentOwner(LockOwner newCurrentOwner)
		{
			lock (this)
			{
				this.m_currentOwner = newCurrentOwner;
				this.m_lockAcquiredTimeUtc = DateTime.UtcNow;
				this.m_depthCount = 1;
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003D360 File Offset: 0x0003B560
		private int Priority(LockOwner owner)
		{
			return (int)(owner / LockOwner.Component);
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0003D366 File Offset: 0x0003B566
		private LockOwner HighestPriorityPending
		{
			get
			{
				this.UpdateHighestPriorityPending(false);
				return this.m_highestPriorityPending;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0003D375 File Offset: 0x0003B575
		public string Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		// Token: 0x040005C6 RID: 1478
		private string m_databaseName;

		// Token: 0x040005C7 RID: 1479
		private string m_identity;

		// Token: 0x040005C8 RID: 1480
		private LockOwner m_currentOwner;

		// Token: 0x040005C9 RID: 1481
		private DateTime m_lockAcquiredTimeUtc;

		// Token: 0x040005CA RID: 1482
		private int m_depthCount;

		// Token: 0x040005CB RID: 1483
		private List<LockOwner> m_pendingOwners = new List<LockOwner>();

		// Token: 0x040005CC RID: 1484
		private LockOwner m_highestPriorityPending;
	}
}
