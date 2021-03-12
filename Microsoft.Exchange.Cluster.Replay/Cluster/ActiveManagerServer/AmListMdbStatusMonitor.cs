using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000050 RID: 80
	internal class AmListMdbStatusMonitor
	{
		// Token: 0x0600037A RID: 890 RVA: 0x000136F4 File Offset: 0x000118F4
		public void SetTestKillStoreDelegate(AmListMdbStatusMonitor.KillStoreDelegate killStore)
		{
			this.killStoreDelegate = killStore;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600037B RID: 891 RVA: 0x000136FD File Offset: 0x000118FD
		public static AmListMdbStatusMonitor Instance
		{
			get
			{
				if (AmListMdbStatusMonitor.sm_instance == null)
				{
					AmListMdbStatusMonitor.sm_instance = new AmListMdbStatusMonitor();
				}
				return AmListMdbStatusMonitor.sm_instance;
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00013718 File Offset: 0x00011918
		public void RecordFailure(AmServerName failingServer)
		{
			if (RegistryParameters.ListMdbStatusMonitorDisabled)
			{
				AmListMdbStatusMonitor.Tracer.TraceDebug(0L, "AmListMdbStatusMonitor disabled via regkey");
				return;
			}
			AmListMdbStatusMonitor.StatusRecord statusRecord = null;
			if (this.records.TryGetValue(failingServer, out statusRecord))
			{
				statusRecord.FailCount++;
				if (statusRecord.TimeOfFirstFailure == null)
				{
					statusRecord.TimeOfFirstFailure = new DateTime?(DateTime.UtcNow);
					return;
				}
				if (DateTime.UtcNow > statusRecord.TimeOfFirstFailure.Value + this.FailureSuppressionWindow)
				{
					this.AttemptRecovery(statusRecord);
					return;
				}
			}
			else
			{
				statusRecord = new AmListMdbStatusMonitor.StatusRecord();
				statusRecord.ServerName = failingServer;
				statusRecord.FailCount = 1;
				statusRecord.TimeOfFirstFailure = new DateTime?(DateTime.UtcNow);
				this.records[failingServer] = statusRecord;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000137E0 File Offset: 0x000119E0
		public void RecordSuccess(AmServerName succeedingServer)
		{
			AmListMdbStatusMonitor.StatusRecord statusRecord = null;
			if (this.records.TryGetValue(succeedingServer, out statusRecord))
			{
				statusRecord.FailCount = 0;
				statusRecord.TimeOfFirstFailure = null;
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001383C File Offset: 0x00011A3C
		private void AttemptRecovery(AmListMdbStatusMonitor.StatusRecord rec)
		{
			DateTime curTime = DateTime.UtcNow;
			if (rec.TimeOfLastRecoveryAction == null || curTime > rec.TimeOfLastRecoveryAction.Value + TimeSpan.FromSeconds((double)RegistryParameters.ListMdbStatusRecoveryLimitInSec))
			{
				rec.TimeOfLastRecoveryAction = new DateTime?(curTime);
				if (rec.ServerName.IsLocalComputerName)
				{
					ReplayCrimsonEvents.AmListMdbStatusMonitorLocalRecoveryStarts.Log();
					ThreadPool.QueueUserWorkItem(delegate(object param0)
					{
						this.killStoreDelegate(curTime, "ListMdbStatusMonitor");
					});
					return;
				}
			}
			else
			{
				AmListMdbStatusMonitor.Tracer.TraceDebug(0L, "AmListMdbStatusMonitor skipped recovery");
			}
		}

		// Token: 0x04000191 RID: 401
		public static readonly Trace Tracer = ExTraceGlobals.ActiveManagerTracer;

		// Token: 0x04000192 RID: 402
		private static AmListMdbStatusMonitor sm_instance;

		// Token: 0x04000193 RID: 403
		private AmListMdbStatusMonitor.KillStoreDelegate killStoreDelegate = new AmListMdbStatusMonitor.KillStoreDelegate(AmStoreServiceMonitor.KillStoreIfRunningBefore);

		// Token: 0x04000194 RID: 404
		private Dictionary<AmServerName, AmListMdbStatusMonitor.StatusRecord> records = new Dictionary<AmServerName, AmListMdbStatusMonitor.StatusRecord>();

		// Token: 0x04000195 RID: 405
		public TimeSpan FailureSuppressionWindow = TimeSpan.FromSeconds((double)RegistryParameters.ListMdbStatusFailureSuppressionWindowInSec);

		// Token: 0x02000051 RID: 81
		// (Invoke) Token: 0x06000382 RID: 898
		public delegate Exception KillStoreDelegate(DateTime limitTimeUtc, string reason);

		// Token: 0x02000052 RID: 82
		private class StatusRecord
		{
			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000385 RID: 901 RVA: 0x00013931 File Offset: 0x00011B31
			// (set) Token: 0x06000386 RID: 902 RVA: 0x00013939 File Offset: 0x00011B39
			public AmServerName ServerName { get; set; }

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000387 RID: 903 RVA: 0x00013942 File Offset: 0x00011B42
			// (set) Token: 0x06000388 RID: 904 RVA: 0x0001394A File Offset: 0x00011B4A
			public DateTime? TimeOfLastRecoveryAction { get; set; }

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06000389 RID: 905 RVA: 0x00013953 File Offset: 0x00011B53
			// (set) Token: 0x0600038A RID: 906 RVA: 0x0001395B File Offset: 0x00011B5B
			public DateTime? TimeOfFirstFailure { get; set; }

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600038B RID: 907 RVA: 0x00013964 File Offset: 0x00011B64
			// (set) Token: 0x0600038C RID: 908 RVA: 0x0001396C File Offset: 0x00011B6C
			public int FailCount { get; set; }
		}
	}
}
