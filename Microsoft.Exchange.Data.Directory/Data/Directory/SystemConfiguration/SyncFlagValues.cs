using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005C7 RID: 1479
	[Flags]
	internal enum SyncFlagValues
	{
		// Token: 0x04002E9D RID: 11933
		None = 0,
		// Token: 0x04002E9E RID: 11934
		Mirrored = 1,
		// Token: 0x04002E9F RID: 11935
		SyncNow = 2,
		// Token: 0x04002EA0 RID: 11936
		Calendar = 4
	}
}
