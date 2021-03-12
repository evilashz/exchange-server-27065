using System;

namespace Microsoft.Exchange.Nspi
{
	// Token: 0x02000917 RID: 2327
	[Flags]
	public enum NspiGetHierarchyFlags
	{
		// Token: 0x04002B49 RID: 11081
		None = 0,
		// Token: 0x04002B4A RID: 11082
		Dos = 1,
		// Token: 0x04002B4B RID: 11083
		OneOff = 2,
		// Token: 0x04002B4C RID: 11084
		Unicode = 4,
		// Token: 0x04002B4D RID: 11085
		Admin = 8,
		// Token: 0x04002B4E RID: 11086
		Paged = 16
	}
}
