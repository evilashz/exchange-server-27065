using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000206 RID: 518
	public enum BusyType
	{
		// Token: 0x04000ECE RID: 3790
		Unknown = -1,
		// Token: 0x04000ECF RID: 3791
		Free,
		// Token: 0x04000ED0 RID: 3792
		Tentative,
		// Token: 0x04000ED1 RID: 3793
		Busy,
		// Token: 0x04000ED2 RID: 3794
		OOF,
		// Token: 0x04000ED3 RID: 3795
		WorkingElseWhere
	}
}
