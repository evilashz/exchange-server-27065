using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200025E RID: 606
	[Flags]
	internal enum WorkingSetFlags
	{
		// Token: 0x04001218 RID: 4632
		Exchange = 1,
		// Token: 0x04001219 RID: 4633
		WorkingSet = 2,
		// Token: 0x0400121A RID: 4634
		Subscribed = 4,
		// Token: 0x0400121B RID: 4635
		Pinned = 8,
		// Token: 0x0400121C RID: 4636
		Groups = 16,
		// Token: 0x0400121D RID: 4637
		SPO = 32,
		// Token: 0x0400121E RID: 4638
		Yammer = 64
	}
}
