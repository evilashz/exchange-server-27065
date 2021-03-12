using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000D5 RID: 213
	internal class ReminderEmitting : CalendarNotificationAction
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x0003CF24 File Offset: 0x0003B124
		public ReminderEmitting(ExDateTime expectedTime, MailboxData mailboxData, CalendarInfo calInfo, List<ICalendarNotificationEmitter> emitters) : base(expectedTime, mailboxData)
		{
			this.CalendarInfo = calInfo;
			this.Emitters = emitters;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0003CF3D File Offset: 0x0003B13D
		public ReminderEmitting(ExDateTime expectedTime, MailboxData mailboxData, CalendarInfo calInfo) : base(expectedTime, mailboxData)
		{
			this.CalendarInfo = calInfo;
			this.Emitters = new List<ICalendarNotificationEmitter>();
			this.Emitters.Add(new TextNotificationFactory.TextMessagingEmitter(mailboxData));
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0003CF6A File Offset: 0x0003B16A
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x0003CF72 File Offset: 0x0003B172
		internal CalendarInfo CalendarInfo { get; private set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0003CF7B File Offset: 0x0003B17B
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0003CF83 File Offset: 0x0003B183
		internal List<ICalendarNotificationEmitter> Emitters { get; private set; }

		// Token: 0x0600090B RID: 2315 RVA: 0x0003CF8C File Offset: 0x0003B18C
		protected override void OnPerforming(long cookie)
		{
			using (base.Context.CreateReadLock())
			{
				if (base.ShouldContinue(cookie))
				{
					foreach (ICalendarNotificationEmitter calendarNotificationEmitter in this.Emitters)
					{
						calendarNotificationEmitter.Emit(null, CalendarNotificationType.Reminder, new CalendarInfo[]
						{
							this.CalendarInfo
						});
					}
				}
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0003D020 File Offset: 0x0003B220
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ReminderEmitting>(this);
		}
	}
}
