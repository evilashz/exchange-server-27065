using System;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000329 RID: 809
	[Flags]
	public enum ServiceConfigurationType
	{
		// Token: 0x04000F6D RID: 3949
		None = 0,
		// Token: 0x04000F6E RID: 3950
		MailTips = 1,
		// Token: 0x04000F6F RID: 3951
		UnifiedMessagingConfiguration = 2,
		// Token: 0x04000F70 RID: 3952
		ProtectionRules = 4,
		// Token: 0x04000F71 RID: 3953
		PolicyNudges = 8
	}
}
