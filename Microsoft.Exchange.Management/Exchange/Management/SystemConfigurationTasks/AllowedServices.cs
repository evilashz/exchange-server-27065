using System;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AB9 RID: 2745
	[Flags]
	public enum AllowedServices
	{
		// Token: 0x0400355D RID: 13661
		None = 0,
		// Token: 0x0400355E RID: 13662
		IMAP = 1,
		// Token: 0x0400355F RID: 13663
		POP = 2,
		// Token: 0x04003560 RID: 13664
		UM = 4,
		// Token: 0x04003561 RID: 13665
		IIS = 8,
		// Token: 0x04003562 RID: 13666
		SMTP = 16,
		// Token: 0x04003563 RID: 13667
		Federation = 32,
		// Token: 0x04003564 RID: 13668
		UMCallRouter = 64
	}
}
