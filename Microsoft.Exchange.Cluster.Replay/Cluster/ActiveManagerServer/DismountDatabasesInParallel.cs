using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200006E RID: 110
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DismountDatabasesInParallel
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x000194A8 File Offset: 0x000176A8
		public DismountDatabasesInParallel(MdbStatus[] mdbStatuses)
		{
			this.m_mdbStatuses = mdbStatuses;
			this.m_totalRequests = mdbStatuses.Length;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000194CC File Offset: 0x000176CC
		public bool Execute(int waitTimeoutMs, string hint)
		{
			bool flag = false;
			Stopwatch stopwatch = new Stopwatch();
			AmStoreHelper.DismountDelegate dismountDelegate = new AmStoreHelper.DismountDelegate(AmStoreHelper.RemoteDismount);
			this.m_completedEvent = new ManualResetEvent(false);
			stopwatch.Start();
			DateTime utcNow = DateTime.UtcNow;
			try
			{
				ReplayCrimsonEvents.ForceDismountingDatabases.Log<AmServerName, string>(AmServerName.LocalComputerName, hint);
				if (this.m_mdbStatuses != null && this.m_mdbStatuses.Length > 0)
				{
					AmTrace.Debug("DismountDatabasesInParallel.Execute() now starting with timeout of {0} ms...", new object[]
					{
						waitTimeoutMs
					});
					foreach (MdbStatus mdbStatus in this.m_mdbStatuses)
					{
						DismountDatabasesInParallel.AsyncDismountState @object = new DismountDatabasesInParallel.AsyncDismountState(mdbStatus.MdbGuid, dismountDelegate);
						dismountDelegate.BeginInvoke(null, mdbStatus.MdbGuid, UnmountFlags.SkipCacheFlush, false, new AsyncCallback(this.DismountCompletionCallback), @object);
					}
					if (this.m_completedEvent.WaitOne(waitTimeoutMs))
					{
						AmTrace.Debug("DismountDatabasesInParallel.Execute() finished dismounting DBs in {0} ms.", new object[]
						{
							stopwatch.ElapsedMilliseconds
						});
						flag = true;
					}
					else
					{
						AmTrace.Error("DismountDatabasesInParallel.Execute() timed out waiting for DBs to finish dismounting.", new object[0]);
						AmStoreServiceMonitor.KillStoreIfRunningBefore(utcNow, "DismountDatabasesInParallel");
					}
				}
			}
			finally
			{
				ReplayCrimsonEvents.ForceDismountAllDatabasesComplete.Log<bool>(flag);
				lock (this.m_locker)
				{
					this.m_completedEvent.Close();
					this.m_completedEvent = null;
				}
			}
			return flag;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00019670 File Offset: 0x00017870
		private void DismountCompletionCallback(IAsyncResult ar)
		{
			DismountDatabasesInParallel.AsyncDismountState state = (DismountDatabasesInParallel.AsyncDismountState)ar.AsyncState;
			Exception ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
			{
				state.DismountFunction.EndInvoke(ar);
			});
			if (ex == null)
			{
				AmTrace.Debug("DismountDatabasesInParallel: Finished dismount for DB {0}.", new object[]
				{
					state.MdbGuid
				});
			}
			else
			{
				AmTrace.Debug("DismountDatabasesInParallel: Dismount for DB {0} failed with {1}.", new object[]
				{
					state.MdbGuid,
					ex.Message
				});
			}
			lock (this.m_locker)
			{
				this.m_completedCount++;
				if (this.m_completedCount == this.m_totalRequests && this.m_completedEvent != null)
				{
					this.m_completedEvent.Set();
				}
			}
		}

		// Token: 0x040001F6 RID: 502
		private readonly int m_totalRequests;

		// Token: 0x040001F7 RID: 503
		private readonly MdbStatus[] m_mdbStatuses;

		// Token: 0x040001F8 RID: 504
		private int m_completedCount;

		// Token: 0x040001F9 RID: 505
		private ManualResetEvent m_completedEvent;

		// Token: 0x040001FA RID: 506
		private object m_locker = new object();

		// Token: 0x0200006F RID: 111
		private class AsyncDismountState
		{
			// Token: 0x060004B3 RID: 1203 RVA: 0x00019770 File Offset: 0x00017970
			public AsyncDismountState(Guid mdbGuid, AmStoreHelper.DismountDelegate dismountFunc)
			{
				this.MdbGuid = mdbGuid;
				this.DismountFunction = dismountFunc;
			}

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00019786 File Offset: 0x00017986
			// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0001978E File Offset: 0x0001798E
			public Guid MdbGuid { get; private set; }

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00019797 File Offset: 0x00017997
			// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0001979F File Offset: 0x0001799F
			public AmStoreHelper.DismountDelegate DismountFunction { get; private set; }
		}
	}
}
