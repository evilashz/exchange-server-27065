using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x0200024C RID: 588
	internal class ReminderLogger : ExtensibleLogger
	{
		// Token: 0x06001601 RID: 5633 RVA: 0x0007B748 File Offset: 0x00079948
		private ReminderLogger() : base(new ReminderLogConfiguration())
		{
		}

		// Token: 0x04000D02 RID: 3330
		public static readonly ReminderLogger Instance = new ReminderLogger();
	}
}
