using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002A6 RID: 678
	[Flags]
	internal enum PropertyDefinitionFlags
	{
		// Token: 0x04000E5D RID: 3677
		None = 0,
		// Token: 0x04000E5E RID: 3678
		ReadOnly = 1,
		// Token: 0x04000E5F RID: 3679
		MultiValued = 2,
		// Token: 0x04000E60 RID: 3680
		Calculated = 4,
		// Token: 0x04000E61 RID: 3681
		FilterOnly = 8,
		// Token: 0x04000E62 RID: 3682
		Mandatory = 16,
		// Token: 0x04000E63 RID: 3683
		PersistDefaultValue = 32,
		// Token: 0x04000E64 RID: 3684
		WriteOnce = 64,
		// Token: 0x04000E65 RID: 3685
		Binary = 128,
		// Token: 0x04000E66 RID: 3686
		TaskPopulated = 256
	}
}
