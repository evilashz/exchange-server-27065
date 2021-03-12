using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200024E RID: 590
	[Flags]
	internal enum SubmitMessageFlags
	{
		// Token: 0x040011B1 RID: 4529
		None = 0,
		// Token: 0x040011B2 RID: 4530
		Preprocess = 1,
		// Token: 0x040011B3 RID: 4531
		NeedsSpooler = 2,
		// Token: 0x040011B4 RID: 4532
		IgnoreSendAsRight = 4
	}
}
