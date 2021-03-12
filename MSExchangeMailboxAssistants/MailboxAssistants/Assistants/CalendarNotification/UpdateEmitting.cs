using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000D6 RID: 214
	internal class UpdateEmitting : ReminderEmitting
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x0003D028 File Offset: 0x0003B228
		public UpdateEmitting(MailboxData mailboxData, CalendarInfo calInfo, CalendarNotificationType type) : base(ExDateTime.Now, mailboxData, calInfo)
		{
			this.Type = type;
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0003D03E File Offset: 0x0003B23E
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x0003D046 File Offset: 0x0003B246
		public CalendarNotificationType Type { get; private set; }

		// Token: 0x06000910 RID: 2320 RVA: 0x0003D050 File Offset: 0x0003B250
		protected override void OnPerforming(long cookie)
		{
			using (base.Context.CreateReadLock())
			{
				if (base.ShouldContinue(cookie))
				{
					foreach (ICalendarNotificationEmitter calendarNotificationEmitter in base.Emitters)
					{
						ExTraceGlobals.AssistantTracer.TraceDebug<CalendarNotificationType>((long)this.GetHashCode(), "Emitting emitter with type = {0}", this.Type);
						calendarNotificationEmitter.Emit(null, this.Type, new CalendarInfo[]
						{
							base.CalendarInfo
						});
					}
				}
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0003D104 File Offset: 0x0003B304
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UpdateEmitting>(this);
		}
	}
}
