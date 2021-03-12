using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000249 RID: 585
	[Flags]
	internal enum SaveChangesFlags
	{
		// Token: 0x04001046 RID: 4166
		KeepOpenRead = 1,
		// Token: 0x04001047 RID: 4167
		KeepOpenReadWrite = 2,
		// Token: 0x04001048 RID: 4168
		ForceSave = 4,
		// Token: 0x04001049 RID: 4169
		SkipQuotaCheck = 64,
		// Token: 0x0400104A RID: 4170
		ChangeIMAPId = 128,
		// Token: 0x0400104B RID: 4171
		ForceNotificationPublish = 256
	}
}
