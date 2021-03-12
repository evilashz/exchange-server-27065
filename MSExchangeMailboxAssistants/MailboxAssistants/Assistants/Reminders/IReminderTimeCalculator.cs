using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000255 RID: 597
	internal interface IReminderTimeCalculator
	{
		// Token: 0x0600165B RID: 5723
		ExDateTime CalculateReminderTime(IModernReminder reminder, ExDateTime nowInUTC);
	}
}
