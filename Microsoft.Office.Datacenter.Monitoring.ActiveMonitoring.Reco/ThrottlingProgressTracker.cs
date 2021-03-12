using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200004C RID: 76
	internal class ThrottlingProgressTracker
	{
		// Token: 0x06000333 RID: 819 RVA: 0x0000B534 File Offset: 0x00009734
		internal ThrottlingProgressTracker(long instanceId, RecoveryActionId actionId, string resourceName, string requesterName, TimeSpan timeout)
		{
			this.InstanceId = instanceId;
			this.actionId = actionId;
			this.resourceName = resourceName;
			this.requesterName = requesterName;
			this.timeout = timeout;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000B561 File Offset: 0x00009761
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000B569 File Offset: 0x00009769
		internal long InstanceId { get; private set; }

		// Token: 0x06000336 RID: 822 RVA: 0x0000B574 File Offset: 0x00009774
		internal void MarkBegin()
		{
			DateTime localTime = ExDateTime.Now.LocalTime;
			RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo progressInfo = new RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo
			{
				InstanceId = this.InstanceId,
				ActionId = this.actionId,
				ResourceName = this.resourceName,
				RequesterName = this.requesterName,
				OperationStartTime = localTime,
				OperationExpectedEndTime = localTime + this.timeout,
				WorkerStartTime = RecoveryActionHelper.CurrentProcessStartTime
			};
			RpcSetThrottlingInProgressImpl.Reply reply = Dependencies.LamRpc.SetThrottlingInProgress(Dependencies.ThrottleHelper.Tunables.LocalMachineName, progressInfo, false, false, 30000);
			if (reply.IsSuccess)
			{
				this.current = progressInfo;
				this.isBeginMarked = true;
				return;
			}
			throw new ThrottlingInProgressException(this.InstanceId, this.actionId.ToString(), this.resourceName, this.requesterName, reply.CurrentProgressInfo.RequesterName, reply.CurrentProgressInfo.OperationStartTime, reply.CurrentProgressInfo.OperationExpectedEndTime);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000B670 File Offset: 0x00009870
		internal void MarkEnd()
		{
			if (!this.isBeginMarked || this.isEndMarked)
			{
				return;
			}
			RpcSetThrottlingInProgressImpl.Reply reply = Dependencies.LamRpc.SetThrottlingInProgress(Dependencies.ThrottleHelper.Tunables.LocalMachineName, this.current, true, false, 30000);
			if (reply.IsSuccess)
			{
				this.isEndMarked = true;
				return;
			}
			throw new ThrottlingOverlapException(this.current.InstanceId, reply.CurrentProgressInfo.InstanceId, this.current.RequesterName, reply.CurrentProgressInfo.RequesterName, this.current.OperationStartTime, reply.CurrentProgressInfo.OperationStartTime);
		}

		// Token: 0x040001EE RID: 494
		private readonly RecoveryActionId actionId;

		// Token: 0x040001EF RID: 495
		private readonly string resourceName;

		// Token: 0x040001F0 RID: 496
		private readonly string requesterName;

		// Token: 0x040001F1 RID: 497
		private readonly TimeSpan timeout;

		// Token: 0x040001F2 RID: 498
		private bool isBeginMarked;

		// Token: 0x040001F3 RID: 499
		private bool isEndMarked;

		// Token: 0x040001F4 RID: 500
		private RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo current;
	}
}
