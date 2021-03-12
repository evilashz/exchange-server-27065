using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Net.DiagnosticsAggregation;

namespace Microsoft.Exchange.Servicelets.DiagnosticsAggregation
{
	// Token: 0x0200000F RID: 15
	internal class ServerQueuesSnapshot
	{
		// Token: 0x0600006B RID: 107 RVA: 0x0000405C File Offset: 0x0000225C
		public ServerQueuesSnapshot(ADObjectId localServer)
		{
			if (localServer == null)
			{
				throw new ArgumentNullException("localServer");
			}
			this.serverIdentity = localServer;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004084 File Offset: 0x00002284
		public ServerQueuesSnapshot(ADObjectId localServer, IEnumerable<LocalQueueInfo> queues, DateTime timeStampOfQueues)
		{
			if (localServer == null)
			{
				throw new ArgumentNullException("localServer");
			}
			if (queues == null)
			{
				throw new ArgumentNullException("queues");
			}
			this.serverIdentity = localServer;
			this.UpdateSuccess(queues, timeStampOfQueues);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000040C2 File Offset: 0x000022C2
		public IEnumerable<LocalQueueInfo> Queues
		{
			get
			{
				if (this.queues == null)
				{
					throw new InvalidOperationException("queues has not been set yet");
				}
				return this.queues;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000040DD File Offset: 0x000022DD
		public DateTime TimeStampOfQueues
		{
			get
			{
				if (this.timeStampOfQueues == null)
				{
					throw new InvalidOperationException("timeStampOfQueues has not been set yet");
				}
				return this.timeStampOfQueues.Value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004102 File Offset: 0x00002302
		public string LastError
		{
			get
			{
				return this.lastError;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000410C File Offset: 0x0000230C
		public void UpdateSuccess(LocalViewResponse response)
		{
			this.queues = response.QueueLocalViewResponse.LocalQueues;
			this.timeStampOfQueues = new DateTime?(response.QueueLocalViewResponse.Timestamp);
			this.timeOfLastSuccess = response.ServerSnapshotStatus.TimeOfLastSuccess;
			this.timeOfLastFailure = response.ServerSnapshotStatus.TimeOfLastFailure;
			this.lastError = response.ServerSnapshotStatus.LastError;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004173 File Offset: 0x00002373
		public void UpdateSuccess(IEnumerable<LocalQueueInfo> localQueues, DateTime timeStampOfQueues)
		{
			this.queues = localQueues;
			this.timeStampOfQueues = new DateTime?(timeStampOfQueues);
			this.timeOfLastSuccess = new DateTime?(DateTime.UtcNow);
			this.timeOfLastFailure = null;
			this.lastError = string.Empty;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000041AF File Offset: 0x000023AF
		public void UpdateFailure(string errorMessage)
		{
			this.lastError = errorMessage;
			this.timeOfLastFailure = new DateTime?(DateTime.UtcNow);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000041C8 File Offset: 0x000023C8
		public void SetAsFailed(string errorMessage)
		{
			this.queues = null;
			this.timeStampOfQueues = null;
			this.timeOfLastSuccess = null;
			this.timeOfLastFailure = new DateTime?(DateTime.UtcNow);
			this.lastError = errorMessage;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004200 File Offset: 0x00002400
		public ServerSnapshotStatus GetServerSnapshotStatus()
		{
			return new ServerSnapshotStatus(this.serverIdentity.ToString())
			{
				TimeOfLastSuccess = this.timeOfLastSuccess,
				TimeOfLastFailure = this.timeOfLastFailure,
				LastError = this.lastError
			};
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004244 File Offset: 0x00002444
		public ServerQueuesSnapshot Clone()
		{
			return new ServerQueuesSnapshot(this.serverIdentity)
			{
				queues = this.queues,
				timeStampOfQueues = this.timeStampOfQueues,
				timeOfLastSuccess = this.timeOfLastSuccess,
				timeOfLastFailure = this.timeOfLastFailure,
				lastError = this.lastError
			};
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000429A File Offset: 0x0000249A
		public bool IsEmpty()
		{
			return this.queues == null;
		}

		// Token: 0x04000052 RID: 82
		private readonly ADObjectId serverIdentity;

		// Token: 0x04000053 RID: 83
		private IEnumerable<LocalQueueInfo> queues;

		// Token: 0x04000054 RID: 84
		private DateTime? timeStampOfQueues;

		// Token: 0x04000055 RID: 85
		private DateTime? timeOfLastSuccess;

		// Token: 0x04000056 RID: 86
		private DateTime? timeOfLastFailure;

		// Token: 0x04000057 RID: 87
		private string lastError = string.Empty;
	}
}
