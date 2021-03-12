using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Net.DiagnosticsAggregation;
using Microsoft.Exchange.Transport.DiagnosticsAggregationService;

namespace Microsoft.Exchange.Servicelets.DiagnosticsAggregation
{
	// Token: 0x0200000E RID: 14
	internal class LocalQueuesDataProvider : ILocalQueuesDataProvider
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00003F8F File Offset: 0x0000218F
		public LocalQueuesDataProvider(DiagnosticsAggregationLog log, ADObjectId localServerId)
		{
			this.log = log;
			this.localServerId = localServerId;
			this.serverQueuesSnapshot = new ServerQueuesSnapshot(localServerId);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003FB1 File Offset: 0x000021B1
		public void Start()
		{
			this.RefreshLocalServerQueues();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003FB9 File Offset: 0x000021B9
		public void Stop()
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003FBB File Offset: 0x000021BB
		public ADObjectId GetLocalServerId()
		{
			return this.localServerId;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003FC3 File Offset: 0x000021C3
		public ServerQueuesSnapshot GetLocalServerQueues()
		{
			this.RefreshLocalServerQueues();
			return this.serverQueuesSnapshot;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003FD4 File Offset: 0x000021D4
		private void RefreshLocalServerQueues()
		{
			QueueAggregationInfo queueAggregationInfo;
			Exception ex;
			bool flag = DiagnosticsAggregationHelper.TryGetParsedQueueInfo(DiagnosticsAggregationServicelet.LocalServer.QueueLogPath, out queueAggregationInfo, out ex);
			if (flag)
			{
				this.log.Log(DiagnosticsAggregationEvent.QueueSnapshotFileReadSucceeded, string.Empty, new object[0]);
				this.serverQueuesSnapshot.UpdateSuccess(queueAggregationInfo.QueueInfo, queueAggregationInfo.Time);
				return;
			}
			this.log.Log(DiagnosticsAggregationEvent.QueueSnapshotFileReadFailed, "Refreshing local queue information failed. Details {0}", new object[]
			{
				ex.Message
			});
			this.serverQueuesSnapshot.UpdateFailure(ex.Message);
		}

		// Token: 0x0400004F RID: 79
		private DiagnosticsAggregationLog log;

		// Token: 0x04000050 RID: 80
		private ADObjectId localServerId;

		// Token: 0x04000051 RID: 81
		private ServerQueuesSnapshot serverQueuesSnapshot;
	}
}
