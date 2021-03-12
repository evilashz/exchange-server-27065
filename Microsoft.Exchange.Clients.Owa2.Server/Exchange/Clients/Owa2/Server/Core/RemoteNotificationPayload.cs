using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001CE RID: 462
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoteNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x06001060 RID: 4192 RVA: 0x0003EE9A File Offset: 0x0003D09A
		public RemoteNotificationPayload(int notificationsCount, string remotePayload, string[] channelIds)
		{
			this.NotificationsCount = notificationsCount;
			this.RemotePayload = remotePayload;
			this.ChannelIds = channelIds;
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0003EEB7 File Offset: 0x0003D0B7
		// (set) Token: 0x06001062 RID: 4194 RVA: 0x0003EEBF File Offset: 0x0003D0BF
		[DataMember]
		public string RemotePayload { get; set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x0003EEC8 File Offset: 0x0003D0C8
		// (set) Token: 0x06001064 RID: 4196 RVA: 0x0003EED0 File Offset: 0x0003D0D0
		[DataMember]
		public int NotificationsCount { get; set; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0003EED9 File Offset: 0x0003D0D9
		// (set) Token: 0x06001066 RID: 4198 RVA: 0x0003EEE1 File Offset: 0x0003D0E1
		[DataMember]
		public string[] ChannelIds { get; set; }
	}
}
