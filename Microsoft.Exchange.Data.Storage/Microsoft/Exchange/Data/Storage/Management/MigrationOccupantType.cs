using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A30 RID: 2608
	[Flags]
	internal enum MigrationOccupantType
	{
		// Token: 0x0400363D RID: 13885
		Regular = 0,
		// Token: 0x0400363E RID: 13886
		DryRun = 1,
		// Token: 0x0400363F RID: 13887
		Test = 2
	}
}
