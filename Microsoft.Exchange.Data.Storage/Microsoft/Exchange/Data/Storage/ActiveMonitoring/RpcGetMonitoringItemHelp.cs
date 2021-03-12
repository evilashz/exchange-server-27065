using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Data.Storage.ActiveMonitoring
{
	// Token: 0x0200032A RID: 810
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RpcGetMonitoringItemHelp
	{
		// Token: 0x06002415 RID: 9237 RVA: 0x0009392C File Offset: 0x00091B2C
		public static List<PropertyInformation> Invoke(string serverName, string monitorIdentity, int timeoutInMSec = 900000)
		{
			RpcGetMonitoringItemHelp.Request attachedRequest = new RpcGetMonitoringItemHelp.Request(monitorIdentity);
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(attachedRequest, ActiveMonitoringGenericRpcCommandId.GetMonitoringItemHelp, 1, 0);
			RpcGetMonitoringItemHelp.Reply reply = ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcGetMonitoringItemHelp.Reply>(requestInfo, serverName, timeoutInMSec);
			return reply.HelpEntries;
		}

		// Token: 0x04001595 RID: 5525
		public const int MajorVersion = 1;

		// Token: 0x04001596 RID: 5526
		public const int MinorVersion = 0;

		// Token: 0x04001597 RID: 5527
		public const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.GetMonitoringItemHelp;

		// Token: 0x0200032B RID: 811
		[Serializable]
		public class Request
		{
			// Token: 0x06002416 RID: 9238 RVA: 0x0009395C File Offset: 0x00091B5C
			public Request(string monitorIdentity)
			{
				this.MonitorIdentity = monitorIdentity;
			}

			// Token: 0x17000C0A RID: 3082
			// (get) Token: 0x06002417 RID: 9239 RVA: 0x0009396B File Offset: 0x00091B6B
			// (set) Token: 0x06002418 RID: 9240 RVA: 0x00093973 File Offset: 0x00091B73
			public string MonitorIdentity { get; set; }
		}

		// Token: 0x0200032C RID: 812
		[Serializable]
		public class Reply
		{
			// Token: 0x17000C0B RID: 3083
			// (get) Token: 0x06002419 RID: 9241 RVA: 0x0009397C File Offset: 0x00091B7C
			// (set) Token: 0x0600241A RID: 9242 RVA: 0x00093984 File Offset: 0x00091B84
			public List<PropertyInformation> HelpEntries { get; set; }
		}
	}
}
