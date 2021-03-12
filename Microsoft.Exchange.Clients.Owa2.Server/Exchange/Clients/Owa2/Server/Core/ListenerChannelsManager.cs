using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001C2 RID: 450
	internal class ListenerChannelsManager
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0003D559 File Offset: 0x0003B759
		public static ListenerChannelsManager Instance
		{
			get
			{
				return ListenerChannelsManager.instance;
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003D560 File Offset: 0x0003B760
		public static string GeneratedChannelId()
		{
			return Guid.NewGuid().ToString();
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003D580 File Offset: 0x0003B780
		public void AddPendingGetChannel(string channelId, PendingRequestManager pendingRequestManager)
		{
			lock (this.syncRoot)
			{
				if (!this.channelIdChannelsMapping.ContainsKey(channelId))
				{
					this.channelIdChannelsMapping.Add(channelId, pendingRequestManager);
				}
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0003D5D8 File Offset: 0x0003B7D8
		public void RemovePendingGetChannel(string channelId)
		{
			lock (this.syncRoot)
			{
				this.channelIdChannelsMapping.Remove(channelId);
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0003D620 File Offset: 0x0003B820
		public PendingRequestManager GetPendingGetManager(string channelId)
		{
			PendingRequestManager result = null;
			this.channelIdChannelsMapping.TryGetValue(channelId, out result);
			return result;
		}

		// Token: 0x0400097C RID: 2428
		private static ListenerChannelsManager instance = new ListenerChannelsManager();

		// Token: 0x0400097D RID: 2429
		private readonly object syncRoot = new object();

		// Token: 0x0400097E RID: 2430
		private readonly Dictionary<string, PendingRequestManager> channelIdChannelsMapping = new Dictionary<string, PendingRequestManager>();
	}
}
