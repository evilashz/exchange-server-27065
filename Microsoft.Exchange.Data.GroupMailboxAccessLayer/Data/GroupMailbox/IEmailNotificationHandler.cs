using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200003B RID: 59
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IEmailNotificationHandler
	{
		// Token: 0x060001B3 RID: 435
		void AddNotificationRecipient(IMailboxLocator recipient);
	}
}
