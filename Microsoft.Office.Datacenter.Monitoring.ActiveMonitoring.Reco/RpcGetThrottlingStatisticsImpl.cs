using System;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200001A RID: 26
	public class RpcGetThrottlingStatisticsImpl
	{
		// Token: 0x060000AF RID: 175 RVA: 0x000041D0 File Offset: 0x000023D0
		public static RpcGetThrottlingStatisticsImpl.ThrottlingStatistics SendRequest(string serverName, RecoveryActionId actionId, string resourceName, int maxAllowedInOneHour, int maxAllowedInOneDay, bool isStopSearchWhenLimitExceeds = false, bool isCountFailedActions = false, int timeoutInMsec = 30000)
		{
			RpcGetThrottlingStatisticsImpl.Request attachedRequest = new RpcGetThrottlingStatisticsImpl.Request
			{
				ActionId = actionId,
				ResourceName = resourceName,
				MaxAllowedInOneHour = maxAllowedInOneHour,
				MaxAllowedInOneDay = maxAllowedInOneDay,
				IsStopSearchWhenLimitExceeds = isStopSearchWhenLimitExceeds,
				IsCountFailedActions = isCountFailedActions
			};
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(attachedRequest, ActiveMonitoringGenericRpcCommandId.GetThrottlingStatistics, 1, 0);
			return ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcGetThrottlingStatisticsImpl.ThrottlingStatistics>(requestInfo, serverName, timeoutInMsec);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004228 File Offset: 0x00002428
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcGetThrottlingStatisticsImpl.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcGetThrottlingStatisticsImpl.Request>(requestInfo, 1, 0);
			RpcGetThrottlingStatisticsImpl.ThrottlingStatistics throttlingStatistics = RecoveryActionsRepository.Instance.GetThrottlingStatistics(request.ActionId, request.ResourceName, request.MaxAllowedInOneHour, request.MaxAllowedInOneDay, false, true);
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, throttlingStatistics, 1, 0);
		}

		// Token: 0x04000068 RID: 104
		public const int MajorVersion = 1;

		// Token: 0x04000069 RID: 105
		public const int MinorVersion = 0;

		// Token: 0x0400006A RID: 106
		internal const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.GetThrottlingStatistics;

		// Token: 0x0200001B RID: 27
		[Serializable]
		public class ThrottlingStatistics
		{
			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004276 File Offset: 0x00002476
			// (set) Token: 0x060000B3 RID: 179 RVA: 0x0000427E File Offset: 0x0000247E
			public string ServerName { get; set; }

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004287 File Offset: 0x00002487
			// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000428F File Offset: 0x0000248F
			public int TotalEntriesSearched { get; set; }

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004298 File Offset: 0x00002498
			// (set) Token: 0x060000B7 RID: 183 RVA: 0x000042A0 File Offset: 0x000024A0
			public int NumberOfActionsInOneHour { get; set; }

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x000042A9 File Offset: 0x000024A9
			// (set) Token: 0x060000B9 RID: 185 RVA: 0x000042B1 File Offset: 0x000024B1
			public RecoveryActionHelper.RecoveryActionEntrySerializable EntryExceedingOneHourLimit { get; set; }

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060000BA RID: 186 RVA: 0x000042BA File Offset: 0x000024BA
			// (set) Token: 0x060000BB RID: 187 RVA: 0x000042C2 File Offset: 0x000024C2
			public int NumberOfActionsInOneDay { get; set; }

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060000BC RID: 188 RVA: 0x000042CB File Offset: 0x000024CB
			// (set) Token: 0x060000BD RID: 189 RVA: 0x000042D3 File Offset: 0x000024D3
			public RecoveryActionHelper.RecoveryActionEntrySerializable EntryExceedingOneDayLimit { get; set; }

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060000BE RID: 190 RVA: 0x000042DC File Offset: 0x000024DC
			// (set) Token: 0x060000BF RID: 191 RVA: 0x000042E4 File Offset: 0x000024E4
			public RecoveryActionHelper.RecoveryActionEntrySerializable MostRecentEntry { get; set; }

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060000C0 RID: 192 RVA: 0x000042ED File Offset: 0x000024ED
			// (set) Token: 0x060000C1 RID: 193 RVA: 0x000042F5 File Offset: 0x000024F5
			public RpcSetThrottlingInProgressImpl.ThrottlingProgressInfo ThrottleProgressInfo { get; set; }

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060000C2 RID: 194 RVA: 0x000042FE File Offset: 0x000024FE
			// (set) Token: 0x060000C3 RID: 195 RVA: 0x00004306 File Offset: 0x00002506
			public bool IsThrottlingInProgress { get; set; }

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000430F File Offset: 0x0000250F
			// (set) Token: 0x060000C5 RID: 197 RVA: 0x00004317 File Offset: 0x00002517
			public bool IsRecoveryInProgress { get; set; }

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004320 File Offset: 0x00002520
			// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000433B File Offset: 0x0000253B
			public DateTime HostProcessStartTime
			{
				get
				{
					return this.HostProcessStartTimeUtc.ToLocalTime();
				}
				set
				{
					this.HostProcessStartTimeUtc = value.ToUniversalTime();
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000434A File Offset: 0x0000254A
			// (set) Token: 0x060000C9 RID: 201 RVA: 0x00004352 File Offset: 0x00002552
			public DateTime HostProcessStartTimeUtc { get; set; }

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000CA RID: 202 RVA: 0x0000435C File Offset: 0x0000255C
			// (set) Token: 0x060000CB RID: 203 RVA: 0x00004377 File Offset: 0x00002577
			public DateTime WorkerProcessStartTime
			{
				get
				{
					return this.WorkerProcessStartTimeUtc.ToLocalTime();
				}
				set
				{
					this.WorkerProcessStartTimeUtc = value.ToUniversalTime();
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000CC RID: 204 RVA: 0x00004386 File Offset: 0x00002586
			// (set) Token: 0x060000CD RID: 205 RVA: 0x0000438E File Offset: 0x0000258E
			public DateTime WorkerProcessStartTimeUtc { get; set; }

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000CE RID: 206 RVA: 0x00004398 File Offset: 0x00002598
			// (set) Token: 0x060000CF RID: 207 RVA: 0x000043B3 File Offset: 0x000025B3
			public DateTime SystemBootTime
			{
				get
				{
					return this.SystemBootTimeUtc.ToLocalTime();
				}
				set
				{
					this.SystemBootTimeUtc = value.ToUniversalTime();
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000D0 RID: 208 RVA: 0x000043C2 File Offset: 0x000025C2
			// (set) Token: 0x060000D1 RID: 209 RVA: 0x000043CA File Offset: 0x000025CA
			public DateTime SystemBootTimeUtc { get; set; }
		}

		// Token: 0x0200001C RID: 28
		[Serializable]
		internal class Request
		{
			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x000043DB File Offset: 0x000025DB
			// (set) Token: 0x060000D4 RID: 212 RVA: 0x000043E3 File Offset: 0x000025E3
			public RecoveryActionId ActionId { get; set; }

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060000D5 RID: 213 RVA: 0x000043EC File Offset: 0x000025EC
			// (set) Token: 0x060000D6 RID: 214 RVA: 0x000043F4 File Offset: 0x000025F4
			public string ResourceName { get; set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060000D7 RID: 215 RVA: 0x000043FD File Offset: 0x000025FD
			// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004405 File Offset: 0x00002605
			public int MaxAllowedInOneHour { get; set; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000440E File Offset: 0x0000260E
			// (set) Token: 0x060000DA RID: 218 RVA: 0x00004416 File Offset: 0x00002616
			public int MaxAllowedInOneDay { get; set; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060000DB RID: 219 RVA: 0x0000441F File Offset: 0x0000261F
			// (set) Token: 0x060000DC RID: 220 RVA: 0x00004427 File Offset: 0x00002627
			public bool IsStopSearchWhenLimitExceeds { get; set; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060000DD RID: 221 RVA: 0x00004430 File Offset: 0x00002630
			// (set) Token: 0x060000DE RID: 222 RVA: 0x00004438 File Offset: 0x00002638
			public bool IsCountFailedActions { get; set; }
		}
	}
}
