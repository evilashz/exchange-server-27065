using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000C3 RID: 195
	internal class SyncAssistantPastDeadlineException : SkipException
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x00039E2C File Offset: 0x0003802C
		public SyncAssistantPastDeadlineException(string message) : base(new LocalizedString(message))
		{
		}
	}
}
