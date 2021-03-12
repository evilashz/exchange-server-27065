using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001C3 RID: 451
	internal class ListenerDelivery
	{
		// Token: 0x06000FFB RID: 4091 RVA: 0x0003D669 File Offset: 0x0003B869
		internal ListenerDelivery(ListenerChannelsManager listenerChannelsManager)
		{
			this.listenerChannelsManager = listenerChannelsManager;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0003D678 File Offset: 0x0003B878
		internal string[] DeliverRemoteNotification(IEnumerable<string> channelIds, RemoteNotificationPayload remoteNotificationPayload)
		{
			List<string> list = new List<string>();
			Dictionary<PendingRequestManager, bool> dictionary = new Dictionary<PendingRequestManager, bool>();
			foreach (string text in channelIds)
			{
				PendingRequestManager pendingGetManager = this.listenerChannelsManager.GetPendingGetManager(text);
				if (pendingGetManager == null)
				{
					list.Add(text);
				}
				else if (!dictionary.ContainsKey(pendingGetManager))
				{
					pendingGetManager.GetRemoteNotifier.AddRemoteNotificationPayload(remoteNotificationPayload);
					pendingGetManager.GetRemoteNotifier.PickupData();
					dictionary.Add(pendingGetManager, true);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0400097F RID: 2431
		private ListenerChannelsManager listenerChannelsManager;
	}
}
