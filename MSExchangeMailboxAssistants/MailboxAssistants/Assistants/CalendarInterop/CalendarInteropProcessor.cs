using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.Interop;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarInterop
{
	// Token: 0x02000262 RID: 610
	internal sealed class CalendarInteropProcessor
	{
		// Token: 0x060016A7 RID: 5799 RVA: 0x000802C8 File Offset: 0x0007E4C8
		public void ProcessPendingActions(MailboxSession mailboxSession, StoreObject item)
		{
			CalendarItemSeries calendarItemSeries = item as CalendarItemSeries;
			if (calendarItemSeries == null)
			{
				return;
			}
			if (calendarItemSeries.CalendarInteropActionQueueHasData)
			{
				CalendarInterop.ExecutePendingInteropActions(mailboxSession, calendarItemSeries, null, null, null);
			}
		}
	}
}
