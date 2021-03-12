using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000256 RID: 598
	internal interface IReminderTimeCalculatorFactory
	{
		// Token: 0x0600165C RID: 5724
		IReminderTimeCalculator Create(IMailboxSession session);
	}
}
