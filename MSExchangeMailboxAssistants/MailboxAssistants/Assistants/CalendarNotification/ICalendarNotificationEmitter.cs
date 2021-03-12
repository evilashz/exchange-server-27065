using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000E3 RID: 227
	internal interface ICalendarNotificationEmitter
	{
		// Token: 0x06000996 RID: 2454
		void Emit(MailboxSession session, CalendarNotificationType type, IList<CalendarInfo> events);
	}
}
