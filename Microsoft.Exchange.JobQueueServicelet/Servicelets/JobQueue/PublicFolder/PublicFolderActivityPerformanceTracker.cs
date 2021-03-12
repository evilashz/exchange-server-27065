using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Servicelets.JobQueue.PublicFolder
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PublicFolderActivityPerformanceTracker : PerformanceTrackerBase
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002710 File Offset: 0x00000910
		public PublicFolderActivityPerformanceTracker(SyncActivity trackedActivity, FolderOperationCounter folderOperationCounter, SyncStateCounter syncStateCounter, LatencyInfo mrsProxyLatencyInfo, PerformanceDataProvider transientRetryDelayTracker)
		{
			ArgumentValidator.ThrowIfNull("folderOperationCounter", folderOperationCounter);
			ArgumentValidator.ThrowIfNull("syncStateCounter", syncStateCounter);
			ArgumentValidator.ThrowIfNull("mrsProxyLatencyInfo", mrsProxyLatencyInfo);
			ArgumentValidator.ThrowIfNull("transientRetryDelayTracker", transientRetryDelayTracker);
			this.folderOperationCounter = folderOperationCounter;
			this.syncStateCounter = syncStateCounter;
			this.transientRetryDelayTracker = transientRetryDelayTracker;
			this.trackedActivity = trackedActivity;
			this.mrsProxyLatencyInfo = mrsProxyLatencyInfo;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002778 File Offset: 0x00000978
		public override void Start()
		{
			base.Start();
			this.invokeCount += 1U;
			this.startFoldersAdded = this.folderOperationCounter.Added;
			this.startFoldersUpdated = this.folderOperationCounter.Updated;
			this.startFoldersDeleted = this.folderOperationCounter.Deleted;
			this.startOrphanFoldersDetected = this.folderOperationCounter.OrphanDetected;
			this.startOrphanFoldersFixed = this.folderOperationCounter.OrphanFixed;
			this.startParentChainMissing = this.folderOperationCounter.ParentChainMissing;
			this.startSyncStateBytesReceived = this.syncStateCounter.BytesReceived;
			this.startSyncStateBytesSent = this.syncStateCounter.BytesSent;
			this.startWebServiceCount = this.mrsProxyLatencyInfo.TotalNumberOfRemoteCalls;
			this.startWebServiceDuration = this.mrsProxyLatencyInfo.TotalRemoteCallDuration;
			this.startTransientRetryDelayCount = this.transientRetryDelayTracker.RequestCount;
			this.startTransientRetryDelayLatency = this.transientRetryDelayTracker.Latency;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002868 File Offset: 0x00000A68
		public override void Stop()
		{
			base.Stop();
			this.foldersAdded += this.folderOperationCounter.Added - this.startFoldersAdded;
			this.foldersUpdated += this.folderOperationCounter.Updated - this.startFoldersUpdated;
			this.foldersDeleted += this.folderOperationCounter.Deleted - this.startFoldersDeleted;
			this.orphanFoldersDetected += this.folderOperationCounter.OrphanDetected - this.startOrphanFoldersDetected;
			this.orphanFoldersFixed += this.folderOperationCounter.OrphanFixed - this.startOrphanFoldersFixed;
			this.parentChainMissing += this.folderOperationCounter.ParentChainMissing - this.startParentChainMissing;
			this.syncStateBytesReceived += this.syncStateCounter.BytesReceived - this.startSyncStateBytesReceived;
			this.syncStateBytesSent += this.syncStateCounter.BytesSent - this.startSyncStateBytesSent;
			this.webServiceCount += this.mrsProxyLatencyInfo.TotalNumberOfRemoteCalls - this.startWebServiceCount;
			this.webServiceLatency += this.mrsProxyLatencyInfo.TotalRemoteCallDuration - this.startWebServiceDuration;
			this.transientRetryDelayCount += this.transientRetryDelayTracker.RequestCount - this.startTransientRetryDelayCount;
			this.transientRetryDelayLatency += this.transientRetryDelayTracker.Latency - this.startTransientRetryDelayLatency;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A00 File Offset: 0x00000C00
		public void AppendLogData(StringBuilder logData)
		{
			logData.Append("Activity=");
			logData.Append(this.trackedActivity);
			logData.Append(";CPU=");
			logData.Append(base.CpuTime.TotalMilliseconds);
			logData.Append(";DC=");
			logData.Append(base.DirectoryCount);
			logData.Append(";DL=");
			logData.Append(base.DirectoryLatency.TotalMilliseconds);
			logData.Append(";E=");
			logData.Append(base.ElapsedTime.TotalMilliseconds);
			logData.Append(";RPCC=");
			logData.Append(base.StoreRpcCount);
			logData.Append(";RPCL=");
			logData.Append(base.StoreRpcLatency.TotalMilliseconds);
			logData.Append(";C=");
			logData.Append(this.invokeCount);
			logData.Append(";FA=");
			logData.Append(this.foldersAdded);
			logData.Append(";FU=");
			logData.Append(this.foldersUpdated);
			logData.Append(";FD=");
			logData.Append(this.foldersDeleted);
			logData.Append(";SSR=");
			logData.Append(this.syncStateBytesReceived);
			logData.Append(";SSS=");
			logData.Append(this.syncStateBytesSent);
			logData.Append(";WC=");
			logData.Append(this.webServiceCount);
			logData.Append(";WL=");
			logData.Append(this.webServiceLatency.TotalMilliseconds);
			logData.Append(";TRDC=");
			logData.Append(this.transientRetryDelayCount);
			logData.Append(";TRDL=");
			logData.Append(this.transientRetryDelayLatency.TotalMilliseconds);
			logData.Append(";OD=");
			logData.Append(this.orphanFoldersDetected);
			logData.Append(";OF=");
			logData.Append(this.orphanFoldersFixed);
			logData.Append(";MPC=");
			logData.Append(this.parentChainMissing);
			logData.Append(";");
		}

		// Token: 0x04000019 RID: 25
		private readonly FolderOperationCounter folderOperationCounter;

		// Token: 0x0400001A RID: 26
		private readonly SyncStateCounter syncStateCounter;

		// Token: 0x0400001B RID: 27
		private readonly PerformanceDataProvider transientRetryDelayTracker;

		// Token: 0x0400001C RID: 28
		private readonly SyncActivity trackedActivity;

		// Token: 0x0400001D RID: 29
		private readonly LatencyInfo mrsProxyLatencyInfo;

		// Token: 0x0400001E RID: 30
		private int startFoldersAdded;

		// Token: 0x0400001F RID: 31
		private int startFoldersUpdated;

		// Token: 0x04000020 RID: 32
		private int startFoldersDeleted;

		// Token: 0x04000021 RID: 33
		private int startOrphanFoldersDetected;

		// Token: 0x04000022 RID: 34
		private int startOrphanFoldersFixed;

		// Token: 0x04000023 RID: 35
		private int startParentChainMissing;

		// Token: 0x04000024 RID: 36
		private long startSyncStateBytesReceived;

		// Token: 0x04000025 RID: 37
		private long startSyncStateBytesSent;

		// Token: 0x04000026 RID: 38
		private int startWebServiceCount;

		// Token: 0x04000027 RID: 39
		private TimeSpan startWebServiceDuration;

		// Token: 0x04000028 RID: 40
		private uint invokeCount;

		// Token: 0x04000029 RID: 41
		private int foldersAdded;

		// Token: 0x0400002A RID: 42
		private int foldersUpdated;

		// Token: 0x0400002B RID: 43
		private int foldersDeleted;

		// Token: 0x0400002C RID: 44
		private int orphanFoldersDetected;

		// Token: 0x0400002D RID: 45
		private int orphanFoldersFixed;

		// Token: 0x0400002E RID: 46
		private int parentChainMissing;

		// Token: 0x0400002F RID: 47
		private uint startTransientRetryDelayCount;

		// Token: 0x04000030 RID: 48
		private TimeSpan startTransientRetryDelayLatency;

		// Token: 0x04000031 RID: 49
		private uint transientRetryDelayCount;

		// Token: 0x04000032 RID: 50
		private TimeSpan transientRetryDelayLatency;

		// Token: 0x04000033 RID: 51
		private TimeSpan webServiceLatency;

		// Token: 0x04000034 RID: 52
		private int webServiceCount;

		// Token: 0x04000035 RID: 53
		private long syncStateBytesReceived;

		// Token: 0x04000036 RID: 54
		private long syncStateBytesSent;
	}
}
