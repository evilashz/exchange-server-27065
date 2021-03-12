using System;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200006A RID: 106
	[Flags]
	internal enum MigrationFailureFlags
	{
		// Token: 0x04000238 RID: 568
		None = 0,
		// Token: 0x04000239 RID: 569
		Fatal = 1,
		// Token: 0x0400023A RID: 570
		Corruption = 3
	}
}
