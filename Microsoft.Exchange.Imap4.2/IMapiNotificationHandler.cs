using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000006 RID: 6
	internal interface IMapiNotificationHandler
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25
		// (set) Token: 0x0600001A RID: 26
		MapiNotificationManager Manager { get; set; }

		// Token: 0x0600001B RID: 27
		void SubscribeNotification();

		// Token: 0x0600001C RID: 28
		void ProcessNotification(Notification notification);

		// Token: 0x0600001D RID: 29
		void UnsubscribeNotification();
	}
}
