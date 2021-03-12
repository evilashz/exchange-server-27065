using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000017 RID: 23
	internal class PushNotificationQueueItem<TNotif> where TNotif : PushNotification
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004016 File Offset: 0x00002216
		// (set) Token: 0x060000CB RID: 203 RVA: 0x0000401E File Offset: 0x0000221E
		public TNotif Notification { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004027 File Offset: 0x00002227
		// (set) Token: 0x060000CD RID: 205 RVA: 0x0000402F File Offset: 0x0000222F
		public AverageTimeCounterBase QueueTimeCounter { get; set; }
	}
}
