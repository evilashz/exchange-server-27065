using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.ActiveMonitoring;

namespace Microsoft.Exchange.Data.Storage.ActiveMonitoring
{
	// Token: 0x0200032E RID: 814
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RpcGetMonitoringItemIdentity
	{
		// Token: 0x0600241C RID: 9244 RVA: 0x00093998 File Offset: 0x00091B98
		public static List<RpcGetMonitoringItemIdentity.RpcMonitorItemIdentity> Invoke(string serverName, string healthSetName, int timeoutInMSec = 900000)
		{
			RpcGetMonitoringItemIdentity.Request attachedRequest = new RpcGetMonitoringItemIdentity.Request(healthSetName);
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(attachedRequest, ActiveMonitoringGenericRpcCommandId.GetMonitoringItemIdentity, 1, 0);
			RpcGetMonitoringItemIdentity.Reply reply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcGetMonitoringItemIdentity.Reply>(requestInfo, serverName, timeoutInMSec);
			return reply.MonitorIdentities;
		}

		// Token: 0x040015A0 RID: 5536
		public const int MajorVersion = 1;

		// Token: 0x040015A1 RID: 5537
		public const int MinorVersion = 0;

		// Token: 0x040015A2 RID: 5538
		public const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.GetMonitoringItemIdentity;

		// Token: 0x0200032F RID: 815
		[Serializable]
		public class Request
		{
			// Token: 0x0600241D RID: 9245 RVA: 0x000939C8 File Offset: 0x00091BC8
			public Request(string healthSetName)
			{
				this.HealthSetName = healthSetName;
			}

			// Token: 0x17000C0C RID: 3084
			// (get) Token: 0x0600241E RID: 9246 RVA: 0x000939D7 File Offset: 0x00091BD7
			// (set) Token: 0x0600241F RID: 9247 RVA: 0x000939DF File Offset: 0x00091BDF
			public string HealthSetName { get; set; }
		}

		// Token: 0x02000330 RID: 816
		[Serializable]
		public class Reply
		{
			// Token: 0x17000C0D RID: 3085
			// (get) Token: 0x06002420 RID: 9248 RVA: 0x000939E8 File Offset: 0x00091BE8
			// (set) Token: 0x06002421 RID: 9249 RVA: 0x000939F0 File Offset: 0x00091BF0
			public List<RpcGetMonitoringItemIdentity.RpcMonitorItemIdentity> MonitorIdentities { get; set; }
		}

		// Token: 0x02000331 RID: 817
		[Serializable]
		public class RpcMonitorItemIdentity
		{
			// Token: 0x17000C0E RID: 3086
			// (get) Token: 0x06002424 RID: 9252 RVA: 0x00093A09 File Offset: 0x00091C09
			// (set) Token: 0x06002425 RID: 9253 RVA: 0x00093A11 File Offset: 0x00091C11
			public string HealthSetName { get; set; }

			// Token: 0x17000C0F RID: 3087
			// (get) Token: 0x06002426 RID: 9254 RVA: 0x00093A1A File Offset: 0x00091C1A
			// (set) Token: 0x06002427 RID: 9255 RVA: 0x00093A22 File Offset: 0x00091C22
			public string Name { get; set; }

			// Token: 0x17000C10 RID: 3088
			// (get) Token: 0x06002428 RID: 9256 RVA: 0x00093A2B File Offset: 0x00091C2B
			// (set) Token: 0x06002429 RID: 9257 RVA: 0x00093A33 File Offset: 0x00091C33
			public string TargetResource { get; set; }

			// Token: 0x17000C11 RID: 3089
			// (get) Token: 0x0600242A RID: 9258 RVA: 0x00093A3C File Offset: 0x00091C3C
			// (set) Token: 0x0600242B RID: 9259 RVA: 0x00093A44 File Offset: 0x00091C44
			public string ItemType { get; set; }
		}
	}
}
