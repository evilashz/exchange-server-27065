using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001E4 RID: 484
	[Flags]
	internal enum ClientSubscriptionFlags
	{
		// Token: 0x04000A63 RID: 2659
		None = 0,
		// Token: 0x04000A64 RID: 2660
		FolderCount = 1,
		// Token: 0x04000A65 RID: 2661
		FolderChange = 2,
		// Token: 0x04000A66 RID: 2662
		NewMail = 4,
		// Token: 0x04000A67 RID: 2663
		Reminders = 8,
		// Token: 0x04000A68 RID: 2664
		StaticSearch = 16
	}
}
