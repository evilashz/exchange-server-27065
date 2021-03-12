using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000003 RID: 3
	internal class InvalidNotificationEventArgs : EventArgs
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002138 File Offset: 0x00000338
		public InvalidNotificationEventArgs(PushNotification notification, Exception ex)
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			ArgumentValidator.ThrowIfNull("ex", ex);
			this.Notification = notification;
			this.Exception = ex;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002164 File Offset: 0x00000364
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000216C File Offset: 0x0000036C
		public PushNotification Notification { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002175 File Offset: 0x00000375
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000217D File Offset: 0x0000037D
		public Exception Exception { get; private set; }
	}
}
