using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009DF RID: 2527
	[Flags]
	public enum NewItemNotification
	{
		// Token: 0x040033D7 RID: 13271
		None = 0,
		// Token: 0x040033D8 RID: 13272
		Sound = 1,
		// Token: 0x040033D9 RID: 13273
		EMailToast = 2,
		// Token: 0x040033DA RID: 13274
		VoiceMailToast = 4,
		// Token: 0x040033DB RID: 13275
		FaxToast = 8,
		// Token: 0x040033DC RID: 13276
		All = 15
	}
}
