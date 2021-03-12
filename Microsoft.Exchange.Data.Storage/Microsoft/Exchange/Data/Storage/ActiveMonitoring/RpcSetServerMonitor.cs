using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.ActiveMonitoring;

namespace Microsoft.Exchange.Data.Storage.ActiveMonitoring
{
	// Token: 0x02000337 RID: 823
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RpcSetServerMonitor
	{
		// Token: 0x06002490 RID: 9360 RVA: 0x00093DD8 File Offset: 0x00091FD8
		public static void Invoke(string serverName, string monitorName, string targetResource, bool? isRepairing, int timeoutInMSec = 30000)
		{
			RpcSetServerMonitor.Request attachedRequest = new RpcSetServerMonitor.Request(monitorName, targetResource, isRepairing);
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(attachedRequest, ActiveMonitoringGenericRpcCommandId.SetServerMonitor, 1, 0);
			ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcSetServerMonitor.Reply>(requestInfo, serverName, timeoutInMSec);
		}

		// Token: 0x040015DC RID: 5596
		public const int MajorVersion = 1;

		// Token: 0x040015DD RID: 5597
		public const int MinorVersion = 0;

		// Token: 0x040015DE RID: 5598
		public const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.SetServerMonitor;

		// Token: 0x02000338 RID: 824
		[Serializable]
		public class Request
		{
			// Token: 0x06002491 RID: 9361 RVA: 0x00093E02 File Offset: 0x00092002
			public Request(string monitorName, string targetResource, bool? isRepairing)
			{
				this.MonitorName = monitorName;
				this.TargetResource = targetResource;
				this.IsRepairing = isRepairing;
			}

			// Token: 0x17000C42 RID: 3138
			// (get) Token: 0x06002492 RID: 9362 RVA: 0x00093E1F File Offset: 0x0009201F
			// (set) Token: 0x06002493 RID: 9363 RVA: 0x00093E27 File Offset: 0x00092027
			public string MonitorName { get; set; }

			// Token: 0x17000C43 RID: 3139
			// (get) Token: 0x06002494 RID: 9364 RVA: 0x00093E30 File Offset: 0x00092030
			// (set) Token: 0x06002495 RID: 9365 RVA: 0x00093E38 File Offset: 0x00092038
			public string TargetResource { get; set; }

			// Token: 0x17000C44 RID: 3140
			// (get) Token: 0x06002496 RID: 9366 RVA: 0x00093E41 File Offset: 0x00092041
			// (set) Token: 0x06002497 RID: 9367 RVA: 0x00093E49 File Offset: 0x00092049
			public bool? IsRepairing { get; set; }
		}

		// Token: 0x02000339 RID: 825
		[Serializable]
		public class Reply
		{
		}
	}
}
