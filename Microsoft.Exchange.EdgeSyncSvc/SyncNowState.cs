using System;
using System.Collections.Generic;
using Microsoft.Exchange.EdgeSync.Common;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000009 RID: 9
	internal class SyncNowState
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003DFC File Offset: 0x00001FFC
		public SyncNowState(EdgeSync edgeSync)
		{
			this.edgeSync = edgeSync;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003E16 File Offset: 0x00002016
		public bool Running
		{
			get
			{
				return this.running;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003E20 File Offset: 0x00002020
		public bool Start(string targetServer, bool forceFullSync, bool forceUpdateCookie)
		{
			bool result;
			lock (this)
			{
				this.AbandonStaleRun();
				if (this.running)
				{
					result = false;
				}
				else
				{
					this.running = true;
					this.pendingEdges = new Dictionary<SyncNowState.SyncResultId, EdgeServer>();
					this.results = new Queue<Status>();
					EdgeSyncTopologyPerfCounters.SyncNowPendingSyncs.RawValue = 0L;
					this.edgeSync.SynchronizeNow(targetServer, forceFullSync, forceUpdateCookie);
					EdgeSyncTopologyPerfCounters.SyncNowStarted.Increment();
					result = this.running;
				}
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003EB4 File Offset: 0x000020B4
		public void AddPendingEdge(string dn, EdgeServer edgeServer)
		{
			lock (this)
			{
				foreach (SyncTreeType type in edgeServer.Types)
				{
					this.pendingEdges.Add(new SyncNowState.SyncResultId(type, dn), edgeServer);
					EdgeSyncTopologyPerfCounters.SyncNowPendingSyncs.Increment();
				}
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003F3C File Offset: 0x0000213C
		public Status TryGetNextResult()
		{
			Status result;
			lock (this)
			{
				if (this.results.Count > 0)
				{
					result = this.results.Dequeue();
				}
				else
				{
					if (this.running && this.pendingEdges.Count == 0)
					{
						this.running = false;
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003FAC File Offset: 0x000021AC
		public virtual void RecordResult(Status status)
		{
			this.RecordResultInternal(status, true, false);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003FB7 File Offset: 0x000021B7
		public void RecordResultIntermediate(Status status)
		{
			this.RecordResultInternal(status, true, true);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003FC4 File Offset: 0x000021C4
		private void RecordResultInternal(Status status, bool requirePending, bool intermediate)
		{
			lock (this)
			{
				SyncNowState.SyncResultId key = new SyncNowState.SyncResultId(status.Type, status.Name);
				if (this.running)
				{
					bool flag2 = this.pendingEdges.ContainsKey(key);
					if (!requirePending || flag2)
					{
						if (flag2 && !intermediate)
						{
							this.pendingEdges.Remove(key);
							EdgeSyncTopologyPerfCounters.SyncNowPendingSyncs.Decrement();
						}
						this.results.Enqueue(status);
						this.lastResult = DateTime.UtcNow;
					}
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000405C File Offset: 0x0000225C
		private void AbandonStaleRun()
		{
			if (this.pendingEdges != null && this.pendingEdges.Count == 0 && DateTime.UtcNow - this.lastResult >= new TimeSpan(0, 1, 0))
			{
				this.running = false;
			}
		}

		// Token: 0x0400002A RID: 42
		private readonly EdgeSync edgeSync;

		// Token: 0x0400002B RID: 43
		private bool running;

		// Token: 0x0400002C RID: 44
		private DateTime lastResult = DateTime.MinValue;

		// Token: 0x0400002D RID: 45
		private Dictionary<SyncNowState.SyncResultId, EdgeServer> pendingEdges;

		// Token: 0x0400002E RID: 46
		private Queue<Status> results;

		// Token: 0x0200000A RID: 10
		private struct SyncResultId
		{
			// Token: 0x0600004F RID: 79 RVA: 0x00004099 File Offset: 0x00002299
			public SyncResultId(SyncTreeType type, string dn)
			{
				this.type = type;
				this.dn = dn;
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000050 RID: 80 RVA: 0x000040A9 File Offset: 0x000022A9
			public SyncTreeType Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000051 RID: 81 RVA: 0x000040B1 File Offset: 0x000022B1
			public string Dn
			{
				get
				{
					return this.dn;
				}
			}

			// Token: 0x0400002F RID: 47
			private SyncTreeType type;

			// Token: 0x04000030 RID: 48
			private string dn;
		}
	}
}
