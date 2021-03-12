using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.ActiveMonitoring;

namespace Microsoft.Exchange.Data.Storage.ActiveMonitoring
{
	// Token: 0x02000326 RID: 806
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RpcGetMonitorHealthStatus
	{
		// Token: 0x060023D8 RID: 9176 RVA: 0x000936F4 File Offset: 0x000918F4
		public static List<RpcGetMonitorHealthStatus.RpcMonitorHealthEntry> Invoke(string serverName, int timeoutInMSec = 30000)
		{
			return RpcGetMonitorHealthStatus.Invoke(serverName, null, timeoutInMSec);
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x00093700 File Offset: 0x00091900
		public static List<RpcGetMonitorHealthStatus.RpcMonitorHealthEntry> Invoke(string serverName, string healthSetName, int timeoutInMSec = 30000)
		{
			RpcGetMonitorHealthStatus.Request attachedRequest = new RpcGetMonitorHealthStatus.Request(healthSetName);
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(attachedRequest, ActiveMonitoringGenericRpcCommandId.GetMonitorHealthStatus, 1, 2);
			RpcGetMonitorHealthStatus.Reply reply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcGetMonitorHealthStatus.Reply>(requestInfo, serverName, timeoutInMSec);
			return reply.HealthEntries;
		}

		// Token: 0x04001576 RID: 5494
		public const int MajorVersion = 1;

		// Token: 0x04001577 RID: 5495
		public const int MinorVersion = 2;

		// Token: 0x04001578 RID: 5496
		public const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.GetMonitorHealthStatus;

		// Token: 0x02000327 RID: 807
		[Serializable]
		public class Request
		{
			// Token: 0x060023DA RID: 9178 RVA: 0x0009372F File Offset: 0x0009192F
			public Request(string healthSetName)
			{
				this.HealthSetName = healthSetName;
			}

			// Token: 0x17000BEE RID: 3054
			// (get) Token: 0x060023DB RID: 9179 RVA: 0x0009373E File Offset: 0x0009193E
			// (set) Token: 0x060023DC RID: 9180 RVA: 0x00093746 File Offset: 0x00091946
			public string HealthSetName { get; set; }
		}

		// Token: 0x02000328 RID: 808
		[Serializable]
		public class Reply
		{
			// Token: 0x17000BEF RID: 3055
			// (get) Token: 0x060023DD RID: 9181 RVA: 0x0009374F File Offset: 0x0009194F
			// (set) Token: 0x060023DE RID: 9182 RVA: 0x00093757 File Offset: 0x00091957
			public List<RpcGetMonitorHealthStatus.RpcMonitorHealthEntry> HealthEntries { get; set; }
		}

		// Token: 0x02000329 RID: 809
		[Serializable]
		public class RpcMonitorHealthEntry
		{
			// Token: 0x17000BF0 RID: 3056
			// (get) Token: 0x060023E0 RID: 9184 RVA: 0x00093768 File Offset: 0x00091968
			// (set) Token: 0x060023E1 RID: 9185 RVA: 0x00093770 File Offset: 0x00091970
			public string Name { get; set; }

			// Token: 0x17000BF1 RID: 3057
			// (get) Token: 0x060023E2 RID: 9186 RVA: 0x00093779 File Offset: 0x00091979
			// (set) Token: 0x060023E3 RID: 9187 RVA: 0x00093781 File Offset: 0x00091981
			public string TargetResource { get; set; }

			// Token: 0x17000BF2 RID: 3058
			// (get) Token: 0x060023E4 RID: 9188 RVA: 0x0009378A File Offset: 0x0009198A
			// (set) Token: 0x060023E5 RID: 9189 RVA: 0x00093792 File Offset: 0x00091992
			public string Description { get; set; }

			// Token: 0x17000BF3 RID: 3059
			// (get) Token: 0x060023E6 RID: 9190 RVA: 0x0009379B File Offset: 0x0009199B
			// (set) Token: 0x060023E7 RID: 9191 RVA: 0x000937A3 File Offset: 0x000919A3
			public bool IsHaImpacting { get; set; }

			// Token: 0x17000BF4 RID: 3060
			// (get) Token: 0x060023E8 RID: 9192 RVA: 0x000937AC File Offset: 0x000919AC
			// (set) Token: 0x060023E9 RID: 9193 RVA: 0x000937B4 File Offset: 0x000919B4
			public int RecurranceInterval { get; set; }

			// Token: 0x17000BF5 RID: 3061
			// (get) Token: 0x060023EA RID: 9194 RVA: 0x000937BD File Offset: 0x000919BD
			// (set) Token: 0x060023EB RID: 9195 RVA: 0x000937C5 File Offset: 0x000919C5
			public DateTime DefinitionCreatedTime { get; set; }

			// Token: 0x17000BF6 RID: 3062
			// (get) Token: 0x060023EC RID: 9196 RVA: 0x000937CE File Offset: 0x000919CE
			// (set) Token: 0x060023ED RID: 9197 RVA: 0x000937D6 File Offset: 0x000919D6
			public string HealthSetName { get; set; }

			// Token: 0x17000BF7 RID: 3063
			// (get) Token: 0x060023EE RID: 9198 RVA: 0x000937DF File Offset: 0x000919DF
			// (set) Token: 0x060023EF RID: 9199 RVA: 0x000937E7 File Offset: 0x000919E7
			public string HealthSetDescription { get; set; }

			// Token: 0x17000BF8 RID: 3064
			// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000937F0 File Offset: 0x000919F0
			// (set) Token: 0x060023F1 RID: 9201 RVA: 0x000937F8 File Offset: 0x000919F8
			public string HealthGroupName { get; set; }

			// Token: 0x17000BF9 RID: 3065
			// (get) Token: 0x060023F2 RID: 9202 RVA: 0x00093801 File Offset: 0x00091A01
			// (set) Token: 0x060023F3 RID: 9203 RVA: 0x00093809 File Offset: 0x00091A09
			public string ServerComponentName { get; set; }

			// Token: 0x17000BFA RID: 3066
			// (get) Token: 0x060023F4 RID: 9204 RVA: 0x00093812 File Offset: 0x00091A12
			// (set) Token: 0x060023F5 RID: 9205 RVA: 0x0009381A File Offset: 0x00091A1A
			public string AlertValue { get; set; }

			// Token: 0x17000BFB RID: 3067
			// (get) Token: 0x060023F6 RID: 9206 RVA: 0x00093823 File Offset: 0x00091A23
			// (set) Token: 0x060023F7 RID: 9207 RVA: 0x0009382B File Offset: 0x00091A2B
			public DateTime FirstAlertObservedTime { get; set; }

			// Token: 0x17000BFC RID: 3068
			// (get) Token: 0x060023F8 RID: 9208 RVA: 0x00093834 File Offset: 0x00091A34
			// (set) Token: 0x060023F9 RID: 9209 RVA: 0x0009383C File Offset: 0x00091A3C
			public DateTime LastTransitionTime { get; set; }

			// Token: 0x17000BFD RID: 3069
			// (get) Token: 0x060023FA RID: 9210 RVA: 0x00093845 File Offset: 0x00091A45
			// (set) Token: 0x060023FB RID: 9211 RVA: 0x0009384D File Offset: 0x00091A4D
			public DateTime LastExecutionTime { get; set; }

			// Token: 0x17000BFE RID: 3070
			// (get) Token: 0x060023FC RID: 9212 RVA: 0x00093856 File Offset: 0x00091A56
			// (set) Token: 0x060023FD RID: 9213 RVA: 0x0009385E File Offset: 0x00091A5E
			public string LastExecutionResult { get; set; }

			// Token: 0x17000BFF RID: 3071
			// (get) Token: 0x060023FE RID: 9214 RVA: 0x00093867 File Offset: 0x00091A67
			// (set) Token: 0x060023FF RID: 9215 RVA: 0x0009386F File Offset: 0x00091A6F
			public string CurrentHealthSetState { get; set; }

			// Token: 0x17000C00 RID: 3072
			// (get) Token: 0x06002400 RID: 9216 RVA: 0x00093878 File Offset: 0x00091A78
			// (set) Token: 0x06002401 RID: 9217 RVA: 0x00093880 File Offset: 0x00091A80
			public int ResultId { get; set; }

			// Token: 0x17000C01 RID: 3073
			// (get) Token: 0x06002402 RID: 9218 RVA: 0x00093889 File Offset: 0x00091A89
			// (set) Token: 0x06002403 RID: 9219 RVA: 0x00093891 File Offset: 0x00091A91
			public int WorkItemId { get; set; }

			// Token: 0x17000C02 RID: 3074
			// (get) Token: 0x06002404 RID: 9220 RVA: 0x0009389A File Offset: 0x00091A9A
			// (set) Token: 0x06002405 RID: 9221 RVA: 0x000938A2 File Offset: 0x00091AA2
			public int TimeoutSeconds { get; set; }

			// Token: 0x17000C03 RID: 3075
			// (get) Token: 0x06002406 RID: 9222 RVA: 0x000938AB File Offset: 0x00091AAB
			// (set) Token: 0x06002407 RID: 9223 RVA: 0x000938B3 File Offset: 0x00091AB3
			public bool IsStale { get; set; }

			// Token: 0x17000C04 RID: 3076
			// (get) Token: 0x06002408 RID: 9224 RVA: 0x000938BC File Offset: 0x00091ABC
			// (set) Token: 0x06002409 RID: 9225 RVA: 0x000938C4 File Offset: 0x00091AC4
			public string Error { get; set; }

			// Token: 0x17000C05 RID: 3077
			// (get) Token: 0x0600240A RID: 9226 RVA: 0x000938CD File Offset: 0x00091ACD
			// (set) Token: 0x0600240B RID: 9227 RVA: 0x000938D5 File Offset: 0x00091AD5
			public string Exception { get; set; }

			// Token: 0x17000C06 RID: 3078
			// (get) Token: 0x0600240C RID: 9228 RVA: 0x000938DE File Offset: 0x00091ADE
			// (set) Token: 0x0600240D RID: 9229 RVA: 0x000938E6 File Offset: 0x00091AE6
			public bool IsNotified { get; set; }

			// Token: 0x17000C07 RID: 3079
			// (get) Token: 0x0600240E RID: 9230 RVA: 0x000938EF File Offset: 0x00091AEF
			// (set) Token: 0x0600240F RID: 9231 RVA: 0x000938F7 File Offset: 0x00091AF7
			public int LastFailedProbeId { get; set; }

			// Token: 0x17000C08 RID: 3080
			// (get) Token: 0x06002410 RID: 9232 RVA: 0x00093900 File Offset: 0x00091B00
			// (set) Token: 0x06002411 RID: 9233 RVA: 0x00093908 File Offset: 0x00091B08
			public int LastFailedProbeResultId { get; set; }

			// Token: 0x17000C09 RID: 3081
			// (get) Token: 0x06002412 RID: 9234 RVA: 0x00093911 File Offset: 0x00091B11
			// (set) Token: 0x06002413 RID: 9235 RVA: 0x00093919 File Offset: 0x00091B19
			public int ServicePriority { get; set; }
		}
	}
}
