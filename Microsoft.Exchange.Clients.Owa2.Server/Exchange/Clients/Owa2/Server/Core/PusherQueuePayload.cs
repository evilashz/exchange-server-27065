using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001CA RID: 458
	public class PusherQueuePayload
	{
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0003E613 File Offset: 0x0003C813
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x0003E61B File Offset: 0x0003C81B
		public NotificationPayloadBase Payload { get; private set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0003E624 File Offset: 0x0003C824
		// (set) Token: 0x0600103D RID: 4157 RVA: 0x0003E62C File Offset: 0x0003C82C
		public IEnumerable<string> ChannelIds { get; private set; }

		// Token: 0x0600103E RID: 4158 RVA: 0x0003E635 File Offset: 0x0003C835
		public PusherQueuePayload(NotificationPayloadBase payload, IEnumerable<string> channelIds)
		{
			this.Payload = payload;
			this.ChannelIds = channelIds;
		}
	}
}
