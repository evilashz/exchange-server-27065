using System;

namespace Microsoft.Exchange.Nspi
{
	// Token: 0x0200090E RID: 2318
	[Flags]
	public enum NspiGetHierarchyInfoFlags
	{
		// Token: 0x04002B2B RID: 11051
		None = 0,
		// Token: 0x04002B2C RID: 11052
		Dos = 1,
		// Token: 0x04002B2D RID: 11053
		OneOff = 2,
		// Token: 0x04002B2E RID: 11054
		Unicode = 4,
		// Token: 0x04002B2F RID: 11055
		Admin = 8,
		// Token: 0x04002B30 RID: 11056
		Paged = 16
	}
}
