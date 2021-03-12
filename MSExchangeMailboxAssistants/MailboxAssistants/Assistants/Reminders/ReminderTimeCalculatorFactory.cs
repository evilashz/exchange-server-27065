using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x0200025A RID: 602
	internal class ReminderTimeCalculatorFactory : IReminderTimeCalculatorFactory
	{
		// Token: 0x06001667 RID: 5735 RVA: 0x0007E350 File Offset: 0x0007C550
		public IReminderTimeCalculator Create(IMailboxSession session)
		{
			IReminderTimeCalculatorContextFactory reminderTimeCalculatorContextFactory = new ReminderTimeCalculatorContextFactory(session);
			ReminderTimeCalculatorContext reminderTimeCalculatorContext = reminderTimeCalculatorContextFactory.Create();
			return new WorkingHoursAwareReminderTimeCalculator(reminderTimeCalculatorContext, reminderTimeCalculatorContext.WorkingHours);
		}
	}
}
