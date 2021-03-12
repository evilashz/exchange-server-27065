using System;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.ActiveMonitoring;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200001D RID: 29
	public static class RpcSetThrottlingInProgressImpl
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x0000444C File Offset: 0x0000264C
		public static RpcSetThrottlingInProgressImpl.Reply SendRequest(string serverName, RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo progressInfo, bool isClear, bool isForce = false, int timeoutInMsec = 30000)
		{
			RpcSetThrottlingInProgressImpl.Request attachedRequest = new RpcSetThrottlingInProgressImpl.Request
			{
				ProgressInfo = progressInfo,
				IsClear = isClear,
				IsForce = isForce
			};
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(attachedRequest, ActiveMonitoringGenericRpcCommandId.SetThrottlingInProgress, 1, 0);
			return ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcSetThrottlingInProgressImpl.Reply>(requestInfo, serverName, timeoutInMsec);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000448C File Offset: 0x0000268C
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcSetThrottlingInProgressImpl.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcSetThrottlingInProgressImpl.Request>(requestInfo, 1, 0);
			RpcSetThrottlingInProgressImpl.Reply attachedReply = RecoveryActionsRepository.Instance.UpdateThrottlingProgress(request);
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, attachedReply, 1, 0);
		}

		// Token: 0x0400007E RID: 126
		public const int MajorVersion = 1;

		// Token: 0x0400007F RID: 127
		public const int MinorVersion = 0;

		// Token: 0x04000080 RID: 128
		internal const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.SetThrottlingInProgress;

		// Token: 0x0200001E RID: 30
		[Serializable]
		public class Reply
		{
			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060000E2 RID: 226 RVA: 0x000044B9 File Offset: 0x000026B9
			// (set) Token: 0x060000E3 RID: 227 RVA: 0x000044C1 File Offset: 0x000026C1
			public bool IsSuccess { get; set; }

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060000E4 RID: 228 RVA: 0x000044CA File Offset: 0x000026CA
			// (set) Token: 0x060000E5 RID: 229 RVA: 0x000044D2 File Offset: 0x000026D2
			public bool IsThrottlingAlreadyInProgress { get; set; }

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060000E6 RID: 230 RVA: 0x000044DB File Offset: 0x000026DB
			// (set) Token: 0x060000E7 RID: 231 RVA: 0x000044E3 File Offset: 0x000026E3
			public RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo CurrentProgressInfo { get; set; }
		}

		// Token: 0x0200001F RID: 31
		[Serializable]
		public class Request
		{
			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060000E9 RID: 233 RVA: 0x000044F4 File Offset: 0x000026F4
			// (set) Token: 0x060000EA RID: 234 RVA: 0x000044FC File Offset: 0x000026FC
			public RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo ProgressInfo { get; set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060000EB RID: 235 RVA: 0x00004505 File Offset: 0x00002705
			// (set) Token: 0x060000EC RID: 236 RVA: 0x0000450D File Offset: 0x0000270D
			public bool IsClear { get; set; }

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x060000ED RID: 237 RVA: 0x00004516 File Offset: 0x00002716
			// (set) Token: 0x060000EE RID: 238 RVA: 0x0000451E File Offset: 0x0000271E
			public bool IsForce { get; set; }
		}

		// Token: 0x02000020 RID: 32
		[Serializable]
		public class ThrottlingProgressInfo
		{
			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000452F File Offset: 0x0000272F
			// (set) Token: 0x060000F1 RID: 241 RVA: 0x00004537 File Offset: 0x00002737
			internal long InstanceId { get; set; }

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004540 File Offset: 0x00002740
			// (set) Token: 0x060000F3 RID: 243 RVA: 0x00004548 File Offset: 0x00002748
			internal RecoveryActionId ActionId { get; set; }

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004551 File Offset: 0x00002751
			// (set) Token: 0x060000F5 RID: 245 RVA: 0x00004559 File Offset: 0x00002759
			internal string ResourceName { get; set; }

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004562 File Offset: 0x00002762
			// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000456A File Offset: 0x0000276A
			internal string RequesterName { get; set; }

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004574 File Offset: 0x00002774
			// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000458F File Offset: 0x0000278F
			internal DateTime OperationStartTime
			{
				get
				{
					return this.OperationStartTimeUtc.ToLocalTime();
				}
				set
				{
					this.OperationStartTimeUtc = value.ToUniversalTime();
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060000FA RID: 250 RVA: 0x0000459E File Offset: 0x0000279E
			// (set) Token: 0x060000FB RID: 251 RVA: 0x000045A6 File Offset: 0x000027A6
			internal DateTime OperationStartTimeUtc { get; set; }

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x060000FC RID: 252 RVA: 0x000045B0 File Offset: 0x000027B0
			// (set) Token: 0x060000FD RID: 253 RVA: 0x000045CB File Offset: 0x000027CB
			internal DateTime OperationExpectedEndTime
			{
				get
				{
					return this.OperationExpectedEndTimeUtc.ToLocalTime();
				}
				set
				{
					this.OperationExpectedEndTimeUtc = value.ToUniversalTime();
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x060000FE RID: 254 RVA: 0x000045DA File Offset: 0x000027DA
			// (set) Token: 0x060000FF RID: 255 RVA: 0x000045E2 File Offset: 0x000027E2
			internal DateTime OperationExpectedEndTimeUtc { get; set; }

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000100 RID: 256 RVA: 0x000045EC File Offset: 0x000027EC
			// (set) Token: 0x06000101 RID: 257 RVA: 0x00004607 File Offset: 0x00002807
			internal DateTime WorkerStartTime
			{
				get
				{
					return this.WorkerStartTimeUtc.ToLocalTime();
				}
				set
				{
					this.WorkerStartTimeUtc = value.ToUniversalTime();
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000102 RID: 258 RVA: 0x00004616 File Offset: 0x00002816
			// (set) Token: 0x06000103 RID: 259 RVA: 0x0000461E File Offset: 0x0000281E
			internal DateTime WorkerStartTimeUtc { get; set; }

			// Token: 0x06000104 RID: 260 RVA: 0x00004628 File Offset: 0x00002828
			internal bool IsInProgress(DateTime workerProcessStartTime)
			{
				return !string.IsNullOrEmpty(this.RequesterName) && !(this.OperationStartTime == DateTime.MinValue) && !(this.OperationExpectedEndTime == DateTime.MinValue) && !(ExDateTime.Now.LocalTime > this.OperationExpectedEndTime) && !(workerProcessStartTime > this.WorkerStartTime) && !(workerProcessStartTime > this.OperationStartTime);
			}
		}
	}
}
