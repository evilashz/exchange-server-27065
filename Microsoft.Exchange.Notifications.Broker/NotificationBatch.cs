using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200002B RID: 43
	public class NotificationBatch
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00009EC7 File Offset: 0x000080C7
		internal NotificationBatch(List<BrokerNotification> notifications, RemoteMessenger remoteMessenger)
		{
			this.Notifications = notifications;
			this.RemoteMessenger = remoteMessenger;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009EDD File Offset: 0x000080DD
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00009EE5 File Offset: 0x000080E5
		internal List<BrokerNotification> Notifications { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00009EEE File Offset: 0x000080EE
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00009EF6 File Offset: 0x000080F6
		internal RemoteMessenger RemoteMessenger { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00009EFF File Offset: 0x000080FF
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00009F07 File Offset: 0x00008107
		internal int Id { get; set; }
	}
}
