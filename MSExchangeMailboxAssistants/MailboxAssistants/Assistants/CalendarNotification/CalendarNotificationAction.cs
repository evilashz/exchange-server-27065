using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000D4 RID: 212
	internal abstract class CalendarNotificationAction : ScheduledAction<MailboxData>
	{
		// Token: 0x06000903 RID: 2307 RVA: 0x0003CF07 File Offset: 0x0003B107
		public CalendarNotificationAction(ExDateTime expectedTime, MailboxData mailboxData) : base(expectedTime, mailboxData)
		{
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0003CF11 File Offset: 0x0003B111
		protected sealed override void OnPerformed(long cookie)
		{
			CalendarNotificationInitiator.CompleteAction(this, base.GetType().Name);
		}
	}
}
