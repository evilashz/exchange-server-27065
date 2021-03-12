using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000F5 RID: 245
	[Flags]
	internal enum SystemFlagsEnum
	{
		// Token: 0x04000535 RID: 1333
		None = 0,
		// Token: 0x04000536 RID: 1334
		NotReplicate = 1,
		// Token: 0x04000537 RID: 1335
		NtdsNamingContext = 1,
		// Token: 0x04000538 RID: 1336
		Replicate = 2,
		// Token: 0x04000539 RID: 1337
		NtdsDomain = 2,
		// Token: 0x0400053A RID: 1338
		ConstructAttribute = 4,
		// Token: 0x0400053B RID: 1339
		Category1 = 16,
		// Token: 0x0400053C RID: 1340
		DeleteImmediately = 33554432,
		// Token: 0x0400053D RID: 1341
		Unmovable = 67108864,
		// Token: 0x0400053E RID: 1342
		Unrenameable = 134217728,
		// Token: 0x0400053F RID: 1343
		MovableWithRestrictions = 268435456,
		// Token: 0x04000540 RID: 1344
		Movable = 536870912,
		// Token: 0x04000541 RID: 1345
		Renamable = 1073741824,
		// Token: 0x04000542 RID: 1346
		Indispensable = -2147483648
	}
}
