using System;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000FD RID: 253
	internal enum MigrationProcessorResult
	{
		// Token: 0x040004DD RID: 1245
		Working,
		// Token: 0x040004DE RID: 1246
		Waiting,
		// Token: 0x040004DF RID: 1247
		Completed,
		// Token: 0x040004E0 RID: 1248
		Failed,
		// Token: 0x040004E1 RID: 1249
		Deleted,
		// Token: 0x040004E2 RID: 1250
		Suspended
	}
}
