using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200006A RID: 106
	[Flags]
	public enum VariableScopedOptions
	{
		// Token: 0x04000103 RID: 259
		AllScope = 8,
		// Token: 0x04000104 RID: 260
		Constant = 2,
		// Token: 0x04000105 RID: 261
		None = 0,
		// Token: 0x04000106 RID: 262
		Private = 4,
		// Token: 0x04000107 RID: 263
		ReadOnly = 1,
		// Token: 0x04000108 RID: 264
		Unspecified = 16
	}
}
