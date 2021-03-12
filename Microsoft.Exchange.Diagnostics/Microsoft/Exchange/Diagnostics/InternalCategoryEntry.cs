using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200013A RID: 314
	public struct InternalCategoryEntry
	{
		// Token: 0x0400061A RID: 1562
		public int SpinLock;

		// Token: 0x0400061B RID: 1563
		public int CategoryNameHashCode;

		// Token: 0x0400061C RID: 1564
		public int CategoryNameOffset;

		// Token: 0x0400061D RID: 1565
		public int FirstInstanceOffset;

		// Token: 0x0400061E RID: 1566
		public int NextCategoryOffset;

		// Token: 0x0400061F RID: 1567
		public int IsConsistent;
	}
}
