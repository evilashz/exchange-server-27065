using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200052F RID: 1327
	public enum MailTipsAccessLevel
	{
		// Token: 0x0400280D RID: 10253
		[LocDescription(DirectoryStrings.IDs.MailTipsAccessLevelNone)]
		None,
		// Token: 0x0400280E RID: 10254
		[LocDescription(DirectoryStrings.IDs.MailTipsAccessLevelLimited)]
		Limited,
		// Token: 0x0400280F RID: 10255
		[LocDescription(DirectoryStrings.IDs.MailTipsAccessLevelAll)]
		All
	}
}
