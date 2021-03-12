using System;

namespace Microsoft.Mapi
{
	// Token: 0x020001BA RID: 442
	[Flags]
	internal enum CopyMessagesFlags
	{
		// Token: 0x040005AF RID: 1455
		None = 0,
		// Token: 0x040005B0 RID: 1456
		Move = 1,
		// Token: 0x040005B1 RID: 1457
		DeclineOk = 4,
		// Token: 0x040005B2 RID: 1458
		SendEntryId = 32,
		// Token: 0x040005B3 RID: 1459
		DontUpdateSource = 64
	}
}
