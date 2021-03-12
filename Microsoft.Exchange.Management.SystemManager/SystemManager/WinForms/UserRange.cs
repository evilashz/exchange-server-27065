using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000265 RID: 613
	public enum UserRange : short
	{
		// Token: 0x040009D6 RID: 2518
		AllUsersWithoutMailAttributes = 3,
		// Token: 0x040009D7 RID: 2519
		AccountDisabledUsers = 1,
		// Token: 0x040009D8 RID: 2520
		AccountEnabledUsers,
		// Token: 0x040009D9 RID: 2521
		AllUsersWithMailAttributes = 192,
		// Token: 0x040009DA RID: 2522
		MailEnabledUsers = 128,
		// Token: 0x040009DB RID: 2523
		MailboxUsers = 64
	}
}
