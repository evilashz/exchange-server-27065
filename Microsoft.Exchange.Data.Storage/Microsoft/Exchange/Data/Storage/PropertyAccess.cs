using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AAC RID: 2732
	[Flags]
	internal enum PropertyAccess
	{
		// Token: 0x0400383F RID: 14399
		None = 0,
		// Token: 0x04003840 RID: 14400
		Read = 1,
		// Token: 0x04003841 RID: 14401
		Write = 2,
		// Token: 0x04003842 RID: 14402
		ReadWrite = 3
	}
}
