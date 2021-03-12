using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000255 RID: 597
	[Flags]
	internal enum ResolveMethod
	{
		// Token: 0x040011E9 RID: 4585
		Default = 0,
		// Token: 0x040011EA RID: 4586
		LastWriterWins = 1,
		// Token: 0x040011EB RID: 4587
		NoConflictNotification = 2
	}
}
